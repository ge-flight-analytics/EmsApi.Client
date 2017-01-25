
analytic <-
  function(conn, ems_id, new_data = FALSE)
  {
    obj <- list()
    class(obj) <- "analytic"
    obj$ems_id        <- ems_id
    obj$connection    <- conn
    if ( paramfile_exists(obj) & (!new_data)) {
      obj <- load_paramtable(obj)
    } else {
      obj$param_table   <- data.frame(stringsAsFactors = F)
    }
    return(obj)
  }


search_param <-
  function(anal, keyword)
  {
    cat(sprintf('Searching params with keyword "%s" from EMS ...', keyword))
    # EMS API Call
    r <- request(anal$connection,
                 uri_keys = c('analytic','search'),
                 uri_args = anal$ems_id,
                 body = list(text = keyword))
    # Param set JSON to R list
    prm <- content(r)
    if ( length(prm)==0 ) {
      stop(sprintf("No parameter found with search keyword %s.", keyword))
    } else if ( length(prm) > 1 ) {
      # If the param set has more than one param, order than by length
      # of their names
      word_len <- sapply(prm, function(x) nchar(x$name))
      prm <- prm[order(word_len)]
    }
    cat("Done.\n")
    return(prm)
  }


get_param <-
  function(anal, keyword, unique = T)
  {
    if ( nrow(anal$param_table)==0 ) {
      return(list(id="", name="", description="", units=""))
    }
    # Make sure the special characters are treated as raw characters, not
    # POSIX meta-characters
    sp_chr <- c("\\.", "\\^", "\\(", "\\)", "\\[", "\\]", "\\{", "\\}",
                "\\-", "\\+", "\\?", "\\!", "\\*", "\\$", "\\|", "\\&")
    for ( x in sp_chr ) {
      keyword <- gsub(x, paste("\\", x, sep=""), keyword)
    }

    df <- subset(anal$param_table, grepl(keyword, name, ignore.case = T))
    if ( nrow(df)==0 ) {
      return(list(id="", name="", description="", units=""))
    }
    df <- df[order(nchar(df$name)), ]
    if ( unique ) {
      prm <- as.list(df[1, ])
      return(prm)
    }
    prm <- lapply(1:nrow(df), function(x) as.list(df[x,]))
    return(prm)
  }


save_paramtable <-
  function(anal, file_name = NULL)
  {
    if ( is.null(file_name) ) {
      file_name <- file.path(path.package("Rems"), "data", sprintf("param_table_ems_id_%d.rds", anal$ems_id))
    }
    saveRDS(anal$param_table, file_name)
  }


load_paramtable <-
  function(anal, file_name = NULL)
  {
    if ( is.null(file_name) ) {
      file_name <- file.path(path.package("Rems"), "data", sprintf("param_table_ems_id_%d.rds", anal$ems_id))
    }
    anal$param_table <- readRDS(file_name)
    return(anal)
  }


paramfile_exists <-
  function(anal)
  {
    file.exists(file.path(path.package("Rems"), 'data', sprintf("param_table_ems_id_%d.rds", anal$ems_id)))
  }
