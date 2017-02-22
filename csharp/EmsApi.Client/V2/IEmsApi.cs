using System.Collections.Generic;
using System.Threading.Tasks;

using Refit;
using EmsApi.Client.V2.Model;

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
    }
}
