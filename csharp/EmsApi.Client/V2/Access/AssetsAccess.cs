using System.Collections.Generic;
using System.Threading.Tasks;
using EmsApi.Client.V2.Model;

namespace EmsApi.Client.V2.Access
{
    public class AssetsAccess : CachedEmsIdRouteAccess
    {
        public Task<IEnumerable<Fleet>> GetAllFleetsAsync( int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetFleets( ems ) );
        }

        public IEnumerable<Fleet> GetAllFleets( int emsSystem = NoEmsServerSpecified )
        {
            return SafeAccessEnumerableTask( GetAllFleetsAsync( emsSystem ) );
        }

        public Task<Fleet> GetFleetAsync( int fleetId, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetFleet( ems, fleetId ) );
        }

        public Fleet GetFleet( int fleetId, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetFleetAsync( fleetId, emsSystem ) );
        }

        public Task<IEnumerable<Aircraft>> GetAllAircraftAsync( int? fleetId = null, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetAllAircraft( ems, fleetId ) );
        }

        public IEnumerable<Aircraft> GetAllAircraft( int? fleetId = null, int emsSystem = NoEmsServerSpecified )
        {
            return SafeAccessEnumerableTask( GetAllAircraftAsync( fleetId, emsSystem ) );
        }

        public Task<Aircraft> GetAircraftAsync( int aircraftId, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetSingleAircraft( ems, aircraftId ) );
        }

        public Aircraft GetAircraft( int aircraftId, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetAircraftAsync( aircraftId, emsSystem ) );
        }

        public Task<IEnumerable<Airport>> GetAllAirportsAsync( int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetAirports( ems ) );
        }

        public IEnumerable<Airport> GetAllAirports( int emsSystem = NoEmsServerSpecified )
        {
            return SafeAccessEnumerableTask( GetAllAirportsAsync( emsSystem ) );
        }

        public Task<Airport> GetAirportAsync( int airportId, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetAirport( ems, airportId ) );
        }

        public Airport GetAirport( int airportId, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetAirportAsync( airportId, emsSystem ) );
        }

        public Task<IEnumerable<FlightPhase>> GetAllFlightPhasesAsync( int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetFlightPhases( ems ) );
        }

        public IEnumerable<FlightPhase> GetAllFlightPhases( int emsSystem = NoEmsServerSpecified )
        {
            return SafeAccessEnumerableTask( GetAllFlightPhasesAsync( emsSystem ) );
        }

        public Task<FlightPhase> GetFlightPhaseAsync( int phaseId, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetFlightPhase( ems, phaseId ) );
        }

        public FlightPhase GetFlightPhase( int phaseId, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetFlightPhaseAsync( phaseId, emsSystem ) );
        }
    }
}
