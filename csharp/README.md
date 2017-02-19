# C\# EMS API Tools and Documentation

## Prerequisites
The build the projects, you will need either:
* VS2015 with Update 5 and the latest version of .NET core tooling (preview 2): 
	* https://marketplace.visualstudio.com/items?itemName=JacquesEloff.MicrosoftASPNETandWebTools-9689
* VS2017 Release Candidate, with the cross-platform development package installed (.net core tools).

## Current Status
* Only the Ems System routes are implemented, since they were the easiest to work through as a start. 
* It's fairly straightforward to implement new routes (see section below).

## C\# DTO classes
The C\# DTO classes are available upon request.

## How to get started
* Clone the repository.
* Open EmsApi.Client.sln for Visual Studio 2017, or EmsApi.Client.Vs2015.sln for Visual Studio 2015.
* Build the project, the generated assembly will end up in EmsApi.Client\bin.
* Include the generated assembly in your project.
	* You will also need to include a nuget reference to "refit 3.0.1", until this library is also diributed as a nuget package.
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
* Fix VS2015 project for DotnetCoreConsole example. It does not exist right now. It needs to use xproj format.
* The client library project files for VS2015 and VS2017 use different formats. The VS2017 format automatically includes everything under the directory unless it is explicitly told to ignore it. The VS2015 project must explicitly list the cs files in the csproj file. Therefore, when files are added or moved right now, the VS2015 project will need to be updated.
* Add DTO classes

## Bugs
* If DNS cannot resolve the endpoint URI it never times out (or takes a long time).
* The WPF example isn't working, there are some issues with references. When debugging it I cannot even load symbols for the API client lib. Maybe I'm building the lib wrong.
* I had to include a redirect to change System.Net.Http 4.1 to 4.0, and had to add the Refit nuget package to get the WPF app to work, but the dotnet core one did not require any dependencies?
    * Even after doing that, when it calls the first API method, the output window shows: Exception thrown: 'System.ArgumentNullException' in mscorlib.dll, and I don't think it ever calls down into the API component.
    * The refit package brings in some dotnet core stuff, I wonder if there is confusion happening there