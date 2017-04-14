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

        public Task<IEnumerable<Profile>> GetDefinitionsAsync( string parentGroupId = null, string search = null, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetProfiles( ems, parentGroupId, search ) );
        }

        public IEnumerable<Profile> GetDefinitions( string parentGroupId = null, string search = null, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetDefinitionsAsync( parentGroupId, search, emsSystem ) );
        }

        public Task<ProfileGroup> GetGroupAsync( string groupId = null, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetProfileGroup( ems, groupId ) );
        }

        public ProfileGroup GetGroup( string groupId = null, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetGroupAsync( groupId, emsSystem ) );
        }

        public Task<ProfileGlossary> GetGlossaryAsync( string profileId, int? profileVersionNumber = null, string format = null, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetProfileGlossary( ems, profileId, profileVersionNumber, format ) );
        }

        public ProfileGlossary GetGlossary( string profileId, int? profileVersionNumber = null, string format = null, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetGlossaryAsync( profileId, profileVersionNumber, format, emsSystem ) );
        }
    }
}
