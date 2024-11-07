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
        /// Returns basic information for a specific export service on the system.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service to return.
        /// </param>
        /// <param name="serviceType">
        /// The type of export service (analytic or raw flight data).
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual ServiceInfo GetExport( string serviceId, ServiceType serviceType, CallContext context = null )
        {
            return AccessTaskResult( GetExportAsync( serviceId, serviceType, context ) );
        }

        /// <summary>
        /// Returns basic information for a specific export service on the system.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service to return.
        /// </param>
        /// <param name="serviceType">
        /// The type of export service (analytic or raw flight data).
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<ServiceInfo> GetExportAsync( string serviceId, ServiceType serviceType, CallContext context = null )
        {
            return CallApiTask( api => api.GetExportService( serviceId, serviceType, context ) );
        }

        /// <summary>
        /// Returns processing status information for an analytic export service on the system.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service status to return.
        /// </param>
        /// <param name="serviceType">
        /// The type of export service (analytic or raw flight data).
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual ServiceStatus GetExportStatus( string serviceId, ServiceType serviceType, CallContext context = null )
        {
            return AccessTaskResult( GetExportStatusAsync( serviceId, serviceType, context ) );
        }

        /// <summary>
        /// Returns processing status information for an export service on the system.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service status to return.
        /// </param>
        /// <param name="serviceType">
        /// The type of export service (analytic or raw flight data).
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<ServiceStatus> GetExportStatusAsync( string serviceId, ServiceType serviceType, CallContext context = null )
        {
            return CallApiTask( api => api.GetExportServiceStatus( serviceId, serviceType, context ) );
        }

        /// <summary>
        /// Enables an analytic export service.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service status to enable.
        /// </param>
        /// <param name="serviceType">
        /// The type of export service (analytic or raw flight data).
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual void EnableExport( string serviceId, ServiceType serviceType, CallContext context = null )
        {
            EnableExportAsync( serviceId, serviceType, context ).Wait();
        }

        /// <summary>
        /// Enables an analytic export service.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service status to enable.
        /// </param>
        /// <param name="serviceType">
        /// The type of export service (analytic or raw flight data).
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task EnableExportAsync( string serviceId, ServiceType serviceType, CallContext context = null )
        {
            return CallApiTask( api => api.EnableExportService( serviceId, serviceType, context ) );
        }

        /// <summary>
        /// Disables an analytic export service.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service status to disable.
        /// </param>
        /// <param name="serviceType">
        /// The type of export service (analytic or raw flight data).
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual void DisableExport( string serviceId, ServiceType serviceType, CallContext context = null )
        {
            DisableExportAsync( serviceId, serviceType, context ).Wait();
        }

        /// <summary>
        /// Disables an analytic export service.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service status to disable.
        /// </param>
        /// <param name="serviceType">
        /// The type of export service (analytic or raw flight data).
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task DisableExportAsync( string serviceId, ServiceType serviceType, CallContext context = null )
        {
            return CallApiTask( api => api.DisableExportService( serviceId, serviceType, context ) );
        }

        /// <summary>
        /// Create (or update) an analytic export service using the supplied export definition.
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
        /// Create (or update) an analytic export service using the supplied export definition.
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
        /// Create (or update) a raw flight data export service using the supplied export definition.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service to create/update.
        /// </param>
        /// <param name="definition">
        /// The definition of the raw flight data export to create/update.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual void UpdateRawFlightDataExport ( string serviceId, RawFlightDataExportDefinition definition, CallContext context = null )
        {
            UpdateRawFlightDataExportAsync( serviceId, definition, context ).Wait();
        }

        /// <summary>
        /// Create (or update) a raw flight data export service using the supplied export definition.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service to create/update.
        /// </param>
        /// <param name="definition">
        /// The definition of the raw flight data export to create/update.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task UpdateRawFlightDataExportAsync( string serviceId, RawFlightDataExportDefinition definition, CallContext context = null )
        {
            return CallApiTask( api => api.UpdateRawFlightDataExportService( serviceId, definition, context ) );
        }


        /// <summary>
        /// Deletes an analytic export service.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service to delete.
        /// </param>
        /// <param name="serviceType">
        /// The type of export service (analytic or raw flight data).
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual void DeleteExport( string serviceId, ServiceType serviceType, CallContext context = null )
        {
            DeleteExportAsync( serviceId, serviceType, context ).Wait();
        }

        /// <summary>
        /// Deletes an analytic export service.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service to delete.
        /// </param>
        /// <param name="serviceType">
        /// The type of export service (analytic or raw flight data).
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task DeleteExportAsync( string serviceId, ServiceType serviceType, CallContext context = null )
        {
            return CallApiTask( api => api.DeleteExportService( serviceId, serviceType, context ) );
        }

        /// <summary>
        /// Reprocess objects for an analytic export service.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service to reprocess objects.
        /// </param>
        /// <param name="serviceType">
        /// The type of export service (analytic or raw flight data).
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        /// <returns></returns>
        public virtual void ReprocessExportServiceObjects( string serviceId, ServiceType serviceType, ReprocessObjectsRequest reprocessObjectsRequest, CallContext context = null )
        {
            ReprocessExportServiceObjectsAsync( serviceId, serviceType, reprocessObjectsRequest, context ).Wait();
        }

        /// <summary>
        /// Reprocess objects for an analytic export service.
        /// </summary>
        /// <param name="serviceId">
        /// The unique identifier for the export service to reprocess objects.
        /// </param>
        /// <param name="serviceType">
        /// The type of export service (analytic or raw flight data).
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        /// <returns></returns>
        public virtual Task ReprocessExportServiceObjectsAsync( string serviceId, ServiceType serviceType, ReprocessObjectsRequest reprocessObjectsRequest, CallContext context = null )
        {
            return CallApiTask( api => api.ReprocessExportServiceObjects( serviceId, serviceType, reprocessObjectsRequest, context ) );
        }
    }
}
