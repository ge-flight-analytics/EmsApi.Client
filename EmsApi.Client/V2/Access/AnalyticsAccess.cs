using System.Collections.Generic;
using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    public class AnalyticsAccess : CachedEmsIdRouteAccess
    {
        /// <summary>
        /// Searches for analytics by name.
        /// </summary>
        /// <param name="emsSystem">
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
        public Task<IEnumerable<AnalyticInfo>> SearchAsync( string text, string groupId = null, int? maxResults = null, Category category = Category.Full, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetAnalytics( ems, text, groupId, maxResults, category ) );
        }

        /// <summary>
        /// Searches for analytics by name.
        /// </summary>
        /// <param name="emsSystem">
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
        public IEnumerable<AnalyticInfo> Search( string text, string groupId = null, int? maxResults = null, Category category = Category.Full, int emsSystem = NoEmsServerSpecified )
        {
            return SafeAccessEnumerableTask( SearchAsync( text, groupId, maxResults, category, emsSystem ) );
        }

        /// <summary>
        /// Searches for analytics by name for a specific flight.
        /// </summary>
        /// <param name="emsSystem">
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
        public Task<IEnumerable<AnalyticInfo>> SearchAsync( int flightId, string text, string groupId = null, int? maxResults = null, Category category = Category.Full, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetAnalyticsWithFlight( ems, flightId, text, groupId, maxResults, category ) );
        }

        /// <summary>
        /// Searches for analytics by name.
        /// </summary>
        /// <param name="emsSystem">
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
        public IEnumerable<AnalyticInfo> Search( int flightId, string text, string groupId = null, int? maxResults = null, Category category = Category.Full, int emsSystem = NoEmsServerSpecified )
        {
            return SafeAccessEnumerableTask( SearchAsync( flightId, text, groupId, maxResults, category, emsSystem ) );
        }

        /// <summary>
        /// Retrieves metadata information associated with an analytic such as a description or units.
        /// </summary>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="analyticId">
        /// The analytic ID for which data is retrieved. These identifiers are typically obtained from nodes in an analytic group tree.
        /// </param>
        public Task<AnalyticInfo> GetInfoAsync( string analyticId, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            var analyticIdObj = new AnalyticId { Id = analyticId };
            return CallApiTask( api => api.GetAnalyticInfo( ems, analyticIdObj ) );
        }

        /// <summary>
        /// Retrieves metadata information associated with an analytic such as a description or units.
        /// </summary>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="analyticId">
        /// The analytic ID for which data is retrieved. These identifiers are typically obtained from nodes in an analytic group tree.
        /// </param>
        public AnalyticInfo GetInfo( string analyticId, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetInfoAsync( analyticId, emsSystem ) );
        }

        /// <summary>
        /// Retrieves metadata information associated with an analytic such as a description or units.
        /// </summary>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="flightId">
        /// The integer ID of the flight record to use when retrieving the analytic information.
        /// </param>
        /// <param name="analyticId">
        /// The analytic ID for which data is retrieved. These identifiers are typically obtained from nodes in an analytic group tree.
        /// </param>
        public Task<AnalyticInfo> GetInfoAsync( int flightId, string analyticId, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            var analyticIdObj = new AnalyticId { Id = analyticId };
            return CallApiTask( api => api.GetAnalyticInfoWithFlight( ems, flightId, analyticIdObj ) );
        }

        /// <summary>
        /// Retrieves metadata information associated with an analytic such as a description or units.
        /// </summary>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="flightId">
        /// The integer ID of the flight record to use when retrieving the analytic information.
        /// </param>
        /// <param name="analyticId">
        /// The analytic ID for which data is retrieved. These identifiers are typically obtained from nodes in an analytic group tree.
        /// </param>
        public AnalyticInfo GetInfo( int flightId, string analyticId, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetInfoAsync( flightId, analyticId, emsSystem ) );
        }

        /// <summary>
        /// Retrieves the contents of an analytic group, which is a hierarchical tree structure used to organize analytics.
        /// </summary>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="analyticGroupId">
        /// The ID of the group whose contents to retrieve. If not specified, the contents of the root group will be returned.
        /// </param>
        /// <param name="category">
        /// The category of analytics we are interested in. "Full", "Physical" or "Logical". A null value specifies the default
        /// analytic set, which represents the full set of values exposed by the backing EMS system.
        /// </param>
        public Task<AnalyticGroupContents> GetGroupAsync( string analyticGroupId = null, Category category = Category.Full, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetAnalyticGroup( ems, analyticGroupId, category ) );
        }

        /// <summary>
        /// Retrieves the contents of an analytic group, which is a hierarchical tree structure used to organize analytics.
        /// </summary>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="analyticGroupId">
        /// The ID of the group whose contents to retrieve. If not specified, the contents of the root group will be returned.
        /// </param>
        /// <param name="category">
        /// The category of analytics we are interested in. "Full", "Physical" or "Logical". A null value specifies the default
        /// analytic set, which represents the full set of values exposed by the backing EMS system.
        /// </param>
        public AnalyticGroupContents GetGroup( string analyticGroupId = null, Category category = Category.Full, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetGroupAsync( analyticGroupId, category, emsSystem ) );
        }

        /// <summary>
        /// Retrieves the contents of an analytic group, which is a hierarchical tree structure used to organize analytics.
        /// </summary>
        /// <param name="emsSystem">
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
        public Task<AnalyticGroupContents> GetGroupAsync( int flightId, string analyticGroupId = null, Category category = Category.Full, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetAnalyticGroupWithFlight( ems, flightId, analyticGroupId, category ) );
        }

        /// <summary>
        /// Retrieves the contents of an analytic group, which is a hierarchical tree structure used to organize analytics.
        /// </summary>
        /// <param name="emsSystem">
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
        public AnalyticGroupContents GetGroup( int flightId, string analyticGroupId = null, Category category = Category.Full, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetGroupAsync( flightId, analyticGroupId, category, emsSystem ) );
        }

        /// <summary>
        /// Queries offsets and values in time-series data for a specified flight and analytic.
        /// </summary>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="flightId">
        /// The integer ID of the flight record for which to query data.
        /// </param>
        /// <param name="query">
        /// The information used to construct a query for which results are returned.
        /// </param>
        public Task<QueryResult> QueryResultsAsync( int flightId, AnalyticQuery query, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetAnalyticResults( ems, flightId, query.Raw ) );
        }

        /// <summary>
        /// Queries offsets and values in time-series data for a specified flight and analytic.
        /// </summary>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="flightId">
        /// The integer ID of the flight record for which to query data.
        /// </param>
        /// <param name="query">
        /// The information used to construct a query for which results are returned.
        /// </param>
        public QueryResult QueryResults( int flightId, AnalyticQuery query, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( QueryResultsAsync( flightId, query, emsSystem ) );
        }

        /// <summary>
        /// Returns the analytic metadata for a flight.
        /// </summary>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="flightId">
        /// The integer ID of the flight record for which to retrieve data.
        /// </param>
        /// <param name="analyticId">
        /// The analytic ID (wrapped in double quotes) for which metadata is retrieved.
        /// These identifiers are typically obtained from nodes in an analytic group tree.
        /// </param>
        public Task<Metadata> GetMetadataAsync( int flightId, string analyticId, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetAnalyticMetadata( ems, flightId, analyticId ) );
        }

        /// <summary>
        /// Returns the analytic metadata for a flight.
        /// </summary>
        /// <param name="emsSystem">
        /// The unique identifier of the EMS system.
        /// </param>
        /// <param name="flightId">
        /// The integer ID of the flight record for which to retrieve data.
        /// </param>
        /// <param name="analyticId">
        /// The analytic ID (wrapped in double quotes) for which metadata is retrieved.
        /// These identifiers are typically obtained from nodes in an analytic group tree.
        /// </param>
        public Metadata GetMetadata( int flightId, string analyticId, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetMetadataAsync( flightId, analyticId, emsSystem ) );
        }
    }
}
