using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmsApi.Client.V2;
using EmsApi.Dto.V2;
using FluentAssertions;
using Moq;
using Xunit;

namespace EmsApi.Tests
{
    public class DatabaseTests : TestBase
    {
        [Fact( DisplayName = "Get field should return discrete values" )]
        public void Get_field_should_return_discrete_values()
        {
            using var api = NewService();

            const string flights = "[ems-core][entity-type][foqa-flights]";
            string flightRecordField = "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-core][base-field][flight.uid]]]";
            Field noDiscrete = api.Databases.GetField( flights, flightRecordField );
            noDiscrete.DiscreteValues.Should().BeNull();

            string eventStatusField = "[-hub-][field][[[ems-apm][entity-type][events:profile-a7483c449db94a449eb5f67681ee52b0]][[ems-apm][event-field][event-status:profile-a7483c449db94a449eb5f67681ee52b0]]]";
            Field withDiscrete = api.Databases.GetField( "[ems-apm][entity-type][events:profile-a7483c449db94a449eb5f67681ee52b0]", eventStatusField );
            withDiscrete.DiscreteValues.Should().NotBeNull();

            // City pair values are larger than an int32.
            string cityPairField = "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-core][base-field][city-pair.pair]]]";
            Field cityPair = api.Databases.GetField( flights, cityPairField );
            withDiscrete.DiscreteValues.Should().NotBeNull();
        }

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

