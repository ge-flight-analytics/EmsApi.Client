# C\# EMS API Tools and Documentation

[![Build status](https://ci.appveyor.com/api/projects/status/396p7tm0hfyeyd98?svg=true)](https://ci.appveyor.com/project/GEAviationFlightAnalytics/emsapi-client)

# Getting Started

## Prerequisites
* Visual Studio 2015 with Update 3
	* Community edition is supported, but not C# Express, because it does not contain all the necessary portable project types.
* With the [Latest version of .NET core tooling (preview 2)](https://marketplace.visualstudio.com/items?itemName=JacquesEloff.MicrosoftASPNETandWebTools-9689)
* And the EditorConfig extension, to load the project .editorconfig file (this is natively supported in VS2017). This will enforce the code formatting rules:
	* Spaces for indentation (4 spaces per indentation).
	* Lines must end in crlf with no trailing whitespace.
* And the Specflow extension, if you are writing or running tests.

## Build the repository
* Open `EmsApi.sln` and build the whole solution, or run the `build.ps1` file.

## Try out the examples
* Clone the repository.
* Open `Examples\EmsApi.Example.sln`
* Rebuild the solution.
* Right click one of the examples in the Solution Explorer and select "Set as StartUp Project". Use the Debug > Start Debugging menu item to run the example.

## Create a new example project
* Add a new project in the `Examples\EmsApi.Example.sln` solution. Choose your desired flavor of .NET and add the project to the Examples directory.
	* *Note:* If you're using .NET framework, the target framework of the project needs to be updated to at least ".NET Framework 4.6.1".
* In the Solution Explorer, right click the References entry under the project and choose Add Reference...
* Select Browse, select `bin\EmsApi.Client.dll`
	* Make sure the checkbox next to the file is checked in visual studio, and press OK.
* Add nuget references for `System.Net.Http 4.3`+ and `Refit 3.0.1` in your project.

## Include the library in your own project (using nuget)
* Start a new project or solution in Visual Studio.
	* *Note:* If you're using .NET framework, the target framework of the project needs to be updated to at least ".NET Framework 4.6.1".
* In the Solution Explorer, right click the References entry under the project and select Manage Nuget Packages...
* In the Package source box at the top right, select the ems-api-sdk package source.
	* If it's not already in the list, click the gear icon next to the dropdown, and add a new source with the name "ems-api-sdk", and the [Appveyor project nuget package source](https://ci.appveyor.com/nuget/ems-api-sdk) URL.
	* The appveyor feed is the only way to get packages currently. Once the library reaches 1.0 we will provide nuget.org packages as well.
* Search or browse for "EmsApi.Client" and install.

## Include the library in your own project (using source)
* Download this repository and build `EmsApi.sln`
* Start a new project or solution in Visual Studio.
* In the Solution Explorer, right click the References entry under the project and choose Add Reference...
* Select Browse, locate `bin\EmsApi.Client.dll` under this directory.
	* Make sure the checkbox next to the file is checked in visual studio, and press OK.
* Add nuget references for `System.Net.Http 4.3`+ and `Refit 3.0.1` in your project.
* The library has a compilation target of .NET standard 1.4, which means it can be used directly from .NET Framework 4.6.1+ and .NET Core 1.1, and some other stuff like UWP and Xamarin.

# Details

## Current Status
* All of the routes have been implemented. 
* Now the focus is on 
	* Building more .NET friendly ways of using the routes, through the Access classes.
	* Updating documentation
	* Building examples


## Todo
* Finish implementing non-admin routes, the following still need to be completed:
	* Transfer APIs: /v2/ems-systems/{emsSystemId}/uploads/...
* Add specflow tests for Analytics routes.
* Add tests for async query pagination.
* Finish AnalyticQuery object
* Fix analytic metadata test
* Add an anlytic test that pulls a parameter instead of a constant.
* Copy doc comments from IEmsApi to the access classes, since those are used directly by clients.
* Make release process, have releases push to nuget.org
* Implement trusted service authentication, only user / pass works currently.
* Make authentication properly async, and figure out if there are other authentication threading issues with respect to getting a new token.
	* The only implementation is synchronous right now.
* Write some async tests, none of the async code has been used / tested currently
	* Although the synchronous methods actually call the async methods and await the result, error handling might not work correctly when using the async methods directly from client code.
* Create a script for building library and example with dotnet.exe
* Create a dotnet core docker example.
* Create a dotnet framework docker example.

## Bugs
* If DNS cannot resolve the endpoint URI, authentication takes a long time to time out.