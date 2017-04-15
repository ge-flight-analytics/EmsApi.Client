using System;
using Xunit;
using FluentAssertions;

using EmsApi.Dto.V2;

namespace EmsApi.Tests
{
    public class DatabaseTests : TestBase
    {
        [Fact( DisplayName = "A simple query should return rows" )]
        public void Simple_query_should_return_rows()
        {
            using( var api = NewService() )
            {
                api.CachedEmsSystem = 1;
                var query = CreateQuery();

                // Limit the number of flights returned for the simple query test.
                const int numRows = 10;
                query.Top = numRows;

                // The simple query uses the non-async database route.
                DatabaseQueryResult result = api.Databases.SimpleQuery( Monikers.FlightDatabase, query );
                result.Rows.Count.ShouldBeEquivalentTo( numRows );

                foreach( DatabaseQueryResult.Row row in result.Rows )
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
            }
        }

        [Fact( DisplayName = "A query should return rows" )]
        public void Query_should_return_rows()
        {
            using( var api = NewService() )
            {
                api.CachedEmsSystem = 1;
                var query = CreateQuery();

                // The regular query uses async-query under the covers and handles pagination.
                DatabaseQueryResult result = api.Databases.Query( Monikers.FlightDatabase, query );
                foreach( DatabaseQueryResult.Row row in result.Rows )
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
            }
        }

        private QueryWrapper CreateQuery()
        {
            var query = new QueryWrapper();

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
            query.OrderByField( Monikers.FlightId );

            // Use display formatting.
            query.ValueFormat = Query2Format.Display;
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
