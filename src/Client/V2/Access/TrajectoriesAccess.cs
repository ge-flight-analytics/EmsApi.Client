
using System.Collections.Generic;
using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    /// <summary>
    /// Provides access to EMS API "trajectories" routes.
    /// </summary>
    public class TrajectoriesAccess : RouteAccess
    {
        /// <summary>
        /// Returns the current trajectory configuration.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<IEnumerable<TrajectoryConfiguration>> GetAllConfigurationsAsync( CallContext context = null )
        {
            return CallApiTask( api => api.GetTrajectoryConfigurations( context ) );
        }

        /// <summary>
        /// Returns the current trajectory configuration.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual IEnumerable<TrajectoryConfiguration> GetAllConfigurations( CallContext context = null )
        {
            return SafeAccessEnumerableTask( GetAllConfigurationsAsync( context ) );
        }

        /// <summary>
        /// Returns a trajectory path for the given flight.
        /// </summary>
        /// <param name="flightId">
        /// The flight id to return trajectories for.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<TrajectoryValueArray> GetTrajectoryAsync( int flightId, CallContext context = null )
        {
            return CallApiTask( api => api.GetTrajectory( flightId, context ) );
        }

        /// <summary>
        /// Returns a trajectory path for the given flight.
        /// </summary>
        /// <param name="flightId">
        /// The flight id to return trajectories for.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual TrajectoryValueArray GetTrajectory( int flightId, CallContext context = null )
        {
            return AccessTaskResult( GetTrajectoryAsync( flightId, context ) );
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
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<string> GetTrajectoryKmlAsync( int flightId, string trajectoryId, CallContext context = null )
        {
            return CallApiTask( api => api.GetTrajectoryKml( flightId, trajectoryId, context ) );
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
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual string GetTrajectoryKml( int flightId, string trajectoryId, CallContext context = null )
        {
            return AccessTaskResult( GetTrajectoryKmlAsync( flightId, trajectoryId, context ) );
        }
    }
}
