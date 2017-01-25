# R - EMS API Tools and Documentation

## Rems

Rems is a R wrapper for the EMS database API.

With Rems package, you will be able to retrieve the EMS data from R without low-level knowledge of the EMS RESTful API. The project is still in very early alpha stage, so is not guarranteed to be working reliably nor well documented.

Dependencies:
* httr
* jsonlite

### Installation
```r
install.packages("devtools")
devtools::install_git("https://github.com/ge-flight-analytics/ems-api-sdk")
```

### Connect to EMS
The first step is to create a connection using your credentials. 

```r
library(Rems)
conn <- connect({efoqa_usr}, {efoqa_pwd})
```

In case you are behind a proxy, provide the proxy details with the optional `proxies` argument.

```r
conn <- connect({username},
                {password},
                proxies = list(url  = {proxy_address},
                               port = {port},
                               usr  = {proxy_user},
                               pwd  = {proxy_password}))
```
### Flight Querying

#### Instantiate Query
```r
qry <- flt_query(conn, "ems9")
```
Current limitations:

* The current query object only supports the FDW Flight data source, which seems to be reasonable for POC functionality.
* Right now a query can be instantiated only with a single EMS system connection. I think a query object with multi-EMS connection could be quite useful for data analysts who want to study pseudo-global patterns.
    + Ex) `query <- flt_query(c, ems_name = c('ems9', 'ems10', 'ems11'))`

#### Database Setup

The EMS system uses a tree structure for organizing fields. This field tree manages mappings between names and field IDs as well as the field groups of fields. In order to send a flight query via EMS API, the Rems package will automatically generate a data file that stores a static, frequently used portion of the field tree and load it by default. This bare field tree includes fields from the following field groups:

* Flight Information (sub-field groups Processing and Profile 16 Extra Data were excluded)
* Aircraft Information 
* Flight Review
* Data Information
* Navigation Information
* Weather Information

In case you want to query with fields that are not included in this default, stripped-down data tree, you'll have to add the field group in which the fields you want belong and update your data field tree. For example, if you want to add a field group branch such as Profiles --> Standard Library Profiles --> Block-Cost Model --> P301: Block-Cost Model Planned Fuel Setup and Tests --> Measured Items --> Ground Operations (before takeoff), the execution of the following method will add the fields and their related subtree structure to the basic tree structure. You can use either the full name or just a fraction of consecutive keywords of each field group. The keyword is case insensitive.
 
**Caution**: the process of adding a subtree usually requires a very large number of recursive RESTful API calls which takes quite a long time. Please try to specify the subtree at as low a level as possible to avoid a long processing time.
 
```r
qry <- update_datatree(qry, "profiles", "standard", "block-cost", "p301", "measured", "ground operations (before takeoff)")
```
As it runs successfully, the console will print the progress similar to below. 
```
## === Starting to add subtree from 'Ground Operations (before takeoff)' ===
## On Ground Operations (before takeoff) ...
## On Start and Push ...
## On Measurements ...
## On Fuel ...
## On Fuel Management (total) ...
##    .
##    .
## On English ...
## -- Added 5 fields
## On Metric ...
## -- Added 5 fields
##    . 
##    .
##    .

```

You can save and load the updated data tree for later usage in R's Rds file format.
```r
save_datatree(qry, file_name = "my_datatree.rds")

qry <- load_datatree(qry, file_name = "my_datatree.rds")
```

#### Build Flight Query

##### Select
Select the columns to include in your query. Again you can pass consecutive words of the full field names as keywords which are case insensitive.
```r
qry <- select(qry, "flight date", "customer id", "takeoff valid", "takeoff airport code")
```
You will have to make a separate `select` function call if you want to aggregate a field.
```r
qry <- select(qry,
              "P301: duration from first indication of engines running to start",
              aggregate = "avg")
```

##### Group by & Order by

Similarly, you can pass the grouping and ordering conditions:
```r
qry <- group_by(qry, "flight date", "customer id", "takeoff valid", "takeoff airport code")
```

