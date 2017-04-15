using System;
using Xunit;
using FluentAssertions;

using EmsApi.Dto.V2;

namespace EmsApi.Tests
{
    public class DatabaseTests : TestBase
    {
        [Fact( DisplayName = "A simple query with ordered results should return rows" )]
        public void Simple_query_with_ordered_results_should_return_rows()
        {
            TestSimple( orderResults: true );
        }

        [Fact( DisplayName = "A simple query should return rows" )]
        public void Simple_query_should_return_rows()
        {
            TestSimple( orderResults: false );
        }

        [Fact( DisplayName = "An advanced query should return rows" )]
        public void Advanced_query_should_return_rows()
        {
            TestAdvanced( orderResults: false );
        }

        [Fact( DisplayName = "An advanced query with ordered results should return rows" )]
        public void Advanced_query_with_ordered_results_should_return_rows()
        {
            TestAdvanced( orderResults: true );
        }

        [Fact( DisplayName = "An advanced query should fire callbacks" )]
        public void Advanced_query_should_fire_callbacks()
        {
            using( var api = NewService() )
            {
                api.CachedEmsSystem = 1;
                var query = CreateQuery( orderResults: true );

                // Limit the result set to 20 items and make sure we get 20 callbacks.
                const int numItems = 20;
                query.Top = numItems;

                int numCallbacks = 0;
                Action<DatabaseQueryResult.Row> callback = row =>
                {
                    TestRow( row );
                    numCallbacks++;
                };

                api.Databases.Query( Monikers.FlightDatabase, query, callback );
                numCallbacks.ShouldBeEquivalentTo( numItems );
            }
        }

        private void TestSimple( bool orderResults )
        {
            using( var api = NewService() )
            {
                api.CachedEmsSystem = 1;
                var query = CreateQuery( orderResults );

                // Limit the number of flights returned for the simple query test.
                const int numRows = 10;
                query.Top = numRows;

                // The simple query uses the non-async database route.
                DatabaseQueryResult result = api.Databases.SimpleQuery( Monikers.FlightDatabase, query );
                result.Rows.Count.ShouldBeEquivalentTo( numRows );

                foreach( DatabaseQueryResult.Row row in result.Rows )
                    TestRow( row );
            }
        }

        private void TestAdvanced( bool orderResults )
        {
            using( var api = NewService() )
            {
                api.CachedEmsSystem = 1;
                var query = CreateQuery( orderResults );

                // Limit to 100 rows to save bandwidth.
                query.Top = 100;

                // The regular query uses async-query under the covers and handles pagination.
                DatabaseQueryResult result = api.Databases.Query( Monikers.FlightDatabase, query );
                foreach( DatabaseQueryResult.Row row in result.Rows )
                    TestRow( row );
            }
        }

        private void TestRow( DatabaseQueryResult.Row row )
        {
            int flightId = Convert.ToInt32( row[Monikers.FlightId] );
            string tail = row[Monikers.TailNumber].ToString();
            string cityPair = row[Monikers.CityPair].ToString();
            string takeoffAirport = row[Monikers.TakeoffAirportName].ToString();
            string landingAirport = row[Monikers.LandingAirportName].ToString();

            flightId.Should().BeGreaterThan( 0 );
            tail.Should().NotBeNullOrEmpty();
            cityPair.Should().NotBeNullOrEmpty();
            takeoffAirport.Should().NotBeNullOrEmpty();
            landingAirport.Should().NotBeNullOrEmpty();
        }

        private DatabaseQuery CreateQuery( bool orderResults )
        {
            var query = new DatabaseQuery();

            // Select a few columns.
            query.SelectField( Monikers.FlightId );
            query.SelectField( Monikers.TailNumber );
            query.SelectField( Monikers.CityPair );
            query.SelectField( Monikers.TakeoffAirportName );
            query.SelectField( Monikers.LandingAirportName );

            // Filter for takeoff and landing valid.
            query.AddBooleanFilter( Monikers.TakeoffValid, true );
            query.AddBooleanFilter( Monikers.LandingValid, true );

            // Order by flight id.
            if( orderResults )
                query.OrderByField( Monikers.FlightId );

            // Use display formatting.
            query.ValueFormat = DbQueryFormat.Display;
            return query;
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
