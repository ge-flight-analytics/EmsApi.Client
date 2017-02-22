using Xunit;
using FluentAssertions;

namespace EmsApi.Client.Tests
{
    public class CallbackTests : TestBase
    {
        [Fact( DisplayName = "Authentication failure callbacks should fire" )]
        public void Authentication_failure_callbacks_should_fire()
        {
            string message = string.Empty;
            using( var service = NewNoThrowInvalidLoginService() )
            {
                service.RegisterAuthFailedCallback( err => message = err );
                service.Authenticate();
            }

            message.Should().NotBeNullOrEmpty();
        }

        [Fact( DisplayName = "API exception callbacks should fire" )]
        public void Api_exception_callbacks_should_fire()
        {
            string message = string.Empty;
            using( var service = NewNoThrowInvalidLoginService() )
            {
                service.RegisterApiExceptionCallback( err => message = err );
                service.EmsSystems.GetAll();
            }

            message.Should().NotBeNullOrEmpty();
        }
    }
}
