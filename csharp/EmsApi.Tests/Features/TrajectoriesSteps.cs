using System;
using TechTalk.SpecFlow;
using EmsApi.Dto.V2;
using FluentAssertions;

namespace EmsApi.Tests.Features
{
    [Binding, Scope( Feature = "Trajectories" )]
    public class TrajectoriesSteps : FeatureTest
    {
        [When(@"I run GetAllConfigurations")]
        public void WhenIRunGetAllConfigurations()
        {
            m_result.Enumerable = m_api.Trajectories.GetAllConfigurations();
        }

        [Then(@"TrajectoryConfiguration objects are returned")]
        public void ThenTrajectoryConfigurationObjectsAreReturned()
        {
            m_result.Enumerable.ShouldNotBeNullOrEmptyOfType<TrajectoryConfiguration>();
        }


        [When( @"I run GetTrajectory and enter a value of (.*)" )]
        public void WhenIRunGetTrajectoryAndEnterAValueOf( int p0 )
        {
            m_result.Object = m_api.Trajectories.GetTrajectory( p0 );
        }

        [Then( @"A TrajectoryValueArray object is returned" )]
        public void ThenATrajectoryValueArrayObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<TrajectoryValueArray>();
        }

        [When( @"I run GetTrajectoryKml and enter a value of (.*) and a value of '(.*)'" )]
        public void WhenIRunGetTrajectoryKmlAndEnterAValueOfAndAValueOf( int p0, string p1 )
        {
            m_result.Object = m_api.Trajectories.GetTrajectoryKml( p0, p1 );
        }

        [Then( @"An XML string representing the KML document is returned" )]
        public void ThenAnXMLStringRepresentingTheKMLDocumentIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<string>();
            ((string)m_result.Object).EndsWith( "</kml>" ).Should().BeTrue();
        }
    }
}
