basic_ops <- list(
  "==" = 'equal', "!=" = 'notEqual', "<" = 'lessThan',
  "<=" = 'lessThanOrEqual', ">" = 'greaterThan', ">=" = 'greaterThanOrEqual'
)
sp_ops <- list(
  ' in '= 'in',
  ' not in '= 'notIn'
)

#' @export
flt_query <-
  function(conn, ems_name, new_data = FALSE)
  {
    obj <- list()
    class(obj) <- 'FltQuery'
    # Instantiating other objects
    obj$connection <- conn
    obj$ems        <- ems(conn)
    obj$ems_id     <- get_id(obj$ems, ems_name)
    obj$flight     <- flight(conn, obj$ems_id, new_data)

    # object data
    obj$queryset <- list()
    obj$columns  <- list()
    obj <- reset(obj)

    return(obj)
  }

#' @export
reset.FltQuery <-
  function(qry)
  {
    qry$queryset <- list(
      select = list(),
      groupBy = list(),
      orderBy = list(),
      distinct = TRUE,
      format = "none"
    )
    qry$columns <- list()
    qry
  }

#' @export
select.FltQuery <-
  function(qry, ..., aggregate = c('none','avg','count','max','min','stdev','sum','var'))
  {
    aggregate <- match.arg(aggregate)

    fields <- search_fields(qry$flight, ...)

    for ( fld in fields ) {

      d <- list(
        fieldId = fld$id,
        aggregate = aggregate
      )
      n_sel <- length(qry$queryset$select)
      qry$queryset$select[[n_sel + 1]] <- d
      qry$columns[[n_sel + 1]] <- fld
    }
    return(qry)
  }

#' @export
group_by <-
  function(qry, ...)
  {
    for ( fld in search_fields(qry$flight, ...)) {
      n_gby <- length(qry$queryset$groupBy)
      qry$queryset$groupBy[[n_gby + 1]] <- list(fieldId = fld$id)
    }
    return(qry)
  }

#' @export
order_by <-
  function(qry, field, order = c('asc', 'desc'))
  {
    order <- match.arg(order)
    fld   <- search_fields(qry$flight, field)[[1]]
    n_oby <- length(qry$queryset$orderBy)
    qry$queryset$orderBy[[n_oby + 1]] <- list(fieldId = fld$id,
                                              order = order,
                                              aggregate = 'none')
    return(qry)
  }

#' @export
filter <-
  function(qry, expr)
  {
    if ( is.null(qry$queryset$filter) ) {
      qry$queryset$filter = list(operator = 'and',
                                 args = list())
    }
    spl_expr <- split_expr(expr)
    a_fltr   <- translate_expr(qry$flight, spl_expr)
    n_fltr   <- length(qry$queryset$filter$args)
    qry$queryset$filter$args[[n_fltr + 1]] <- a_fltr

    return(qry)
  }


split_expr <-
  function(expr)
  {
    for ( pattern in c( "[=!<>]=?", names(sp_ops) ) ) {
      l <- regexpr(pattern, expr)
      if (l[1]!=-1) break
    }
    op <- substring(expr, l[1], l[1] + attr(l, "match.length") - 1)
    lr <- strsplit(expr, op)[[1]]
    return(list(
      field = eval(parse(text=lr[1])),
      op = op,
      value = eval(parse(text=lr[2]))
      ))
  }


translate_expr <-
  function(flt, spl_expr)
  {
    expr <- spl_expr

    # Field information
    fld <- search_fields(flt, expr$field)[[1]]
    fld_info <- list(type = 'field', value = fld$id)
    fld_type <- fld$type

    # Condition value
    val_info <- lapply(expr$value, function(v) list(type = 'constant', value = v))


    # Translate differently for different field types
    if ( fld_type == "boolean" ) {
      fltr <- boolean_filter(expr$op, fld_info, val_info)
    } else if ( fld_type == "discrete" ) {
      fltr <- discrete_filter(expr$op, fld_info, val_info, flt)
    } else if ( fld_type == "number" ) {
      fltr <- number_filter(expr$op, fld_info, val_info)
    } else if ( fld_type == "string" ) {
      fltr <- string_filter(expr$op, fld_info, val_info)
    } else if ( fld_type == "dateTime" ) {
      fltr <- datetime_filter(expr$op, fld_info, val_info)
    } else {
      stop(sprintf("%s has an unknown field type %s.", fld$name, fld_type))
    }
    return(fltr)
  }

#' @export
distinct <-
  function(qry, d = TRUE)
  {
    qry$queryset$distinct <- d
    return(qry)
  }

#' @export
get_top <-
  function(qry, n)
  {
    qry$queryset$top <- n
    return(qry)
  }

# readable_output <-
#   function(qry, r=TRUE)
#   {
#     if (r) {
#       y <- "display"
#     } else {
#       y <- "none"
#     }
#     qry$queryset$format <- y
#     return(qry)
#   }

