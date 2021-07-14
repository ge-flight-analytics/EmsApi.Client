using System;
using System.Collections.Generic;
using System.Linq;
using EmsApi.Client.V2;
using FluentAssertions;
using Moq;
using Xunit;

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
            setConfig.Should().Throw<EmsApiConfigurationException>();
        }

        [Fact( DisplayName = "Valid configuration should change service state" )]
        public void Valid_configuration_should_change_service_state()
        {
            using var service = NewService();

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

        [Fact( DisplayName = "Service should shut down gracefully" )]
        public void Service_should_shut_down_gracefully()
        {
            var service = NewService();
            service.Dispose();
        }

        [Fact( DisplayName = "Requests should have custom headers set from service configuration" )]
        public void Api_Requests_Should_Append_Custom_Headers()
        {
            var lastHandler = new TestMessageHandler();

            using var api = NewService( null, lastHandler );

            const string username = "test-user";
            string correlationId = new Guid().ToString();
            api.ServiceConfig.CustomHeaders = new Dictionary<string, string>
                {
                    { HttpHeaderNames.ClientUsername, username },
                    { HttpHeaderNames.CorrelationId, correlationId }
                };
            api.EmsSystem.Get();

            lastHandler.CallCount.Should().Be( 2 ); // One for the token and one for our actual call.
            var reqHeaders = lastHandler.LastRequest.Headers;
            reqHeaders.Contains( HttpHeaderNames.ClientUsername ).Should().BeTrue();
            reqHeaders.GetValues( HttpHeaderNames.ClientUsername ).First().Should().BeEquivalentTo( username );
            reqHeaders.Contains( HttpHeaderNames.CorrelationId ).Should().BeTrue();
            reqHeaders.GetValues( HttpHeaderNames.CorrelationId ).First().Should().BeEquivalentTo( correlationId );

            const string customHeader = "X-Custom-Header";
            const string customValue = "custom header";
            api.ServiceConfig.CustomHeaders = new Dictionary<string, string>
                {
                    { customHeader, customValue }
                };
            api.EmsSystem.Get();

            reqHeaders = lastHandler.LastRequest.Headers;
            reqHeaders.Contains( customHeader ).Should().BeTrue();
            reqHeaders.GetValues( customHeader ).First().Should().BeEquivalentTo( customValue );
        }

        [Fact( DisplayName = "Synchronous service methods can be mocked" )]
        public void Service_Methods_Can_Be_Mocked()
        {
            using var api = NewService();

            var mock = new Mock<Client.V2.Access.EmsSystemAccess>();
            mock.Setup( mk => mk.Get( It.IsAny<CallContext>() ) ).Returns( new Dto.V2.EmsSystem
            {
                Id = 1,
                Name = "Mocked EMS",
                Description = "Not a real system",
                DirAdi = @"\\mockems\adi"
            } );

            api.EmsSystem = mock.Object;
            Dto.V2.EmsSystem result = api.EmsSystem.Get();
            api.Authenticated.Should().BeFalse();
            result.Id.Should().Be( 1 );
            result.Name.Should().Be( "Mocked EMS" );
            result.Description.Should().Be( "Not a real system" );
        }

        [Fact( DisplayName = "Async service methods can be mocked" )]
        public async void Service_Methods_Can_Be_Mocked_Async()
        {
            using var api = NewService();

            var mock = new Mock<Client.V2.Access.EmsSystemAccess>();
            mock.Setup( mk => mk.GetAsync( It.IsAny<CallContext>() ) ).Returns( System.Threading.Tasks.Task.FromResult( new Dto.V2.EmsSystem
            {
                Id = 1,
                Name = "Mocked EMS",
                Description = "Not a real system",
                DirAdi = @"\\mockems\adi"
            } ) );

            api.EmsSystem = mock.Object;
            Dto.V2.EmsSystem result = await api.EmsSystem.GetAsync();
            api.Authenticated.Should().BeFalse();
            result.Id.Should().Be( 1 );
            result.Name.Should().Be( "Mocked EMS" );
            result.Description.Should().Be( "Not a real system" );
        }

        [Fact( DisplayName = "Test basic call context" )]
        public void Use_Call_Context()
        {
            var lastHandler = new TestMessageHandler();

            using var api = NewService( null, lastHandler );

            string username = "bob@burgers.com";
            string correlationId = "123456";
            var ctx = new CallContext
            {
                ClientUsername = username,
                CorrelationId = correlationId,
            };
            api.EmsSystem.Get( ctx );

            lastHandler.CallCount.Should().Be( 2 ); // One for the token and one for our actual call.
            var reqHeaders = lastHandler.LastRequest.Headers;
            reqHeaders.Contains( HttpHeaderNames.ClientUsername ).Should().BeTrue();
            reqHeaders.GetValues( HttpHeaderNames.ClientUsername ).First().Should().BeEquivalentTo( username );
            reqHeaders.Contains( HttpHeaderNames.CorrelationId ).Should().BeTrue();
            reqHeaders.GetValues( HttpHeaderNames.CorrelationId ).First().Should().BeEquivalentTo( correlationId );
        }
    }
}
