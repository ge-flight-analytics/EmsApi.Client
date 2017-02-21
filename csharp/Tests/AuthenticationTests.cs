using System;
using Xunit;

using EmsApi.Client.V2;

namespace EmsApi.Client.Tests
{
    public class AuthenticationTests : TestBase
    {
        private const string Header = "Authentication: ";

        [Fact( DisplayName = Header + "A valid login should be authenticated" )]
        public void Valid_login_should_be_authenticated()
        {
            var service = NewService();
            Assert.True( service.Authenticate() );
        }

        [Fact( DisplayName = Header + "An invalid login should not be authenticated" )]
        public void Invalid_login_should_not_be_authenticated()
        {
            using( var service = NewInvalidLoginService() )
            {
                Exception ex = Assert.Throws<EmsApiAuthenticationException>( () => service.Authenticate() );
                Assert.True( !string.IsNullOrEmpty( ex.Message ) );
            }
        }
    }
}
