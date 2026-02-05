using EmsApi.Dto.V2;
using TechTalk.SpecFlow;
using System;
using System.Linq;

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

            m_result.Enumerable = m_api.IncomingFile.GetIncomingFilesAsync(
                dateStart, dateEnd, fileName, status, sourceType, null, activityIds
            ).Result;
        }

        [Then( @"IncomingFiles are returned" )]
        public void ThenIncomingFilesAreReturned()
        {
            m_result.Enumerable.ShouldNotBeNullOrEmptyOfType<IncomingFile>();
        }
    }
}