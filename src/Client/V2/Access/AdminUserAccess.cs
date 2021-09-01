using System.Collections.Generic;
using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    /// <summary>
    /// Provides access to EMS API admin-user routes.
    /// </summary>
    /// <remarks>
    /// To call these routes you must have Admin level access to the EMS API.
    /// </remarks>
    public class AdminUserAccess : RouteAccess
    {
        /// <summary>
        /// Returns a list of the user accounts on the EMS API instance.
        /// </summary>
        /// <param name="username">
        /// The optional username search string used to search the list of users. Only users that contain this search
        /// string in their user name are returned.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<IEnumerable<AdminUser>> GetUsersAsync( string username = null, CallContext context = null )
        {
            return CallApiTask( api => api.AdminGetUsers( username, context ) );
        }

        /// <summary>
        /// Returns a list of the user accounts on the EMS API instance.
        /// </summary>
        /// <param name="username">
        /// The optional username search string used to search the list of users. Only users that contain this search
        /// string in their user name are returned.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual IEnumerable<AdminUser> GetUsers( string username = null, CallContext context = null )
        {
            return AccessTaskResult( GetUsersAsync( username, context ) );
        }
    }
}
