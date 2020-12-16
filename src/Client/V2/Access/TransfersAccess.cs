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
    public class TransfersAccess : CachedEmsIdRouteAccess
    {
        /// <summary>
        /// Starts a new upload.
        /// </summary>
        /// <param name="request">
        /// The parameters for the upload.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public Task<UploadParameters> StartUploadAsync( UploadRequest request, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.StartUpload( ems, request ) );
        }

        /// <summary>
        /// Starts a new upload.
        /// </summary>
        /// <param name="request">
        /// The parameters for the upload.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public UploadParameters StartUpload( UploadRequest request, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( StartUploadAsync( request, emsSystem ) );
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
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <remarks>
        /// The practical limit for a single chunk is less than 4MB or so, dependent on the web server's configuration. 
        /// If you receive 500 responses, try smaller chunk sizes.
        /// </remarks>
        public Task<UploadResult> UploadChunkAsync( string transferId, int first, int last, byte[] chunk, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.UploadChunk( ems, transferId, first, last, chunk ) );
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
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <remarks>
        /// The practical limit for a single chunk is less than 4MB or so, dependent on the web server's configuration. 
        /// If you receive 500 responses, try smaller chunk sizes.
        /// </remarks>
        public UploadResult UploadChunk( string transferId, int first, int last, byte[] chunk, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( UploadChunkAsync( transferId, first, last, chunk, emsSystem ) );
        }

        /// <summary>
        /// Gets the status of an upload in progress.
        /// </summary>
        /// <param name="transferId">
        /// The ID of the upload, returned originally by the upload start call.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public Task<UploadStatus> GetUploadStatusAsync( string transferId, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetUploadStatus( ems, transferId ) );
        }

        /// <summary>
        /// Gets the status of an upload in progress.
        /// </summary>
        /// <param name="transferId">
        /// The ID of the upload, returned originally by the upload start call.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public UploadStatus GetUploadStatus( string transferId, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetUploadStatusAsync( transferId, emsSystem ) );
        }

        /// <summary>
        /// Gets the list of upload records from the server.
        /// </summary>
        /// <param name="maxEntries">
        /// The maximum number of entries to return; this is capped at 50, and 50 will be used if it's not specified.
        /// </param>
        public Task<IEnumerable<UploadRecord>> GetUploadsAsync( int maxEntries = 50 )
        {
            return CallApiTask( api => api.GetUploads( maxEntries ) );
        }

        /// <summary>
        /// Gets the list of upload records from the server.
        /// </summary>
        /// <param name="maxEntries">
        /// The maximum number of entries to return; this is capped at 50, and 50 will be used if it's not specified.
        /// </param>
        public IEnumerable<UploadRecord> GetUploads( int maxEntries = 50 )
        {
            return AccessTaskResult( GetUploadsAsync( maxEntries ) );
        }

        /// <summary>
        /// Completes an existing upload in progress.
        /// </summary>
        /// <param name="transferId">
        /// The ID of the upload, returned originally by the upload start call.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public Task<UploadRecord> FinishUploadAsync( string transferId, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.FinishUpload( ems, transferId ) );
        }

        /// <summary>
        /// Completes an existing upload in progress.
        /// </summary>
        /// <param name="transferId">
        /// The ID of the upload, returned originally by the upload start call.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public UploadRecord FinishUpload( string transferId, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( FinishUploadAsync( transferId, emsSystem ) );
        }

        /// <summary>
        /// Cancels an existing upload in progress.
        /// </summary>
        /// <param name="transferId">
        /// The ID of the upload, returned originally by the upload start call.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public Task<UploadRecord> CancelUploadAsync( string transferId, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.CancelUpload( ems, transferId ) );
        }

        /// <summary>
        /// Cancels an existing upload in progress.
        /// </summary>
        /// <param name="transferId">
        /// The ID of the upload, returned originally by the upload start call.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public UploadRecord CancelUpload( string transferId, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( CancelUploadAsync( transferId, emsSystem ) );
        }

        /// <summary>
        /// Gets the EMS processing status for a single upload.
        /// </summary>
        /// <param name="uploadId">
        /// The ID of the upload for which to return status information.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public Task<UploadProcessingStatus> GetProcessingStatusAsync( string uploadId, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetProcessingStatusSingle( ems, uploadId ) );
        }

        /// <summary>
        /// Gets the EMS processing status for a single upload.
        /// </summary>
        /// <param name="uploadId">
        /// The ID of the upload for which to return status information.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public UploadProcessingStatus GetProcessingStatus( string uploadId, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetProcessingStatusAsync( uploadId, emsSystem ) );
        }

        /// <summary>
        /// Gets the EMS processing status for a set of uploads.
        /// </summary>
        /// <param name="ids">
        /// An array of upload ids for which to return information.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public Task<IEnumerable<UploadProcessingStatus>> GetProcessingStatusAsync( string[] ids, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetProcessingStatusMultiple( emsSystem, ids ) );
        }

        /// <summary>
        /// Gets the EMS processing status for a set of uploads.
        /// </summary>
        /// <param name="ids">
        /// An array of upload ids for which to return information.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public IEnumerable<UploadProcessingStatus> GetProcessingStatus( string[] ids, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetProcessingStatusAsync( ids, emsSystem ) );
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
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public async Task<UploadRecord> UploadFileAsync( string path, UploadRequest request, int chunkSizeBytes = DefaultChunkSize, int emsSystem = NoEmsServerSpecified )
        {
            if( !File.Exists( path ) )
                throw new FileNotFoundException( string.Format( "Could not upload file with path {0} because it does not exist.", path ) );

            using( FileStream file = File.OpenRead( path ) )
                return await UploadStreamAsync( file, request, chunkSizeBytes, emsSystem );
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
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public UploadRecord UploadFile( string path, UploadRequest request, int chunkSizeBytes = DefaultChunkSize, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( UploadFileAsync( path, request, chunkSizeBytes, emsSystem ) );
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
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public async Task<UploadRecord> UploadStreamAsync( Stream stream, UploadRequest request, int chunkSizeBytes = DefaultChunkSize, int emsSystem = NoEmsServerSpecified )
        {
            UploadParameters info = await StartUploadAsync( request, emsSystem );

            int bytesRead = 0, totalBytesRead = 0;
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
                UploadResult chunkResult = await UploadChunkAsync( info.TransferId, uploadStart, uploadEnd, buffer, emsSystem );

                // Cancel the upload if the chunk failed.
                if( chunkResult.TransferSuccessful.HasValue && !chunkResult.TransferSuccessful.Value )
                    return await CancelUploadAsync( info.TransferId, emsSystem );

                // Track our current position.
                totalBytesRead += bytesRead;
            }

            // Finish the transfer.
            return await FinishUploadAsync( info.TransferId, emsSystem );
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
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public UploadRecord UploadStream( Stream stream, UploadRequest request, int chunkSizeBytes = DefaultChunkSize, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( UploadStreamAsync( stream, request, chunkSizeBytes, emsSystem ) );
        }

        /// <summary>
        /// The default number of bytes to include in a single chunk of an upload (around 3MB).
        /// </summary>
        public const int DefaultChunkSize = 3145728;
    }
}
