#' @export
tseries_query <-
  function(conn, ems_name, new_data = FALSE)
  {
    obj <- list()
    class(obj) <- 'TsQuery'

    # Instantiating other objects
    obj$connection <- conn
    obj$ems        <- ems(conn)
    obj$ems_id     <- get_id(obj$ems, ems_name)
    obj$analytic   <- analytic(conn, obj$ems_id, new_data)

    # object data
    obj$queryset <- list()
    obj$columns  <- list()
    obj <- reset(obj)

    return(obj)
  }

#' @export
reset.TsQuery <-
  function(qry)
  {
    qry$queryset <- list()
    qry$columns <- list()
    qry
  }

#' @export
select.TsQuery <-
  function(qry, ...)
  {
    keywords <- list(...)

    save_table = F
    for ( kw in keywords ) {
      # Get the param from the param table
      prm <- get_param(qry$analytic, kw)
      if ( prm$id=="" ) {
        # If param's not found, search from EMS API
        res <- search_param(qry$analytic, kw)
        # Stack them into the param_table to reuse them
        df <- data.frame(matrix(NA, nrow = length(res), ncol = length(res[[1]])), stringsAsFactors = F)
        names(df) <- names(res[[1]])
        for (i in seq_along(res)) {
          df[i, ] <- res[[i]]
        }
        qry$analytic$param_table <- rbind(qry$analytic$param_table, df)
        prm <- res[[1]]
        save_table <- T
      }
      # Add the selected param into the query set
      n_sel <- length(qry$columns)
      qry$columns[[n_sel+1]] <- prm
    }
    if ( save_table) {
      save_paramtable(qry$analytic)
    }
    return(qry)
  }

#' @export
range <-
  function(qry, tstart, tend)
  {
    if ( !is.numeric(c(tstart, tend)) ) {
      stop(sprintf("The values for time range should be numeric. Given values are from %s to %s.", tstart, tend))
    }
    qry$queryset[["start"]] <- tstart
    qry$queryset[["end"]]   <- tend
    return(qry)
  }

#' @export
run.TsQuery <-
  function(qry, flight, start = NULL, end = NULL, timepoint = NULL)
  {
    for (i in seq_along(qry$columns)) {
      if ( !(is.null(start) || is.null(end)) ) {
        qry <- range(qry, start, end)
      }
      if ( !is.null(timepoint) ) {
        warning("run.TsQuery: Defining time points is not yet supported.")
      }
      p <- qry$columns[[i]]
      q <- qry$queryset
      q$select <- list(list(analyticId = p$id))
      r <- request(qry$connection, rtype="POST",
                   uri_keys = c("analytic", "query"),
                   uri_args = c(qry$ems_id, flight),
                   jsondata = q)
      res <- content(r)
      if ( !is.null(res$message) ) {
        stop(sprintf('API query for flight = %s, parameter = "%s" was unsuccessful.\nHere is the massage from API: %s',
                     flight, p$name, res$message))
      }
      if (i == 1) {
        df <- data.frame(unlist(res$offsets))
        names(df) <- "Time (sec)"
      }
      df <- cbind(df, unlist(res$results[[1]]$values))
      names(df)[i+1] <- p$name
    }
    return(df)
  }

#' @export
run_multiflts <-
  function(qry, flight, start = NULL, end = NULL, timepoint = NULL)
  {

    # input argument "flight" as multi-column data
    res <- list()

    attr_flag <- F
    if ( class(flight) == "data.frame" ) {
      FR <- flight[ , "Flight Record"]
      attr_flag <- T
    } else {
      FR <- flight
    }
    cat(sprintf("=== Start running time series data querying for %d flights ===\n", length(FR)))
    for (i in 1:length(FR)) {
      cat(sprintf("%d / %d: FR %d\n", i, length(FR), FR[i]))
      res[[i]] <- list()
      if ( attr_flag ) {
        res[[i]]$flt_data <- as.list(flight[i, ])
      } else {
        res[[i]]$flt_data <- list("Flight Record" = FR[i])
      }
      res[[i]]$ts_data <- run.TsQuery(qry, FR[i], start = start[i], end = end[i], timepoint = timepoint[i])
    }
    return(res)
  }





