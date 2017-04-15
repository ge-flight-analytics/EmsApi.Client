using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using FluentAssertions;

using EmsApi.Dto.V2;

namespace EmsApi.Tests.Features
{
    [Binding, Scope( Feature = "Analytics" )]
    public sealed class AnalyticsSteps : FeatureTest
    {
        [When( @"I run Search and enter a search string of '(.*)' and a group id of '(.*)'" )]
        public void WhenIRunSearchAndEnterASearchStringOfAndAGroupIdOf( string p0, string p1 )
        {
            m_result.Enumerable = m_api.Analytics.Search( p0, p1 );
        }

        [When( @"I run Search and enter a flight id of (.*) and a search string of '(.*)' and a group id of '(.*)'" )]
        public void WhenIRunSearchAndEnterAFlightIdOfAndASearchStringOfAndAGroupIdOf( int p0, string p1, string p2 )
        {
            m_result.Enumerable = m_api.Analytics.Search( p0, p1, p2 );
        }

        [Then( @"A single AnalyticInfo object is returned" )]
        public void ThenASingleAnalyticInfoObjectIsReturned()
        {
            m_result.Enumerable.ShouldNotBeNullOrEmptyOfType<AnalyticInfo>();
            m_result.Enumerable.Should().ContainSingle();
        }

        [When( @"I run GetInfo and enter an analytic id of '(.*)'" )]
        public void WhenIRunGetInfoAndEnterAnAnalyticIdOf( string p0 )
        {
            m_result.Object = m_api.Analytics.GetInfo( p0 );
        }

        [When( @"I run GetInfo and enter a flight id of (.*) and an analytic id of '(.*)'" )]
        public void WhenIRunGetInfoAndEnterAFlightIdOfAndAnAnalyticIdOf( int p0, string p1 )
        {
            m_result.Object = m_api.Analytics.GetInfo( p0, p1 );
        }

        [Then( @"An AnlyticInfo object is returned" )]
        public void ThenAnAnlyticInfoObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<AnalyticInfo>();
        }

        [Then( @"It has the name '(.*)'" )]
        public void ThenItHasTheName( string p0 )
        {
            m_result.GetPropertyValue<string>( "Name" ).ShouldBeEquivalentTo( p0 );
        }

        [When( @"I run GetGroup and enter an an analytic group id of '(.*)'" )]
        public void WhenIRunGetGroupAndEnterAnAnAnalyticGroupIdOf( string p0 )
        {
            m_result.Object = m_api.Analytics.GetGroup( p0 );
        }

        [Then( @"An AnalyticGroupContents object is returned" )]
        public void ThenAnAnalyticGroupContentsObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<AnalyticGroupContents>();
        }

        [Then( @"It contains an analytic with the name '(.*)'" )]
        public void ThenItContainsAnAnalyticWithTheName( string p0 )
        {
            var contents = (AnalyticGroupContents)m_result.Object;
            contents.Analytics.Any( a => a.Name == p0 ).Should().BeTrue();
        }

        [When( @"I run QueryResults and enter a flight id of (.*) and a query with an analytic id of '(.*)'" )]
        public void WhenIRunQueryResultsAndEnterAFlightIdOfAndAQueryWithAnAnalyticIdOf( int p0, string p1 )
        {
            var query = new AnalyticQuery();
            query.SelectAnalytic( p1 );
            m_result.Object = m_api.Analytics.QueryResults( p0, query );
        }

        [Then( @"A QueryResult object is returned" )]
        public void ThenAQueryResultObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<QueryResult>();
            var queryResult = (QueryResult)m_result.Object;

            // This is the local id for the dimension value.
            queryResult.Results.First().Values.First().ShouldBeEquivalentTo( 38 );
        }

        [When( @"I run GetMetadata and enter a flight id of (.*) and an analytic id of '(.*)'" )]
        public void WhenIRunGetMetadataAndEnterAFlightIdOfAndAnAnalyticIdOf( int p0, string p1 )
        {
            m_result.Object = m_api.Analytics.GetMetadata( p0, p1 );
        }

        [Then( @"a Metadata object is returned" )]
        public void ThenAMetadataObjectIsReturned()
        {
            m_result.Object.ShouldNotBeNullOfType<Metadata>();
        }

    }
}
