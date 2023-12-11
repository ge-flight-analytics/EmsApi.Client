using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    /// <summary>
    /// Provides access to EMS API export service routes.
    /// </summary>
    public class ExportServiceAccess : RouteAccess
    {
        /// <summary>
        /// Returns basic information for a specific analytic export service on the system.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service to return.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual ServiceInfo GetAnalyticExport( string serviceId, CallContext context = null )
        {
            return AccessTaskResult( GetAnalyticExportAsync( serviceId, context ) );
        }

        /// <summary>
        /// Returns basic information for a specific analytic export service on the system.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service to return.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<ServiceInfo> GetAnalyticExportAsync( string serviceId, CallContext context = null )
        {
            return CallApiTask( api => api.GetAnalyticExportService( serviceId, context ) );
        }

        /// <summary>
        /// Returns processing status information for an analytic export service on the system.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service status to return.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual ServiceStatus GetAnalyticExportStatus( string serviceId, CallContext context = null )
        {
            return AccessTaskResult( GetAnalyticExportStatusAsync( serviceId, context ) );
        }

        /// <summary>
        /// Returns processing status information for an analytic export service on the system.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service status to return.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<ServiceStatus> GetAnalyticExportStatusAsync( string serviceId, CallContext context = null )
        {
            return CallApiTask( api => api.GetAnalyticExportServiceStatus( serviceId, context ) );
        }

        /// <summary>
        /// Enables an analytic export service.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service status to enable.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual void EnableAnalyticExport( string serviceId, CallContext context = null )
        {
            EnableAnalyticExportAsync( serviceId, context ).Wait();
        }

        /// <summary>
        /// Enables an analytic export service.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service status to enable.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task EnableAnalyticExportAsync( string serviceId, CallContext context = null )
        {
            return CallApiTask( api => api.EnableAnalyticExportService( serviceId, context ) );
        }

        /// <summary>
        /// Disables an analytic export service.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service status to disable.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual void DisableAnalyticExport( string serviceId, CallContext context = null )
        {
            DisableAnalyticExportAsync( serviceId, context ).Wait();
        }

        /// <summary>
        /// Disables an analytic export service.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service status to disable.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task DisableAnalyticExportAsync( string serviceId, CallContext context = null )
        {
            return CallApiTask( api => api.DisableAnalyticExportService( serviceId, context ) );
        }

        /// <summary>
        /// Creates or updates an analytic export service using the supplied export definition.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service to create/update.
        /// </param>
        /// <param name="definition">
        /// The definition of the analytic export to create/update.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual void UpdateAnalyticExport( string serviceId, AnalyticExportDefinition definition, CallContext context = null )
        {
            UpdateAnalyticExportAsync( serviceId, definition, context ).Wait();
        }

        /// <summary>
        /// Creates or updates an analytic export service using the supplied export definition.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service to create/update.
        /// </param>
        /// <param name="definition">
        /// The definition of the analytic export to create/update.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task UpdateAnalyticExportAsync( string serviceId, AnalyticExportDefinition definition, CallContext context = null )
        {
            return CallApiTask( api => api.UpdateAnalyticExportService( serviceId, definition, context ) );
        }

        /// <summary>
        /// Deletes an analytic export service.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service to delete.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual void DeleteAnalyticExport( string serviceId, CallContext context = null )
        {
            DeleteAnalyticExportAsync( serviceId, context ).Wait();
        }

        /// <summary>
        /// Deletes an analytic export service.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service to delete.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task DeleteAnalyticExportAsync( string serviceId, CallContext context = null )
        {
            return CallApiTask( api => api.DeleteAnalyticExportService( serviceId, context ) );
        }

        /// <summary>
        /// Reprocess objects for an analytic export service.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service to reprocess objects.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        /// <returns></returns>
        public virtual void ReprocessAnalyticExportServiceObjects( string serviceId, AnalyticExportReprocessObjectsRequest reprocessObjectsRequest, CallContext context = null )
        {
            ReprocessAnalyticExportServiceObjectsAsync( serviceId, reprocessObjectsRequest, context ).Wait();
        }

        /// <summary>
        /// Reprocess objects for an analytic export service.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service to reprocess objects.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        /// <returns></returns>
        public virtual Task ReprocessAnalyticExportServiceObjectsAsync( string serviceId, AnalyticExportReprocessObjectsRequest reprocessObjectsRequest, CallContext context = null )
        {
            return CallApiTask( api => api.ReprocessAnalyticExportServiceObjects( serviceId, reprocessObjectsRequest, context ) );
        }
    }
}
