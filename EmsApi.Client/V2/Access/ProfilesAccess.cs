using System.Collections.Generic;
using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    public class ProfilesAccess : CachedEmsIdRouteAccess
    {
        public Task<ProfileResults> GetResultsAsync( int flightId, string profileId, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetProfileResults( ems, flightId, profileId ) );
        }

        public ProfileResults GetResults( int flightId, string profileId, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetResultsAsync( flightId, profileId, emsSystem ) );
        }

        public Task<IEnumerable<EmsProfile>> GetAllAsync( int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetProfiles( ems ) );
        }

        public IEnumerable<EmsProfile> GetAll( int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetAllAsync( emsSystem ) );
        }

        public Task<EmsProfileGlossary> GetGlossaryAsync( string profileId, int? profileVersionNumber = null, string format = null, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetProfileGlossary( ems, profileId, profileVersionNumber, format ) );
        }

        public EmsProfileGlossary GetGlossary( string profileId, int? profileVersionNumber = null, string format = null, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetGlossaryAsync( profileId, profileVersionNumber, format, emsSystem ) );
        }
    }
}
