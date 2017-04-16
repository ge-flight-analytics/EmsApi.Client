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
        Task<IEnumerable<EmsSystem>> GetEmsSystems();

        /// <summary>
        /// Returns some additional server information about the ems system.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}" )]
        Task<EmsSystemInfo> GetEmsSystemInfo( int emsSystemId );

        /// <summary>
        /// Ping an EMS system to verify that the specified system is currently up and running.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/ping" )]
        Task<bool> PingEmsSystem( int emsSystemId );

        /// <summary>
        /// Returns the list of fleets the user has access to in their security context.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/assets/fleets" )]
        Task<IEnumerable<Fleet>> GetFleets( int emsSystemId );

        /// <summary>
        /// Returns information for a fleet on the system.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="fleetId">
        /// The unique identifier of the fleet.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/assets/fleets/{fleetId}" )]
        Task<Fleet> GetFleet( int emsSystemId, int fleetId );

        /// <summary>
        /// Returns the list of aircraft the user has access to in their security context.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="fleetId">
        /// The fleet id to filter by, if any.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/assets/aircraft" )]
        Task<IEnumerable<Aircraft>> GetAllAircraft( int emsSystemId, [AliasAs("fleetId")] int? fleetId = null );

        /// <summary>
        /// Returns info for an aircraft on the system.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="aircraftId">
        /// The unique identifier of the aircraft.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/assets/aircraft/{aircraftId}" )]
        Task<Aircraft> GetSingleAircraft( int emsSystemId, int aircraftId );

        /// <summary>
        /// Returns the list of flight phases.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/assets/flight-phases" )]
        Task<IEnumerable<FlightPhase>> GetFlightPhases( int emsSystemId );

        /// <summary>
        /// Retruns information for a flight phase on the system.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="flightPhaseId">
        /// The unique identifier of the flight phase.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/assets/flight-phases/{flightPhaseId}" )]
        Task<FlightPhase> GetFlightPhase( int emsSystemId, int flightPhaseId );

        /// <summary>
        /// Returns the list of airports that have been visited by the EMS system.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/assets/airports" )]
        Task<IEnumerable<Airport>> GetAirports( int emsSystemId );

        /// <summary>
        /// Returns information for an airport on the system.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="airportId">
        /// The unique identifier for the airport.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/assets/airports/{airportId}" )]
        Task<Airport> GetAirport( int emsSystemId, int airportId );

        /// <summary>
        /// Returns the current trajectory configuration.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/trajectory-configurations" )]
        Task<IEnumerable<TrajectoryConfiguration>> GetTrajectoryConfigurations( int emsSystemId );

        /// <summary>
        /// Returns a trajectory path for the given flight.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="flightId">
        /// The flight id to return trajectories for.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/flights/{flightId}/trajectories" )]
        Task<TrajectoryValueArray> GetTrajectory( int emsSystemId, int flightId );

        /// <summary>
        /// Returns a KML document XML for the given flight and trajectory id, as a raw string.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="flightId">
        /// The flight id to return a trajectory for.
        /// </param>
        /// <param name="trajectoryId">
        /// The string identifier for the trajectory type to return.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/flights/{flightId}/kml-trajectories/{trajectoryId}" )]
        Task<string> GetTrajectoryKml( int emsSystemId, int flightId, string trajectoryId );

        /// <summary>
        /// Returns information about the set of APM profiles on the given EMS system.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="parentGroupId">
        /// The optional parent profile group ID whose contents to search.
        /// </param>
        /// <param name="search">
        /// An optional profile name search string used to match profiles to return.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/profiles" )]
        Task<IEnumerable<Profile>> GetProfiles( int emsSystemId, string parentGroupId = null, string search = null );

        /// <summary>
        /// Returns a profile group with a matching ID containing only its immediate 
        /// children in a hierarchical tree used to organize profiles.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="groupId">
        /// The unique identifier of the profile group whose contents to return. If 
        /// not specified, the contents of the root group are returned.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/profile-groups" )]
        Task<ProfileGroup> GetProfileGroup( int emsSystemId, string groupId = null );

        /// <summary>
        /// Returns APM profile results for the given flight and profile id.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="flightId">
        /// The flight id to return APM results for.
        /// </param>
        /// <param name="profileId">
        /// The APM profile guid to return results for, e.g. "A7483C44-9DB9-4A44-9EB5-F67681EE52B0"
        /// for the library flight safety events profile.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/flights/{flightId}/profiles/{profileId}/query" )]
        Task<ProfileResults> GetProfileResults( int emsSystemId, int flightId, string profileId );

        /// <summary>
        /// Returns a "glossary" for a specific profile and version, which helps define the 
        /// results that can be returned in a profile.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="profileId">
        /// The unique identifier of the profile whose glossary to return, e.g. "A7483C44-9DB9-4A44-9EB5-F67681EE52B0".
        /// </param>
        /// <param name="profileVersionNumber">
        /// Integer version of the profile to return. If not specified the current version of the profile is used by default.
        /// </param>
        /// <param name="format">
        /// The format of the returned glossary. Options are "json" or "csv". Defaults to JSON.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/profiles/{profileId}/glossary" )]
        Task<ProfileGlossary> GetProfileGlossary( int emsSystemId, string profileId, int? profileVersionNumber = null, string format = null );

        /// <summary>
        /// Returns information about the parameter sets on the given EMS system.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="groupId">
        /// The optional ID of the parameter set group to return.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/parameter-sets" )]
        Task<ParameterSetGroup> GetParameterSets( int emsSystemId, string groupId = null );

        /// <summary>
        /// Searches for analytics by name.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
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
        [Get( "/v2/ems-systems/{emsSystemId}/analytics" )]
        Task<IEnumerable<AnalyticInfo>> GetAnalytics( int emsSystemId, string text, string group = null, int? maxResults = null, Category category = Category.Full );

        /// <summary>
        /// Searches for analytics by name for a specific flight.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
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
        [Get( "/v2/ems-systems/{emsSystemId}/flights/{flightId}/analytics" )]
        Task<IEnumerable<AnalyticInfo>> GetAnalyticsWithFlight( int emsSystemId, int flightId, string text, string group = null, int? maxResults = null, Category category = Category.Full );

        /// <summary>
        /// Retrieves metadata information associated with an analytic such as a description or units.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="analyticId">
        /// The analytic ID for which data is retrieved. These identifiers are typically obtained from nodes in an analytic group tree.
        /// </param>
        [Post( "/v2/ems-systems/{emsSystemId}/analytics" )]
        Task<AnalyticInfo> GetAnalyticInfo( int emsSystemId, AnalyticId analyticId );

        /// <summary>
        /// Retrieves metadata information associated with an analytic such as a description or units.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="flightId">
        /// The integer ID of the flight record to use when retrieving the analytic information.
        /// </param>
        /// <param name="analyticId">
        /// The analytic ID for which data is retrieved. These identifiers are typically obtained from nodes in an analytic group tree.
        /// </param>
        [Post( "/v2/ems-systems/{emsSystemId}/flights/{flightId}/analytics" )]
        Task<AnalyticInfo> GetAnalyticInfoWithFlight( int emsSystemId, int flightId, AnalyticId analyticId );

        /// <summary>
        /// Retrieves the contents of an analytic group, which is a hierarchical tree structure used to organize analytics.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="analyticGroupId">
        /// The ID of the group whose contents to retrieve. If not specified, the contents of the root group will be returned.
        /// </param>
        /// <param name="category">
        /// The category of analytics we are interested in. "Full", "Physical" or "Logical". A null value specifies the default
        /// analytic set, which represents the full set of values exposed by the backing EMS system.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/analytic-groups" )]
        Task<AnalyticGroupContents> GetAnalyticGroup( int emsSystemId, string analyticGroupId = null, Category category = Category.Full );

        /// <summary>
        /// Retrieves the contents of an analytic group, which is a hierarchical tree structure used to organize analytics.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
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
        [Get( "/v2/ems-systems/{emsSystemId}/flights/{flightId}/analytic-groups" )]
        Task<AnalyticGroupContents> GetAnalyticGroupWithFlight( int emsSystemId, int flightId, string analyticGroupId = null, Category category = Category.Full );

        /// <summary>
        /// Queries offsets and values in time-series data for a specified flight and analytic.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="flightId">
        /// The integer ID of the flight record for which to query data.
        /// </param>
        /// <param name="query">
        /// The information used to construct a query for which results are returned.
        /// </param>
        [Post( "/v2/ems-systems/{emsSystemId}/flights/{flightId}/analytics/query" )]
        Task<QueryResult> GetAnalyticResults( int emsSystemId, int flightId, Query query );

        /// <summary>
        /// Returns the analytic metadata for a flight.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="flightId">
        /// The integer ID of the flight record for which to retrieve data.
        /// </param>
        /// <param name="analyticId">
        /// The analytic ID (wrapped in double quotes) for which metadata is retrieved.
        /// These identifiers are typically obtained from nodes in an analytic group tree.
        /// </param>
        [Post( "/v2/ems-systems/{emsSystemId}/flights/{flightId}/analytics/metadata" )]
        Task<Metadata> GetAnalyticMetadata( int emsSystemId, int flightId, AnalyticId analyticId );

        /// <summary>
        /// Returns a database group with a matching ID containing only its immediate children 
        /// in a hierarchical tree used to organize databases.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="groupId">
        /// The unique identifier of the EMS database group whose contents to return. If not specified, 
        /// the contents of the root group are returned.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/database-groups" )]
        Task<DatabaseGroup> GetDatabaseGroup( int emsSystemId, string groupId = null );

        /// <summary>
        /// Returns a field group with a matching ID containing only its immediate children in a 
        /// hierarchical tree used to organize fields.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database containing a field group to return.
        /// </param>
        /// <param name="groupId">
        /// The unique identifier of a field group whose contents to return. If not specified, 
        /// the contents of the root group are returned.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/databases/{databaseId}/field-groups" )]
        Task<FieldGroup> GetDatabaseFieldGroup( int emsSystemId, string databaseId, string groupId = null );

        /// <summary>
        /// Returns information about a database field matching the specified ID.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database containing a field to return.
        /// </param>
        /// <param name="fieldId">
        /// The unique identifier of the field whose information to return.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/databases/{databaseId}/fields/{fieldId}" )]
        Task<Field> GetDatabaseFieldDefinition( int emsSystemId, string databaseId, string fieldId );

        /// <summary>
        /// Returns all the fields matching the specified search options.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
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
        [Get( "/v2/ems-systems/{emsSystemId}/databases/{databaseId}/fields" )]
        Task<IEnumerable<Field>> SearchDatabaseFields( int emsSystemId, string databaseId, string search = null, string fieldGroupId = null, bool includeProfiles = false, int maxResults = 200 );

        /// <summary>
        /// Queries a database for information, composing the query with information provided in the 
        /// specified query structure.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to query.
        /// </param>
        /// <param name="query">
        /// The information used to construct a query for which results are returned.
        /// </param>
        [Post( "/v2/ems-systems/{emsSystemId}/databases/{databaseId}/query" )]
        Task<DbQueryResult> QueryDatabase( int emsSystemId, string databaseId, DbQuery query );

        /// <summary>
        /// Creates a query on a database using the provided query structure and returns an ID that 
        /// can be used to fetch result data through other async-query routes.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to query.
        /// </param>
        /// <param name="query">
        /// The information used to construct a query for which results are returned.
        /// </param>
        [Post( "/v2/ems-systems/{emsSystemId}/databases/{databaseId}/async-query" )]
        Task<AsyncQueryInfo> StartAsyncDatabaseQuery( int emsSystemId, string databaseId, DbQuery query );

        /// <summary>
        /// Returns rows between (inclusive) the start and end indexes from the async query with the given ID.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
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
        [Get( "/v2/ems-systems/{emsSystemId}/databases/{databaseId}/async-query/{queryId}/read/{start}/{end}" )]
        Task<AsyncQueryData> ReadAsyncDatabaseQuery( int emsSystemId, string databaseId, string queryId, int start, int end );

        /// <summary>
        /// Stops the async query with the given ID.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to query.
        /// </param>
        /// <param name="queryId">
        /// The unique identifier of the query created by the API.
        /// </param>
        [Delete( "/v2/ems-systems/{emsSystemId}/databases/{databaseId}/async-query/{queryId}" )]
        Task StopAsyncDatabaseQuery( int emsSystemId, string databaseId, string queryId );

        /// <summary>
        /// Returns the swagger specification as a raw JSON string.
        /// </summary>
        /// <param name="apiVersion">
        /// The version of the API to return the specification for. The default is "v2".
        /// </param>
        [Get( "/{apiVersion}/swagger" )]
        Task<string> GetSwaggerSpecification( string apiVersion = "v2" );
    }
}
