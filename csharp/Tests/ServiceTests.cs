using System;
using Xunit;

using EmsApi.Client.V2;

namespace EmsApi.Client.Tests
{
    public class ServiceTests : TestBase
    {
        private const string Header = "Service: ";

        [Fact( DisplayName = Header + "An invalid configuration should throw an exception" ) ]
        public void Invalid_configuration_should_throw_exception()
        {
            var service = new EmsApiService();
            var badConfig = new EmsApiServiceConfiguration()
            {
                UserName = string.Empty,
                Password = null
            };

            Exception ex = Assert.Throws<EmsApiConfigurationException>( () => service.ServiceConfig = badConfig );
            Assert.True( !string.IsNullOrEmpty( ex.Message ) );
        }

        [Fact( DisplayName = Header + "The service should shut down gracefully" )]
        public void Service_should_shut_down_gracefully()
        {
            var service = NewService();
            service.Dispose();
            service = null;
        }
    }
}
