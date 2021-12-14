using EmsApi.Dto.V2;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace EmsApi.Tests.SpecFlow
{
    [Binding, Scope( Feature = "EmsSecurables" )]
    public class EmsSecurablesSteps : FeatureTest
    {
        [When( @"I run GetEmsSecurables" )]
        public void WhenIRunGetEmsSecurables()
        {
            m_result.Object = m_api.EmsSecurables.GetEmsSecurables();
        }

        [Then( @"EmsSecurableContainer is returned" )]
        public void ThenEmsSecurableContainerIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<EmsSecurableContainer>();
        }

        [When( @"I run GetAccessForSecurable with securableId '(.*)' and accessRight '(.*)'" )]
        public void WhenIRunGetAccessForSecurableWithSecurableIdAndAccessRight( string securableId, string accessRight )
        {
            m_result.Object = m_api.EmsSecurables.GetAccessForSecurable( securableId, accessRight );
        }


        [Then( @"EmsSecurableEffectiveAccess is returned" )]
        public void ThenEmsSecurableEffectiveAccessIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<EmsSecurableEffectiveAccess>();
        }

        [Then( @"The HasAccess property is (true|false)" )]
        public void ThenTheHasAccessPropertyIsExpected(bool hasAccess)
        {
            m_result.GetPropertyValue<bool>( "HasAccess" ).Should().Be( hasAccess );
        }



    }
}
