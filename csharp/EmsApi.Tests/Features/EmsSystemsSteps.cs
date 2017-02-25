using TechTalk.SpecFlow;
using FluentAssertions;

using EmsApi.Client.V2.Model;

namespace EmsApi.Client.Tests.Features
{
    [Binding, Scope( Feature = "EmsSystems" )]
    public class EmsSystemsSteps : FeatureTest
    {
        [When( @"I run GetAll" )]
        public void WhenIRunGetAll()
        {
            m_result.Enumerable = m_api.EmsSystems.GetAll();
        }

        [Then( @"EmsSystem objects are returned" )]
        public void ThenEmsSystemObjectsAreReturned()
        {
            m_result.Enumerable.ShouldNotBeNullOrEmptyOfType<EmsSystem>();
        }

        [When( @"I run Get and enter the value (.*)" )]
        public void WhenIRunGetAndEnterTheValue( int p0 )
        {
            m_result.Object = m_api.EmsSystems.Get( p0 );
        }

        [Then( @"An EmsSystem object is returned" )]
        public void ThenAnEmsSystemObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<EmsSystem>();
        }

        [When( @"I run ping and enter the value (.*)" )]
        public void WhenIRunPingAndEnterTheValue( int p0 )
        {
            m_result.Bool = false;
            m_result.Bool = m_api.EmsSystems.Ping( p0 );
        }

        [Then( @"The result is true" )]
        public void ThenTheResultIsTrue()
        {
            m_result.Bool.Should().BeTrue();
        }

        [When( @"I run GetSystemInfo and enter the value (.*)" )]
        public void WhenIRunGetSystemInfoAndEnterTheValue( int p0 )
        {
            m_result.Object = m_api.EmsSystems.GetSystemInfo( p0 );
        }

        [Then( @"An EmsSystemInfo object is returned" )]
        public void ThenAnEmsSystemInfoObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<EmsSystemInfo>();
        }
    }
}
