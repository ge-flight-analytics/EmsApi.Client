using System;
using Xunit;
using FluentAssertions;

using EmsApi.Client.V2;

namespace EmsApi.Tests
{
    public class AuthenticationTrustedTests : TestBase
    {
        [Fact( DisplayName = "Client id but no client secret should fail" )]
        public void Client_id_but_no_secret_should_fail()
        {
            EmsApiServiceConfiguration config = m_config.Clone();
            config.UserName = null;
            config.Password = null;
            config.ApiClientId = "foobarbaz";
            config.ApiClientSecret = null;
            Action create = () => _ = new EmsApiService( config );
            create.Should().Throw<EmsApiConfigurationException>();
        }

        [Fact( DisplayName = "Client secret but no id should fail" )]
        public void Client_secret_but_no_id_should_fail()
        {
            EmsApiServiceConfiguration config = m_config.Clone();
            config.UserName = null;
            config.Password = null;
            config.ApiClientId = null;
            config.ApiClientSecret = "not so secret";
            Action create = () => _ = new EmsApiService( config );
            create.Should().Throw<EmsApiConfigurationException>();
        }

        [Fact( DisplayName = "Trusted value but no name should fail" )]
        public void Trusted_value_but_no_name()
        {
            using var service = NewService();
            var ctx = new CallContext { TrustedAuthValue = "ksk" };
            Action getSystem = () => service.EmsSystem.Get( ctx );
            getSystem.Should().Throw<EmsApiException>();
        }

        [Fact( DisplayName = "Trusted info but no client id should fail" )]
        public void Trusted_info_but_no_client_id()
        {
            EmsApiServiceConfiguration config = m_config.Clone();
            config.ApiClientId = null;
            config.ApiClientSecret = null;
            using var service = new EmsApiService( config );
            var ctx = new CallContext
            {
                TrustedAuthName = "SAMAccountName",
                TrustedAuthValue = "ksk"
            };
            Action getSystem = () => service.EmsSystem.Get( ctx );
            getSystem.Should().Throw<EmsApiException>();
        }

        [Fact( DisplayName = "Trusted name but no value should fail" )]
        public void Trusted_name_but_no_value()
        {
            using var service = NewService();
            var ctx = new CallContext { TrustedAuthName = "propertyX" };
            Action getSystem = () => service.EmsSystem.Get( ctx );
            getSystem.Should().Throw<EmsApiException>();
        }

        [Fact( DisplayName = "Trusted info in context should succeed" )]
        public void Trusted_info_in_context()
        {
            using var service = NewService();

            var ctx = new CallContext
            {
                TrustedAuthName = "SAMAccountName",
                TrustedAuthValue = "ksk"
            };
            var system = service.EmsSystem.Get( ctx );
            system.Id.Should().Be( 1 );
            service.HasAuthenticatedWithTrusted( ctx.TrustedAuthName, ctx.TrustedAuthValue ).Should().BeTrue();
            service.Authenticated.Should().BeFalse();
        }

        [Fact( DisplayName = "Trusted value in context and name in config should succeed" )]
        public void Trusted_value_in_context_name_in_config()
        {
            EmsApiServiceConfiguration config = m_config.Clone();
            config.TrustedAuthName = "SAMAccountName";
            using var service = new EmsApiService( config );
            var ctx = new CallContext
            {
                TrustedAuthValue = "ksk"
            };
            var system = service.EmsSystem.Get( ctx );
            system.Id.Should().Be( 1 );
            service.HasAuthenticatedWithTrusted( config.TrustedAuthName, ctx.TrustedAuthValue ).Should().BeTrue();
            service.Authenticated.Should().BeFalse();
        }

        [Fact( DisplayName = "Trusted and then password both work" )]
        public void Trusted_then_password()
        {
            using var service = NewService();

            // First perform the trusted auth.
            var ctx = new CallContext
            {
                TrustedAuthName = "SAMAccountName",
                TrustedAuthValue = "ksk"
            };
            var system = service.EmsSystem.Get( ctx );
            system.Id.Should().Be( 1 );
            service.HasAuthenticatedWithTrusted( ctx.TrustedAuthName, ctx.TrustedAuthValue ).Should().BeTrue();
            service.Authenticated.Should().BeFalse();

            // Then perform the password auth.
            system = service.EmsSystem.Get();
            system.Id.Should().Be( 1 );
            service.Authenticated.Should().BeTrue();
        }

