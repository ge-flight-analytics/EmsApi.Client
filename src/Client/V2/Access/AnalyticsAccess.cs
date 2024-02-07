using System.Collections.Generic;
using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    /// <summary>
    /// Provides access to EMS API "analytics" routes.
    /// </summary>
    public class AnalyticsAccess : RouteAccess
    {
        /// <summary>
        /// Searches for analytics by name.
        /// </summary>
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
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<IEnumerable<AnalyticInfo>> SearchAsync( string text, string groupId = null, int? maxResults = null, Category category = Category.Full, CallContext context = null )
        {
            return CallApiTask( api => api.GetAnalytics( text, groupId, maxResults, category, context ) );
        }

        /// <summary>
        /// Searches for analytics by name.
        /// </summary>
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
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual IEnumerable<AnalyticInfo> Search( string text, string groupId = null, int? maxResults = null, Category category = Category.Full, CallContext context = null )
        {
            return SafeAccessEnumerableTask( SearchAsync( text, groupId, maxResults, category, context ) );
        }

        /// <summary>
        /// Searches for analytics by name for a specific flight.
        /// </summary>
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
        /// is used. Use 0 to return the maximum of 1000 results.
        /// </param>
        /// <param name="category">
        /// The category of analytics to search, including "Full", "Physical" or "Logical". A null value specifies
        /// the default analytic set, which represents the full set of values exposed by the backing EMS system.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<IEnumerable<AnalyticInfo>> SearchAsync( int flightId, string text, string groupId = null, int? maxResults = null, Category category = Category.Full, CallContext context = null )
        {
            return CallApiTask( api => api.GetAnalyticsWithFlight( flightId, text, groupId, maxResults, category, context ) );
        }

        /// <summary>
        /// Searches for analytics by name.
        /// </summary>
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
        /// is used. Use 0 to return the maximum of 1000 results.
        /// </param>
        /// <param name="category">
        /// The category of analytics to search, including "Full", "Physical" or "Logical". A null value specifies
        /// the default analytic set, which represents the full set of values exposed by the backing EMS system.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual IEnumerable<AnalyticInfo> Search( int flightId, string text, string groupId = null, int? maxResults = null, Category category = Category.Full, CallContext context = null )
        {
            return SafeAccessEnumerableTask( SearchAsync( flightId, text, groupId, maxResults, category, context ) );
        }

        /// <summary>
        /// Retrieves metadata information associated with an analytic such as a description or units.
        /// </summary>
        /// <param name="analyticId">
        /// The analytic ID for which data is retrieved. These identifiers are typically obtained from nodes in an analytic group tree.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<AnalyticInfo> GetInfoAsync( string analyticId, CallContext context = null )
        {
            var analyticIdObj = new AnalyticId { Id = analyticId };
            return CallApiTask( api => api.GetAnalyticInfo( analyticIdObj, context ) );
        }

        /// <summary>
        /// Retrieves metadata information associated with an analytic such as a description or units.
        /// </summary>
        /// <param name="analyticId">
        /// The analytic ID for which data is retrieved. These identifiers are typically obtained from nodes in an analytic group tree.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual AnalyticInfo GetInfo( string analyticId, CallContext context = null )
        {
            return AccessTaskResult( GetInfoAsync( analyticId, context ) );
        }

        /// <summary>
        /// Retrieves metadata information associated with an analytic such as a description or units.
        /// </summary>
        /// <param name="flightId">
        /// The integer ID of the flight record to use when retrieving the analytic information.
        /// </param>
        /// <param name="analyticId">
        /// The analytic ID for which data is retrieved. These identifiers are typically obtained from nodes in an analytic group tree.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<AnalyticInfo> GetInfoAsync( int flightId, string analyticId, CallContext context = null )
        {
            var analyticIdObj = new AnalyticId { Id = analyticId };
            return CallApiTask( api => api.GetAnalyticInfoWithFlight( flightId, analyticIdObj, context ) );
        }

        /// <summary>
        /// Retrieves metadata information associated with an analytic such as a description or units.
        /// </summary>
        /// <param name="flightId">
        /// The integer ID of the flight record to use when retrieving the analytic information.
        /// </param>
        /// <param name="analyticId">
        /// The analytic ID for which data is retrieved. These identifiers are typically obtained from nodes in an analytic group tree.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual AnalyticInfo GetInfo( int flightId, string analyticId, CallContext context = null )
        {
            return AccessTaskResult( GetInfoAsync( flightId, analyticId, context ) );
        }

        /// <summary>
        /// Retrieves the contents of an analytic group, which is a hierarchical tree structure used to organize analytics.
        /// </summary>
        /// <param name="analyticGroupId">
        /// The ID of the group whose contents to retrieve. If not specified, the contents of the root group will be returned.
        /// </param>
        /// <param name="category">
        /// The category of analytics we are interested in. "Full", "Physical" or "Logical". A null value specifies the default
        /// analytic set, which represents the full set of values exposed by the backing EMS system.
        /// </param>
        /// <param name="includeMetadata">
        /// When true, metadata will be returned along with analytic items.
        /// </param>
        /// <param name="ignoreIndex">
        /// When true, the API will not attempt to use a pre-built index to answer the request.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<AnalyticGroupContents> GetGroupAsync( string analyticGroupId = null, Category category = Category.Full, bool includeMetadata = false, bool ignoreIndex = false, CallContext context = null )
        {
            return CallApiTask( api => api.GetAnalyticGroup( analyticGroupId, category, includeMetadata, ignoreIndex, context ) );
        }

        /// <summary>
        /// Retrieves the contents of an analytic group, which is a hierarchical tree structure used to organize analytics.
        /// </summary>
        /// <param name="analyticGroupId">
        /// The ID of the group whose contents to retrieve. If not specified, the contents of the root group will be returned.
        /// </param>
        /// <param name="category">
        /// The category of analytics we are interested in. "Full", "Physical" or "Logical". A null value specifies the default
        /// analytic set, which represents the full set of values exposed by the backing EMS system.
        /// </param>
        /// <param name="includeMetadata">
        /// When true, metadata will be returned along with analytic items.
        /// </param>
        /// <param name="ignoreIndex">
        /// When true, the API will not attempt to use a pre-built index to answer the request.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual AnalyticGroupContents GetGroup( string analyticGroupId = null, Category category = Category.Full, bool includeMetadata = false, bool ignoreIndex = false, CallContext context = null )
        {
            return AccessTaskResult( GetGroupAsync( analyticGroupId, category, includeMetadata, ignoreIndex, context ) );
        }

        /// <summary>
        /// Retrieves the contents of an analytic group, which is a hierarchical tree structure used to organize analytics.
        /// </summary>
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
        /// <param name="includeMetadata">
        /// When true, metadata will be returned along with analytic items.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<AnalyticGroupContents> GetGroupAsync( int flightId, string analyticGroupId = null, Category category = Category.Full, bool includeMetadata = false, CallContext context = null )
        {
            return CallApiTask( api => api.GetAnalyticGroupWithFlight( flightId, analyticGroupId, category, includeMetadata, context ) );
        }

        /// <summary>
        /// Retrieves the contents of an analytic group, which is a hierarchical tree structure used to organize analytics.
        /// </summary>
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
        /// <param name="includeMetadata">
        /// When true, metadata will be returned along with analytic items.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual AnalyticGroupContents GetGroup( int flightId, string analyticGroupId = null, Category category = Category.Full, bool includeMetadata = false, CallContext context = null )
        {
            return AccessTaskResult( GetGroupAsync( flightId, analyticGroupId, category, includeMetadata, context ) );
        }

        /// <summary>
        /// Queries offsets and values in time-series data for a specified flight and analytic.
        /// </summary>
        /// <param name="flightId">
        /// The integer ID of the flight record for which to query data.
        /// </param>
        /// <param name="query">
        /// The information used to construct a query for which results are returned.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<QueryResult> QueryResultsAsync( int flightId, AnalyticQuery query, CallContext context = null )
        {
            return CallApiTask( api => api.GetAnalyticResults( flightId, query.Raw, context ) );
        }

        /// <summary>
        /// Queries offsets and values in time-series data for a specified flight and analytic.
        /// </summary>
        /// <param name="flightId">
        /// The integer ID of the flight record for which to query data.
        /// </param>
        /// <param name="query">
        /// The information used to construct a query for which results are returned.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual QueryResult QueryResults( int flightId, AnalyticQuery query, CallContext context = null )
        {
            return AccessTaskResult( QueryResultsAsync( flightId, query, context ) );
        }

        /// <summary>
        /// Returns the analytic metadata for a flight.
        /// </summary>
        /// <param name="flightId">
        /// The integer ID of the flight record for which to retrieve data.
        /// </param>
        /// <param name="analyticId">
        /// The analytic ID (wrapped in double quotes) for which metadata is retrieved.
        /// These identifiers are typically obtained from nodes in an analytic group tree.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<Metadata> GetMetadataAsync( int flightId, string analyticId, CallContext context = null )
        {
            return CallApiTask( api => api.GetAnalyticMetadata( flightId, new AnalyticId { Id = analyticId }, context ) );
        }

        /// <summary>
        /// Returns the analytic metadata for a flight.
        /// </summary>
        /// <param name="flightId">
        /// The integer ID of the flight record for which to retrieve data.
        /// </param>
        /// <param name="analyticId">
        /// The analytic ID (wrapped in double quotes) for which metadata is retrieved.
        /// These identifiers are typically obtained from nodes in an analytic group tree.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Metadata GetMetadata( int flightId, string analyticId, CallContext context = null )
        {
            return AccessTaskResult( GetMetadataAsync( flightId, analyticId, context ) );
        }

        /// <summary>
        /// Creates a new formula analytic.
        /// </summary>
        /// <param name="formula">
        /// Information needed to create the formula analytic.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual AnalyticInfo CreateFormula( AnalyticFormula formula, CallContext context = null )
        {
            return AccessTaskResult( CreateFormulaAsync( formula, context ) );
        }

        /// <summary>
        /// Creates a new formula analytic.
        /// </summary>
        /// <param name="formula">
        /// Information needed to create the formula analytic.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<AnalyticInfo> CreateFormulaAsync( AnalyticFormula formula, CallContext context = null )
        {
            return CallApiTask( api => api.CreateFormulaAnalytic( formula, context ) );
        }
    }
}
