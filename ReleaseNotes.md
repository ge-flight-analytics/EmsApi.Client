# Release 2.29.0
* Adds new query parameter for the profile results endpoint to turn off diagnostics in the response.
* Changes the ProfileResults DTO to use ProfileResults2 instead of ProfileResults. This already existed and is the correct response type.
* Updates to the ems-api.json file to reflect the latest from EMS. Note: the are some manual tweaks here to support empty ProcessingInformation for ProfileResults2.