using TechTalk.SpecFlow;
using FluentAssertions;

using EmsApi.Dto.V2;

namespace EmsApi.Tests.Features
{
    [Binding, Scope( Feature = "EmsSystem" )]
    public class EmsSystemSteps : FeatureTest
    {
         [When( @"I run Get" )]
        public void WhenIRunGetAndEnterTheValue()
        {
            m_result.Object = m_api.EmsSystem.Get();
        }

        [Then( @"An EmsSystem object is returned" )]
        public void ThenAnEmsSystemObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<EmsSystem>();
        }

        [When( @"I run ping" )]
        public void WhenIRunPingAndEnterTheValue()
        {
            m_result.Bool = false;
            m_result.Bool = m_api.EmsSystem.Ping();
        }

        [Then( @"The result is true" )]
        public void ThenTheResultIsTrue()
        {
            m_result.Bool.Should().BeTrue();
        }

        [When( @"I run GetSystemInfo" )]
        public void WhenIRunGetSystemInfoAndEnterTheValue()
        {
            m_result.Object = m_api.EmsSystem.GetSystemInfo();
        }

        [Then( @"An EmsSystemInfo object is returned" )]
        public void ThenAnEmsSystemInfoObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<EmsSystemInfo>();
        }
    }
}
