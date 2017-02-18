# C\# EMS API Tools and Documentation

## Current Status
* Only the Ems System routes are implemented, since they were the easiest to work through as a start. 
* It's fairly straightforward to implement new routes (see section below).

## C\# DTO classes
The C\# DTO classes are available upon request.

## How to get started
* Clone the repository and build EmsApi.Client with Visual Studio 2015 or 2017.
* Include the EmsApi.Client assmembly in your project.
* Take a look at the examples for help, the DotnetCoreConsole example is intended to be very straightforward.

## How to implement new routes
* Add model objects for the responses to V2/Model
* Add the route definitions to Service/IEmsApi.cs
* Implement a wrapper class for the route based on Wrappers/EmsApiRouteWrapper.cs
* Add a public property for the wrapper class to Service/EmsApiService.cs, and add it to the Initialize function.

## Todo
* Implement trusted service authentication, only user / pass works currently.
* Figure out if there are threading issues with the authentication class.
* Add unit test project to test EmsApiService (would work similar to how the dotnet core example works now)
* Add some reasonable timeouts and handling for authentication errors.
* Add DTO classes

## Bugs
* If DNS cannot resolve the endpoint URI it never times out (or takes a long time).
* The WPF example isn't working, there are some issues with references. When debugging it I cannot even load symbols for the API client lib. Maybe I'm building the lib wrong.
* I had to include a redirect to change System.Net.Http 4.1 to 4.0, and had to add the Refit nuget package to get the WPF app to work, but the dotnet core one did not require any dependencies?
    * Even after doing that, when it calls the first API method, the output window shows: Exception thrown: 'System.ArgumentNullException' in mscorlib.dll, and I don't think it ever calls down into the API component.
    * The refit package brings in some dotnet core stuff, I wonder if there is confusion happening there