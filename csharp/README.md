# C\# EMS API Tools and Documentation

[![Build status](https://ci.appveyor.com/api/projects/status/h45t0p9hd6cutcyw?svg=true)](https://ci.appveyor.com/project/GEAviationFlightAnalytics/ems-api-sdk)

# Getting Started

## Prerequisites
To build the projects, you will need either:
* Visual Studio 2015 with Update 3
	* [Latest version of .NET core tooling (preview 2)](https://marketplace.visualstudio.com/items?itemName=JacquesEloff.MicrosoftASPNETandWebTools-9689)
	* EditorConfig extension, to load the project .editorconfig file (this is natively supported in VS2017). This will enforce the code formatting rules:
		* Spaces for indentation (4 spaces per indentation).
		* Lines must end in crlf with no trailing whitespace.
* Visual Studio 2017 Release Candidate, with the cross-platform development package installed (.net core tools).

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

## Include the library in your own project (the nuget way).
* Start a new project or solution in Visual Studio.
* In the Solution Explorer, right click the References entry under the project and select Manage Nuget Packages...
* In the Package source box at the top right, select the ems-api-sdk package source.
	* If it's not already in the list, click the gear icon next to the dropdown, and add a new source with the name "ems-api-sdk", and the [Appveyor project nuget package source](https://ci.appveyor.com/nuget/ems-api-sdk) URL.
	* The appveyor feed is the only way to get packages currently. Once the library reaches 1.0 we will provide nuget.org packages as well.
* Search or browse for "EmsApi.Client" and install.

## Include the library in your own project (the build it yourself way).
* Download this repository and build EmsApi.Client.sln
* Start a new project or solution in Visual Studio.
* In the Solution Explorer, right click the References entry under the project and choose Add Reference...
* Select Browse, locate EmsApi.Client\bin\EmsApi.Client.dll under this directory.
	* Make sure the checkbox next to the file is checked in visual studio, and press OK.
* Add nuget references for "System.Net.Http 4.3" and "Refit 3.0.1" in your project.
* The library has a compilation target of .NET standard 1.4, which means it can be used directly from .NET Framework 4.6.1+ and .NET Core 1.1, and some other stuff like UWP and Xamarin.

# Details

## Current Status
* Only the Ems System routes are implemented, since they were the easiest to work through as a start. 
* It's fairly straightforward to implement new routes (see section below).

## C\# DTO classes
The C\# DTO classes are available upon request.		

## How to implement new routes
* Add model objects for the responses to V2\Model
* Add the route definitions to IEmsApi.cs
* Implement an access class for the route deriving from Access\EmsApiRouteAccess.cs
* Add a public property for the access class to Service\EmsApiService.cs, and add it to the InitializeAccessProperties function.

## Todo
* Implement the rest of the routes.
* Figure out if an example project can reference the library by the VS Project reference thing.
* Implement trusted service authentication, only user / pass works currently.
* Figure out if there are threading issues with the authentication class.
    * Specifically if there's a problem with race conditions when getting a new token.
* The client library project files for VS2015 and VS2017 use different formats. The VS2017 format automatically includes everything under the directory unless it is explicitly told to ignore it. The VS2015 project must explicitly list the cs files in the csproj file. Therefore, when files are added or moved right now, the VS2015 project will need to be updated.
* Test to see if everything builds on VS2015 and VS2017 community editions.
* Create a script for building library and example with dotnet.exe
* Create a dotnet core docker example.
* Create a dotnet framework docker example.
* Add DTO classes

## Bugs
* If DNS cannot resolve the endpoint URI it never times out (or takes a long time).