        [Fact( DisplayName = "A simple query should fire callbacks" )]
        [Obsolete]
        public void Simple_query_should_fire_callbacks()
        {
            using var api = NewService();

            var query = CreateQuery( orderResults: true );

            // Limit the result set to 10 items and make sure we get 10 callbacks.
            const int numItems = 10;
            query.Top = numItems;

            int numCallbacks = 0;
            void callback( DatabaseQueryResult.Row row )
            {
                TestRow( row );
                numCallbacks++;
            }

            api.Databases.SimpleQuery( Monikers.FlightDatabase, query, callback );
            numCallbacks.Should().Be( numItems );
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
        [Obsolete]
        public void Advanced_query_should_fire_callbacks()
        {
            using var api = NewService();

            var query = CreateQuery( orderResults: true );

            // Limit the result set to 20 items and make sure we get 20 callbacks.
            const int numItems = 20;
            query.Top = numItems;

            int numCallbacks = 0;
            void callback( DatabaseQueryResult.Row row )
            {
                TestRow( row );
                numCallbacks++;
            }

            api.Databases.Query( Monikers.FlightDatabase, query, callback );
            numCallbacks.Should().Be( numItems );
        }

        [Fact( DisplayName = "An advanced query should handle pagination" )]
        [Obsolete]
        public void Advanced_query_should_handle_pagination()
        {
            using var api = NewService();

            // Note: To be deterministic, we have to use ordered results so the
            // callbacks fire in order.
            var query = CreateQuery( orderResults: true );

            const int numItems = 30;
            query.Top = numItems;

            int numCallbacks = 0;
            void callback( DatabaseQueryResult.Row row )
            {
                TestRow( row );
                numCallbacks++;
            }

            api.Databases.Query( Monikers.FlightDatabase, query, callback, rowsPerCall: 10 );
            numCallbacks.Should().Be( numItems );
        }

        [Fact( DisplayName = "An advanced query can return an async enumerable of dictionaries" )]
        public async Task Advanced_query_can_return_async_dictionary()
        {
            using var api = NewService();
            var query = CreateQuery( orderResults: false );

            const int numItems = 20;
            query.Top = numItems;

            int received = 0;
            string[] fieldIds = QueryFields;
            await foreach( IReadOnlyDictionary<string, object> dict in api.Databases.QueryDictionaryAsync( Monikers.FlightDatabase, query ) )
            {
                received++;
                dict.Keys.Should().Equal( fieldIds );
            }

            received.Should().Be( numItems );
        }

        [Fact( DisplayName = "An advanced query can return an async enumerable of lists" )]
        public async Task Advanced_query_can_return_async_list()
        {
            using var api = NewService();
            var query = CreateQuery( orderResults: false );

            const int numItems = 20;
            query.Top = numItems;

            int received = 0;
            string[] fieldIds = QueryFields;
            await foreach( IList<object> values in api.Databases.QueryValuesAsync( Monikers.FlightDatabase, query ) )
            {
                received++;
                values.Count.Should().Be( fieldIds.Length );
            }

            received.Should().Be( numItems );
        }

        [Fact( DisplayName = "An advanced dictionary query can be started manually" )]
        public async Task Advanced_dictionary_query_can_call_start()
        {
            using var api = NewService();
            var query = CreateQuery( orderResults: false );

            const int numItems = 20;
            query.Top = numItems;

            int received = 0;
            string[] fieldIds = QueryFields;
            AsyncQueryInfo info = null;
            try
            {
                info = await api.Databases.StartQueryAsync( Monikers.FlightDatabase, query );
                await foreach( IReadOnlyDictionary<string, object> dict in api.Databases.ReadQueryDictionaryAsync( Monikers.FlightDatabase, info ) )
                {
                    received++;
                    dict.Keys.Should().Equal( fieldIds );
                }
            }
            finally
            {
                if( info != null )
                    await api.Databases.StopQueryAsync( Monikers.FlightDatabase, info.Id );
            }
        }

        [Fact( DisplayName = "An advanced list query can be started manually" )]
        public async Task Advanced_list_query_can_call_start()
        {
            using var api = NewService();
            var query = CreateQuery( orderResults: false );

            const int numItems = 20;
            query.Top = numItems;

            int received = 0;
            string[] fieldIds = QueryFields;
            AsyncQueryInfo info = null;
            try
            {
                info = await api.Databases.StartQueryAsync( Monikers.FlightDatabase, query );
                await foreach( IList<object> values in api.Databases.ReadQueryValuesAsync( Monikers.FlightDatabase, info ) )
                {
                    received++;
                    values.Count.Should().Be( fieldIds.Length );
                }
            }
            finally
            {
                if( info != null )
                    await api.Databases.StopQueryAsync( Monikers.FlightDatabase, info.Id );
            }
        }

#pragma warning disable xUnit1004 // Test methods should not be skipped
        [Fact( DisplayName = "A create comment query should add a new comment", Skip = "We don't want to add a new comment every time the tests are run." )]
#pragma warning restore xUnit1004 // Test methods should not be skipped
        public void Create_Comment_should_add_comment()
        {
            using var api = NewService();
            var newComment = new NewComment
            {
                Comment = "look at that data quality",
                // This value will depend on the EMS system. It should be a flight record number.
                EntityIdentifier = { 3135409 }
            };
            api.Databases.CreateComment( Monikers.FlightDatabase, Monikers.FlightDataQualityField, newComment );
        }

        [Fact( DisplayName = "Async query without wait will retry" )]
        public async Task Async_Query_Without_Wait_Retries()
        {
            // This should never trigger a login because we mock the refit method.
            using var api = NewInvalidLoginService();

            int invocations = 0;
            var mockRefitApi = new Mock<IEmsApi>();
            mockRefitApi.Setup( mk => mk.ReadAsyncDatabaseQuery( It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.Is<bool>( b => !b ), It.IsAny<CallContext>() ) )
                .Returns( () =>
                {
                    invocations++;
                    return Task.FromResult( new System.Net.Http.HttpResponseMessage
                    {
                        StatusCode = System.Net.HttpStatusCode.Accepted
                    } );
                } );
            ;

            api.RefitApi = mockRefitApi.Object;

            try
            {
                var cts = new CancellationTokenSource( TimeSpan.FromSeconds( 2.5 ) );
                AsyncQueryData data = await api.Databases.ReadQueryWhenReadyAsync( Monikers.FlightDatabase, Guid.NewGuid().ToString(), 0, 19999, TimeSpan.FromSeconds( 1 ), backoffFactor: 1.0f, cts.Token );
            }
            catch( OperationCanceledException )
            {

            }

            invocations.Should().BeGreaterOrEqualTo( 2 );
        }

        [Fact( DisplayName = "Async query try get data returns false" )]
        public async Task Async_Query_Try_Get_Data_Returns_False()
        {
            // This should never trigger a login because we mock the refit method.
            using var api = NewInvalidLoginService();

            var mockRefitApi = new Mock<IEmsApi>();
            mockRefitApi.Setup( mk => mk.ReadAsyncDatabaseQuery( It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.Is<bool>( b => !b ), It.IsAny<CallContext>() ) )
                .Returns( Task.FromResult( new System.Net.Http.HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.Accepted
                } ) );

            api.RefitApi = mockRefitApi.Object;
            (bool dataIsReady, AsyncQueryData data) = await api.Databases.TryReadQueryAsync( Monikers.FlightDatabase, Guid.NewGuid().ToString(), 0, 19999 );
            dataIsReady.Should().BeFalse();
            data.Should().BeNull();
        }

