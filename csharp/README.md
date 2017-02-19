# C\# EMS API Tools and Documentation

## Prerequisites
To build the projects, you will need either:
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
* In the EmsApi.Client directory, open EmsApi.Client.sln for Visual Studio 2017, or EmsApi.Client.Vs2015.sln for Visual Studio 2015.
* Build the project, and the generated assembly will end up in EmsApi.Client\bin.
* Build and examine the example projects using Examples\EmsApi.Examples.sln
* Include the EmsApi.Client.dll assembly in your project.
	* You will also need ot include nuget references for "System.Net.Http 4.3" and "Refit 3.0.1", until EmsApi.Client.dll is also diributed as a nuget package.
	* The library has a compilation target of .NET standard 1.4, which means it can be used directly from .NET Framework 4.6.1+ and .NET Core 1.1.
		* Also some other stuff like UWP and Xamarin.

## How to implement new routes
* Add model objects for the responses to V2/Model
* Add the route definitions to Service/IEmsApi.cs
* Implement a wrapper class for the route based on Wrappers/EmsApiRouteWrapper.cs
* Add a public property for the wrapper class to Service/EmsApiService.cs, and add it to the Initialize function.

## Todo
* Implement trusted service authentication, only user / pass works currently.
* Figure out if there are threading issues with the authentication class.
    * Specifically if there's a problem with race conditions when getting a new token.
* Add unit test project (would work similar to how the dotnet core example works now)
* The client library project files for VS2015 and VS2017 use different formats. The VS2017 format automatically includes everything under the directory unless it is explicitly told to ignore it. The VS2015 project must explicitly list the cs files in the csproj file. Therefore, when files are added or moved right now, the VS2015 project will need to be updated.
* Add DTO classes

## Bugs
* If DNS cannot resolve the endpoint URI it never times out (or takes a long time).
* Authentication failure is not well handled by the synchronous methods that call async interface methods, like EmsApiService.EmsSystems.GetAll().