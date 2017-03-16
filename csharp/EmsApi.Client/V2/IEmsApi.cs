using System;
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
    [Headers( "User-Agent: EmsApi.Client (dotnet)" )]
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
        [Get( "/v2/ems-systems/{emsSystemId}/assets/fleets" )]
        Task<IEnumerable<Fleet>> GetFleets( int emsSystemId );

        /// <summary>
        /// Returns information for a fleet on the system.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system that owns the fleet.
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
        /// The unique identifier of the EMS system that owns the fleet.
        /// </param>
        /// <param name="fleetId">
        /// The fleet id to filter by, if any.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/assets/aircraft" )]
        Task<IEnumerable<Aircraft>> GetAllAircraft( int emsSystemId, [AliasAs("fleetId")] int? fleetId = null );

        /// <summary>
        /// Returns info for an aircraft on the system,.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system that owns the fleet.
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
        /// The unique identifier of the EMS system that owns the fleet.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/assets/flight-phases" )]
        Task<IEnumerable<FlightPhase>> GetFlightPhases( int emsSystemId );

        /// <summary>
        /// Retruns information for a flight phase on the system.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system that owns the fleet.
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
        /// The unique identifier of the EMS system that owns the fleet.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/assets/airports" )]
        Task<IEnumerable<Airport>> GetAirports( int emsSystemId );

        /// <summary>
        /// Returns information for an airport on the system.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system that owns the fleet.
        /// </param>
        /// <param name="airportId">
        /// The unique identifier for the airport.
        /// </param>
        /// <returns></returns>
        [Get( "/v2/ems-systems/{emsSystemId}/assets/airports/{airportId}" )]
        Task<Airport> GetAirport( int emsSystemId, int airportId );

        /// <summary>
        /// Returns the current trajectory configuration.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system that owns the trajectories.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/trajectory-configurations" )]
        Task<IEnumerable<TrajectoryConfiguration>> GetTrajectoryConfigurations( int emsSystemId );

        /// <summary>
        /// Returns a trajectory path for the given flight.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system that owns the trajectories.
        /// </param>
        /// <param name="flightId">
        /// The flight id to return trajectories for.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/flights/{flightId}/trajectories" )]
        Task<TrajectoryValueArray> GetTrajectory( int emsSystemId, int flightId );

        /// <summary>
        /// Returns a KML document XML for the given flight and trajectory id.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system that owns the trajectories.
        /// </param>
        /// <param name="flightId">
        /// The flight id to return a trajectory for.
        /// </param>
        /// <param name="trajectoryId">
        /// The string identifier for the trajectory type to return.
        /// </param>
        /// <returns></returns>
        [Get( "/v2/ems-systems/{emsSystemId}/flights/{flightId}/kml-trajectories/{trajectoryId}" )]
        Task<string> GetTrajectoryKml( int emsSystemId, int flightId, string trajectoryId );

        /// <summary>
        /// Returns APM profile results for the given flight and profile id.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system that owns the profile.
        /// </param>
        /// <param name="flightId">
        /// The flight id to return APM results for.
        /// </param>
        /// <param name="profileId">
        /// The APM profile guid to return results for, e.g. "A7483C44-9DB9-4A44-9EB5-F67681EE52B0"
        /// for the library flight safety events profile.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/flights/{flightId}/ems-profiles/{profileId}" )]
        Task<ProfileResults> GetProfileResults( int emsSystemId, int flightId, string profileId );

        /// <summary>
        /// Returns information about the set of APM profiles on the given EMS system.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system that owns the profiles.
        /// </param>
        [Get( "/v2/ems-systems/{emsSystemId}/ems-profiles" )]
        Task<IEnumerable<EmsProfile>> GetProfiles( int emsSystemId );

        /// <summary>
        /// Returns the file content of the APM profile glossary for the given profile id.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system that owns the profile.
        /// </param>
        /// <param name="profileId">
        /// The APM profile guid to return the glossary for, e.g. "A7483C44-9DB9-4A44-9EB5-F67681EE52B0"
        /// for the library flight safety events profile.
        /// </param>
        /// <param name="profileVersionNumber">
        /// The optional version number of the profile glossary to return. If this is not specified,
        /// the current version will be returned.
        /// </param>
        /// <param name="format">
        /// The format that the glossary should be returned in ("csv" or "json"), defaults to CSV.
        /// </param>
        /// <returns></returns>
        [Get( "/v2/ems-systems/{emsSystemId}/ems-profiles/{profileId}/glossary" )]
        Task<EmsProfileGlossary> GetProfileGlossary( int emsSystemId, string profileId, int? profileVersionNumber = null, string format = null );

        /// <summary>
        /// Returns information about the parameter sets on the given EMS system.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system that owns the parameter sets.
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
        /// <param name="groupId">
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
        Task<IEnumerable<AnalyticInfo>> GetAnalytics( int emsSystemId, string text, string groupId = null, int? maxResults = null, string category = null );

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
        /// <param name="groupId">
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
        Task<IEnumerable<AnalyticInfo>> GetAnalytics( int emsSystemId, int flightId, string text, string groupId = null, int? maxResults = null, string category = null );

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
        Task<AnalyticInfo> GetAnalyticInfo( int emsSystemId, int flightId, AnalyticId analyticId );

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
        Task<AnalyticGroupContents> GetAnalyticGroup( int emsSystemId, string analyticGroupId = null, string category = null );

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
        Task<AnalyticGroupContents> GetAnalyticGroup( int emsSystemId, int flightId, string analyticGroupId = null, string category = null );

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
        Task<Metadata> GetAnalyticMetadata( int emsSystemId, int flightId, string analyticId );

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
