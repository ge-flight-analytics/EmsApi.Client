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