```r
qry <- order_by(qry, "flight date")
```

##### Additional Conditions

If you want to get unique rows only (which is already set on as default),
```r
qry <- distinct(qry) # identical to distinct(qry, TRUE)
# If you want to turn off "distinct", do qry <- distinct(qry, FALSE)
```
Optionally you can control the number of rows that will be returned. The following code will set the top 5000 rows as your returned data.
```r
qry <- get_top(qry, 5000)
```

##### Filtering

Currently the following conditional operators are supported with respect to the data field types:
* Number: "==", "!=", "<", "<=", ">", ">="
* Discrete: "==", "!=", "in", "not in" (Filtering condition made with value, not discrete integer key)
* Boolean: "==", "!="
* String: "==", "!=", "in", "not in"

Following is an example:
```r
qry <- filter(qry, "'flight date' >= '2016-1-1'")
qry <- filter(qry, "'takeoff valid' == TRUE")
qry <- filter(qry, "'customer id' in c('CQH', 'EVA')")
qry <- filter(qry, "'takeoff airport code' == 'TPE'")
```
The current filter method has the following limitation:
* Single filtering condition for each filter method call
* Each filtering condition is combined only by "AND" relationship
* The field keyword must be on the left-hand side of a conditional expression
* No support for NULL value filtering, which is being worked on now
* The datetime condition only supports ISO8601 format

### Checking the Generated JSON Query

You can check what the JSON query would look like before sending the query.
```r
json_str(qry)
```
This will print the following JSON string:
```
## {
##   "select": [
##     {
##       "fieldId": "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-core][base-field][flight.exact-date]]]",
##       "aggregate": "none"
##     },
##     {
##       "fieldId": "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-fcs][base-field][fdw-flight-extra.customer]]]",
##       "aggregate": "none"
##     },
##     {
##       "fieldId": "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-core][base-field][flight.exist-takeoff]]]",
##       "aggregate": "none"
##     },
##     {
##       "fieldId": "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-core][base-field][flight.airport-takeoff]]]",
##       "aggregate": "none"
##     },
##     {
##       "fieldId": "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-apm][flight-field][msmt:profile-b1ff12e2a2ff4da68bfbadfbe8a14acc:msmt-2df6b3503603472abf4b52feba8bf128]]]",
##       "aggregate": "avg"
##     }
##   ],
##   "groupBy": [
##     {
##       "fieldId": "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-core][base-field][flight.exact-date]]]"
##     },
##     {
##       "fieldId": "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-fcs][base-field][fdw-flight-extra.customer]]]"
##     },
##     {
##       "fieldId": "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-core][base-field][flight.exist-takeoff]]]"
##     },
##     {
##       "fieldId": "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-core][base-field][flight.airport-takeoff]]]"
##     }
##   ],
##   "orderBy": [
##     {
##       "fieldId": "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-core][base-field][flight.exact-date]]]",
##       "order": "asc",
##       "aggregate": "none"
##     }
##   ],
##   "distinct": true,
##   "top": 5000,
##   "format": "none",
##   "filter": {
##     "operator": "and",
##     "args": [
##       {
##         "type": "filter",
##         "value": {
##           "operator": "dateTimeOnAfter",
##           "args": [
##             {
##               "type": "field",
##               "value": "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-core][base-field][flight.exact-date]]]"
##             },
##             {
##               "type": "constant",
##               "value": "2016-1-1"
##             },
##             {
##               "type": "constant",
##               "value": "Utc"
##             }
##           ]
##         }
##       },
##       {
##         "type": "filter",
##         "value": {
##           "operator": "isTrue",
##           "args": [
##             {
##               "type": "field",
##               "value": "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-core][base-field][flight.exist-takeoff]]]"
##             }
##           ]
##         }
##       },
##       {
##         "type": "filter",
##         "value": {
##           "operator": "in",
##           "args": [
##             {
##               "type": "field",
##               "value": "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-fcs][base-field][fdw-flight-extra.customer]]]"
##             },
##             {
##               "type": "constant",
##               "value": 18
##             },
##             {
##               "type": "constant",
##               "value": 11
##             }
##           ]
##         }
##       },
##       {
##         "type": "filter",
##         "value": {
##           "operator": "equal",
##           "args": [
##             {
##               "type": "field",
##               "value": "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-core][base-field][flight.airport-takeoff]]]"
##             },
##             {
##               "type": "constant",
##               "value": 5256
##             }
##           ]
##         }
##       }
##     ]
##   }
## }
```
#### Reset Flight Query
In case you want to start over for a fresh new query,
```r
qry <- reset(qry)
```
Which will erase all the previous query settings.

