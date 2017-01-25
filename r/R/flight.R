sp_chr <- c("\\.", "\\^", "\\(", "\\)", "\\[", "\\]", "\\{", "\\}",
            "\\-", "\\+", "\\?", "\\!", "\\*", "\\$", "\\|", "\\&")


storage <- setRefClass("Storage",
                       fields = list(kv_map = "list"),
                       methods = list(
                         set = function(key, val) {
                           kv_map[[key]] <<- val
                         },
                         has_key = function(key) {
                           return(!is.null(kv_map[[key]]))
                         },
                         get = function(key) {
                           return(kv_map[[key]])
                         }
                       ))

flight <-
  function(conn, ems_id, new_data = FALSE)
  {
    obj <- list()
    class(obj) <- "flight"
    obj$ems_id       <- ems_id
    obj$connection   <- conn
    obj$tree         <- data.frame()
    obj$database     <- list()
    obj$cntr <- 0
    obj$key_maps     <- storage$new()

    if (treefile_exists(obj) & (!new_data)) {
      obj <- load_tree(obj)
    } else {
      obj <- generate_tree(obj)
    }
  }

search_fields <-
  function(flt, ...)
  {
    flist <- list(...)
    res   <- list()
    for ( f in flist ) {
      if ( length(f) == 1 ) {
        # Single keyword case
        names <- tolower(flt$tree$name)
        for ( x in sp_chr ) {
          f <- gsub(x, paste("\\", x, sep=""), f)
        }
        df <- subset(flt$tree, (nodetype=='field') & ((f==names) | grepl(f, names, ignore.case = T)) )
        df <- df[order(nchar(df$name)), ]
        res[[length(res)+1]] <- as.list(df[1,])

      } else if ( length(f) > 1 ){
        # Vector of hierarchical keyword set
        chld <- flt$tree
        for ( i in seq_along(f) ) {
          for ( x in sp_chr ) {
            f[i] <- gsub(x, paste("\\", x, sep=""), f[i])
          }
          names   <- tolower(chld$name)
          node_id <- chld[(f[i]==names) | grep(f[i], names, ignore.case = T), 'id']
          if (i < length(f)) {
            chld    <- subset(flt$tree, parent %in% node_id)
          } else {
            chld    <- subset(chld, (nodetype=='field') & ((f[i]==names) | grepl(f[i], names, ignore.case = T)) )
          }
        }
        res[[length(res)+1]] <- as.list( chld[order(nchar(chld$name))[1], ] )
      }
    }

    if ( any(is.na(sapply(res, function(x) x$id))) ) {
      stop("Some of the search results for give field keywords did not return fields. Please check if the keywords are valid.")
    }

    # if ( length(res) == 1 ) {
    #   res <- res[[1]]
    # }
    return(res)
  }


list_allvalues <-
  function(flt, field = NULL, field_id = NULL, in_list=FALSE)
  {
    fld_id <- field_id

    if ( is.null(field_id) ) {
      fld <- search_fields(flt, field)[[1]]
      fld_type <- fld$type
      fld_id   <- fld$id
      if ( fld_type != "discrete" )  {
        stop("Queried field should be discrete type to get the list of possible values.")
      }
    }

    if (flt$key_maps$has_key(fld_id)) {
      kmap <- flt$key_maps$get(fld_id)
    } else {
      db_id <- flt$database$id
      cat("Getting key-value mappings from API. (Caution: runway ID takes much longer)\n")
      r <- request(flt$connection,
                   uri_keys = c('database', 'field'),
                   uri_args = c(flt$ems_id, db_id, fld_id))
      kmap <- content(r)$discreteValues
      flt$key_maps$set(fld_id, kmap)
    }

    if ( in_list ) {
      return(kmap)
    }
    return(kmap)
  }


get_value_id <-
  function(flt, value, field=NULL, field_id=NULL)
  {
    val_map <- list_allvalues(flt, field = field, field_id = field_id, in_list = T)
    id <- which(val_map==value)

    if ( length(id)==0 ) {
      stop(sprintf("The queried value '%s' is not part of the possible values of the discrete field.", value))
    }
    id <- as.integer(names(id))
    return(id)
  }


