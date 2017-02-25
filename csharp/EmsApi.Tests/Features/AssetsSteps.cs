using System;
using System.Linq;
using TechTalk.SpecFlow;
using FluentAssertions;

using EmsApi.Client.V2.Model;

namespace EmsApi.Client.Tests.Features
{
    [Binding, Scope( Feature = "Assets" )]
    public class AssetsSteps : FeatureTest
    {
        [When( @"I run GetAllFleets" )]
        public void WhenIRunGetAllFleets()
        {
            m_result.Enumerable = m_api.Assets.GetAllFleets();
        }

        [Then( @"Fleet objects are returned" )]
        public void ThenFleetObjectsAreReturned()
        {
            m_result.Enumerable.ShouldNotBeNullOrEmptyOfType<Fleet>();
        }

        [When( @"I run GetFleet and enter the value (.*)" )]
        public void WhenIRunGetFleetAndEnterTheValue( int p0 )
        {
            m_result.Object = m_api.Assets.GetFleet( p0 );
        }

        [Then( @"A Fleet object is returned" )]
        public void ThenAFleetObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<Fleet>();
        }

        [When( @"I run GetAllAircraft" )]
        public void WhenIRunGetAllAircraft()
        {
            m_result.Enumerable = m_api.Assets.GetAllAircraft();
        }

        [Then( @"Aircraft objects are returned" )]
        public void ThenAircraftObjectsAreReturned()
        {
            m_result.Enumerable.ShouldNotBeNullOrEmptyOfType<Aircraft>();
        }

        [When( @"I run GetAllAircraft and enter a fleet id of (.*)" )]
        public void WhenIRunGetAllAircraftAndEnterAFleetIdOf( int p0 )
        {
            m_result.Enumerable = m_api.Assets.GetAllAircraft( fleetId: p0 );
        }

        [Then( @"Their FleetIds property contains the value (.*)" )]
        public void ThenTheirFleetIdsPropertyContainsTheValue( int p0 )
        {
            m_result.Enumerable.ShouldNotBeNullOrEmptyOfType<Aircraft>();
            foreach( var aircraft in m_result.Enumerable.OfType<Aircraft>() )
                aircraft.FleetIds.Should().Contain( p0 );
        }

        [When( @"I run GetAircraft and enter the value (.*)" )]
        public void WhenIRunGetAircraftAndEnterTheValue( int p0 )
        {
            m_result.Object = m_api.Assets.GetAircraft( p0 );
        }

        [Then( @"An Aircraft object is returned" )]
        public void ThenAnAircraftObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<Aircraft>();
        }

        [Then( @"The FleetIds property contains values" )]
        public void ThenTheFleetIdsPropertyContainsValues()
        {
            m_result.Object.As<Aircraft>().FleetIds.Should().NotBeNullOrEmpty();
        }

        [When( @"I run GetAllFlightPhases" )]
        public void WhenIRunGetAllFlightPhases()
        {
            m_result.Enumerable = m_api.Assets.GetAllFlightPhases();
        }

        [Then( @"FlightPhase objects are returned" )]
        public void ThenFlightPhaseObjectsAreReturned()
        {
            m_result.Enumerable.ShouldNotBeNullOrEmptyOfType<FlightPhase>();
        }

        [When( @"I run GetFlightPhase and enter the value (.*)" )]
        public void WhenIRunGetFlightPhaseAndEnterTheValue( int p0 )
        {
            m_result.Object = m_api.Assets.GetFlightPhase( p0 );
        }

        [Then( @"A FlightPhase object is returned" )]
        public void ThenAFlightPhaseObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<FlightPhase>();
        }

        [When( @"I run GetAllAirports" )]
        public void WhenIRunGetAllAirports()
        {
            m_result.Enumerable = m_api.Assets.GetAllAirports();
        }

        [Then( @"Airport objects are returned" )]
        public void ThenAirportObjectsAreReturned()
        {
            m_result.Enumerable.ShouldNotBeNullOrEmptyOfType<Airport>();
        }

        [When( @"I run GetAirport and enter the value (.*)" )]
        public void WhenIRunGetAirportAndEnterTheValue( int p0 )
        {
            m_result.Object = m_api.Assets.GetAirport( p0 );
        }

        [Then( @"An Airport object is returned" )]
        public void ThenAnAirportObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<Airport>();
        }

        [Then( @"The AirportCode is not empty" )]
        public void ThenTheAirportCodeIsNotEmpty()
        {
            m_result.Object.As<Airport>().AirportCode.Should().NotBeNullOrEmpty();
        }

    }
}
