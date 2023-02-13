using System;
using Xunit;
using FluentAssertions;

namespace EmsApi.Tests
{
    /// <summary>
    /// This class contains some tests that should be skipped for automatic testing
    /// (they cause some side effects we want to be able to easily run manually).
    /// </summary>
    public class ManualTests : TestBase
    {
        [Fact( DisplayName = "Users can be added", Skip = "User may already exist" )]
        public void Users_Can_Be_Added()
        {
            using var api = NewService();
            Dto.V2.User user = api.AdminUser.AddUser( "EmsApiTest" );
            user.Should().NotBeNull();
            user.Username.Should().Be( "EmsApiTest" );
        }

        [Fact( DisplayName = "Users can be assigned to EMS systems", Skip = "User may not exist" )]
        public void Users_Can_Be_Assigned_To_Ems_Systems()
        {
            using var api = NewService();
            int userId = 147;
            int emsSystemId = 1;
            api.AdminUser.AssignUserEmsSystem( userId, emsSystemId );
        }

        [Fact( DisplayName = "Async query without wait will retry", Skip = "API repsonse time is not consistent" )]
        public async Task Async_Query_Without_Wait_Retries()
        {
            using var api = NewService();
            AsyncQueryInfo info = null;
            string database = DatabaseTests.Monikers.FlightDatabase;
            try
            {
                DatabaseQuery query = DatabaseTests.CreateQuery( false );
                info = await api.Databases.StartQueryAsync( database, query );
                AsyncQueryData data = await api.Databases.ReadQueryWhenReadyAsync( database, info.Id, 1, 19999, TimeSpan.FromSeconds( 10 ), backoffFactor: 1.0f );
            }
            finally
            {
                if( info != null )
                    await api.Databases.StopQueryAsync( database, info.Id );
            }
        }
    }
}
