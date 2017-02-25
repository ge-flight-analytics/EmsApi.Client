# C\# EMS API Tools and Documentation

[![Build status](https://ci.appveyor.com/api/projects/status/h45t0p9hd6cutcyw?svg=true)](https://ci.appveyor.com/project/GEAviationFlightAnalytics/ems-api-sdk)

# Getting Started

## Prerequisites
* Visual Studio 2015 with Update 3
	* [Latest version of .NET core tooling (preview 2)](https://marketplace.visualstudio.com/items?itemName=JacquesEloff.MicrosoftASPNETandWebTools-9689)
	* EditorConfig extension, to load the project .editorconfig file (this is natively supported in VS2017). This will enforce the code formatting rules:
		* Spaces for indentation (4 spaces per indentation).
		* Lines must end in crlf with no trailing whitespace.
	* Specflow extension, if you are writing or running tests.

# Build the repository
* Open `csharp\EmsApi.sln` and build the whole solution, or run the `csharp\build.ps1` file.

## Try out the examples
* Clone the repository.
* Open `csharp\Examples\EmsApi.Example.sln`
* Rebuild the solution.
* Right click one of the examples in the Solution Explorer and select "Set as StartUp Project". Use the Debug > Start Debugging menu item to run the example.

## Start a new example project
* Add a new project in the `csharp\Examples\EmsApi.Example.sln` solution. Choose your desired flavor of .NET and add the project to the Examples directory.
* In the Solution Explorer, right click the References entry under the project and choose Add Reference...
* Select Browse, select `csharp\bin\EmsApi.Client.dll`
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
* Download this repository and build `csharp\EmsApi.sln`
* Start a new project or solution in Visual Studio.
* In the Solution Explorer, right click the References entry under the project and choose Add Reference...
* Select Browse, locate `csharp\bin\EmsApi.Client.dll` under this directory.
	* Make sure the checkbox next to the file is checked in visual studio, and press OK.
* Add nuget references for "System.Net.Http 4.3" and "Refit 3.0.1" in your project.
* The library has a compilation target of .NET standard 1.4, which means it can be used directly from .NET Framework 4.6.1+ and .NET Core 1.1, and some other stuff like UWP and Xamarin.

# Details

## Current Status
* Only a few routes are implemented, but it's fairly straightforward to implement new routes (see section below).

## How to implement new routes
* Add model objects for the responses to V2\Model
* Add the route definitions to IEmsApi.cs
* Implement an access class for the route deriving from Access\EmsApiRouteAccess.cs
* Add a public property for the access class to Service\EmsApiService.cs, and add it to the InitializeAccessProperties function.
* Add a SpecFlow test for the new routes.

## Todo
* Implement the rest of the routes.
* Implement trusted service authentication, only user / pass works currently.
* Make authentication properly async, and figure out if there are other authentication threading issues with respect to getting a new token.
* Test to see if everything builds on VS2015 community edition.
* Create a script for building library and example with dotnet.exe
* Create a dotnet core docker example.
* Create a dotnet framework docker example.

## Bugs
* If DNS cannot resolve the endpoint URI it never times out (or takes a long time).