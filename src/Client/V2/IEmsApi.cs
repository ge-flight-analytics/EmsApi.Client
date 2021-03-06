using System.Collections.Generic;
using System.Threading.Tasks;

using Refit;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2
{
    /// <summary>
    /// The interface methods that match the REST signature for the EMS API.
    /// </summary>
    /// <remarks>
    /// These methods are used by the Refit library to generate a implementation to
    /// make the actual HTTP calls, so they need to mirror the exposed routes exactly.
    /// The library uses code generation to compile the stub implementation into this
    /// assembly, so every time this project is built a RefitStubs.g.cs file is generated
    /// in the obj folder and included.
    ///
    /// Note: It's important to not use constants in the REST attributes below, the library
    /// does not properly generate stubs for these (it seems to omit them).
    /// </remarks>
    public interface IEmsApi
    {
        /// <summary>
        /// Returns a set of EMS systems the currently logged in user is able to access.
        /// </summary>
        [Get( "/v2/ems-systems" )]
        Task<IEnumerable<EmsSystem>> GetEmsSystems( [Property] CallContext context = null );

        /// <summary>
        /// Returns some additional server information about the ems system.
        /// </summary>
        [Get( "/v2/ems-systems/1" )]
        Task<EmsSystemInfo> GetEmsSystemInfo( [Property] CallContext context = null );

        /// <summary>
        /// Ping an EMS system to verify that the specified system is currently up and running.
        /// </summary>
        [Get( "/v2/ems-systems/1/ping" )]
        Task<bool> PingEmsSystem( [Property] CallContext context = null );

        /// <summary>
        /// Returns the list of fleets the user has access to in their security context.
        /// </summary>
        [Get( "/v2/ems-systems/1/assets/fleets" )]
        Task<IEnumerable<Fleet>> GetFleets( [Property] CallContext context = null );

        /// <summary>
        /// Returns information for a fleet on the system.
        /// </summary>
        /// <param name="fleetId">
        /// The unique identifier of the fleet.
        /// </param>
        [Get( "/v2/ems-systems/1/assets/fleets/{fleetId}" )]
        Task<Fleet> GetFleet( int fleetId, [Property] CallContext context = null );

        /// <summary>
        /// Returns the list of aircraft the user has access to in their security context.
        /// </summary>
        /// <param name="fleetId">
        /// The fleet id to filter by, if any.
        /// </param>
        [Get( "/v2/ems-systems/1/assets/aircraft" )]
        Task<IEnumerable<Aircraft>> GetAllAircraft( int? fleetId = null, [Property] CallContext context = null );

        /// <summary>
        /// Returns info for an aircraft on the system.
        /// </summary>
        /// <param name="aircraftId">
        /// The unique identifier of the aircraft.
        /// </param>
        [Get( "/v2/ems-systems/1/assets/aircraft/{aircraftId}" )]
        Task<Aircraft> GetSingleAircraft( int aircraftId, [Property] CallContext context = null );

        /// <summary>
        /// Returns the list of flight phases.
        /// </summary>
        [Get( "/v2/ems-systems/1/assets/flight-phases" )]
        Task<IEnumerable<FlightPhase>> GetFlightPhases( [Property] CallContext context = null );

        /// <summary>
        /// Retruns information for a flight phase on the system.
        /// </summary>
        /// <param name="flightPhaseId">
        /// The unique identifier of the flight phase.
        /// </param>
        [Get( "/v2/ems-systems/1/assets/flight-phases/{flightPhaseId}" )]
        Task<FlightPhase> GetFlightPhase( int flightPhaseId, [Property] CallContext context = null );

        /// <summary>
        /// Returns the list of airports that have been visited by the EMS system.
        /// </summary>
        [Get( "/v2/ems-systems/1/assets/airports" )]
        Task<IEnumerable<Airport>> GetAirports( [Property] CallContext context = null );

        /// <summary>
        /// Returns information for an airport on the system.
        /// </summary>
        /// <param name="airportId">
        /// The unique identifier for the airport.
        /// </param>
        [Get( "/v2/ems-systems/1/assets/airports/{airportId}" )]
        Task<Airport> GetAirport( int airportId, [Property] CallContext context = null );

        /// <summary>
        /// Returns the current trajectory configuration.
        /// </summary>
        [Get( "/v2/ems-systems/1/trajectory-configurations" )]
        Task<IEnumerable<TrajectoryConfiguration>> GetTrajectoryConfigurations( [Property] CallContext context = null );

        /// <summary>
        /// Returns a trajectory path for the given flight.
        /// </summary>
        /// <param name="flightId">
        /// The flight id to return trajectories for.
        /// </param>
        [Get( "/v2/ems-systems/1/flights/{flightId}/trajectories" )]
        Task<TrajectoryValueArray> GetTrajectory( int flightId, [Property] CallContext context = null );

        /// <summary>
        /// Returns a KML document XML for the given flight and trajectory id, as a raw string.
        /// </summary>
        /// <param name="flightId">
        /// The flight id to return a trajectory for.
        /// </param>
        /// <param name="trajectoryId">
        /// The string identifier for the trajectory type to return.
        /// </param>
        [Get( "/v2/ems-systems/1/flights/{flightId}/kml-trajectories/{trajectoryId}" )]
        Task<string> GetTrajectoryKml( int flightId, string trajectoryId, [Property] CallContext context = null );

        /// <summary>
        /// Returns information about the set of APM profiles on the given EMS system.
        /// </summary>
        /// <param name="parentGroupId">
        /// The optional parent profile group ID whose contents to search.
        /// </param>
        /// <param name="search">
        /// An optional profile name search string used to match profiles to return.
        /// </param>
        [Get( "/v2/ems-systems/1/profiles" )]
        Task<IEnumerable<Profile>> GetProfiles( string parentGroupId = null, string search = null, [Property] CallContext context = null );

        /// <summary>
        /// Returns a profile group with a matching ID containing only its immediate 
        /// children in a hierarchical tree used to organize profiles.
        /// </summary>
        /// <param name="groupId">
        /// The unique identifier of the profile group whose contents to return. If 
        /// not specified, the contents of the root group are returned.
        /// </param>
        [Get( "/v2/ems-systems/1/profile-groups" )]
        Task<ProfileGroup> GetProfileGroup( string groupId = null, [Property] CallContext context = null );

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
        [Get( "/v2/ems-systems/1/flights/{flightId}/profiles/{profileId}/query" )]
        Task<ProfileResults> GetProfileResults( int flightId, string profileId, [Property] CallContext context = null );

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
        [Get( "/v2/ems-systems/1/profiles/{profileId}/glossary" )]
        Task<ProfileGlossary> GetProfileGlossary( string profileId, int? profileVersionNumber = null, string format = null, [Property] CallContext context = null );

        /// <summary>
        /// Returns the events for a specific profile.
        /// </summary>
        /// <param name="profileId">
        /// The unique identifier of the profile whose events to return, e.g. "A7483C44-9DB9-4A44-9EB5-F67681EE52B0".
        /// </param>
        [Get( "/v2/ems-systems/1/profiles/{profileId}/events" )]
        Task<IEnumerable<Event>> GetProfileEvents( string profileId, [Property] CallContext context = null );

        /// <summary>
        /// Returns an event for a specific profile.
        /// </summary>
        /// <param name="profileId">
        /// The unique identifier of the profile whose events to return, e.g. "A7483C44-9DB9-4A44-9EB5-F67681EE52B0".
        /// </param>
        /// <param name="eventId">
        /// The integer ID for the event.
        /// </param>
        [Get( "/v2/ems-systems/1/profiles/{profileId}/events/{eventId}" )]
        Task<Event> GetProfileEvent( string profileId, int eventId, [Property] CallContext context = null );

        /// <summary>
        /// Returns information about the parameter sets on the given EMS system.
        /// </summary>
        /// <param name="groupId">
        /// The optional ID of the parameter set group to return.
        /// </param>
        [Get( "/v2/ems-systems/1/parameter-sets" )]
        Task<ParameterSetGroup> GetParameterSets( string groupId = null, [Property] CallContext context = null );

        /// <summary>
        /// Searches for analytics by name.
        /// </summary>
        /// <param name="text">
        /// The search terms used to find a list of analytics by name.
        /// </param>
        /// <param name="group">
        /// An optional group ID to specify where to limit the search. If not specified, all groups are searched.
        /// </param>
        /// <param name="maxResults">
        /// The optional maximum number of matching results to return. If not specified, a default value of 200
        /// is used. Use 0 to return all results.
        /// </param>
        /// <param name="category">
        /// The category of analytics to search, including "Full", "Physical" or "Logical". A null value specifies
        /// the default analytic set, which represents the full set of values exposed by the backing EMS system.
        /// </param>
        [Get( "/v2/ems-systems/1/analytics" )]
        Task<IEnumerable<AnalyticInfo>> GetAnalytics( string text, string group = null, int? maxResults = null, Category category = Category.Full, [Property] CallContext context = null );

        /// <summary>
        /// Searches for analytics by name for a specific flight.
        /// </summary>
        /// <param name="flightId">
        /// The integer ID of the flight record to use when searching analytics.
        /// </param>
        /// <param name="text">
        /// The search terms used to find a list of analytics by name.
        /// </param>
        /// <param name="group">
        /// An optional group ID to specify where to limit the search. If not specified, all groups are searched.
        /// </param>
        /// <param name="maxResults">
        /// The optional maximum number of matching results to return. If not specified, a default value of 200
        /// is used. Use 0 to return all results.
        /// </param>
        /// <param name="category">
        /// The category of analytics to search, including "Full", "Physical" or "Logical". A null value specifies
        /// the default analytic set, which represents the full set of values exposed by the backing EMS system.
        /// </param>
        [Get( "/v2/ems-systems/1/flights/{flightId}/analytics" )]
        Task<IEnumerable<AnalyticInfo>> GetAnalyticsWithFlight( int flightId, string text, string group = null, int? maxResults = null, Category category = Category.Full, [Property] CallContext context = null );

        /// <summary>
        /// Retrieves metadata information associated with an analytic such as a description or units.
        /// </summary>
        /// <param name="analyticId">
        /// The analytic ID for which data is retrieved. These identifiers are typically obtained from nodes in an analytic group tree.
        /// </param>
        [Post( "/v2/ems-systems/1/analytics" )]
        Task<AnalyticInfo> GetAnalyticInfo( [Body] AnalyticId analyticId, [Property] CallContext context = null );

        /// <summary>
        /// Retrieves metadata information associated with an analytic such as a description or units.
        /// </summary>
        /// <param name="flightId">
        /// The integer ID of the flight record to use when retrieving the analytic information.
        /// </param>
        /// <param name="analyticId">
        /// The analytic ID for which data is retrieved. These identifiers are typically obtained from nodes in an analytic group tree.
        /// </param>
        [Post( "/v2/ems-systems/1/flights/{flightId}/analytics" )]
        Task<AnalyticInfo> GetAnalyticInfoWithFlight( int flightId, [Body] AnalyticId analyticId, [Property] CallContext context = null );

        /// <summary>
        /// Retrieves the contents of an analytic group, which is a hierarchical tree structure used to organize analytics.
        /// </summary>
        /// <param name="analyticGroupId">
        /// The ID of the group whose contents to retrieve. If not specified, the contents of the root group will be returned.
        /// </param>
        /// <param name="category">
        /// The category of analytics we are interested in. "Full", "Physical" or "Logical". A null value specifies the default
        /// analytic set, which represents the full set of values exposed by the backing EMS system.
        /// </param>
        [Get( "/v2/ems-systems/1/analytic-groups" )]
        Task<AnalyticGroupContents> GetAnalyticGroup( string analyticGroupId = null, Category category = Category.Full, [Property] CallContext context = null );

        /// <summary>
        /// Retrieves the contents of an analytic group, which is a hierarchical tree structure used to organize analytics.
        /// </summary>
        /// <param name="flightId">
        /// The integer ID of the flight record to use when retrieving the analytic information.
        /// </param>
        /// <param name="analyticGroupId">
        /// The ID of the group whose contents to retrieve. If not specified, the contents of the root group will be returned.
        /// </param>
        /// <param name="category">
        /// The category of analytics we are interested in. "Full", "Physical" or "Logical". A null value specifies the default
        /// analytic set, which represents the full set of values exposed by the backing EMS system.
        /// </param>
        [Get( "/v2/ems-systems/1/flights/{flightId}/analytic-groups" )]
        Task<AnalyticGroupContents> GetAnalyticGroupWithFlight( int flightId, string analyticGroupId = null, Category category = Category.Full, [Property] CallContext context = null );

        /// <summary>
        /// Queries offsets and values in time-series data for a specified flight and analytic.
        /// </summary>
        /// <param name="flightId">
        /// The integer ID of the flight record for which to query data.
        /// </param>
        /// <param name="query">
        /// The information used to construct a query for which results are returned.
        /// </param>
        [Post( "/v2/ems-systems/1/flights/{flightId}/analytics/query" )]
        Task<QueryResult> GetAnalyticResults( int flightId, [Body] Query query, [Property] CallContext context = null );

        /// <summary>
        /// Returns the analytic metadata for a flight.
        /// </summary>
        /// <param name="flightId">
        /// The integer ID of the flight record for which to retrieve data.
        /// </param>
        /// <param name="analyticId">
        /// The analytic ID (wrapped in double quotes) for which metadata is retrieved.
        /// These identifiers are typically obtained from nodes in an analytic group tree.
        /// </param>
        [Post( "/v2/ems-systems/1/flights/{flightId}/analytics/metadata" )]
        Task<Metadata> GetAnalyticMetadata( int flightId, [Body] AnalyticId analyticId, [Property] CallContext context = null );

        /// <summary>
        /// Returns a database group with a matching ID containing only its immediate children 
        /// in a hierarchical tree used to organize databases.
        /// </summary>
        /// <param name="groupId">
        /// The unique identifier of the EMS database group whose contents to return. If not specified, 
        /// the contents of the root group are returned.
        /// </param>
        [Get( "/v2/ems-systems/1/database-groups" )]
        Task<DatabaseGroup> GetDatabaseGroup( string groupId = null, [Property] CallContext context = null );

        /// <summary>
        /// Returns a field group with a matching ID containing only its immediate children in a 
        /// hierarchical tree used to organize fields.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database containing a field group to return.
        /// </param>
        /// <param name="groupId">
        /// The unique identifier of a field group whose contents to return. If not specified, 
        /// the contents of the root group are returned.
        /// </param>
        [Get( "/v2/ems-systems/1/databases/{databaseId}/field-groups" )]
        Task<FieldGroup> GetDatabaseFieldGroup( string databaseId, string groupId = null, [Property] CallContext context = null );

        /// <summary>
        /// Returns information about a database field matching the specified ID.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database containing a field to return.
        /// </param>
        /// <param name="fieldId">
        /// The unique identifier of the field whose information to return.
        /// </param>
        [Get( "/v2/ems-systems/1/databases/{databaseId}/fields/{fieldId}" )]
        Task<Field> GetDatabaseFieldDefinition( string databaseId, string fieldId, [Property] CallContext context = null );

        /// <summary>
        /// Returns all the fields matching the specified search options.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the database containing fields to return.
        /// </param>
        /// <param name="search">
        /// An optional field name search string used to match fields to return.
        /// </param>
        /// <param name="fieldGroupId">
        /// The optional parent field group ID whose contents to search.
        /// </param>
        /// <param name="includeProfiles">
        /// An optional setting to indicate whether to search fields in profiles. By default, 
        /// this is false since including profile fields will significantly increase search time.
        /// </param>
        /// <param name="maxResults">
        /// The maximum number of fields to return. This defaults to 200 fields. If this is set to 
        /// 0 all the results will be returned.
        /// </param>
        [Get( "/v2/ems-systems/1/databases/{databaseId}/fields" )]
        Task<IEnumerable<Field>> SearchDatabaseFields( string databaseId, string search = null, string fieldGroupId = null, bool includeProfiles = false, int maxResults = 200, [Property] CallContext context = null );

        /// <summary>
        /// Queries a database for information, composing the query with information provided in the 
        /// specified query structure.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to query.
        /// </param>
        /// <param name="query">
        /// The information used to construct a query for which results are returned.
        /// </param>
        [Post( "/v2/ems-systems/1/databases/{databaseId}/query" )]
        Task<DbQueryResult> QueryDatabase( string databaseId, [Body] DbQuery query, [Property] CallContext context = null );

        /// <summary>
        /// Creates a query on a database using the provided query structure and returns an ID that 
        /// can be used to fetch result data through other async-query routes.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to query.
        /// </param>
        /// <param name="query">
        /// The information used to construct a query for which results are returned.
        /// </param>
        [Post( "/v2/ems-systems/1/databases/{databaseId}/async-query" )]
        Task<AsyncQueryInfo> StartAsyncDatabaseQuery( string databaseId, [Body] DbQuery query, [Property] CallContext context = null );

        /// <summary>
        /// Returns rows between (inclusive) the start and end indexes from the async query with the given ID.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to query.
        /// </param>
        /// <param name="queryId">
        /// The unique identifier of the query created by the API.
        /// </param>
        /// <param name="start">
        /// The zero-based index of the first row to return.
        /// </param>
        /// <param name="end">
        /// The zero-based index of the last row to return.
        /// </param>
        [Get( "/v2/ems-systems/1/databases/{databaseId}/async-query/{queryId}/read/{start}/{end}" )]
        Task<AsyncQueryData> ReadAsyncDatabaseQuery( string databaseId, string queryId, int start, int end, [Property] CallContext context = null );

        /// <summary>
        /// Stops the async query with the given ID.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to query.
        /// </param>
        /// <param name="queryId">
        /// The unique identifier of the query created by the API.
        /// </param>
        [Delete( "/v2/ems-systems/1/databases/{databaseId}/async-query/{queryId}" )]
        Task StopAsyncDatabaseQuery( string databaseId, string queryId, [Property] CallContext context = null );

        /// <summary>
        /// Creates one or more new data entities in the database.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to add data entities to.
        /// </param>
        /// <param name="create">
        /// The information used to create one or more new data entities.
        /// </param>
        [Post( "/v2/ems-systems/1/databases/{databaseId}/create" )]
        Task<CreateResult> CreateDatabaseEntity( string databaseId, [Body] Create create, [Property] CallContext context = null );

        /// <summary>
        /// Runs an update query on one or more rows of data in the database.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to update.
        /// </param>
        /// <param name="update">
        /// The information used to construct an update query.
        /// </param>
        [Put( "/v2/ems-systems/1/databases/{databaseId}/update" )]
        Task<UpdateResult> UpdateDatabase( string databaseId, [Body] Update update, [Property] CallContext context = null );

        /// <summary>
        /// Deletes one or more existing data entities in the database.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to delete data entities from.
        /// </param>
        /// <param name="delete">
        /// The information used to delete one or more data entities.
        /// </param>
        [Post( "/v2/ems-systems/1/databases/{databaseId}/delete" )]
        Task<DeleteResult> DeleteDatabaseEntity( string databaseId, [Body] Delete delete, [Property] CallContext context = null );

        /// <summary>
        /// Adds a comment to a specific comment field.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database the comment field exists on.
        /// </param>
        /// <param name="commentFieldId">
        /// The unique identifier of the comment field to add a comment to.
        /// </param>
        /// <param name="newComment">
        /// The context and information of the new comment.
        /// </param>
        [Post( "/v2/ems-systems/1/databases/{databaseId}/comments/{commentFieldId}" )]
        Task CreateComment( string databaseId, string commentFieldId, [Body] NewComment newComment, [Property] CallContext context = null );

        /// <summary>
        /// Starts a new upload.
        /// </summary>
        /// <param name="request">
        /// The parameters for the upload.
        /// </param>
        [Post( "/v2/ems-systems/1/uploads" )]
        Task<UploadParameters> StartUpload( [Body] UploadRequest request, [Property] CallContext context = null );

        /// <summary>
        /// Uploads a chunk of a file. This will fail if any chunks have been skipped in the specified file.
        /// </summary>
        /// <param name="transferId">
        /// The ID of the upload, returned originally by the upload start call.
        /// </param>
        /// <param name="first">
        /// The byte index of the first byte that will be uploaded.
        /// </param>
        /// <param name="last">
        /// The byte index of the last byte that will be uploaded.
        /// </param>
        /// <param name="chunk">
        /// The bytes to upload with the chunk.
        /// </param>
        /// <remarks>
        /// The practical limit for a single chunk is less than 4MB or so, dependent on the web server's configuration. 
        /// If you receive 500 responses, try smaller chunk sizes.
        /// </remarks>
        [Put( "/v2/ems-systems/1/uploads/{transferId}/{first}/{last}" )]
        Task<UploadResult> UploadChunk( string transferId, long first, long last, [Body] byte[] chunk, [Property] CallContext context = null );

        /// <summary>
        /// Gets the status of an upload in progress.
        /// </summary>
        /// <param name="transferId">
        /// The ID of the upload, returned originally by the upload start call.
        /// </param>
        [Get( "/v2/ems-systems/1/uploads/{transferId}" )]
        Task<UploadStatus> GetUploadStatus( string transferId, [Property] CallContext context = null );

        /// <summary>
        /// Gets the list of upload records from the server.
        /// </summary>
        /// <param name="maxEntries">
        /// The maximum number of entries to return; this is capped at 50, and 50 
        /// will be used if it's not specified.
        /// </param>
        [Get( "/v2/uploads" )]
        Task<IEnumerable<UploadRecord>> GetUploads( int maxEntries = 50, [Property] CallContext context = null );

        /// <summary>
        /// Completes an existing upload in progress.
        /// </summary>
        /// <param name="transferId">
        /// The ID of the upload, returned originally by the upload start call.
        /// </param>
        [Get( "/v2/ems-systems/1/uploads/{transferId}/finish" )]
        Task<UploadRecord> FinishUpload( string transferId, [Property] CallContext context = null );

        /// <summary>
        /// Cancels an existing upload in progress.
        /// </summary>
        /// <param name="transferId">
        /// The ID of the upload, returned originally by the upload start call.
        /// </param>
        [Get( "/v2/ems-systems/1/uploads/{transferId}/cancel" )]
        Task<UploadRecord> CancelUpload( string transferId, [Property] CallContext context = null );

        /// <summary>
        /// Gets the EMS processing status for a single upload.
        /// </summary>
        /// <param name="uploadId">
        /// The ID of the upload for which to return status information.
        /// </param>
        [Get( "/v2/ems-systems/1/uploads/processing-status/{uploadId}" )]
        Task<UploadProcessingStatus> GetProcessingStatusSingle( string uploadId, [Property] CallContext context = null );

        /// <summary>
        /// Gets the EMS processing status for a set of uploads.
        /// </summary>
        /// <param name="ids">
        /// An array of upload ids for which to return information.
        /// </param>
        [Post( "/v2/ems-systems/1/uploads/processing-status" )]
        Task<IEnumerable<UploadProcessingStatus>> GetProcessingStatusMultiple( [Body] string[] ids, [Property] CallContext context = null );


        /// <summary>
        /// Returns the swagger specification as a raw JSON string.
        /// </summary>
        /// <param name="apiVersion">
        /// The version of the API to return the specification for. The default is "v2".
        /// </param>
        [Get( "/{apiVersion}/swagger" )]
        Task<string> GetSwaggerSpecification( string apiVersion = "v2", [Property] CallContext context = null );
    }
}
