
using TechTalk.SpecFlow;
using EmsApi.Dto.V2;
using FluentAssertions;

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

        [When( @"I run GetGlossary and enter a profile id of '(.*)' with version (.*)" )]
        public void WhenIRunGetGlossaryAndEnterAProfileIdOfWithVersion( string p0, int p1 )
        {
            m_result.Object = m_api.Profiles.GetGlossary( p0, p1 );
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

        [Then( @"A ProfileGlossary object is returned with version (.*)" )]
        public void ThenAEmsProfileGlossaryObjectIsReturnedWithVersion(int p0)
        {
            m_result.Object.ShouldNotBeNullOfType<ProfileGlossary>();
            var glossary = m_result.Object as ProfileGlossary;
            glossary.CurrentVersion.Should().Be(p0);
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

        [When( @"I run GetEvents and enter a profile id of '(.*)'" )]
        public void WhenIRunGetEventsAndEnterAProfileIdOf(string p0)
        {
            m_result.Enumerable = m_api.Profiles.GetEvents( p0 );
        }

        [Then( @"Event objects are returned" )]
        public void ThenEventObjectsAreReturned()
        {
            m_result.Enumerable.ShouldNotBeNullOrEmptyOfType<Event>();
        }

        [When( @"I run GetEvent and enter a profile id of '(.*)' and an event id of (.*)" )]
        public void WhenIRunGetEventAndEnterAProfileIdOfAndAnEventIdOf(string p0, int p1)
        {
            m_result.Object = m_api.Profiles.GetEvent( p0, p1 );
        }

        [Then( @"An Event object is returned" )]
        public void ThenAnEventObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<Event>();
        }

        [Then( @"The ParameterSet is not null" )]
        public void ThenTheParameterSetIsNotNull()
        {
            var ev = m_result.Object as Event;
            ev.ParameterSet.ShouldNotBeNullOfType<ParameterSet>();
        }
    }
}
