using System;
using Xunit;
using FluentAssertions;

using EmsApi.Client.V2;

namespace EmsApi.Client.Tests
{
    public class ServiceTests : TestBase
    {
        [Fact( DisplayName = "Invalid configuration should throw an exception" ) ]
        public void Invalid_configuration_should_throw_exception()
        {
            var service = new EmsApiService();
            var badConfig = new EmsApiServiceConfiguration()
            {
                UserName = string.Empty,
                Password = null
            };

            Action setConfig = () => service.ServiceConfig = badConfig;
            setConfig.ShouldThrowExactly<EmsApiConfigurationException>();
        }

        [Fact( DisplayName = "Valid configuration should change service state" )]
        public void Valid_configuration_should_change_service_state()
        {
            using( var service = NewService() )
            {
                // Authenticate the first time.
                service.Authenticate().Should().BeTrue();
                service.Authenticated.Should().BeTrue();

                // Change the configuration.
                var newConfig = m_config.Clone();
                Action setConfig = () => service.ServiceConfig = newConfig;
                setConfig.ShouldNotThrow();

                // Make sure we are no longer authenticated.
                service.Authenticated.Should().BeFalse();
            }
        }

        [Fact( DisplayName = "Service should shut down gracefully" )]
        public void Service_should_shut_down_gracefully()
        {
            var service = NewService();
            service.Dispose();
            service = null;
        }
    }
}
