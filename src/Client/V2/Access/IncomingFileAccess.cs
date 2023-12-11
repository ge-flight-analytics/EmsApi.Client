using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    public class IncomingFileAccess : RouteAccess
    {
        /// <summary>
        /// Returns a list of incoming files to EMS with their status.
        /// </summary>
        /// <param name="statusModifiedDateRangeStart">
        /// The start date to filter status modified date.
        /// </param>
        /// <param name="statusModifiedDateRangeEnd">
        /// The end date to filter status modified date.
        /// </param>
        /// <param name="fileName">
        /// The file name to search.
        /// </param>
        /// <param name="status">
        /// The status of incoming file. (0=Received, 1=Fetched, 2=Preprocessed, 3=PartiallyIngested, 4=Ingested, -1=All)
        /// </param>
        /// <param name="sourceType">
        /// The source type of incoming file. (0=Undefined, 1=SFTP, 2=Wasabi, 3=API, 4= Other, -1:All)
        /// </param>
        public virtual Task<IEnumerable<IncomingFile>> GetIncomingFilesAsync( DateTime? statusModifiedDateRangeStart, DateTime? statusModifiedDateRangeEnd, string fileName, int status, int sourceType, CallContext context = null )
        {
            return CallApiTask( ( IEmsApi api ) => api.GetIncomingFiles( statusModifiedDateRangeStart, statusModifiedDateRangeEnd, fileName, status, sourceType, context ) );
        }

    }
}
