
using Xunit;

namespace EmsApi.Client.Tests
{
    public class ErrorHandlingTests : TestBase
    {
        private const string Header = "Error Handling: ";

        // Tests that an exception is thrown when the configuration value to do so is set to true.
        [Fact( DisplayName = Header + "Using auth exceptions throws an exception" )]
        public void Use_auth_exceptions_throws_an_exception()
        {
            using( var service = NewService() )
            {
                service.ServiceConfig.UserName = "Inv@lidUserDoesNotExist!?";
                service.ServiceConfig.ThrowExceptionOnAuthFailure = true;
                Assert.Throws<V2.EmsApiAuthenticationException>( () => service.Authenticate() );
            }
        }
    }
}
