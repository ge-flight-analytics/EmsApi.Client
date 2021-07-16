using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    /// <summary>
    /// Provides access to the EMS API "uploads" routes.
    /// </summary>
    public class TransfersAccess : RouteAccess
    {
        /// <summary>
        /// Starts a new upload.
        /// </summary>
        /// <param name="request">
        /// The parameters for the upload.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<UploadParameters> StartUploadAsync( UploadRequest request, CallContext context = null )
        {
            return CallApiTask( api => api.StartUpload( request, context ) );
        }

        /// <summary>
        /// Starts a new upload.
        /// </summary>
        /// <param name="request">
        /// The parameters for the upload.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual UploadParameters StartUpload( UploadRequest request, CallContext context = null )
        {
            return AccessTaskResult( StartUploadAsync( request, context ) );
        }

        /// <summary>
        /// Uploads a chunk of a file. This will fail if any chunks have been skipped in the specified file.
        /// </summary>
        /// <param name="transferId">
        /// The ID of the upload, returned originally by the upload start call.
        /// </param>
        /// <param name="first">
        /// The byte index of the first byte that will be uploaded.
        /// </param>
        /// <param name="last">
        /// The byte index of the last byte that will be uploaded.
        /// </param>
        /// <param name="chunk">
        /// The bytes to upload with the chunk.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        /// <remarks>
        /// The practical limit for a single chunk is less than 4MB or so, dependent on the web server's configuration. 
        /// If you receive 500 responses, try smaller chunk sizes.
        /// </remarks>
        public virtual Task<UploadResult> UploadChunkAsync( string transferId, int first, int last, byte[] chunk, CallContext context = null )
        {
            return CallApiTask( api => api.UploadChunk( transferId, first, last, chunk, context ) );
        }

        /// <summary>
        /// Uploads a chunk of a file. This will fail if any chunks have been skipped in the specified file.
        /// </summary>
        /// <param name="transferId">
        /// The ID of the upload, returned originally by the upload start call.
        /// </param>
        /// <param name="first">
        /// The byte index of the first byte that will be uploaded.
        /// </param>
        /// <param name="last">
        /// The byte index of the last byte that will be uploaded.
        /// </param>
        /// <param name="chunk">
        /// The bytes to upload with the chunk.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        /// <remarks>
        /// The practical limit for a single chunk is less than 4MB or so, dependent on the web server's configuration. 
        /// If you receive 500 responses, try smaller chunk sizes.
        /// </remarks>
        public virtual UploadResult UploadChunk( string transferId, int first, int last, byte[] chunk, CallContext context = null )
        {
            return AccessTaskResult( UploadChunkAsync( transferId, first, last, chunk, context ) );
        }

        /// <summary>
        /// Gets the status of an upload in progress.
        /// </summary>
        /// <param name="transferId">
        /// The ID of the upload, returned originally by the upload start call.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<UploadStatus> GetUploadStatusAsync( string transferId, CallContext context = null )
        {
            return CallApiTask( api => api.GetUploadStatus( transferId, context ) );
        }

        /// <summary>
        /// Gets the status of an upload in progress.
        /// </summary>
        /// <param name="transferId">
        /// The ID of the upload, returned originally by the upload start call.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual UploadStatus GetUploadStatus( string transferId, CallContext context = null )
        {
            return AccessTaskResult( GetUploadStatusAsync( transferId, context ) );
        }

        /// <summary>
        /// Gets the list of upload records from the server.
        /// </summary>
        /// <param name="maxEntries">
        /// The maximum number of entries to return; this is capped at 50, and 50 will be used if it's not specified.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<IEnumerable<UploadRecord>> GetUploadsAsync( int maxEntries = 50, CallContext context = null )
        {
            return CallApiTask( api => api.GetUploads( maxEntries, context ) );
        }

        /// <summary>
        /// Gets the list of upload records from the server.
        /// </summary>
        /// <param name="maxEntries">
        /// The maximum number of entries to return; this is capped at 50, and 50 will be used if it's not specified.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual IEnumerable<UploadRecord> GetUploads( int maxEntries = 50, CallContext context = null )
        {
            return AccessTaskResult( GetUploadsAsync( maxEntries, context ) );
        }

        /// <summary>
        /// Completes an existing upload in progress.
        /// </summary>
        /// <param name="transferId">
        /// The ID of the upload, returned originally by the upload start call.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<UploadRecord> FinishUploadAsync( string transferId, CallContext context = null )
        {
            return CallApiTask( api => api.FinishUpload( transferId, context ) );
        }

        /// <summary>
        /// Completes an existing upload in progress.
        /// </summary>
        /// <param name="transferId">
        /// The ID of the upload, returned originally by the upload start call.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual UploadRecord FinishUpload( string transferId, CallContext context = null )
        {
            return AccessTaskResult( FinishUploadAsync( transferId, context ) );
        }

        /// <summary>
        /// Cancels an existing upload in progress.
        /// </summary>
        /// <param name="transferId">
        /// The ID of the upload, returned originally by the upload start call.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<UploadRecord> CancelUploadAsync( string transferId, CallContext context = null )
        {
            return CallApiTask( api => api.CancelUpload( transferId, context ) );
        }

        /// <summary>
        /// Cancels an existing upload in progress.
        /// </summary>
        /// <param name="transferId">
        /// The ID of the upload, returned originally by the upload start call.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual UploadRecord CancelUpload( string transferId, CallContext context = null )
        {
            return AccessTaskResult( CancelUploadAsync( transferId, context ) );
        }

        /// <summary>
        /// Gets the EMS processing status for a single upload.
        /// </summary>
        /// <param name="uploadId">
        /// The ID of the upload for which to return status information.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<UploadProcessingStatus> GetProcessingStatusAsync( string uploadId, CallContext context = null )
        {
            return CallApiTask( api => api.GetProcessingStatusSingle( uploadId, context ) );
        }

        /// <summary>
        /// Gets the EMS processing status for a single upload.
        /// </summary>
        /// <param name="uploadId">
        /// The ID of the upload for which to return status information.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual UploadProcessingStatus GetProcessingStatus( string uploadId, CallContext context = null )
        {
            return AccessTaskResult( GetProcessingStatusAsync( uploadId, context ) );
        }

        /// <summary>
        /// Gets the EMS processing status for a set of uploads.
        /// </summary>
        /// <param name="ids">
        /// An array of upload ids for which to return information.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<IEnumerable<UploadProcessingStatus>> GetProcessingStatusAsync( string[] ids, CallContext context = null )
        {
            return CallApiTask( api => api.GetProcessingStatusMultiple( ids, context ) );
        }

        /// <summary>
        /// Gets the EMS processing status for a set of uploads.
        /// </summary>
        /// <param name="ids">
        /// An array of upload ids for which to return information.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual IEnumerable<UploadProcessingStatus> GetProcessingStatus( string[] ids, CallContext context = null )
        {
            return AccessTaskResult( GetProcessingStatusAsync( ids, context ) );
        }

        /// <summary>
        /// Uploads a file from disk.
        /// </summary>
        /// <param name="path">
        /// The path to the file on disk.
        /// </param>
        /// <param name="request">
        /// The parameters for the upload.
        /// </param>
        /// <param name="chunkSizeBytes">
        /// The size (in bytes) to include in each chunk. This defaults to about 3 megabytes per chunk.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual async Task<UploadRecord> UploadFileAsync( string path, UploadRequest request, int chunkSizeBytes = DefaultChunkSize, CallContext context = null )
        {
            if( !File.Exists( path ) )
                throw new FileNotFoundException( string.Format( "Could not upload file with path {0} because it does not exist.", path ) );

            using( FileStream file = File.OpenRead( path ) )
                return await UploadStreamAsync( file, request, chunkSizeBytes, context );
        }

        /// <summary>
        /// Uploads a file from disk.
        /// </summary>
        /// <param name="path">
        /// The path to the file on disk.
        /// </param>
        /// <param name="request">
        /// The parameters for the upload.
        /// </param>
        /// <param name="chunkSizeBytes">
        /// The size (in bytes) to include in each chunk. This defaults to about 3 megabytes per chunk.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual UploadRecord UploadFile( string path, UploadRequest request, int chunkSizeBytes = DefaultChunkSize, CallContext context = null )
        {
            return AccessTaskResult( UploadFileAsync( path, request, chunkSizeBytes, context ) );
        }

        /// <summary>
        /// Uploads the stream
        /// </summary>
        /// <param name="stream">
        /// The stream to upload. The location must be reset to the start of the stream if it's already been accessed.
        /// </param>
        /// <param name="request">
        /// The parameters for the upload.
        /// </param>
        /// <param name="chunkSizeBytes">
        /// The size (in bytes) to include in each chunk. This defaults to about 3 megabytes per chunk.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual async Task<UploadRecord> UploadStreamAsync( Stream stream, UploadRequest request, int chunkSizeBytes = DefaultChunkSize, CallContext context = null )
        {
            UploadParameters info = await StartUploadAsync( request, context );

            int bytesRead, totalBytesRead = 0;
            byte[] buffer = new byte[chunkSizeBytes];

            while( (bytesRead = await stream.ReadAsync( buffer, 0, chunkSizeBytes )) > 0 )
            {
                // The start and end byte indexes for our upload call.
                int uploadStart = totalBytesRead;
                int uploadEnd = totalBytesRead + bytesRead - 1;

                // If we couldn't read a full chunk from the stream, this must be the end
                // of it. Truncate the byte array that we pass to the request.
                if( bytesRead < chunkSizeBytes )
                    buffer = buffer.Take( bytesRead ).ToArray();

                // Send the chunk.
                UploadResult chunkResult = await UploadChunkAsync( info.TransferId, uploadStart, uploadEnd, buffer, context );

                // Cancel the upload if the chunk failed.
                if( chunkResult.TransferSuccessful.HasValue && !chunkResult.TransferSuccessful.Value )
                    return await CancelUploadAsync( info.TransferId, context );

                // Track our current position.
                totalBytesRead += bytesRead;
            }

            // Finish the transfer.
            return await FinishUploadAsync( info.TransferId, context );
        }

        /// <summary>
        /// Uploads the stream
        /// </summary>
        /// <param name="stream">
        /// The stream to upload. The location must be reset to the start of the stream if it's already been accessed.
        /// </param>
        /// <param name="request">
        /// The parameters for the upload.
        /// </param>
        /// <param name="chunkSizeBytes">
        /// The size (in bytes) to include in each chunk. This defaults to about 3 megabytes per chunk.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual UploadRecord UploadStream( Stream stream, UploadRequest request, int chunkSizeBytes = DefaultChunkSize, CallContext context = null )
        {
            return AccessTaskResult( UploadStreamAsync( stream, request, chunkSizeBytes, context ) );
        }

        /// <summary>
        /// The default number of bytes to include in a single chunk of an upload (around 3MB).
        /// </summary>
        public const int DefaultChunkSize = 3145728;
    }
}