generate_tree <-
  function(flt)
  {
    exclude_dirs <- c("Download Information", "Download Review", "Processing",
                      "Profile 16 Extra Data", "Operational Information",
                      "Operational Information (ODW2)", "Profiles")

    # Connect to EMS and get the data source ID of the FDW Flight
    conn <- flt$connection
    r    <- request(conn, uri_keys = c('database','group'), uri_args = flt$ems_id)

    for (x in content(r)$groups) {
      if (x$name == 'FDW') {
        fdw <- x
        break
      }
    }

    r    <- request(conn, uri_keys = c('database','group'), uri_args = flt$ems_id,
                    body = list("groupId" = fdw$id))

    for (x in content(r)$databases) {
      if (x$singularName == "FDW Flight") {
        fdw_flt <- x
        fdw_flt$name <- fdw_flt$singularName
        fdw_flt$type <- NA
        fdw_flt$nodetype <- 'database'
        fdw_flt$parent = NA
        break
      }
    }

    flt$tree <- data.frame(fdw_flt[c('id','name','type','nodetype','parent')], stringsAsFactors = FALSE)
    flt$database<- fdw_flt[c('id','name','type','nodetype','parent')]
    flt <- add_subtree(flt, fdw_flt, exclude_dirs, save_freq = 50)
    save_tree(flt)
    flt
  }


add_subtree <-
  function(flt, parent_node, exclude = c(), save_freq = NULL) {

    # Store the tree structure for addition of every 50 field groups.
    if (!is.null(save_freq)) {
      flt$cntr <- flt$cntr + 1
      if (flt$cntr >= 50) {
        save_tree(flt)
        flt$cntr <- 0
      }
    }


    cat(paste("On", parent_node['name'], "...\n"))

    # If node type is "field_group", pass the field group id to the GET request to get the
    # field-group specific information.
    if (parent_node['nodetype']=='field_group') {
      body <- list('groupId' = parent_node$id)
    } else {
      body <- NULL
    }
    r    <- request(flt$connection,
                    uri_keys = c('database','field_group'),
                    uri_args = c(flt$ems_id, flt$database$id),
                    body = body)

    ##  Get the children fields/field groups
    d <- content(r)

    # If there is an array of fields as children add them to the tree
    if ( length(d$fields) > 0 ) {
      for (f in d$fields) {
        f$nodetype <- 'field'
        f$parent   <- parent_node$id
        flt$tree   <- rbind(flt$tree, f)
      }
      plural <- ""
      if ( length(d$fields) > 1 ) {
        plural <- "s"
      }
      cat(sprintf("-- Added %d field%s\n", length(d$fields), plural))
    }

    # If there is an array of field group as children add them to the tree and call the function
    # recursively until reaches the fields (leaves).
    if ( length(d$groups) > 0 ) {
      for (g in d$groups) {
        g$type     <- NA
        g$nodetype <- 'field_group'
        g$parent   <- parent_node$id
        flt$tree   <- rbind(flt$tree, g)
        # recursive func call
        if ( !(g$name %in% exclude) ) {
          flt <- add_subtree(flt, g, exclude)
        }
      }
    }
    flt
  }


remove_subtree <-
  function(flt, parent_node)
  {
    rm_list <- parent_node$id
    prnt_id <- parent_node$id

    cntr <- 0
    while(length(prnt_id) > 0) {
      child_id <- unlist(sapply(prnt_id, function(x) get_children_id(flt, x)))
      rm_list <- c(rm_list, child_id)
      prnt_id <- child_id
      cntr <- cntr + 1

      if ( cntr >= 1e4) {
        stop("Something's wrong. subtree removal went over 10,000 iterations.")
      }
    }
    flt$tree <- subset(flt$tree, !(id %in% rm_list))
    cat(sprintf("Deleted the subtree of field group '%s' with total %d fields/groups.\n",
                parent_node$name, length(rm_list)))
    return(flt)
  }



