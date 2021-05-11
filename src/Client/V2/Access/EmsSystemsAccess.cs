using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    /// <summary>
    /// Provides access to EMS API "ems-systems" routes.
    /// </summary>
    public class EmsSystemsAccess : EmsApiRouteAccess
    {
        /// <summary>
        /// Returns all EMS systems connected to the API endpoint that the user has access to.
        /// </summary>
        public virtual Task<IEnumerable<EmsSystem>> GetAllAsync()
        {
            return CallApiTask( api => api.GetEmsSystems() );
        }

        /// <summary>
        /// Returns a single EMS system that the user has access to.
        /// </summary>
        /// <param name="id">
        /// The EMS system id to return.
        /// </param>
        public virtual Task<EmsSystem> GetAsync( int id )
        {
            return GetAllAsync().ContinueWith( t =>
            {
                return t.Result.FirstOrDefault( ems => ems.Id == id );
            } );
        }

        /// <summary>
        /// Returns extended server properties for a single EMS system that the user has access to.
        /// </summary>
        /// <param name="id">
        /// The EMS system id to return information for.
        /// </param>
        public virtual Task<EmsSystemInfo> GetSystemInfoAsync( int id )
        {
            return CallApiTask( api => api.GetEmsSystemInfo( id ) );
        }

        /// <summary>
        /// Returns all EMS systems connected to the API endpoint that the user has access to.
        /// </summary>
        public virtual IEnumerable<EmsSystem> GetAll()
        {
            return SafeAccessEnumerableTask( GetAllAsync() );
        }

        /// <summary>
        /// Returns a single EMS system that the user has access to.
        /// </summary>
        /// <param name="id">
        /// The EMS system id to return.
        /// </param>
        public virtual EmsSystem Get( int id )
        {
            return AccessTaskResult( GetAsync( id ) );
        }

        /// <summary>
        /// Returns extended server properties for a single EMS system that the user has access to.
        /// </summary>
        /// <param name="id">
        /// The EMS system id to return information for.
        /// </param>
        public virtual EmsSystemInfo GetSystemInfo( int id )
        {
            return AccessTaskResult( GetSystemInfoAsync( id ) );
        }

        /// <summary>
        /// Returns true if the EMS system is online.
        /// </summary>
        /// <param name="id">
        /// The EMS system id to check.
        /// </param>
        public virtual Task<bool> PingAsync( int id )
        {
            return CallApiTask( api => api.PingEmsSystem( id ) );
        }

        /// <summary>
        /// Returns true if the EMS system is online.
        /// </summary>
        /// <param name="id">
        /// The EMS system id to check.
        /// </param>
        public virtual bool Ping( int id )
        {
            return AccessTaskResult( PingAsync( id ) );
        }
    }
}
