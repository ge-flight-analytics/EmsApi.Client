# C\# EMS API Tools and Documentation

[![Build status](https://ci.appveyor.com/api/projects/status/396p7tm0hfyeyd98?svg=true)](https://ci.appveyor.com/project/GEAviationFlightAnalytics/emsapi-client)

# Getting started

## Include the nuget package in your project

### .NET Core
* Create a new library or console project using `dotnet new classlib` or `dotnet new console`.
* Add a referece to the EmsApi package with `dotnet add package EmsApi.Client`.

### .NET Framework
* Start a new project or solution in Visual Studio using `.NET Framework 4.6.1+`.
* In Solution Explorer, right click the References entry under the project and select Manage Nuget Packages...
* Search for "EmsApi", select and install "EmsApi.Client".

## Connect to the api

A username and password must always be specified to connect to the API. They may be retrieved from environment variables as well.

```cs
using System;
using EmsApi.Client.V2;

namespace TestApp
{
class Program
{
	static void Main( string[] args )
	{
		var config = new EmsApiServiceConfiguration()
		{
			// If this is not set, it will be retrieved from the "EmsApiEndpoint" environemnt variable.
			// If neither are set, it will default to EmsApiEndpoints.Default.
			Endpoint = "https://myapiserver/api",
			
			// If this is not set, it will be retrieved from the "EmsApiUsername" environment variable.
			UserName = "my-username",

			// If this is not set, it will be retrieved from the "EmsApiPassword" environment variable.
			// The environment variable must be a base64 encoded UTF-8 string containing the password.
			Password = "p@ssw0rd",

			// Optional application name to use, this is passed to the API server to assist in debugging
			ApplicationName = "MyCoolApp"
		};

		using( EmsApiService api = new EmsApiService( config ) )
		{
			// Your code goes here, call functions on the EmsApiService object...
		}
	}
}
}
```

## Set a cached ems system

Most method calls have an emsSystem argument to indicate which ems system to talk to. This may be omitted by setting the CachedEmsSystem property on the service object.

```cs
// This pattern passes the EMS system to every method call:
using( EmsApiService api = new EmsApiService( config ) )
{
	var fleets = api.Assets.GetAllFleets( emsSystem: 1 );
	var aircraft = api.Assets.GetAllAircraft( emsSystem: 1 );
}

// This pattern sets a cached EMS system to use instead:
using( EmsApiService api = new EmsApiService( config ) )
{
	// Send all requests to the ems system with the id 1.
	api.CachedEmsSystem = 1;
	var fleets = api.Assets.GetAllFleets();
	var aircraft = api.Assets.GetAllAircraft();
}
```

## Access different API functions through properties on the EmsApiService object.

Access to different API routes is exposed through various properties on the EmsApiService class. For instance,
to query the database you would use the `api.Databases` property, and call one of the methods on that object.

![API intellisense](docs/api_intellisense.gif)

## Query the database

The EmsApi.Dto.V2 namespace contains objects that are returned from API calls, as well as a few types that are required to make API calls, including the database query routes.

```cs
using System;
using EmsApi.Client.V2;
using EmsApi.Dto.V2;

namespace TestApp
{
class Program
{
	static void Main( string[] args )
	{
		using( EmsApiService api = new EmsApiService() )
		{
			var query = new DatabaseQuery();

			// Select some fields in the query.
			query.SelectField( Monikers.FlightId );
			query.SelectField( Monikers.TailNumber );
			query.SelectField( Monikers.CityPair );
			query.SelectField( Monikers.TakeoffAirportName );
			query.SelectField( Monikers.LandingAirportName );

			// Filter for takeoff and landing valid.
			query.AddBooleanFilter( Monikers.TakeoffValid, true );
			query.AddBooleanFilter( Monikers.LandingValid, true );

			// Order by flight id.
			query.OrderByField( Monikers.FlightId );

			// Limit to first 100 flights.
			query.Top = 100;

			// Execute the query and receive the result.
			DatabaseQueryResult result = api.Databases.Query( Monikers.FlightDatabase, query, emsSystem: 1 );
			foreach( DatabaseQueryResult.Row row in result.Rows )
			{
				// Go through each result row and print out the information.
				int flightId = Convert.ToInt32( row[Monikers.FlightId] );
				string tail = row[Monikers.TailNumber].ToString();
				string cityPair = row[Monikers.CityPair].ToString();
				string takeoffAirport = row[Monikers.TakeoffAirportName].ToString();
				string landingAirport = row[Monikers.LandingAirportName].ToString();

				string msg = string.Format( "Flight {0} with tail number {1} from {2} to {3} ({4}).",
					flightId, tail, takeoffAirport, landingAirport, cityPair );
				Console.WriteLine( msg );
			}
		}
	}

	private static class Monikers
	{
		public static string FlightDatabase = "[ems-core][entity-type][foqa-flights]";
		public static string FlightId = "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-core][base-field][flight.uid]]]";
		public static string TailNumber = "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-core][base-field][flight.aircraft]]]";
		public static string CityPair = "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-core][base-field][city-pair.pair]]]";
		public static string TakeoffAirportName = "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[[nav][type-link][airport-takeoff * foqa-flights]]][[nav][base-field][nav-airport.name]]]";
		public static string LandingAirportName = "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[[nav][type-link][airport-landing * foqa-flights]]][[nav][base-field][nav-airport.name]]]";
		public static string TakeoffValid = "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-core][base-field][flight.exist-takeoff]]]";
		public static string LandingValid = "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-core][base-field][flight.exist-landing]]]";
	}
}
}
```

## Query time-series flight data

This query works similarly to the database query. You construct a query object, and then add some parameter ids to include, and then execute it for a specific flight.

```cs
using System;
using System.Linq;
using EmsApi.Client.V2;
using EmsApi.Dto.V2;

namespace TestApp
{
class Program
{
	static void Main( string[] args )
	{
		using( EmsApiService api = new EmsApiService() )
		{
			var query = new AnalyticQuery();

			// Include radio altitude id in the time-series query.
			query.SelectAnalytic( RadioAltitudeId );

			// Execute the query.
			QueryResult result = api.Analytics.QueryResults( flightId: 1, query: query, emsSystem: 1 );

			// Access the results.

			double[] offsets = result.Offsets.ToArray();

			// Note: The Results array contains one object per parameter in the query, but we only
			// added one parameter so we take the first one here.
			object[] values = result.Results.First().Values.ToArray();
			for( int i = 0; i < offsets.Length; ++i )
			{
				int offset = Convert.ToInt32( offsets[i] );
				double value = Convert.ToDouble( values[i] );

				Console.WriteLine( "{0}: {1}", offset, value );
			}
		}
	}

	private const string RadioAltitudeId = "H4sIAAAAAAAEAG2QQQuCQBCF74H/Qby7qyUUokJQB8EuidB1WzcdWFfbXbOf35ZYSb3DY2DmY95MdGSq5Tdy5iwtmdBwASbte8OFCiF2aq27EONhGNCwQq2s8NLzfHw6ZDmtWUNcEEoTQZkzMm9CvdoKlUQT2gotCdUTH2BvjbcloEISJ7EWth2NKZhMy2QqFdo3KmsroIRH+GtgBuQdoyYz3Zk9NoQCeOxo2Zs8+P9gIeDam1sTb5TvGtu4gfFnNdqkz94f3FpMzfnvkgfLSh/6UgEAAA==";
}
}
```

## Environment variables

The library supports using environment variables for configuration instead of specifying the configuration in your application.

| Variable Name | Description |
| ------------- | ----------- |
| EmsApiEndpoint | The API url to communicate with. This url must include the /api portion, such as "https://localhost/api". |
| EmsApiUsername | The username to use for authentication with the api. |
| EmsApiPassword | The base64 encoded password for the user. |

# Building from source

## Build Prerequisites
* Dotnetcore 2.0+

## Compilation targets
The project is built as a .netstandard2.0 library, which will allow it to work with .NET Framework 4.6.1+ and .NET core 2.0+. Supported frameworks can be found [here](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard2.0.md).

## Build the repository
* Open `EmsApi.sln` and build the whole solution, or run `dotnet build` in the root directory.

## Try out the examples
* Clone the repository.
* Change directories to `Examples\DotnetCoreConsole`
* Run `dotnet run`.
* Open the other examples in the appropriate editor (Winforms / WPF requires Visual Studio)

## Include the library in your own project (using source)
* Download this repository and build `EmsApi.sln`
* Start a new project or solution in Visual Studio.
* In the Solution Explorer, right click the References entry under the project and choose Add Reference...
* Select Browse, locate the output directory under `.\Client\bin`
	* Check the checkbox for `EmsApi.Client.dll` and `EmsApi.Dto.dll`
* Add nuget references for `Refit 4.6.30` and `Newtonsoft JSON 11.0.2` to your project.

# Developing

## Editor Prerequisites
* Text editor or IDE of your choice
* An EditorConfig extension, to enforce code style rules
* The SpecFlow extension for writing unit tests.

## Build
The library can be built using any of the following options:
* Running `dotnet build` in the root of the repository
* Running the `build.ps1` script in powershell or powershell core
* Opening the `EmsApi.sln` file with visual studio and building

## Test
Tests can be run in the following manner:
* Running `dotnet test` in the `.\Tests` directory to run xunit tests
* Running the `test.ps1` script in powershell or powershell core to run all supported tests
* Opening the `.\Tests\SpecFlow\EmsApi.Tests.SpecFlow.sln` file with visual studio and using the test runner to run xunit and specflow tests

Currently tests are split between two projects, one for regular xunit tests and one for SpecFlow tests. Once SpecFlow supports .net core everything can be done with dotnet test and a single project. SpecFlow tests must be run on Windows and should be run before a release.

## Versioning
* Builds for all branches create release candidate nuget packages that are pushed to the appveyor feed.
* Release packages are generated by creating a new github release with the `vN.N.N`  tag, or creating and pushing the tag manually, where N.N.N is the new version number.

## Releases
* Prior to a release, the version number should be updated in `appveyor.yml` and the various `csproj` files in the repository, and pushed to master.
* The release should be created in github using the `vN.N.N` tag, where N.N.N is the new version number.
* If the tests pass on appveyor, the new packages will be published to nuget.org