using TechTalk.SpecFlow;
using FluentAssertions;

using EmsApi.Dto.V2;

namespace EmsApi.Tests.Features
{
    [Binding, Scope( Feature = "ParameterSets" )]
    public class ParameterSetsSteps : FeatureTest
    {
        [When( @"I run GetSets" )]
        public void WhenIRunGetSets()
        {
            m_result.Object = m_api.ParameterSets.GetSets();
        }

        [When( @"I run GetSets and enter a group id of '(.*)'" )]
        public void WhenIRunGetSetsAndEnterAGroupIdOf( string p0 )
        {
            m_result.Object = m_api.ParameterSets.GetSets( p0 );
        }

        [Then( @"A ParameterSetGroup object is returned" )]
        public void ThenAParameterSetGroupObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<ParameterSetGroup>();
        }
    }
}
