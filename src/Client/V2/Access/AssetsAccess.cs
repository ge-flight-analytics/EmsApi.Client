using System.Collections.Generic;
using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    /// <summary>
    /// Provides access to EMS API "assets" routes.
    /// </summary>
    public class AssetsAccess : CachedEmsIdRouteAccess
    {
        /// <summary>
        /// Returns the list of fleets the user has access to in their security context.
        /// </summary>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public virtual Task<IEnumerable<Fleet>> GetAllFleetsAsync( int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetFleets( ems ) );
        }

        /// <summary>
        /// Returns the list of fleets the user has access to in their security context.
        /// </summary>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public virtual IEnumerable<Fleet> GetAllFleets( int emsSystem = NoEmsServerSpecified )
        {
            return SafeAccessEnumerableTask( GetAllFleetsAsync( emsSystem ) );
        }

        /// <summary>
        /// Returns information for a fleet on the system.
        /// </summary>
        /// <param name="fleetId">
        /// The unique identifier of the fleet.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public virtual Task<Fleet> GetFleetAsync( int fleetId, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetFleet( ems, fleetId ) );
        }

        /// <summary>
        /// Returns information for a fleet on the system.
        /// </summary>
        /// <param name="fleetId">
        /// The unique identifier of the fleet.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public virtual Fleet GetFleet( int fleetId, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetFleetAsync( fleetId, emsSystem ) );
        }

        /// <summary>
        /// Returns the list of aircraft the user has access to in their security context.
        /// </summary>
        /// <param name="fleetId">
        /// The fleet id to filter by, if any.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public virtual Task<IEnumerable<Aircraft>> GetAllAircraftAsync( int? fleetId = null, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetAllAircraft( ems, fleetId ) );
        }

        /// <summary>
        /// Returns the list of aircraft the user has access to in their security context.
        /// </summary>
        /// <param name="fleetId">
        /// The fleet id to filter by, if any.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public virtual IEnumerable<Aircraft> GetAllAircraft( int? fleetId = null, int emsSystem = NoEmsServerSpecified )
        {
            return SafeAccessEnumerableTask( GetAllAircraftAsync( fleetId, emsSystem ) );
        }

        /// <summary>
        /// Returns info for an aircraft on the system.
        /// </summary>
        /// <param name="aircraftId">
        /// The unique identifier of the aircraft.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public virtual Task<Aircraft> GetAircraftAsync( int aircraftId, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetSingleAircraft( ems, aircraftId ) );
        }

        /// <summary>
        /// Returns info for an aircraft on the system.
        /// </summary>
        /// <param name="aircraftId">
        /// The unique identifier of the aircraft.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public virtual Aircraft GetAircraft( int aircraftId, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetAircraftAsync( aircraftId, emsSystem ) );
        }

        /// <summary>
        /// Returns the list of airports that have been visited by the EMS system.
        /// </summary>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public virtual Task<IEnumerable<Airport>> GetAllAirportsAsync( int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetAirports( ems ) );
        }

        /// <summary>
        /// Returns the list of airports that have been visited by the EMS system.
        /// </summary>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public virtual IEnumerable<Airport> GetAllAirports( int emsSystem = NoEmsServerSpecified )
        {
            return SafeAccessEnumerableTask( GetAllAirportsAsync( emsSystem ) );
        }

        /// <summary>
        /// Returns information for an airport on the system.
        /// </summary>
        /// <param name="airportId">
        /// The unique identifier for the airport.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public virtual Task<Airport> GetAirportAsync( int airportId, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetAirport( ems, airportId ) );
        }

        /// <summary>
        /// Returns information for an airport on the system.
        /// </summary>
        /// <param name="airportId">
        /// The unique identifier for the airport.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public virtual Airport GetAirport( int airportId, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetAirportAsync( airportId, emsSystem ) );
        }

        /// <summary>
        /// Returns the list of flight phases.
        /// </summary>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public virtual Task<IEnumerable<FlightPhase>> GetAllFlightPhasesAsync( int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetFlightPhases( ems ) );
        }

        /// <summary>
        /// Returns the list of flight phases.
        /// </summary>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public virtual IEnumerable<FlightPhase> GetAllFlightPhases( int emsSystem = NoEmsServerSpecified )
        {
            return SafeAccessEnumerableTask( GetAllFlightPhasesAsync( emsSystem ) );
        }

        /// <summary>
        /// Retruns information for a flight phase on the system.
        /// </summary>
        /// <param name="phaseId">
        /// The unique identifier of the flight phase.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public virtual Task<FlightPhase> GetFlightPhaseAsync( int phaseId, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetFlightPhase( ems, phaseId ) );
        }

        /// <summary>
        /// Retruns information for a flight phase on the system.
        /// </summary>
        /// <param name="phaseId">
        /// The unique identifier of the flight phase.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public virtual FlightPhase GetFlightPhase( int phaseId, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetFlightPhaseAsync( phaseId, emsSystem ) );
        }
    }
}
