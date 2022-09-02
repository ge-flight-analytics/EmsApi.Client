using System.Collections.Generic;
using EmsApi.Dto.V2;
using TechTalk.SpecFlow;

namespace EmsApi.Tests.SpecFlow
{
    [Binding, Scope( Feature = "Navigation" )]
    public class NavigationSteps : FeatureTest
    {
        [When( @"I run GetAirports" )]
        public void WhenIRunGetAirports()
        {
            m_result.Enumerable = m_api.Navigation.GetAirports();
        }

        [Then( @"NavigationAirports are returned" )]
        public void ThenNavigationAirportsAreReturned()
        {
            m_result.Enumerable.ShouldNotBeNullOrEmptyOfType<NavigationAirport>();
        }

        [When( @"I run GetRunways for airport id (.*)" )]
        public void WhenIRunGetRunwaysForAirportId( int airportId )
        {
            m_result.Enumerable = m_api.Navigation.GetRunways( airportId );
        }

        [Then( @"NavigationRunways are returned" )]
        public void ThenNavigationRunwaysAreReturned()
        {
            m_result.Enumerable.ShouldNotBeNullOrEmptyOfType<NavigationRunway>();
        }

        [When( @"I run GetProcedures for airport id (.*)" )]
        public void WhenIRunGetProceduresForAirportId( int airportId )
        {
            m_result.Enumerable = m_api.Navigation.GetProcedures( airportId );
        }

        [Then( @"NavigationProcedures are returned" )]
        public void ThenNavigationProceduresAreReturned()
        {
            m_result.Enumerable.ShouldNotBeNullOrEmptyOfType<NavigationProcedure>();
        }

        [When( @"I run GetSegments for procedure id (.*)" )]
        public void WhenIRunGetSegmentsForProcedureId( int procedureId )
        {
            m_result.Enumerable = m_api.Navigation.GetSegments(procedureId );
        }

        [Then( @"NavigationProcedureSegments are returned" )]
        public void ThenNavigationProcedureSegmentsAreReturned()
        {
            m_result.Enumerable.ShouldNotBeNullOrEmptyOfType<NavigationProcedureSegment>();
        }

        [When( @"I run GetWaypoint for waypoint id (.*)" )]
        public void WhenIRunGetWaypointForWaypointId( int waypointId )
        {
            m_result.Object = m_api.Navigation.GetWaypoint( waypointId );
        }

        [Then( @"a NavigationWaypoint is returned" )]
        public void ThenANavigationWaypointIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<NavigationWaypoint>();
        }

        [When( @"I run GetNavaid for navaid id (.*)" )]
        public void WhenIRunGetNavaidForNavaidId( int navaidId )
        {
            m_result.Object = m_api.Navigation.GetNavaid( navaidId );
        }

        [Then( @"a NavigationNavaid is returned" )]
        public void ThenANavigationNavaidIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<NavigationNavaid>();
        }
    }
}
