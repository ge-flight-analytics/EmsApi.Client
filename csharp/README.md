# C\# EMS API Tools and Documentation

# Getting Started

## Prerequisites
To build the projects, you will need either:
* Visual Studio 2015 with Update 5 and the latest version of .NET core tooling (preview 2).
	* [MicrosoftASPNETandWebTools-9689](https://marketplace.visualstudio.com/items?itemName=JacquesEloff.MicrosoftASPNETandWebTools-9689)
* Visual Studio 2017 Release Candidate, with the cross-platform development package installed (.net core tools).
* All projects should work on the Community edition of Visual Studio, but it has not been tested.

## Try out the examples
* Clone the repository.
* Open EmsApi.Example.sln in the Examples directory if you're using Visual Studio 2017, or EmsApi.Example.Vs2015.sln if you're using Visual Studio 2015.
* Rebuild the solution.
* Right click one of the examples in the Solution Explorer and select "Set as StartUp Project". Use the Debug > Start Debugging menu item to run the example.

## Start a new example project
* Add a new project in the EmsApi.Example solution. Choose your desired flavor of .NET and add the project to the Examples directory.
* Modify the Solution's properties to have the new project depend on EmsApi.Client.
* In the Solution Explorer, right click the References entry under the project and choose Add Reference...
* Select Browse, select EmsApi.Client\bin\EmsApi.Client.dll
	* Make sure the checkbox next to the file is checked in visual studio, and press OK.
* Add nuget references for "System.Net.Http 4.3" and "Refit 3.0.1" in your project, until this library is distributed as a nuget package.

## Include the library in your own project.
* Start a new project or solution in Visual Studio.
* In the Solution Explorer, right click the References entry under the project and choose Add Reference...
* Select Browse, locate EmsApi.Client\bin\EmsApi.Client.dll under this directory.
	* Make sure the checkbox next to the file is checked in visual studio, and press OK.
* Add nuget references for "System.Net.Http 4.3" and "Refit 3.0.1" in your project, until this library is distributed as a nuget package.
* The library has a compilation target of .NET standard 1.4, which means it can be used directly from .NET Framework 4.6.1+ and .NET Core 1.1, and some other stuff like UWP and Xamarin.

# Details

## Current Status
* Only the Ems System routes are implemented, since they were the easiest to work through as a start. 
* It's fairly straightforward to implement new routes (see section below).

## C\# DTO classes
The C\# DTO classes are available upon request.		

## How to implement new routes
* Add model objects for the responses to V2/Model
* Add the route definitions to Service/IEmsApi.cs
* Implement a wrapper class for the route deriving from Wrappers/EmsApiRouteWrapper.cs
* Add a public property for the wrapper class to Service/EmsApiService.cs, and add it to the Initialize function.

## Todo
* Implement the rest of the routes.
* Figure out if an example project can reference the library by the VS Project reference thing.
* Implement trusted service authentication, only user / pass works currently.
* Figure out if there are threading issues with the authentication class.
    * Specifically if there's a problem with race conditions when getting a new token.
* Add unit test project (would work similar to how the dotnet core example works now)
* The client library project files for VS2015 and VS2017 use different formats. The VS2017 format automatically includes everything under the directory unless it is explicitly told to ignore it. The VS2015 project must explicitly list the cs files in the csproj file. Therefore, when files are added or moved right now, the VS2015 project will need to be updated.
* Create a script for building library and example with dotnet.exe
* Create a dotnet core docker example.
* Create a dotnet framework docker example.
* Add DTO classes

## Bugs
* If DNS cannot resolve the endpoint URI it never times out (or takes a long time).
* Authentication failure is not well handled by the synchronous methods that call async interface methods, like EmsApiService.EmsSystems.GetAll().