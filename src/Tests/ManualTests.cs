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
    }
}
