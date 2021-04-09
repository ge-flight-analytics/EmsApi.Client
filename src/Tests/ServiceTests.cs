using System;
using Xunit;
using FluentAssertions;

using EmsApi.Client.V2;
using System.Collections.Generic;

namespace EmsApi.Tests
{
    public class ServiceTests : TestBase
    {
        [Fact( DisplayName = "Invalid configuration should throw an exception" )]
        public void Invalid_configuration_should_throw_exception()
        {
            var service = new EmsApiService();
            var badConfig = new EmsApiServiceConfiguration()
            {
                UserName = string.Empty,
                Password = null
            };

            Action setConfig = () => service.ServiceConfig = badConfig;
            setConfig.Should().ThrowExactly<EmsApiConfigurationException>();
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
                newConfig.Password = "somethingElse";

                Action setConfig = () => service.ServiceConfig = newConfig;
                setConfig.Should().NotThrow();

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

        [Fact( DisplayName = "Requests should have custom headers set from service configuration" )]
        public void Api_Requests_Should_Append_Custom_Headers()
        {
            using( var api = NewService() )
            {
                api.ServiceConfig.CustomHeaders = new Dictionary<string, string>
                {
                    { HttpHeaderNames.ClientUsername, "test-user" },
                    { HttpHeaderNames.CorrelationId, new Guid().ToString() }
                };
                api.EmsSystems.GetAll();

                api.ServiceConfig.CustomHeaders = new Dictionary<string, string>
                {
                    { "X-Custom-Header", "custom header" }
                };
                api.EmsSystems.GetAll();

                // Since we are adding the headers to the HttpRequest and we don't currently
                // have a way to mock or intercept those requests we can't assert against the
                // headers. However since this is actively hitting the EMS API, you can
                // check the headers are set in the logs.
            }
        }
    }
}
