using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    /// <summary>
    /// Provides access to EMS API "parameter-sets" routes.
    /// </summary>
    public class ParameterSetsAccess : RouteAccess
    {
        /// <summary>
        /// Returns information about the parameter sets on the given EMS system.
        /// </summary>
        /// <param name="groupId">
        /// The optional ID of the parameter set group to return.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<ParameterSetGroup> GetSetsAsync( string groupId = null, CallContext context = null )
        {
            return CallApiTask( api => api.GetParameterSets( groupId, context ) );
        }

        /// <summary>
        /// Returns information about the parameter sets on the given EMS system.
        /// </summary>
        /// <param name="groupId">
        /// The optional ID of the parameter set group to return.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual ParameterSetGroup GetSets( string groupId = null, CallContext context = null )
        {
            return AccessTaskResult( GetSetsAsync( groupId, context ) );
        }
    }
}
