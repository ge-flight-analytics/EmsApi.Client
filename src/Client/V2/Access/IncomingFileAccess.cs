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
        /// <param name="activityTimeRangeStartDate">
        /// The start date to filter activation time.
        /// </param>
        /// <param name="activityTimeRangeEndDate">
        /// The end date to filter activation time.
        /// </param>
        /// <param name="fileName">
        /// The file name to search.
        /// </param>
        /// <param name="status">
        /// The status of incoming file. (0=Fetched, 1=Uploaded, 2=Processed, -1=All)
        /// </param>
        /// <param name="sourceType">
        /// The source type of incoming file. (0=Undefined, 1=SFTP, 2=Wasabi, 3=API, 4= Other, -1:All)
        /// </param>
        public virtual Task<IEnumerable<IncomingFile>> GetIncomingFilesAsync( string activityTimeRangeStart, string activityTimeRangeEnd, string fileName, int status, int sourceType, CallContext context = null )
        {
            return CallApiTask( ( IEmsApi api ) => api.GetIncomingFiles( activityTimeRangeStart, activityTimeRangeEnd, fileName, status, sourceType, context ) );
        }

    }
}
