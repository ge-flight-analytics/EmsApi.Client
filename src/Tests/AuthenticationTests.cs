using System;
using Xunit;
using FluentAssertions;

using EmsApi.Client.V2;

namespace EmsApi.Tests
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
                auth.Should().Throw<EmsApiAuthenticationException>();
            }
        }

        [Fact( DisplayName = "Not setting Trusted or Password auth config should fail" )]
        public void No_auth_setup_should_fail()
        {
            EmsApiServiceConfiguration config = m_config.Clone();
            config.UserName = null;
            config.Password = null;
            config.ApiClientId = null;
            config.ApiClientSecret = null;
            Action create = () => new EmsApiService( config );
            create.Should().Throw<EmsApiConfigurationException>();
        }

        [Fact( DisplayName = "Client id but no client secret should fail" )]
        public void Client_id_but_no_secret_should_fail()
        {
            EmsApiServiceConfiguration config = m_config.Clone();
            config.UserName = null;
            config.Password = null;
            config.ApiClientId = "foobarbaz";
            config.ApiClientSecret = null;
            Action create = () => new EmsApiService( config );
            create.Should().Throw<EmsApiConfigurationException>();
        }
    }
}
