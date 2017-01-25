ems <- function(conn) {
  obj <- list()
  class(obj) <- "ems"
  obj$connection <- conn
  obj <- update_list(obj)
  return(obj)
}

update_list <- function(obj) {
  conn <- obj$connection
  r <- request(conn, uri_keys = c('ems_sys', 'list'))
  d <- content(r)
  df <- data.frame(id = character(0), name = character(0), stringsAsFactors = FALSE)
  for ( i in 1:length(d) ) {
    df[i, ] <- unlist(d[[i]][c('id','name')])
  }
  df$id <- as.integer(df$id)
  obj$list <- df
  return(obj)
}


get_id <- function(obj, name) {
  name <- toupper(name)
  x <- obj$list$id[obj$list$name==name]
  if ( length(x)==0 ) {
    stop(sprintf('%s is not available at EMS API.', name))
  } else if ( length(x)>1 ) {
    return(x[1])
  }
  return(x)
}


get_name <- function(obj, id) {
  return(obj$name[obj$id==id])
}