#' @export
json_str <-
  function(qry)
  {
    jsonlite::toJSON(qry$queryset, auto_unbox = T, pretty = T)
  }

#' @export
simple_run.FltQuery <-
  function(qry, output = c("dataframe", "raw"))
  {

    output <- match.arg(output)
    cat("Sending a regular query to EMS ...")
    r <- request(qry$connection, rtype = "POST",
                 uri_keys = c('database', 'query'),
                 uri_args = c(qry$ems_id, qry$flight$database$id),
                 jsondata = qry$queryset)
    cat("Done.\n")
    if ( output == "raw" ) {
      return(content(r))
    }
    else if ( output == "dataframe" ) {
      return(to_dataframe(qry, content(r)))
    }
  }


#' @export
async_run.FltQuery <-
  function(qry, n_row = 25000)
  {
    open_async_query <- function() {
      cat('Sending and opening an async-query to EMS ...\n')
      r <- request(qry$connection, rtype = "POST",
                   uri_keys = c('database', 'open_asyncq'),
                   uri_args = c(qry$ems_id, qry$flight$database$id),
                   jsondata = qry$queryset)
      if (is.null(content(r)$id)) {
        print(headers(r))
        print(content(r))
        stop("Opening Async query did not return the query Id.")
      }
      cat('Done.\n')
      content(r)
    }

    # Open async-query
    async_q <- open_async_query()

    ctr <- 1
    df  <- data.frame(stringsAsFactors = F)
    req_error <- F
    while(T) {
      cat(sprintf("=== Async call: %d === \n", ctr))
      tryCatch({
        # Mini batch async call. Sometimes the issued query ID gets expired.
        # In that case, try up to three times until getting a new query ID.
        for(i in 1:3) {
          r <- request(qry$connection, rtype = "GET",
                       uri_keys = c('database', 'get_asyncq'),
                       uri_args = c(qry$ems_id,
                                    qry$flight$database$id,
                                    async_q$id,
                                    n_row*(ctr-1),
                                    n_row*ctr-1))
          if (is.null(content(r)$rows)) {
            # Reopen the query if not returning data
            async_q <- open_async_query()
          } else {
            break
          }
        }
        resp <- content(r)
        resp$header <- async_q$header
      }, error = function(e) {
        cat("Something's wrong. Returning what has been sent so far.\n")
        print(headers(r))
        # print(content(r))
        break
      })
      if (length(resp$rows) < 1) {
        break
      }
      dff <- to_dataframe(qry, resp)
      df <- rbind(df, dff)
      cat(sprintf("Received up to  %d rows.\n", nrow(df)))
      if (nrow(dff) < n_row) {
        break
      }
      ctr <- ctr + 1
    }
    tryCatch({
      r <- request(qry$connection, rtype = "DELETE",
                   uri_keys = c('database', 'close_asyncq'),
                   uri_args = c(qry$ems_id, qry$flight$database$id, async_q$id))
      cat(sprintf("Async query connection (query ID: %s) deleted.\n", async_q$id))
    }, error = function(e) {
      cat(sprintf("Couldn't delete the async query (query ID: %s). Probably it was already expired.\n", async_q$id))
    })

    cat("Done.\n")
    df
  }


#' @export
run.FltQuery <-
  function(qry, n_row = 25000)
  {
    if (is.null(qry$queryset$top)) {
      Nout <- NULL
    } else {
      Nout <- qry$queryset$top
    }
    if ((!is.null(Nout)) && (Nout <= 25000)) {
      return(simple_run(qry, output = "dataframe"))
    }
    return(async_run(qry, n_row = n_row))
  }


to_dataframe <-
  function(qry, raw_out)
  {
    cat("Raw JSON output to R dataframe...")
    colname <- sapply(raw_out$header, function(h) h$name)
    coltype <- sapply(qry$columns, function(c) c$type)
    col_id  <- sapply(qry$columns, function(c) c$id)

    # JSON raw data to dataframe. Each data element is a string. It will
    # need to be converted to a right type.
    ldata <- lapply(raw_out$rows,
                    function(r) {
                      sapply(r, function(rr) ifelse(is.null(rr), NA, rr))
                    })

    df <- data.frame(matrix(NA, nrow=length(raw_out$rows), ncol=length(colname)),
                     stringsAsFactors = F)
    names(df) <- colname
    for ( i in 1:nrow(df) ) {
      df[i, ] <- ldata[[i]]
    }

    if ( qry$queryset$format == 'display') {
      cat('Done.\n')
      return(df)
    }
    # Do the dirty work of casting a right type for each column of the data
    # Note
    # ====
    # Runway IDs are discrete data but their key-value mapping is not provided
    # because the mapping itself is quite big in size (45K entries). That means
    # the regular routines to handle the discrete data won't work. As a result
    # the discrete data routine has a dirty, custom routine particularly for
    # the runway IDs. What it basically does is to send a separate but redundant
    # query for runway IDs with "queryset$format = display", and then push the
    # this query result at the runway ID column of the original query result.
    # I know this is crappy but it seems the best way I could find.
    for ( i in seq_along(coltype) ) {
      if ( coltype[i] == 'number' ) {
        df[ , i] <- as.numeric(df[ , i])
      } else if ( coltype[i] == "dateTime" ) {
        df[ , i] <- as.POSIXct(df[, i], tz="GMT",
                               format = "%Y-%m-%dT%H:%M:%S")
      } else if ( coltype[i] == "discrete" ) {
        k_map <- list_allvalues(qry$flight, field_id = col_id[i], in_list = T)
        if ( length(k_map) == 0 ) {
          df[ , i] <- get_rwy_id(qry, i)
        } else {
          df[ , i] <- sapply(df[ , i], function(k) k_map[[k]])
        }
      } else if ( coltype[i] == "boolean") {
        df[ , i] <- as.logical(as.integer(df[ , i]))
      }
    }

    cat("Done.\n")
    return(df)
  }

