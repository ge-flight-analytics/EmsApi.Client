
using Xunit;

namespace EmsApi.Client.Tests
{
    public class ErrorHandlingTests : TestBase
    {
        private const string Header = "Error Handling: ";

        // Tests that an exception is thrown when the configuration value to do so is set to true.
        [Fact( DisplayName = Header + "Enabling auth exceptions should throw an exception" )]
        public void Enabling_auth_exceptions_should_throw_an_exception()
        {
            using( var service = NewInvalidLoginService() )
                Assert.Throws<V2.EmsApiAuthenticationException>( () => service.Authenticate() );
        }

        [Fact( DisplayName = Header + "Disabling auth exceptions should not throw an exception" )]
        public void Disabling_auth_exceptions_should_not_throw_an_exception()
        {
            using( var service = NewInvalidLoginService() )
            {
                service.ServiceConfig.ThrowExceptionOnAuthFailure = false;
                service.Authenticate();
            }
        }

        [Fact( DisplayName = Header + "Enabling api exceptions should throw an exception" )]
        public void Enabling_api_exceptions_should_throw_an_exception()
        {
            // This is roughly the same as above except we ignore auth failures, and let the
            // failure happen on the subsequent API call.
            using( var service = NewInvalidLoginService() )
            {
                service.ServiceConfig.ThrowExceptionOnAuthFailure = false;
                service.ServiceConfig.ThrowExceptionOnApiFailure = true;
                Assert.Throws<V2.EmsApiException>( () => service.EmsSystems.GetAll() );
            }
        }

        [Fact( DisplayName = Header + "Disabling api exceptions should not throw an exception" )]
        public void Disabling_api_exceptions_should_not_throw_an_exception()
        {
            // Fixme: This one seems to have a race condition when running all tests
            // at once. It works if you run it individually after.
            using( var service = NewInvalidLoginService() )
            {
                service.ServiceConfig.ThrowExceptionOnApiFailure = false;
                service.ServiceConfig.ThrowExceptionOnAuthFailure = false;
                service.EmsSystems.GetAll();
            }
        }
    }
}
