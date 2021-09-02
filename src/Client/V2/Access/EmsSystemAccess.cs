using System.Linq;
using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    /// <summary>
    /// Provides access to EMS API "ems-system" routes.
    /// </summary>
    public class EmsSystemAccess : RouteAccess
    {
        /// <summary>
        /// Returns the EMS system.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<EmsSystem> GetAsync( CallContext context = null )
        {
            return CallApiTask( api => api.GetEmsSystems( context ) ).ContinueWith( t =>
            {
                return t.Result?.FirstOrDefault();
            } );
        }

        /// <summary>
        /// Returns extended server properties for the EMS system.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<EmsSystemInfo> GetSystemInfoAsync( CallContext context = null )
        {
            return CallApiTask( api => api.GetEmsSystemInfo( context ) );
        }

        /// <summary>
        /// Returns extended server properties for the EMS system.
        /// </summary>
        /// <param name="search">
        /// The case-insensitive search used to filter the returned EMS system information.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<EmsSystemInfo> GetSystemInfoWithSearchAsync( string search, CallContext context = null )
        {
            return CallApiTask( api => api.GetEmsSystemInfoWithSearch( search, context ) );
        }

        /// <summary>
        /// Retrieves the next scheduled maintenance window. Or the current window if one is active at the time of this
        /// call. Or null if there is no current or next scheduled maintenance window.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual async Task<MaintenanceWindow> GetNextMaintenanceWindowAsync( CallContext context = null )
        {
            // Pull out JUST the next maintenance window information.
            var emsInfo = await CallApiTask( api => api.GetEmsSystemInfoWithSearch( "nextMaintenanceWindow", context ) );
            if( emsInfo.NextMaintenanceWindowStart.HasValue & emsInfo.NextMaintenanceWindowEnd.HasValue )
            {
                return new MaintenanceWindow
                {
                    StartUtc = emsInfo.NextMaintenanceWindowStart.Value,
                    EndUtc = emsInfo.NextMaintenanceWindowEnd.Value
                };
            }

            // No next maintenance window is defined.
            return null;
        }

        /// <summary>
        /// Returns the EMS system.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual EmsSystem Get( CallContext context = null )
        {
            return AccessTaskResult( GetAsync( context ) );
        }

        /// <summary>
        /// Returns extended server properties for the EMS system.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual EmsSystemInfo GetSystemInfo( CallContext context = null )
        {
            return AccessTaskResult( GetSystemInfoAsync( context ) );
        }

        /// <summary>
        /// Returns extended server properties for the EMS system.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual EmsSystemInfo GetSystemInfoWithSearch( string search, CallContext context = null )
        {
            return AccessTaskResult( GetSystemInfoWithSearchAsync( search, context ) );
        }

        /// <summary>
        /// Retrieves the next scheduled maintenance window. Or the current window if one is active at the time of this
        /// call. Or null if there is no current or next scheduled maintenance window.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual MaintenanceWindow GetNextMaintenanceWindow( CallContext context = null )
        {
            return AccessTaskResult( GetNextMaintenanceWindowAsync( context ) );
        }

        /// <summary>
        /// Returns true if the EMS system is online.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<bool> PingAsync( CallContext context = null )
        {
            return CallApiTask( api => api.PingEmsSystem( context ) );
        }

        /// <summary>
        /// Returns true if the EMS system is online.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual bool Ping( CallContext context = null )
        {
            return AccessTaskResult( PingAsync( context ) );
        }
    }
}
