
#' Connect to EMS and get the Auth token
#'
#' @param usr String, FOQA username
#' @param pwd String, FOQA password
#' @param proxies List containing the system proxy information. The list contains attributes "url", "port", "usr", "pwd"
#' @return a Connection object.
#' @export
connect <-
  function(usr, pwd, proxies = NULL)
  {
    # Prevent from the Peer certificate error ("Error in curl::curl_fetch_memory(url, handle = handle) :
    # Peer certificate cannot be authenticated with given CA certificates")
    httr::set_config( config( ssl_verifypeer = 0 ) )

    header <- c("Content-Type" = "application/x-www-form-urlencoded")
    body <- list(grant_type = "password",
                username   = usr,
                password   = pwd)
    uri = paste(uri_root, uris$sys$auth, sep="")

    if (is.null(proxies)) {
      r <- POST(uri,
               add_headers(.headers = header),
               body = body,
               encode = "form")
    } else {
      r <- POST(uri,
               use_proxy(proxies$url, port = proxies$port, username = proxies$usr, password = proxies$pwd),
               add_headers(.headers = header),
               body = body,
               encode = "form")
    }
    if ( !is.null(content(r)$message) ) {
      print(paste("Message:", content(r)$message))
    }

    c <- list(
      foqa = list(usr=usr, pwd=pwd),
      proxies = proxies,
      token = content(r)$access_token,
      token_type = content(r)$token_type
    )
    c
  }

#' @export
reconnect <-
  function(conn)
  {
    return(connect(conn$foqa$usr, conn$foqa$pwd, proxies = conn$proxies))
  }


#' @export
request <-
  function(conn, rtype = "GET", uri_keys = NULL, uri_args = NULL,
           headers = NULL, body = NULL, jsondata = NULL,
           verbose = F)
  {
    # Default encoding is "application/x-www-form-urlencoded"
    encoding <- "form"

    # use proxy
    if (!is.null(conn$proxies)) {
      prxy <- use_proxy(conn$proxies$url,
                        port      = conn$proxies$port,
                        username  = conn$proxies$usr,
                        password  = conn$proxies$pwd)
    } else {
      prxy = NULL
    }

    if (is.null(headers)) {
      headers <- c(Authorization = paste(conn$token_type, conn$token))
    }

    if (!is.null(uri_keys)) {
      uri <- paste(uri_root,
                   uris[[uri_keys[1]]][[uri_keys[2]]],
                   sep = "")
    }

    if (!is.null(uri_args)) {
      uri <- do.call(sprintf, as.list(c(uri, uri_args)))
    }

    if (!is.null(jsondata)) {
      body <- jsondata
      encoding <- "json"
    }

    if (rtype=="GET") {
      tryCatch({
        r <- GET(uri, prxy, query = body, add_headers(.headers = headers), encode = encoding)
      }, error = function(err) {
        print(err)
        cat(sprintf("Http status code %s: %s", status_code(r), content(r)))
        cat("Trying to Reconnect EMS...")
        conn = reconnect(conn)
        r <- GET(uri, prxy, query = body, add_headers(.headers = headers), encode = encoding)
      }

      )

    } else if (rtype=="POST") {
      tryCatch({
        r <- POST(uri, prxy, body = body, add_headers(.headers = headers), encode = encoding)
      }, error = function(err) {
        print(err)
        cat(sprintf("Http status code %s: %s", status_code(r), content(r)))
        cat("Trying to Reconnect EMS...\n")
        conn = reconnect(conn)
        r <- POST(uri, prxy, body = body, add_headers(.headers = headers), encode = encoding)
      })

    } else if (rtype=="DELETE") {
      tryCatch({
        r <- DELETE(uri, prxy, body = body, add_headers(.headers = headers), encode = encoding)
      }, error = function(err) {
        print(err)
        cat(sprintf("Http status code %s: %s", status_code(r), content(r)))
        cat("Trying to Reconnect EMS...\n")
        conn = reconnect(conn)
        r <- DELETE(uri, prxy, body = body, add_headers(.headers = headers), encode = encoding)
      })
    } else {
      stop(sprintf("%s: Unsupported request type.", rtype))
    }
    r
  }


