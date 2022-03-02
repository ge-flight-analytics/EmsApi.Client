using EmsApi.Dto.V2;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace EmsApi.Tests.SpecFlow
{
    [Binding, Scope( Feature = "AdminEmsSecurables" )]
    public class AdminEmsSecurablesSteps : FeatureTest
    { 
        [When( @"I run GetAccessForSecurable with securableId '(.*)', accessRight '(.*)', and username '(.*)" )]
        public void WhenIRunGetAccessForSecurableWithSecurableIdAndAccessRight( string securableId, string accessRight, string username )
        {
            m_result.Object = m_api.AdminEmsSecurables.GetAccessForSecurable( securableId, accessRight, username );
        }

        [Then( @"EmsSecurableEffectiveAccess is returned" )]
        public void ThenEmsSecurableEffectiveAccessIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<EmsSecurableEffectiveAccess>();
        }

        [Then( @"The HasAccess property is (true|false)" )]
        public void ThenTheHasAccessPropertyIsExpected( bool hasAccess )
        {
            m_result.GetPropertyValue<bool>( "HasAccess" ).Should().Be( hasAccess );
        }
    }
}