
using TechTalk.SpecFlow;
using EmsApi.Dto.V2;

namespace EmsApi.Tests
{
    [Binding, Scope( Feature = "Profiles" )]
    public class ProfilesSteps : FeatureTest
    {
        [When(@"I run GetResults and enter a flight id of (.*) and a profile id of '(.*)'")]
        public void WhenIRunGetResultsAndEnterAFlightIdOfAndAProfileIdOf(int p0, string p1)
        {
            m_result.Object = m_api.Profiles.GetResults( p0, p1 );
        }

        [When(@"I run GetGlossary and enter a profile id of '(.*)'")]
        public void WhenIRunGetGlossaryAndEnterAProfileIdOf(string p0)
        {
            m_result.Object = m_api.Profiles.GetGlossary( p0 );
        }

        [Then(@"A ProfileResults object is returned")]
        public void ThenAProfileResultsObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<ProfileResults>();
        }

        [Then(@"A ProfileGlossary object is returned")]
        public void ThenAEmsProfileGlossaryObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<ProfileGlossary>();
        }

        [When( @"I run GetDefinitions" )]
        public void WhenIRunGetDefinitions()
        {
            m_result.Enumerable = m_api.Profiles.GetDefinitions();
        }

        [Then( @"Profile objects are returned" )]
        public void ThenProfileObjectsAreReturned()
        {
            m_result.Enumerable.ShouldNotBeNullOrEmptyOfType<Profile>();
        }

        [When( @"I run GetGroup" )]
        public void WhenIRunGetGroup()
        {
            m_result.Object = m_api.Profiles.GetGroup();
        }

        [Then( @"A ProfileGroup object is returned" )]
        public void ThenAProfileGroupObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<ProfileGroup>();
        }

    }
}
