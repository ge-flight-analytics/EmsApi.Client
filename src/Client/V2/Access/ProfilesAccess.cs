using System.Collections.Generic;
using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    /// <summary>
    /// Provides access to EMS API "profiles" routes.
    /// </summary>
    public class ProfilesAccess : CachedEmsIdRouteAccess
    {
        /// <summary>
        /// Returns APM profile results for the given flight and profile id.
        /// </summary>
        /// <param name="flightId">
        /// The flight id to return APM results for.
        /// </param>
        /// <param name="profileId">
        /// The APM profile guid to return results for, e.g. "A7483C44-9DB9-4A44-9EB5-F67681EE52B0"
        /// for the library flight safety events profile.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public Task<ProfileResults> GetResultsAsync( int flightId, string profileId, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetProfileResults( ems, flightId, profileId ) );
        }

        /// <summary>
        /// Returns APM profile results for the given flight and profile id.
        /// </summary>
        /// <param name="flightId">
        /// The flight id to return APM results for.
        /// </param>
        /// <param name="profileId">
        /// The APM profile guid to return results for, e.g. "A7483C44-9DB9-4A44-9EB5-F67681EE52B0"
        /// for the library flight safety events profile.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public ProfileResults GetResults( int flightId, string profileId, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetResultsAsync( flightId, profileId, emsSystem ) );
        }

        /// <summary>
        /// Returns information about the set of APM profiles on the given EMS system.
        /// </summary>
        /// <param name="parentGroupId">
        /// The optional parent profile group ID whose contents to search.
        /// </param>
        /// <param name="search">
        /// An optional profile name search string used to match profiles to return.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public Task<IEnumerable<Profile>> GetDefinitionsAsync( string parentGroupId = null, string search = null, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetProfiles( ems, parentGroupId, search ) );
        }

        /// <summary>
        /// Returns information about the set of APM profiles on the given EMS system.
        /// </summary>
        /// <param name="parentGroupId">
        /// The optional parent profile group ID whose contents to search.
        /// </param>
        /// <param name="search">
        /// An optional profile name search string used to match profiles to return.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public IEnumerable<Profile> GetDefinitions( string parentGroupId = null, string search = null, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetDefinitionsAsync( parentGroupId, search, emsSystem ) );
        }

        /// <summary>
        /// Returns a profile group with a matching ID containing only its immediate 
        /// children in a hierarchical tree used to organize profiles.
        /// </summary>
        /// <param name="groupId">
        /// The unique identifier of the profile group whose contents to return. If 
        /// not specified, the contents of the root group are returned.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public Task<ProfileGroup> GetGroupAsync( string groupId = null, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetProfileGroup( ems, groupId ) );
        }

        /// <summary>
        /// Returns a profile group with a matching ID containing only its immediate 
        /// children in a hierarchical tree used to organize profiles.
        /// </summary>
        /// <param name="groupId">
        /// The unique identifier of the profile group whose contents to return. If 
        /// not specified, the contents of the root group are returned.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public ProfileGroup GetGroup( string groupId = null, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetGroupAsync( groupId, emsSystem ) );
        }

        /// <summary>
        /// Returns a "glossary" for a specific profile and version, which helps define the 
        /// results that can be returned in a profile.
        /// </summary>
        /// <param name="profileId">
        /// The unique identifier of the profile whose glossary to return, e.g. "A7483C44-9DB9-4A44-9EB5-F67681EE52B0".
        /// </param>
        /// <param name="profileVersionNumber">
        /// Integer version of the profile to return. If not specified the current version of the profile is used by default.
        /// </param>
        /// <param name="format">
        /// The format of the returned glossary. Options are "json" or "csv". Defaults to JSON.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public Task<ProfileGlossary> GetGlossaryAsync( string profileId, int? profileVersionNumber = null, string format = null, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetProfileGlossary( ems, profileId, profileVersionNumber, format ) );
        }

        /// <summary>
        /// Returns a "glossary" for a specific profile and version, which helps define the 
        /// results that can be returned in a profile.
        /// </summary>
        /// <param name="profileId">
        /// The unique identifier of the profile whose glossary to return, e.g. "A7483C44-9DB9-4A44-9EB5-F67681EE52B0".
        /// </param>
        /// <param name="profileVersionNumber">
        /// Integer version of the profile to return. If not specified the current version of the profile is used by default.
        /// </param>
        /// <param name="format">
        /// The format of the returned glossary. Options are "json" or "csv". Defaults to JSON.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public ProfileGlossary GetGlossary( string profileId, int? profileVersionNumber = null, string format = null, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetGlossaryAsync( profileId, profileVersionNumber, format, emsSystem ) );
        }
    }
}
