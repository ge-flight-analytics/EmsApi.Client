using System;
using Xunit;

using EmsApi.Client.V2;

namespace EmsApi.Client.Tests
{
    public class AuthenticationTests : TestBase
    {
        private const string Header = "Authentication: ";

        [Fact( DisplayName = Header + "Username and password should be authenticated" )]
        public void Username_password_should_be_authenticated()
        {
            // Fixme: This one seems to have a race condition. It fails when you run all
            // tests, but succeeds when you run it by itself afterwards.
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
