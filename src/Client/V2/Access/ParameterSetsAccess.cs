using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    /// <summary>
    /// Provides access to EMS API "parameter-sets" routes.
    /// </summary>
    public class ParameterSetsAccess : CachedEmsIdRouteAccess
    {
        /// <summary>
        /// Returns information about the parameter sets on the given EMS system.
        /// </summary>
        /// <param name="groupId">
        /// The optional ID of the parameter set group to return.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system that owns the parameter sets.
        /// </param>
        public Task<ParameterSetGroup> GetSetsAsync( string groupId = null, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetParameterSets( ems, groupId ) );
        }

        /// <summary>
        /// Returns information about the parameter sets on the given EMS system.
        /// </summary>
        /// <param name="groupId">
        /// The optional ID of the parameter set group to return.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system that owns the parameter sets.
        /// </param>
        public ParameterSetGroup GetSets( string groupId = null, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetSetsAsync( groupId, emsSystem ) );
        }
    }
}
