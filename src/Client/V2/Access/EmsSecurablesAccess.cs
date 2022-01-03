using System.Collections.Generic;
using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    /// <summary>
    /// Provides access to EMS API "securables" routes.
    /// </summary>
    public class EmsSecurablesAccess : RouteAccess
    {
        /// <summary>
        /// Returns the root container of the EMS securables that the user has access to in their security context.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<EmsSecurableContainer> GetEmsSecurablesAsync( CallContext context = null )
        {
            return CallApiTask( api => api.GetEmsSecurables( context ) );
        }

        /// <summary>
        /// Returns the root container of the EMS securables that the user has access to in their security context.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual EmsSecurableContainer GetEmsSecurables( CallContext context = null )
        {
            return AccessTaskResult( GetEmsSecurablesAsync( context ) );
        }

        /// <summary>
        /// Returns an access check result for whether the user from the context has access to the securable.
        /// </summary>
        /// <param name="securableId">
        /// The identifier of a specific EMS securable item.
        /// </param>
        /// <param name="accessRight">
        /// The securable type-specific access right to check access against.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<EmsSecurableEffectiveAccess> GetAccessForSecurableAsync( string securableId, string accessRight, CallContext context = null )
        {
            return CallApiTask( api => api.GetEmsSecurableAccess( securableId, accessRight, context ) );
        }

        /// <summary>
        /// Returns an access check result for whether the user from the context has access to the securable.
        /// </summary>
        /// <param name="securableId">
        /// The identifier of a specific EMS securable item.
        /// </param>
        /// <param name="accessRight">
        /// The securable type-specific access right to check access against.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual EmsSecurableEffectiveAccess GetAccessForSecurable( string securableId, string accessRight, CallContext context = null )
        {
            return AccessTaskResult( GetAccessForSecurableAsync( securableId, accessRight, context ) );
        }
    }
}
