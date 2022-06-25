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
        public virtual Task<IEnumerable<User>> GetUsersAsync( string username = null, CallContext context = null )
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
        public virtual IEnumerable<User> GetUsers( string username = null, CallContext context = null )
        {
            return AccessTaskResult( GetUsersAsync( username, context ) );
        }

        /// <summary>
        /// Creates a new user account with the provided settings.
        /// </summary>
        /// <param name="username">
        /// The user name to add.
        /// </param>
        /// <param name="roles">
        /// The roles to assign to the user
        /// </param>
        /// <param name="lockoutPolicyEnabled">
        /// True if the user account lockout policy should be enabled
        /// </param>
        /// <param name="tableauUsername">
        /// The tableau user name to assign to the user, if it's different than the regular username.
        /// </param>
        public virtual Task<User> AddUserAsync( string username, string[] roles = null, bool lockoutPolicyEnabled = true, string tableauUsername = null, CallContext context = null )
        {
            var user = new User
            {
                Username = username,
                Roles = roles ?? new string[0],
                LockoutPolicyEnabled = lockoutPolicyEnabled,
                TableauUsername = tableauUsername
            };

            return CallApiTask( api => api.AdminAddUser( user, context ) );
        }

        /// <summary>
        /// Creates a new user account with the provided settings.
        /// </summary>
        /// <param name="username">
        /// The user name to add.
        /// </param>
        /// <param name="roles">
        /// The roles to assign to the user
        /// </param>
        /// <param name="lockoutPolicyEnabled">
        /// True if the user account lockout policy should be enabled
        /// </param>
        /// <param name="tableauUsername">
        /// The tableau user name to assign to the user, if it's different than the regular username.
        /// </param>
        public virtual User AddUser( string username, string[] roles = null, bool lockoutPolicyEnabled = true, string tableauUsername = null, CallContext context = null )
        {
            return AccessTaskResult( AddUserAsync( username, roles, lockoutPolicyEnabled, tableauUsername, context ) );
        }

        /// <summary>
        /// Returns the list of EMS systems associated with the user account.
        /// </summary>
        /// <param name="userId">
        /// The integer ID of the user account for which to return associated EMS systems.
        /// </param>
        public virtual Task<IEnumerable<EmsSystem>> GetUserEmsSystemsAsync( int userId, CallContext context = null )
        {
            return CallApiTask( api => api.AdminGetUserEmsSystems( userId, context ) );
        }

        /// <summary>
        /// Returns the list of EMS systems associated with the user account.
        /// </summary>
        /// <param name="userId">
        /// The integer ID of the user account for which to return associated EMS systems.
        /// </param>
        public virtual IEnumerable<EmsSystem> GetUserEmsSystems( int userId, CallContext context = null )
        {
            return AccessTaskResult( GetUserEmsSystemsAsync( userId, context ) );
        }

        /// <summary>
        /// Associates a user account with an EMS system.
        /// </summary>
        /// <param name="userId">
        /// The integer ID of the user account to associate with the EMS system.
        /// </param>
        /// <param name="emsSystemId">
        /// The integer ID of the EMS system to associate with the user account.
        /// </param>
        public virtual Task AssignUserEmsSystemAsync( int userId, int emsSystemId, CallContext context = null )
        {
            return CallApiTask( api => api.AdminAssignUserEmsSystem( userId, emsSystemId, context ) );
        }

        /// <summary>
        /// Associates a user account with an EMS system.
        /// </summary>
        /// <param name="userId">
        /// The integer ID of the user account to associate with the EMS system.
        /// </param>
        /// <param name="emsSystemId">
        /// The integer ID of the EMS system to associate with the user account.
        /// </param>
        public virtual void AssignUserEmsSystem( int userId, int emsSystemId, CallContext context = null )
        {
            AssignUserEmsSystemAsync( userId, emsSystemId, context ).Wait();
        }
    }
}
