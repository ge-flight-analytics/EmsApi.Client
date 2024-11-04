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
        public virtual ServiceInfo GetAnalyticExport( string serviceId, ServiceType serviceType, CallContext context = null )
        {
            return AccessTaskResult( GetAnalyticExportAsync( serviceId, serviceType, context ) );
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
        public virtual Task<ServiceInfo> GetAnalyticExportAsync( string serviceId, ServiceType serviceType, CallContext context = null )
        {
            return CallApiTask( api => api.GetAnalyticExportService( serviceId, serviceType, context ) );
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
        public virtual ServiceStatus GetAnalyticExportStatus( string serviceId, ServiceType serviceType, CallContext context = null )
        {
            return AccessTaskResult( GetAnalyticExportStatusAsync( serviceId, serviceType, context ) );
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
        public virtual Task<ServiceStatus> GetAnalyticExportStatusAsync( string serviceId, ServiceType serviceType, CallContext context = null )
        {
            return CallApiTask( api => api.GetAnalyticExportServiceStatus( serviceId, serviceType, context ) );
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
        public virtual void EnableAnalyticExport( string serviceId, ServiceType serviceType, CallContext context = null )
        {
            EnableAnalyticExportAsync( serviceId, serviceType, context ).Wait();
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
        public virtual Task EnableAnalyticExportAsync( string serviceId, ServiceType serviceType, CallContext context = null )
        {
            return CallApiTask( api => api.EnableAnalyticExportService( serviceId, serviceType, context ) );
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
        public virtual void DisableAnalyticExport( string serviceId, ServiceType serviceType, CallContext context = null )
        {
            DisableAnalyticExportAsync( serviceId, serviceType, context ).Wait();
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
        public virtual Task DisableAnalyticExportAsync( string serviceId, ServiceType serviceType, CallContext context = null )
        {
            return CallApiTask( api => api.DisableAnalyticExportService( serviceId, serviceType, context ) );
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
        public virtual void DeleteAnalyticExport( string serviceId, ServiceType serviceType, CallContext context = null )
        {
            DeleteAnalyticExportAsync( serviceId, serviceType, context ).Wait();
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
        public virtual Task DeleteAnalyticExportAsync( string serviceId, ServiceType serviceType, CallContext context = null )
        {
            return CallApiTask( api => api.DeleteAnalyticExportService( serviceId, serviceType, context ) );
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
        public virtual void ReprocessAnalyticExportServiceObjects( string serviceId, ServiceType serviceType, ReprocessObjectsRequest reprocessObjectsRequest, CallContext context = null )
        {
            ReprocessAnalyticExportServiceObjectsAsync( serviceId, serviceType, reprocessObjectsRequest, context ).Wait();
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
        public virtual Task ReprocessAnalyticExportServiceObjectsAsync( string serviceId, ServiceType serviceType, ReprocessObjectsRequest reprocessObjectsRequest, CallContext context = null )
        {
            return CallApiTask( api => api.ReprocessAnalyticExportServiceObjects( serviceId, serviceType, reprocessObjectsRequest, context ) );
        }
    }
}
