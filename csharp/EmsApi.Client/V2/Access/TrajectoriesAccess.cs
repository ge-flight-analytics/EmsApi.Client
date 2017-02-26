
using System.Collections.Generic;
using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    public class TrajectoriesAccess : CachedEmsIdRouteAccess
    {
        public Task<IEnumerable<TrajectoryConfiguration>> GetAllConfigurationsAsync( int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetTrajectoryConfigurations( ems ) );
        }

        public IEnumerable<TrajectoryConfiguration> GetAllConfigurations( int emsSystem = NoEmsServerSpecified )
        {
            return SafeAccessEnumerableTask( GetAllConfigurationsAsync( emsSystem ) );
        }

        public Task<TrajectoryValueArray> GetTrajectoryAsync( int flightId, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetTrajectory( ems, flightId ) );
        }

        public TrajectoryValueArray GetTrajectory( int flightId, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetTrajectoryAsync( flightId, emsSystem ) );
        }

        public Task<string> GetTrajectoryKmlAsync( int flightId, string trajectoryId, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetTrajectoryKml( ems, flightId, trajectoryId ) );
        }

        public string GetTrajectoryKml( int flightId, string trajectoryId, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetTrajectoryKmlAsync( flightId, trajectoryId, emsSystem ) );
        }
    }
}
