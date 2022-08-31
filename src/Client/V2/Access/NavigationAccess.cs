using System.Collections.Generic;
using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    /// <summary>
    /// Access to airport navigation information: airports, runways, procedures, segments, waypoints, and NAVAIDs. 
    /// </summary>
    public class NavigationAccess : RouteAccess
    {
        /// <summary>
        /// Gets the active airports for the EMS system.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<IEnumerable<NavigationAirport>> GetAirportsAsync( CallContext context = null )
        {
            return CallApiTask( api => api.GetNavigationAirports( context ) );
        }

        /// <summary>
        /// Gets the active airports for the EMS system.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual IEnumerable<NavigationAirport> GetAirports( CallContext context = null )
        {
            return AccessTaskResult( GetAirportsAsync() );
        }

        /// <summary>
        /// Gets the runways for a specific airport.
        /// </summary>
        /// <param name="airportId">
        /// The airport to get the runways for.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<IEnumerable<NavigationRunway>> GetRunwaysAsync( int airportId, CallContext context = null )
        {
            return CallApiTask( api => api.GetNavigationRunways( airportId, context ) );
        }

        /// <summary>
        /// Gets the runways for a specific airport.
        /// </summary>
        /// <param name="airportId">
        /// The airport to get the runways for.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual IEnumerable<NavigationRunway> GetRunways( int airportId, CallContext context = null )
        {
            return AccessTaskResult( GetRunwaysAsync( airportId, context ) );
        }

        /// <summary>
        /// Gets the procedures for a specific airport.
        /// </summary>
        /// <param name="airportId">
        /// The airport to get the procedures for.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<IEnumerable<NavigationProcedure>> GetProceduresAsync( int airportId, CallContext context = null )
        {
            return CallApiTask( api => api.GetNavigationProcedures( airportId, context ) );
        }

        /// <summary>
        /// Gets the procedures for a specific airport.
        /// </summary>
        /// <param name="airportId">
        /// The airport to get the procedures for.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual IEnumerable<NavigationProcedure> GetProcedures( int airportId, CallContext context = null )
        {
            return AccessTaskResult( GetProceduresAsync( airportId, context ) );
        }

        /// <summary>
        /// Gets the segments for a specific procedure.
        /// </summary>
        /// <param name="procedureId">
        /// The procedure to get the segments for.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<IEnumerable<NavigationProcedureSegment>> GetSegmentsAsync( int procedureId, CallContext context = null )
        {
            return CallApiTask( api => api.GetNavigationSegments( procedureId, context ) );
        }

        /// <summary>
        /// Gets the segments for a specific procedure.
        /// </summary>
        /// <param name="procedureId">
        /// The procedure to get the segments for.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual IEnumerable<NavigationProcedureSegment> GetSegments( int procedureId, CallContext context = null )
        {
            return AccessTaskResult( GetSegmentsAsync( procedureId, context ) );
        }

        /// <summary>
        /// Get a waypoint.
        /// </summary>
        /// <param name="waypointId">
        /// The waypoint to retrieve.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<NavigationWaypoint> GetWaypointAsync( int waypointId, CallContext context = null )
        {
            return CallApiTask( api => api.GetNavigationWaypoint( waypointId, context ) );
        }

        /// <summary>
        /// Get a waypoint.
        /// </summary>
        /// <param name="waypointId">
        /// The waypoint to retrieve.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual NavigationWaypoint GetWaypoint( int waypointId, CallContext context = null )
        {
            return AccessTaskResult( GetWaypointAsync( waypointId, context ) );
        }

        /// <summary>
        /// Get a NAVAID.
        /// </summary>
        /// <param name="navaidId">
        /// The NAVAID to retrieve.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<NavigationNavaid> GetNavaidAsync( int navaidId, CallContext context = null )
        {
            return CallApiTask( api => api.GetNavigationNavaid( navaidId, context ) );
        }

        /// <summary>
        /// Get a NAVAID.
        /// </summary>
        /// <param name="navaidId">
        /// The NAVAID to retrieve.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual NavigationNavaid GetNavaid( int navaidId, CallContext context = null )
        {
            return AccessTaskResult( GetNavaidAsync( navaidId, context ) );
        }
    }
}
