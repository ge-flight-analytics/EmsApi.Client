using System;
using Xunit;

using EmsApi.Client.V2;

namespace EmsApi.Client.Tests
{
    public class AuthenticationTests : TestBase
    {
        private const string Header = "Authentication: ";

        [Fact( DisplayName = Header + "Valid login should be authenticated" )]
        public void Valid_login_should_be_authenticated()
        {
            var service = NewService();
            Assert.True( service.Authenticate() );
        }

        [Fact( DisplayName = Header + "Invalid login should not be authenticated" )]
        public void Invalid_login_should_not_be_authenticated()
        {
            // Use a new test service for this so we don't pollute our legit test login.
            var config = new EmsApiServiceConfiguration( useEnvVars: false );
            config.UserName = "NotARealUser12354";
            config.Password = "badP@sssW0rd";
            config.ThrowExceptionOnApiFailure = true;

            var service = new EmsApiService( config );
            Exception ex = Assert.Throws<EmsApiAuthenticationException>( () => service.Authenticate() );
            Assert.True( !string.IsNullOrEmpty( ex.Message ) );
        }
    }
}
