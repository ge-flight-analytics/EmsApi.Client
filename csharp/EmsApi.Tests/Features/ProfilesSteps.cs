
using TechTalk.SpecFlow;
using EmsApi.Dto.V2;

namespace EmsApi.Tests
{
    [Binding, Scope( Feature = "Profiles" )]
    public class ProfilesSteps : FeatureTest
    {
        [When(@"I run GetAll")]
        public void WhenIRunGetAll()
        {
            m_result.Enumerable = m_api.Profiles.GetAll();
        }

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

        [Then(@"EmsProfile objects are returned")]
        public void ThenEmsProfileObjectsAreReturned()
        {
            m_result.Enumerable.ShouldNotBeNullOrEmptyOfType<EmsProfile>();
        }

        [Then(@"A ProfileResults object is returned")]
        public void ThenAProfileResultsObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<ProfileResults>();
        }

        [Then(@"An EmsProfileGlossary object is returned")]
        public void ThenAnEmsProfileGlossaryObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<EmsProfileGlossary>();
        }
    }
}
