using System;
using Xunit;
using FluentAssertions;

using EmsApi.Client.V2;

namespace EmsApi.Client.Tests
{
    public class AuthenticationTests : TestBase
    {
        [Fact( DisplayName = "A valid login should be authenticated" )]
        public void Valid_login_should_be_authenticated()
        {
            using( var service = NewService() )
                service.Authenticate().Should().BeTrue();
        }

        [Fact( DisplayName = "An invalid login should not be authenticated" )]
        public void Invalid_login_should_not_be_authenticated()
        {
            using( var service = NewInvalidLoginService() )
            {
                Action auth = () => service.Authenticate();
                auth.ShouldThrowExactly<EmsApiAuthenticationException>();
            }
        }
    }
}
