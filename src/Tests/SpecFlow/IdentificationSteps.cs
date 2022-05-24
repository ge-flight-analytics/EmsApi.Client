using EmsApi.Dto.V2;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace EmsApi.Tests.SpecFlow
{
    [Binding, Scope( Feature = "Identification" )]
    public class IdentificationSteps : FeatureTest
    {
        [When( @"I run GetFlightIdentification for flight (.*)" )]
        public void WhenIRunGetFlightIdentification(int flightId)
        {
            m_result.Object = m_api.Identification.GetFlightIdentification( flightId );
        }

        [Then( @"An Identification object is returned" )]
        public void ThenAnIdentificationObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<Identification>();
        }

        [Then( @"It has the fleet '([^']*)'" )]
        public void ThenItHasTheFleet( string fleetName )
        {
            var ident = m_result.Object as Identification;
            ident.Fleet.Value.Should().BeEquivalentTo( fleetName );
        }
    }
}
