using System.Collections.Generic;
using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    /// <summary>
    /// Provides access to EMS API "assets" routes.
    /// </summary>
    public class AssetsAccess : RouteAccess
    {
        /// <summary>
        /// Returns the list of fleets the user has access to in their security context.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<IEnumerable<Fleet>> GetAllFleetsAsync( CallContext context = null )
        {
            return CallApiTask( api => api.GetFleets( context ) );
        }

        /// <summary>
        /// Returns the list of fleets the user has access to in their security context.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual IEnumerable<Fleet> GetAllFleets( CallContext context = null )
        {
            return SafeAccessEnumerableTask( GetAllFleetsAsync( context ) );
        }

        /// <summary>
        /// Returns the list of fleets the user has access to in their security context.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<IEnumerable<Operator>> GetAllOperatorsAsync( CallContext context = null )
        {
            return CallApiTask( api => api.GetOperators( context ) );
        }

        /// <summary>
        /// Returns the list of operators the user has access to in their security context.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual IEnumerable<Operator> GetAllOperators( CallContext context = null )
        {
            return SafeAccessEnumerableTask( GetAllOperatorsAsync( context ) );
        }

        /// <summary>
        /// Returns the list of fleets the user has access to in their security context.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<IEnumerable<Aircraft>> GetOperatorAircraftsAsync( int operatorId, CallContext context = null )
        {
            return CallApiTask( api => api.GetAircraftByOperatorId( operatorId, context ) );
        }
        /// <summary>
        /// Returns information for a fleet on the system.
        /// </summary>
        /// <param name="fleetId">
        /// The unique identifier of the fleet.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<Fleet> GetFleetAsync( int fleetId, CallContext context = null )
        {
            return CallApiTask( api => api.GetFleet( fleetId, context ) );
        }

        /// <summary>
        /// Returns information for a fleet on the system.
        /// </summary>
        /// <param name="fleetId">
        /// The unique identifier of the fleet.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Fleet GetFleet( int fleetId, CallContext context = null )
        {
            return AccessTaskResult( GetFleetAsync( fleetId, context ) );
        }

        /// <summary>
        /// Returns the list of aircraft the user has access to in their security context.
        /// </summary>
        /// <param name="fleetId">
        /// The fleet id to filter by, if any.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<IEnumerable<Aircraft>> GetAllAircraftAsync( int? fleetId = null, CallContext context = null )
        {
            return CallApiTask( api => api.GetAllAircraft( fleetId, context ) );
        }

        /// <summary>
        /// Returns the list of aircraft the user has access to in their security context.
        /// </summary>
        /// <param name="fleetId">
        /// The fleet id to filter by, if any.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual IEnumerable<Aircraft> GetAllAircraft( int? fleetId = null, CallContext context = null )
        {
            return SafeAccessEnumerableTask( GetAllAircraftAsync( fleetId, context ) );
        }

        /// <summary>
        /// Returns info for an aircraft on the system.
        /// </summary>
        /// <param name="aircraftId">
        /// The unique identifier of the aircraft.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<Aircraft> GetAircraftAsync( int aircraftId, CallContext context = null )
        {
            return CallApiTask( api => api.GetSingleAircraft( aircraftId, context ) );
        }

        /// <summary>
        /// Returns info for an aircraft on the system.
        /// </summary>
        /// <param name="aircraftId">
        /// The unique identifier of the aircraft.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Aircraft GetAircraft( int aircraftId, CallContext context = null )
        {
            return AccessTaskResult( GetAircraftAsync( aircraftId, context ) );
        }

        /// <summary>
        /// Returns the list of airports that have been visited by the EMS system.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<IEnumerable<Airport>> GetAllAirportsAsync( CallContext context = null )
        {
            return CallApiTask( api => api.GetAirports( context ) );
        }

        /// <summary>
        /// Returns the list of airports that have been visited by the EMS system.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual IEnumerable<Airport> GetAllAirports( CallContext context = null )
        {
            return SafeAccessEnumerableTask( GetAllAirportsAsync( context ) );
        }

        /// <summary>
        /// Returns information for an airport on the system.
        /// </summary>
        /// <param name="airportId">
        /// The unique identifier for the airport.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<Airport> GetAirportAsync( int airportId, CallContext context = null )
        {
            return CallApiTask( api => api.GetAirport( airportId, context ) );
        }

        /// <summary>
        /// Returns information for an airport on the system.
        /// </summary>
        /// <param name="airportId">
        /// The unique identifier for the airport.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Airport GetAirport( int airportId, CallContext context = null )
        {
            return AccessTaskResult( GetAirportAsync( airportId, context ) );
        }

        /// <summary>
        /// Returns the list of flight phases.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<IEnumerable<FlightPhase>> GetAllFlightPhasesAsync( CallContext context = null )
        {
            return CallApiTask( api => api.GetFlightPhases( context ) );
        }

        /// <summary>
        /// Returns the list of flight phases.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual IEnumerable<FlightPhase> GetAllFlightPhases( CallContext context = null )
        {
            return SafeAccessEnumerableTask( GetAllFlightPhasesAsync( context ) );
        }

        /// <summary>
        /// Retruns information for a flight phase on the system.
        /// </summary>
        /// <param name="phaseId">
        /// The unique identifier of the flight phase.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<FlightPhase> GetFlightPhaseAsync( int phaseId, CallContext context = null )
        {
            return CallApiTask( api => api.GetFlightPhase( phaseId, context ) );
        }

        /// <summary>
        /// Retruns information for a flight phase on the system.
        /// </summary>
        /// <param name="phaseId">
        /// The unique identifier of the flight phase.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual FlightPhase GetFlightPhase( int phaseId, CallContext context = null )
        {
            return AccessTaskResult( GetFlightPhaseAsync( phaseId, context ) );
        }
    }
}