        [Fact( DisplayName = "Get fields returns multiple fields", Skip = "API route not yet deployed" )]
        public async Task Get_Fields_Returns_Multiple_Fields()
        {
            using var api = NewService();
            string[] fieldIds = new[]
            {
                Monikers.FlightId,
                Monikers.TakeoffValid,
                Monikers.TakeoffAirportName
            };

            FieldInfo result = await api.Databases.GetFieldsAsync( Monikers.FlightDatabase, fieldIds );

            result.Should().NotBeNull();
            result.Fields.Should().HaveCount( fieldIds.Length );
            result.InvalidFieldIds.Should().BeNullOrEmpty();
        }

        [Fact( DisplayName = "Get fields returns invalid fields", Skip = "API route not yet deployed" )]
        public async Task Get_Fields_Returns_Invalid_Fields()
        {
            using var api = NewService();
            string[] fieldIds = new[]
            {
                Monikers.FlightId,
                Monikers.TakeoffValid,
                Monikers.TakeoffAirportName,
                Monikers.Invalid
            };

            FieldInfo result = await api.Databases.GetFieldsAsync( Monikers.FlightDatabase, fieldIds );

            result.Should().NotBeNull();
            result.Fields.Should().HaveCount( fieldIds.Length - 1 );
            result.InvalidFieldIds.Should().HaveCount( 1 );
            result.InvalidFieldIds.First().Should().BeEquivalentTo( Monikers.Invalid );
        }

        private static void TestSimple( bool orderResults )
        {
            using var api = NewService();

            var query = CreateQuery( orderResults );

            // Limit the number of flights returned for the simple query test.
            const int numRows = 10;
            query.Top = numRows;

            // The simple query uses the non-async database route.
            DatabaseQueryResult result = api.Databases.SimpleQuery( Monikers.FlightDatabase, query );
            result.Rows.Count.Should().Be( numRows );

            foreach( DatabaseQueryResult.Row row in result.Rows )
                TestRow( row );
        }

        private static void TestAdvanced( bool orderResults )
        {
            using var api = NewService();

            var query = CreateQuery( orderResults );

            // Limit to 100 rows to save bandwidth.
            query.Top = 100;

            // The regular query uses async-query under the covers and handles pagination.
            DatabaseQueryResult result = api.Databases.Query( Monikers.FlightDatabase, query );
            foreach( DatabaseQueryResult.Row row in result.Rows )
                TestRow( row );
        }

        private static void TestRow( DatabaseQueryResult.Row row )
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

        internal static DatabaseQuery CreateQuery( bool orderResults )
        {
            var query = new DatabaseQuery();

            // Select a few columns.
            var fields = QueryFields;
            query.SelectField( fields[0] );
            query.SelectField( fields[1] );
            query.SelectField( fields[2] );
            query.SelectField( fields[3] );
            query.SelectField( fields[4] );

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

        private static string[] QueryFields => new[]
        {
            Monikers.FlightId,
            Monikers.TailNumber,
            Monikers.CityPair,
            Monikers.TakeoffAirportName,
            Monikers.LandingAirportName
        };

        internal static class Monikers
        {
            public static string FlightDatabase = "[ems-core][entity-type][foqa-flights]";
            public static string FlightId = "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-core][base-field][flight.uid]]]";
            public static string TailNumber = "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-core][base-field][flight.aircraft]]]";
            public static string CityPair = "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-core][base-field][city-pair.pair]]]";
            public static string TakeoffAirportName = "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[[nav][type-link][airport-takeoff * foqa-flights]]][[nav][base-field][nav-airport.name]]]";
            public static string LandingAirportName = "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[[nav][type-link][airport-landing * foqa-flights]]][[nav][base-field][nav-airport.name]]]";
            public static string TakeoffValid = "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-core][base-field][flight.exist-takeoff]]]";
            public static string LandingValid = "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-core][base-field][flight.exist-landing]]]";
            public static string FlightDataQualityField = "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[data-quality][base-field][data-quality-comments]]]";
            public static string Invalid = "[-hub-][field][[[ems-core][entity-type][foqa-flights]][[ems-core][base-field][flight.this-field-does-not-really-exist]]]";
        }
    }
}