update_children <-
  function(flt, parent_node)
  {
    # If node type is "field_group", pass the field group id to the GET request to get the
    # field-group specific information.
    if (parent_node['nodetype']=='field_group') {
      body <- list('groupId'= parent_node$id)
    } else {
      body <- NULL
    }
    r    <- request(flt$connection,
                    uri_keys = c('database','field_group'),
                    uri_args = c(flt$ems_id, flt$database$id),
                    body = body)

    ##  Get the children fields/field groups
    d <- content(r)

    # Remove the old field data before appending new field data
    flt$tree <- subset(flt$tree, !((nodetype == 'field') & (parent == parent_node$id)))

    # If there is an array of fields as children add them to the tree
    if ( length(d$fields) > 0 ) {
      for (f in d$fields) {
        f$nodetype <- 'field'
        f$parent   <- parent_node$id
        flt$tree   <- rbind(flt$tree, f)
      }
    }

    # If there is an array of field group as children add them to the tree and call the function
    # recursively until reaches the fields (leaves).

    if ( length(d$groups) > 0 ) {
      old <- subset(flt$tree, ((nodetype=="field_group") & (parent== parent_node$id)))[ ,'id']
      new <- sapply(d$groups, function(x) x$id)

      # Remove the childe nodes that do not exist anymore
      rm_id    <- setdiff(old, new)
      for (n in rm_id) {
        flt$tree <- remove_subtree(flt, as.list(flt$tree[flt$tree$id == rm_id, ]))
      }

      # New ones are included to the tree data
      new_id   <- setdiff(new, old)
      for (g in d$groups) {
        g$type     <- NA
        g$nodetype <- 'field_group'
        g$parent   <- parent_node$id
        if (g$id %in% new_id) {
          flt$tree <- rbind(flt$tree, g)
        }
      }
    }
    return(flt)
  }


update_tree <-
  function(flt, path)
  {

    for ( i in seq_along(tolower(path)) ) {

      if (i == 1) {
        names<- tolower(flt$tree$name)
        prnt <- subset(flt$tree, (path[i]==names) | grepl(path[i], names))
        if (nrow(prnt) == 0) {
          stop(sprintf("Search keyword '%s' did not return any field group. Please check if the keyword is valid.", path[i]))
        }
        prnt <- prnt[order(nchar(prnt$name))[1], ]
        prnt <- as.list(prnt)

      } else {
        flt     <- update_children(flt, prnt)
        chld_df <- get_children_df(flt, prnt$id)
        names   <- tolower(chld_df$name)
        chld    <- subset(chld_df, (path[i]==names) | grepl(path[i], names))
        if (nrow(chld) == 0) {
          stop(sprintf("Search keyword '%s' did not return any field group. Please check if the keyword is valid.", path[i]))
        }
        chld    <- chld[order(nchar(chld$name))[1], ]
        prnt    <- as.list(chld)
      }
    }
    cat(sprintf("=== Starting to add subtree from '%s' ===\n", prnt$name))
    flt <- add_subtree(flt, prnt)
    return(flt)
  }


save_tree <-
  function(flt, file_name = NULL)
  {
    if ( is.null(file_name) ) {
      file_name = file.path(path.package("Rems"), 'data', sprintf("FDW_flt_data_tree_ems_id_%s.rds", flt$ems_id))
    }
    saveRDS(flt$tree, file_name)
  }


load_tree <-
  function(flt, file_name = NULL)
  {
    if ( is.null(file_name) ) {
      file_name = file.path(path.package("Rems"), 'data', sprintf("FDW_flt_data_tree_ems_id_%s.rds", flt$ems_id))
    }
    flt$tree <- readRDS(file_name)
    flt$database <- as.list(flt$tree[flt$tree$nodetype == "database", c('id','name','type','nodetype','parent')])
    flt
  }


treefile_exists <-
  function(flt)
  {
    file.exists(file.path(path.package("Rems"), 'data', sprintf("FDW_flt_data_tree_ems_id_%s.rds", flt$ems_id)))
  }


get_children_df <-
  function(flt, parent_id)
  {
    subset(flt$tree, parent == parent_id)
  }


get_children_id <-
  function(flt, parent_id)
  {
    get_children_df(flt, parent_id)$id
  }