get_rwy_id <-
  function(qry, ci)
  {
    # ==== Deprecated =====
    # Special routine for retreiving the runway IDs
    cat("\n --Running a special routine for querying runway IDs. This will make the querying twice longer.\n")
    qry$queryset$format <- 'display'
    res <- run(qry)
    res[ , ci]
  }

## ---------------------------------------------------------------------------
## Filter-related low-level functions
filter_fmt <-
  function(op, field_info, val_info = NULL)
  {
    fltr <- list(
      type = "filter",
      value = list(
        operator = op,
        args = lapply(c(list(field_info), val_info),
                      function(x) list(type = x$type, value = x$value))
      )
    )
    return(fltr)
  }


boolean_filter <-
  function(op, field_info, val_info)
  {
    val_info <- val_info[[1]]

    if ( typeof(val_info$value)!="logical" ) {
      stop(sprintf("%s: use a boolean value.", val_info$value))
    }
    if ( op == "==" ) {
      t_op = paste('is', ifelse(val_info$value, "True", "False"), sep = "")
    } else if (op == "!=") {
      t_op = paste('is', ifelse(!val_info$value, "True", "False"), sep = "")
    } else {
      stop(sprintf("%s is not supported for boolean fields.", op))
    }
    fltr <- filter_fmt(t_op, field_info)
    return(fltr)
  }


discrete_filter <-
  function(op, field_info, val_info, flt)
  {
    if ( op %in% names(basic_ops) ) {
      t_op     <- basic_ops[[op]]
    } else if ( op %in% c(" in ", " not in ") ) {
      t_op     <- sp_ops[[op]]
    } else {
      stop(sprintf("%s: Unsupported conditional operator for discrete fields.", op))
    }
    val_info <- lapply(val_info,
                       function(v) {
                         v$value=get_value_id(flt, v$value, field_id = field_info$value)
                         v
                       })

    fltr <- filter_fmt(t_op, field_info, val_info)
    return(fltr)
  }


number_filter <-
  function(op, field_info, val_info)
  {
    if ( op %in% names(basic_ops) ) {
      t_op <- basic_ops[[op]]
    } else {
      stop(sprintf("%s: Unsupported conditional operator for discrete fields.", op))
    }
    fltr <- filter_fmt(t_op, field_info, val_info)
    return(fltr)
  }


string_filter <-
  function(op, field_info, val_info)
  {
    if ( op %in% c("==", "!=") ) {
      t_op <- basic_ops[[op]]
    } else if ( op %in% c(" in ", " not in ") ) {
      t_op <- sp_ops[[op]]
    } else {
      stop(sprintf("%s: Unsupported conditional operator for discrete fields.", op))
    }
    fltr <- filter_fmt(t_op, field_info, val_info)
    return(fltr)
  }


datetime_filter <-
  function(op, field_info, val_info)
  {
    date_ops <- list(
      "<" = "dateTimeBefore",
      ">=" = "dateTimeOnAfter"
    )
    if ( op %in% names(date_ops) ) {
      t_op <- date_ops[[op]]
    } else {
      stop(sprintf("%s: Unsupported conditional operator for discrete fields.", op))
    }
    val_info <- c(val_info, list(list(type="constant", value="Utc")))
    fltr     <- filter_fmt(t_op, field_info, val_info)
    return(fltr)
  }

## --------------------------------------------------------------------------
## Functions to interface with the flight object

#' @export
update_datatree <-
  function(qry, ...)
  {
    path <- unlist(list(...))
    qry$flight <- update_tree(qry$flight, path)
    return(qry)
  }

#' @export
save_datatree <-
  function(qry, file_name = NULL)
  {
    save_tree(qry$flight, file_name)
  }

#' @export
load_datatree <-
  function(qry, file_name = NULL)
  {
    qry$flight <- load_tree(qry$flight, file_name)
    return(qry)
  }
## --------------------------------------------------------------------------

