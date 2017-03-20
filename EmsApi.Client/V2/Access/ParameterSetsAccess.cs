using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    public class ParameterSetsAccess : CachedEmsIdRouteAccess
    {
        /// <summary>
        /// Returns information about the parameter sets on the given EMS system.
        /// </summary>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system that owns the parameter sets.
        /// </param>
        /// <param name="groupId">
        /// The optional ID of the parameter set group to return.
        /// </param>
        public Task<ParameterSetGroup> GetSetsAsync( string groupId = null, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetParameterSets( ems, groupId ) );
        }

        /// <summary>
        /// Returns information about the parameter sets on the given EMS system.
        /// </summary>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system that owns the parameter sets.
        /// </param>
        /// <param name="groupId">
        /// The optional ID of the parameter set group to return.
        /// </param>
        public ParameterSetGroup GetSets( string groupId = null, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetSetsAsync( groupId, emsSystem ) );
        }
    }
}
