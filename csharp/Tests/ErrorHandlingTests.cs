using System;
using FluentAssertions;
using Xunit;

namespace EmsApi.Client.Tests
{
    public class ErrorHandlingTests : TestBase
    {
        [Fact( DisplayName = "Enabling auth exceptions should throw an exception" )]
        public void Enabling_auth_exceptions_should_throw_an_exception()
        {
            using( var service = NewInvalidLoginService() )
                Assert.Throws<V2.EmsApiAuthenticationException>( () => service.Authenticate() );
        }

        [Fact( DisplayName = "Disabling auth exceptions should not throw an exception" )]
        public void Disabling_auth_exceptions_should_not_throw_an_exception()
        {
            using( var service = NewNoThrowInvalidLoginService() )
                service.Authenticate();
        }

        [Fact( DisplayName = "Enabling api exceptions should throw an exception" )]
        public void Enabling_api_exceptions_should_throw_an_exception()
        {
            // This is roughly the same as above except we ignore auth failures, and let the
            // failure happen on the subsequent API call.
            using( var service = NewInvalidLoginService() )
            {
                service.ServiceConfig.ThrowExceptionOnAuthFailure = false;
                service.ServiceConfig.ThrowExceptionOnApiFailure = true;

                Action causeFailure = () => service.EmsSystems.GetAll();
                causeFailure.ShouldThrowExactly<V2.EmsApiException>();
            }
        }

        [Fact( DisplayName = "Disabling api exceptions should not throw an exception" )]
        public void Disabling_api_exceptions_should_not_throw_an_exception()
        {
            using( var service = NewNoThrowInvalidLoginService() )
            {
                service.ServiceConfig.ThrowExceptionOnApiFailure = false;
                service.ServiceConfig.ThrowExceptionOnAuthFailure = false;
                service.EmsSystems.GetAll();
            }
        }
    }
}
