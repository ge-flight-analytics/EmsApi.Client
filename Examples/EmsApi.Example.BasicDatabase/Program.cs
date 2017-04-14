using System;
using EmsApi.Client.V2;
using EmsApi.Dto.V2;

namespace EmsApi.Example.BasicDatabase
{
    /// <summary>
    /// Examples using the database access routes.
    /// </summary>
    class Program
    {
        static void Main( string[] args )
        {
            var config = new EmsApiServiceConfiguration();

            // Allow the user to override the endpoint, but provide a default.
            Console.Write( "Enter Endpoint URL [{0}]: ", config.Endpoint );
            string endpoint = Console.ReadLine();
            if( !string.IsNullOrEmpty( endpoint ) )
                config.Endpoint = endpoint;

            Console.Write( "Enter Username [{0}]: ", config.UserName );
            string user = Console.ReadLine();
            if( !string.IsNullOrEmpty( user ) )
                config.UserName = user;

            string defaultPw = !string.IsNullOrEmpty( config.Password ) ? "********" : string.Empty;
            Console.Write( "Enter Password [{0}]: ", defaultPw );
            string password = Console.ReadLine();
            if( !string.IsNullOrEmpty( password ) )
                config.Password = password;

            using( var api = new EmsApiService( config ) )
            {
                api.CachedEmsSystem = 1;
                TestSimpleQuery( api );
                TestLargeQuery( api );
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Runs a simple database query to return a small number of rows.
        /// </summary>
        private static void TestSimpleQuery( EmsApiService api )
        {
            var query = new QueryWrapper();

            // Select a few columns.
            query.SelectField( Monikers.FlightId );
            query.SelectField( Monikers.TakeoffAirport );
            query.SelectField( Monikers.LandingAirport );

            // Limit the number of flights for a simple query.
            const int flightsToPull = 50;
            query.AddConstantFilter( FilterOperator.LessThanOrEqual, Monikers.FlightId, flightsToPull );

            // Order by the uid.
            query.OrderByField( Monikers.FlightId );

            DatabaseQueryResult result = api.Databases.SimpleQuery( Monikers.FlightDatabase, query );
            foreach( DatabaseQueryResult.Row row in result.Rows )
            {
                int flightId = Convert.ToInt32( row[Monikers.FlightId] );
                int takeoffAirport = Convert.ToInt32( row[Monikers.TakeoffAirport] );
                int landingAirport = Convert.ToInt32( row[Monikers.LandingAirport] );

                Console.WriteLine( string.Format( "Flight {0} from {1} to {2}", flightId, takeoffAirport, landingAirport ) );
            }
        }

        private static void TestLargeQuery( EmsApiService api )
        {
            var query = new QueryWrapper();

            // Select a few columns.
            query.SelectField( Monikers.FlightId );
            query.SelectField( Monikers.TakeoffAirport );
            query.SelectField( Monikers.LandingAirport );

            // Limit the number of flights for a simple query.
            const int flightsToPull = 100000;
            query.AddConstantFilter( FilterOperator.LessThanOrEqual, Monikers.FlightId, flightsToPull );

            DatabaseQueryResult result = api.Databases.Query( Monikers.FlightDatabase, query );
            foreach( DatabaseQueryResult.Row row in result.Rows )
            {
                int flightId = Convert.ToInt32( row[Monikers.FlightId] );
                int takeoffAirport = Convert.ToInt32( row[Monikers.TakeoffAirport] );
                int landingAirport = Convert.ToInt32( row[Monikers.LandingAirport] );

                Console.WriteLine( string.Format( "{0},{1},{2}", flightId, takeoffAirport, landingAirport ) );
            }
        }

        private static class Monikers
        {
            public static string FlightDatabase = "[ems-core][entity-type][foqa-flights]";
            public static string FlightId = "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-core][base-field][flight.uid]]]";
            public static string TakeoffAirport = "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-core][base-field][flight.airport-takeoff]]]";
            public static string LandingAirport = "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-core][base-field][flight.airport-landing]]]";
        }
    }
}
