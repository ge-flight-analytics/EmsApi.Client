* Adds support for Trusted Authentication (this can be used stand alone or with Password Authentication in a single instance)
* Removes the EMS System Id as a setting (as this is always 1 in Azure)
* Removed a race condition when multiple authentications were in flight simultaneously
* Added the ability to specify some settings (e.g. X-Adi-CorrelationId) per call
* Removed the standard endpoints as these are per-EMS instance now