        [Fact( DisplayName = "Password and then trusted both work" )]
        public void Password_then_trusted()
        {
            using var service = NewService();

            var ctx = new CallContext
            {
                TrustedAuthName = "SAMAccountName",
                TrustedAuthValue = "ksk"
            };

            // First perform the password auth.
            var system = service.EmsSystem.Get();
            system.Id.Should().Be( 1 );
            service.Authenticated.Should().BeTrue();
            service.HasAuthenticatedWithTrusted( ctx.TrustedAuthName, ctx.TrustedAuthValue ).Should().BeFalse();

            // Then perform the trusted auth.
            system = service.EmsSystem.Get( ctx );
            system.Id.Should().Be( 1 );
            service.HasAuthenticatedWithTrusted( ctx.TrustedAuthName, ctx.TrustedAuthValue ).Should().BeTrue();
        }

        [Fact( DisplayName = "Authentication cache" )]
        public void Auth_cache()
        {
            var lastHandler = new TestMessageHandler();
            using var service = NewService( null, lastHandler );

            var ctx = new CallContext
            {
                TrustedAuthName = "SAMAccountName",
                TrustedAuthValue = "ksk"
            };
            var ctx2 = new CallContext
            {
                TrustedAuthName = "SAMAccountName",
                TrustedAuthValue = "cwo"
            };

            // First perform the password auth - CACHE MISS
            var system = service.EmsSystem.Get();
            lastHandler.CallCount.Should().Be( 2 ); // One for the token and one for our actual call.
            system.Id.Should().Be( 1 );
            service.Authenticated.Should().BeTrue();
            service.HasAuthenticatedWithTrusted( ctx.TrustedAuthName, ctx.TrustedAuthValue ).Should().BeFalse();

            // Then perform the trusted auth - CACHE MISS
            system = service.EmsSystem.Get( ctx );
            lastHandler.CallCount.Should().Be( 4 ); // One for the token and one for our actual call.
            system.Id.Should().Be( 1 );
            service.HasAuthenticatedWithTrusted( ctx.TrustedAuthName, ctx.TrustedAuthValue ).Should().BeTrue();
            service.HasAuthenticatedWithTrusted( ctx2.TrustedAuthName, ctx2.TrustedAuthValue ).Should().BeFalse();

            // Then perform the trusted auth 2 - CACHE MISS
            system = service.EmsSystem.Get( ctx2 );
            lastHandler.CallCount.Should().Be( 6 ); // One for the token and one for our actual call.
            system.Id.Should().Be( 1 );
            service.HasAuthenticatedWithTrusted( ctx2.TrustedAuthName, ctx2.TrustedAuthValue ).Should().BeTrue();

            // Then perform the three calls again - CACHE HIT
            system = service.EmsSystem.Get( ctx2 );
            lastHandler.CallCount.Should().Be( 7 );
            system = service.EmsSystem.Get( ctx );
            lastHandler.CallCount.Should().Be( 8 );
            system = service.EmsSystem.Get();
            lastHandler.CallCount.Should().Be( 9 );

            // Clear the cache and make a few more calls on one authentication path.
            service.ClearAuthenticationCache();
            system = service.EmsSystem.Get( ctx2 );
            lastHandler.CallCount.Should().Be( 11 ); // One for the token and one for our actual call.
            system = service.EmsSystem.Get( ctx2 );
            lastHandler.CallCount.Should().Be( 12 );

            // Expire the tokens and make a few more calls on one authentication path.
            service.ExpireAuthenticationCacheEntries();
            system = service.EmsSystem.Get( ctx2 );
            lastHandler.CallCount.Should().Be( 14 ); // One for the token and one for our actual call.
            system = service.EmsSystem.Get( ctx2 );
            lastHandler.CallCount.Should().Be( 15 );
        }
    }
}
