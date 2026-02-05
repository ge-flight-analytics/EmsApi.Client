using EmsApi.Dto.V2;
using TechTalk.SpecFlow;
using System;
using System.Linq;
using FluentAssertions;

namespace EmsApi.Tests.SpecFlow
{
    [Binding, Scope( Feature = "IncomingFile" )]
    public class IncomingFileSteps : FeatureTest
    {
        [When( @"I run GetIncomingFiles with statusModifiedDateRangeStart ""([^""]*)"", statusModifiedDateRangeEnd ""([^""]*)"", fileName ""([^""]*)"", status (\d+), sourceType (\d+), activityIds ""([^""]*)""" )]
        public void WhenIRunGetIncomingFilesWithFilters( string start, string end, string fileName, int status, int sourceType, string activityIdsCsv )
        {
            DateTime? dateStart = string.IsNullOrEmpty( start ) ? (DateTime?)null : DateTime.Parse( start );
            DateTime? dateEnd = string.IsNullOrEmpty( end ) ? (DateTime?)null : DateTime.Parse( end );
            string[] activityIds = string.IsNullOrEmpty( activityIdsCsv ) ? null : activityIdsCsv.Split( ',' ).ToArray();

            // Log the exact URL being queried (base endpoint + route + query params) for pipeline diagnostics.
            var baseEndpoint = m_api.ServiceConfig?.Endpoint ?? "";
            var route = "/v2/ems-systems/1/incomingFiles";
            var q = new System.Collections.Generic.List<string>();
            if( dateStart.HasValue ) q.Add($"statusModifiedDateRangeStart={dateStart:yyyy-MM-dd}");
            if( dateEnd.HasValue ) q.Add($"statusModifiedDateRangeEnd={dateEnd:yyyy-MM-dd}");
            if( !string.IsNullOrEmpty(fileName) ) q.Add($"fileName={Uri.EscapeDataString(fileName)}");
            q.Add($"status={status}");
            q.Add($"sourceType={sourceType}");
            if( activityIds != null && activityIds.Length > 0 )
                foreach( var id in activityIds ) q.Add($"activityIds={Uri.EscapeDataString(id)}");
            var query = string.Join("&", q);
            Console.WriteLine($"Querying: {baseEndpoint}{route}?{query}");

            m_result.Enumerable = m_api.IncomingFile.GetIncomingFilesAsync(
                dateStart, dateEnd, fileName, status, sourceType, null, activityIds
            ).Result;
        }

        [Then( @"IncomingFiles are returned" )]
        public void ThenIncomingFilesAreReturned()
        {
            m_result.Enumerable.Should().NotBeNull();
            var items = m_result.Enumerable.ToList();
            if( items.Count > 0 )
                items.Should().AllBeOfType<IncomingFile>();
        }
    }
}