#### Finally, Run the Flight Query

`run()` method will send the query and translate the response from EMS in R's dataframe.
```r
df <- run(qry)

# This will return your data in R's dataframe format.
```
EMS API supports two different query executions which are synchronous and asynchronous queries. The synchronous query has a data size limit for the output data, which is 25000 rows. On the other hand, the async query is able to handle large output data by letting you send repeated requests for smaller batches of the large output data.

The `run()` method takes care of the repeated async request for a query whose returning data is expected to be large.

The batch data size for the async request is set to 25,000 rows by default (which is the maximum). If you want to change this size,
```r
# Set the batch size as 20,000 rows per request
df <- query.run(qry, n_row = 20000)
```

### Querying Time-Series Data
You can query data of time-series parameters with respect to individual flight records. Below is a simple example that sends a flight query first in order to retrieve a set of flights and then sends queries to get some of the time-series parameters for each of these flights.

```r
# You should instantiate an EMS connection first.

# Flight query with an APM profile. It will return data for 10 flights
fq <- flt_query(conn, "ems9")
fq <- load_datatree(fq, "stat_taxi_datatree.rds")
fq <- select(fq,
             "customer id", "flight record", "airframe", "flight date (exact)",
             "takeoff airport code", "takeoff airport icao code", "takeoff runway id",
             "takeoff airport longitude", "takeoff airport latitude",
             "p185: processed date", "p185: oooi pushback hour gmt",
             "p185: oooi pushback hour solar local",
             "p185: total fuel burned from first indication of engines running to start of takeoff (kg)")
fq <- order_by(fq, "flight record", order = 'desc')
fq <- get_top(fq, 10)
fq <- filter(fq,
             "'p185: processing state' == 'Succeeded'")
flt <- run(fq)

# === Run time series query for each flight ===

# Instantiate a time-series query for the same EMS9
tsq <- tseries_query(conn, "ems9")

# Select 7 example time-series params that will be retrieved for each of the 10 flights
tsq <- select(tsq, "baro-corrected altitude", "airspeed (calibrated; 1 or only)", "ground speed (best avail)",
                  "egt (left inbd eng)", "egt (right inbd eng)", "N1 (left inbd eng)", "N1 (right inbd eng)")

# Run mult-flight repeated query
xdata <- run_multiflts(tsq, flt, start = rep(0, nrow(flt)), end = rep(15*60, nrow(flt)))
```
The inputs to function `run_mutiflts(...)` are:
* qry  : time-series query instance
* flt  : a vector of Flight Records or flight data in R dataframe format. The R dataframe should have a column of flight records with its column name "Flight Record"
* start: a vector defining the starting times (secs) of the timepoints for individual flights. The vector length must be the same as the number of flight records
* end  : a vector defining the end times (secs) of the timepoints for individual flights. The vector length must be the same as the number of flight records

The output will be R list object whose elements contain the following data:
* flt_data : R list. Copy of the flight data for each flight
* ts_data  : R dataframe. the time series data for each flight

In case you just want to query for a single flight, `run(...)` function will be better suited. Below is an example of time-series querying for a single flight.
```r
xdata <- run(tsq, 1901112, start = 0, end = 900)
```
This function will return an R dataframe that contains timepoints from 0 to 900 secs and corresponding values for selected parameters.
