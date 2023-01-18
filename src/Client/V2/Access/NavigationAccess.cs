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
        /// <param name="releaseId">
        /// The DAFIF release identifier to use for the navigation information.
        /// If not provided the current release is used.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<IEnumerable<NavigationAirport>> GetAirportsAsync( int? releaseId = null, CallContext context = null )
        {
            return CallApiTask( api => api.GetNavigationAirports( releaseId, context ) );
        }

        /// <summary>
        /// Gets the active airports for the EMS system.
        /// </summary>
        /// <param name="releaseId">
        /// The DAFIF release identifier to use for the navigation information.
        /// If not provided the current release is used.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual IEnumerable<NavigationAirport> GetAirports( int? releaseId = null, CallContext context = null )
        {
            return AccessTaskResult( GetAirportsAsync( releaseId, context ) );
        }

        /// <summary>
        /// Gets the runways for a specific airport.
        /// </summary>
        /// <param name="airportId">
        /// The airport to get the runways for.
        /// </param>
        /// <param name="releaseId">
        /// The DAFIF release identifier to use for the navigation information.
        /// If not provided the current release is used.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<IEnumerable<NavigationRunway>> GetRunwaysAsync( int airportId, int? releaseId = null, CallContext context = null )
        {
            return CallApiTask( api => api.GetNavigationRunways( airportId, releaseId, context ) );
        }

        /// <summary>
        /// Gets the runways for a specific airport.
        /// </summary>
        /// <param name="airportId">
        /// The airport to get the runways for.
        /// </param>
        /// <param name="releaseId">
        /// The DAFIF release identifier to use for the navigation information.
        /// If not provided the current release is used.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual IEnumerable<NavigationRunway> GetRunways( int airportId, int? releaseId = null, CallContext context = null )
        {
            return AccessTaskResult( GetRunwaysAsync( airportId, releaseId, context ) );
        }

        /// <summary>
        /// Gets the procedures for a specific airport.
        /// </summary>
        /// <param name="airportId">
        /// The airport to get the procedures for.
        /// </param>
        /// <param name="releaseId">
        /// The DAFIF release identifier to use for the navigation information.
        /// If not provided the current release is used.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<IEnumerable<NavigationProcedure>> GetProceduresAsync( int airportId, int? releaseId = null, CallContext context = null )
        {
            return CallApiTask( api => api.GetNavigationProcedures( airportId, releaseId, context ) );
        }

        /// <summary>
        /// Gets the procedures for a specific airport.
        /// </summary>
        /// <param name="airportId">
        /// The airport to get the procedures for.
        /// </param>
        /// <param name="releaseId">
        /// The DAFIF release identifier to use for the navigation information.
        /// If not provided the current release is used.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual IEnumerable<NavigationProcedure> GetProcedures( int airportId, int? releaseId = null, CallContext context = null )
        {
            return AccessTaskResult( GetProceduresAsync( airportId, releaseId, context ) );
        }

        /// <summary>
        /// Gets the segments for a specific procedure.
        /// </summary>
        /// <param name="procedureId">
        /// The procedure to get the segments for.
        /// </param>
        /// <param name="releaseId">
        /// The DAFIF release identifier to use for the navigation information.
        /// If not provided the current release is used.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<IEnumerable<NavigationProcedureSegment>> GetSegmentsAsync( int procedureId, int? releaseId = null, CallContext context = null )
        {
            return CallApiTask( api => api.GetNavigationSegments( procedureId, releaseId, context ) );
        }

        /// <summary>
        /// Gets the segments for a specific procedure.
        /// </summary>
        /// <param name="procedureId">
        /// The procedure to get the segments for.
        /// </param>
        /// <param name="releaseId">
        /// The DAFIF release identifier to use for the navigation information.
        /// If not provided the current release is used.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual IEnumerable<NavigationProcedureSegment> GetSegments( int procedureId, int? releaseId = null, CallContext context = null )
        {
            return AccessTaskResult( GetSegmentsAsync( procedureId, releaseId, context ) );
        }

        /// <summary>
        /// Get a waypoint.
        /// </summary>
        /// <param name="waypointId">
        /// The waypoint to retrieve.
        /// </param>
        /// <param name="releaseId">
        /// The DAFIF release identifier to use for the navigation information.
        /// If not provided the current release is used.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<NavigationWaypoint> GetWaypointAsync( int waypointId, int? releaseId = null, CallContext context = null )
        {
            return CallApiTask( api => api.GetNavigationWaypoint( waypointId, releaseId, context ) );
        }

        /// <summary>
        /// Get a waypoint.
        /// </summary>
        /// <param name="waypointId">
        /// The waypoint to retrieve.
        /// </param>
        /// <param name="releaseId">
        /// The DAFIF release identifier to use for the navigation information.
        /// If not provided the current release is used.
        /// </param>
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        public virtual NavigationWaypoint GetWaypoint( int waypointId, int? releaseId = null, CallContext context = null )
        {
            return AccessTaskResult( GetWaypointAsync( waypointId, releaseId, context ) );
        }

        /// <summary>
        /// Get a NAVAID.
        /// </summary>
        /// <param name="navaidId">
        /// The NAVAID to retrieve.
        /// </param>
        /// <param name="releaseId">
        /// The DAFIF release identifier to use for the navigation information.
        /// If not provided the current release is used.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<NavigationNavaid> GetNavaidAsync( int navaidId, int? releaseId = null, CallContext context = null )
        {
            return CallApiTask( api => api.GetNavigationNavaid( navaidId, releaseId, context ) );
        }

        /// <summary>
        /// Get a NAVAID.
        /// </summary>
        /// <param name="navaidId">
        /// The NAVAID to retrieve.
        /// </param>
        /// <param name="releaseId">
        /// The DAFIF release identifier to use for the navigation information.
        /// If not provided the current release is used.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual NavigationNavaid GetNavaid( int navaidId, int? releaseId = null, CallContext context = null )
        {
            return AccessTaskResult( GetNavaidAsync( navaidId, releaseId, context ) );
        }

        /// <summary>
        /// Get the procedures for a flight.
        /// </summary>
        /// <param name="flightId">The flight record identifier of the flight procedures to retrieve.</param>
        /// <param name="type">The type of procedures to retrieve: all, approach, departure, arrival.</param>
        /// <param name="runwayIdOverride">An optional runway identifier to override the flight's detected runway.
        /// This cannot be used in combination with the type=all or the includeAll=true.</param>
        /// <param name="includeAll">An optional parameter, when true all the procedures for the airport will be included
        /// rather than just the detected runways procedures.</param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<NavigationFlightProcedures> GetFlightProceduresAsync( int flightId, Type type, int? runwayIdOverride = null, bool? includeAll = null, CallContext context = null )
        {
            return CallApiTask( api => api.GetNavigationFlightProcedures( flightId, type, runwayIdOverride, includeAll, context ) );
        }

        /// <summary>
        /// Get the procedures for a flight.
        /// </summary>
        /// <param name="flightId">The flight record identifier of the flight procedures to retrieve.</param>
        /// <param name="type">The type of procedures to retrieve: all, approach, departure, arrival.</param>
        /// <param name="runwayIdOverride">An optional runway identifier to override the flight's detected runway.
        /// This cannot be used in combination with the type=all or the includeAll=true.</param>
        /// <param name="includeAll">An optional parameter, when true all the procedures for the airport will be included
        /// rather than just the detected runways procedures.</param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual NavigationFlightProcedures GetFlightProcedures( int flightId, Type type, int? runwayIdOverride = null, bool? includeAll = null, CallContext context = null )
        {
            return AccessTaskResult( GetFlightProceduresAsync(flightId, type, runwayIdOverride, includeAll, context ) );
        }
    }
}
