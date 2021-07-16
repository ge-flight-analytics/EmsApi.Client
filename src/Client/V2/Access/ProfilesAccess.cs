using System.Collections.Generic;
using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    /// <summary>
    /// Provides access to EMS API "profiles" routes.
    /// </summary>
    public class ProfilesAccess : RouteAccess
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
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<ProfileResults> GetResultsAsync( int flightId, string profileId, CallContext context = null )
        {
            return CallApiTask( api => api.GetProfileResults( flightId, profileId, context ) );
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
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual ProfileResults GetResults( int flightId, string profileId, CallContext context = null )
        {
            return AccessTaskResult( GetResultsAsync( flightId, profileId, context ) );
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
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<IEnumerable<Profile>> GetDefinitionsAsync( string parentGroupId = null, string search = null, CallContext context = null )
        {
            return CallApiTask( api => api.GetProfiles( parentGroupId, search, context ) );
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
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual IEnumerable<Profile> GetDefinitions( string parentGroupId = null, string search = null, CallContext context = null )
        {
            return AccessTaskResult( GetDefinitionsAsync( parentGroupId, search, context ) );
        }

        /// <summary>
        /// Returns a profile group with a matching ID containing only its immediate 
        /// children in a hierarchical tree used to organize profiles.
        /// </summary>
        /// <param name="groupId">
        /// The unique identifier of the profile group whose contents to return. If 
        /// not specified, the contents of the root group are returned.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<ProfileGroup> GetGroupAsync( string groupId = null, CallContext context = null )
        {
            return CallApiTask( api => api.GetProfileGroup( groupId, context ) );
        }

        /// <summary>
        /// Returns a profile group with a matching ID containing only its immediate 
        /// children in a hierarchical tree used to organize profiles.
        /// </summary>
        /// <param name="groupId">
        /// The unique identifier of the profile group whose contents to return. If 
        /// not specified, the contents of the root group are returned.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual ProfileGroup GetGroup( string groupId = null, CallContext context = null )
        {
            return AccessTaskResult( GetGroupAsync( groupId, context ) );
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
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<ProfileGlossary> GetGlossaryAsync( string profileId, int? profileVersionNumber = null, string format = null, CallContext context = null )
        {
            return CallApiTask( api => api.GetProfileGlossary( profileId, profileVersionNumber, format, context ) );
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
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual ProfileGlossary GetGlossary( string profileId, int? profileVersionNumber = null, string format = null, CallContext context = null )
        {
            return AccessTaskResult( GetGlossaryAsync( profileId, profileVersionNumber, format, context ) );
        }

        /// <summary>
        /// Returns the events for a specific profile.
        /// </summary>
        /// <param name="profileId">
        /// The unique identifier of the profile whose events to return, e.g. "A7483C44-9DB9-4A44-9EB5-F67681EE52B0".
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<IEnumerable<Event>> GetEventsAsync( string profileId, CallContext context = null )
        {
            return CallApiTask( api => api.GetProfileEvents( profileId, context ) );
        }

        /// <summary>
        /// Returns the events for a specific profile.
        /// </summary>
        /// <param name="profileId">
        /// The unique identifier of the profile whose events to return, e.g. "A7483C44-9DB9-4A44-9EB5-F67681EE52B0".
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual IEnumerable<Event> GetEvents( string profileId, CallContext context = null )
        {
            return AccessTaskResult( GetEventsAsync( profileId, context ) );
        }

        /// <summary>
        /// Returns an event for a specific profile.
        /// </summary>
        /// <param name="profileId">
        /// The unique identifier of the profile whose events to return, e.g. "A7483C44-9DB9-4A44-9EB5-F67681EE52B0".
        /// </param>
        /// <param name="eventId">
        /// The integer ID for the event.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<Event> GetEventAsync( string profileId, int eventId, CallContext context = null )
        {
            return CallApiTask( api => api.GetProfileEvent( profileId, eventId, context ) );
        }

        /// <summary>
        /// Returns an event for a specific profile.
        /// </summary>
        /// <param name="profileId">
        /// The unique identifier of the profile whose events to return, e.g. "A7483C44-9DB9-4A44-9EB5-F67681EE52B0".
        /// </param>
        /// <param name="eventId">
        /// The integer ID for the event.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Event GetEvent( string profileId, int eventId, CallContext context = null )
        {
            return AccessTaskResult( GetEventAsync( profileId, eventId, context ) );
        }
    }
}
