# 2.0.0

* Adds support for Trusted Authentication
  * This can be used stand alone or with Password Authentication with a single instance
* Removes the EMS System Id as a setting (as this is always 1 in Azure)
* Removed a race condition when multiple authentications were in flight simultaneously
* Added the ability to specify some settings (e.g. X-Adi-CorrelationId) per call

# 1.4.2

* Automatically detect the client version in the user-agent string
* Enable mocking for the access classes set on EmsApiService