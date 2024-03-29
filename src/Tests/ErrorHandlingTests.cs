using System;
using FluentAssertions;
using Xunit;
using EmsApi.Client.V2;

namespace EmsApi.Tests
{
    public class ErrorHandlingTests : TestBase
    {
        [Fact( DisplayName = "Disabling auth exceptions should not throw an exception" )]
        public void Disabling_auth_exceptions_should_not_throw_an_exception()
        {
            using var service = NewNoThrowInvalidLoginService();
            service.Authenticate();
        }

        [Fact( DisplayName = "Enabling api exceptions should throw an exception" )]
        public void Enabling_api_exceptions_should_throw_an_exception()
        {
            // This is roughly the same as above except we ignore auth failures, and let the
            // failure happen on the subsequent API call.
            using var service = NewInvalidLoginService();
            service.ServiceConfig.ThrowExceptionOnAuthFailure = false;
            service.ServiceConfig.ThrowExceptionOnApiFailure = true;

            Action causeFailure = () => service.EmsSystem.GetSystemInfo();
            causeFailure.Should().Throw<EmsApiException>();
        }

        [Fact( DisplayName = "Disabling api exceptions should not throw an exception" )]
        public void Disabling_api_exceptions_should_not_throw_an_exception()
        {
            using var service = NewNoThrowInvalidLoginService();
            service.ServiceConfig.ThrowExceptionOnApiFailure = false;
            service.ServiceConfig.ThrowExceptionOnAuthFailure = false;
            service.EmsSystem.Get();
        }

        [Fact( DisplayName = "Requiring a call context but not providing one should throw" )]
        public void Require_call_context_but_dont_provide_one()
        {
            var config = m_config.Clone();
            config.RequireCallContext = true;
            using var service = new EmsApiService( config );
            Action causeFailure = () => service.EmsSystem.Get();
            causeFailure.Should().Throw<EmsApiNoCallContextException>();
        }
    }
}
