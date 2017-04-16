
using System.Collections.Generic;
using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    /// <summary>
    /// Provides access to EMS API "trajectories" routes.
    /// </summary>
    public class TrajectoriesAccess : CachedEmsIdRouteAccess
    {
        /// <summary>
        /// Returns the current trajectory configuration.
        /// </summary>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public Task<IEnumerable<TrajectoryConfiguration>> GetAllConfigurationsAsync( int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetTrajectoryConfigurations( ems ) );
        }

        /// <summary>
        /// Returns the current trajectory configuration.
        /// </summary>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public IEnumerable<TrajectoryConfiguration> GetAllConfigurations( int emsSystem = NoEmsServerSpecified )
        {
            return SafeAccessEnumerableTask( GetAllConfigurationsAsync( emsSystem ) );
        }

        /// <summary>
        /// Returns a trajectory path for the given flight.
        /// </summary>
        /// <param name="flightId">
        /// The flight id to return trajectories for.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public Task<TrajectoryValueArray> GetTrajectoryAsync( int flightId, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetTrajectory( ems, flightId ) );
        }

        /// <summary>
        /// Returns a trajectory path for the given flight.
        /// </summary>
        /// <param name="flightId">
        /// The flight id to return trajectories for.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>        
        public TrajectoryValueArray GetTrajectory( int flightId, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetTrajectoryAsync( flightId, emsSystem ) );
        }

        /// <summary>
        /// Returns a KML document XML for the given flight and trajectory id, as a raw string.
        /// </summary>
        /// <param name="flightId">
        /// The flight id to return a trajectory for.
        /// </param>
        /// <param name="trajectoryId">
        /// The string identifier for the trajectory type to return.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public Task<string> GetTrajectoryKmlAsync( int flightId, string trajectoryId, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetTrajectoryKml( ems, flightId, trajectoryId ) );
        }

        /// <summary>
        /// Returns a KML document XML for the given flight and trajectory id, as a raw string.
        /// </summary>
        /// <param name="flightId">
        /// The flight id to return a trajectory for.
        /// </param>
        /// <param name="trajectoryId">
        /// The string identifier for the trajectory type to return.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        public string GetTrajectoryKml( int flightId, string trajectoryId, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetTrajectoryKmlAsync( flightId, trajectoryId, emsSystem ) );
        }
    }
}
