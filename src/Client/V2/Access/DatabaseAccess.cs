using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using EmsApi.Dto.V2;
using Newtonsoft.Json.Linq;
using RowCallback = System.Action<EmsApi.Dto.V2.DatabaseQueryResult.Row>;
namespace EmsApi.Client.V2.Access
{
    /// <summary>
    /// Provides access to EMS API "databases" routes.
    /// </summary>
    public class DatabaseAccess : RouteAccess
    {
        /// <summary>
        /// The default page size when performing async database queries.
        /// </summary>
        public const int DefaultRowsPerPage = 20000;

        /// <summary>
        /// Returns a database group with a matching ID containing only its immediate children 
        /// in a hierarchical tree used to organize databases.
        /// </summary>
        /// <param name="groupId">
        /// The unique identifier of the EMS database group whose contents to return. If not specified, 
        /// the contents of the root group are returned.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<DatabaseGroup> GetDatabaseGroupAsync( string groupId = null, CallContext context = null )
        {
            return CallApiTask( api => api.GetDatabaseGroup( groupId, context ) );
        }

        /// <summary>
        /// Returns a database group with a matching ID containing only its immediate children 
        /// in a hierarchical tree used to organize databases.
        /// </summary>
        /// <param name="groupId">
        /// The unique identifier of the EMS database group whose contents to return. If not specified, 
        /// the contents of the root group are returned.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual DatabaseGroup GetDatabaseGroup( string groupId = null, CallContext context = null )
        {
            return AccessTaskResult( GetDatabaseGroupAsync( groupId, context ) );
        }

        /// <summary>
        /// Returns a field group with a matching ID containing only its immediate children in a 
        /// hierarchical tree used to organize fields.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database containing a field group to return.
        /// </param>
        /// <param name="groupId">
        /// The unique identifier of a field group whose contents to return. If not specified, 
        /// the contents of the root group are returned.
        /// </param>
        /// <param name="ignoreIndex">
        /// If specified as True, the API will not attempt to use a pre-built index to answer the request.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<FieldGroup> GetFieldGroupAsync( string databaseId, string groupId = null,
            bool? ignoreIndex = null, CallContext context = null )
        {
            return CallApiTask( api => api.GetDatabaseFieldGroup( databaseId, groupId, ignoreIndex, context ) );
        }

        /// <summary>
        /// Returns a field group with a matching ID containing only its immediate children in a 
        /// hierarchical tree used to organize fields.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database containing a field group to return.
        /// </param>
        /// <param name="groupId">
        /// The unique identifier of a field group whose contents to return. If not specified, 
        /// the contents of the root group are returned.
        /// </param>
        /// <param name="ignoreIndex">
        /// If specified as True, the API will not attempt to use a pre-built index to answer the request.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual FieldGroup GetFieldGroup( string databaseId, string groupId = null, 
            bool? ignoreIndex = null, CallContext context = null )
        {
            return AccessTaskResult( GetFieldGroupAsync( databaseId, groupId, ignoreIndex, context ) );
        }

        /// <summary>
        /// Returns information about a database field matching the specified ID.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database containing a field to return.
        /// </param>
        /// <param name="fieldId">
        /// The unique identifier of the field whose information to return.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<Field> GetFieldAsync( string databaseId, string fieldId, CallContext context = null )
        {
            return CallApiTask( api => api.GetDatabaseFieldDefinition( databaseId, fieldId, context ) );
        }

        /// <summary>
        /// Returns information about a database field matching the specified ID.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database containing a field to return.
        /// </param>
        /// <param name="fieldId">
        /// The unique identifier of the field whose information to return.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Field GetField( string databaseId, string fieldId, CallContext context = null )
        {
            return AccessTaskResult( GetFieldAsync( databaseId, fieldId, context ) );
        }

        /// <summary>
        /// Returns information about multiple database fields matching the input IDs.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database containing a fields to return.
        /// </param>
        /// <param name="query">
        /// Information about which fields to get definitions for.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<FieldInfo> GetFieldsAsync( string databaseId, string[] fieldIds, CallContext context = null )
        {
            var query = new FieldInfoQuery { FieldIds = fieldIds };
            return CallApiTask( api => api.GetDatabaseFieldDefinitions( databaseId, query, context ) );
        }

        /// <summary>
        /// Returns information about multiple database fields matching the input IDs.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database containing a fields to return.
        /// </param>
        /// <param name="query">
        /// Information about which fields to get definitions for.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual FieldInfo GetFields( string databaseId, string[] fieldIds, CallContext context = null )
        {
            return AccessTaskResult( GetFieldsAsync( databaseId, fieldIds, context ) );
        }

        /// <summary>
        /// Returns all the fields matching the specified search options.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the database containing fields to return.
        /// </param>
        /// <param name="search">
        /// An optional field name search string used to match fields to return.
        /// </param>
        /// <param name="fieldGroupId">
        /// The optional parent field group ID whose contents to search.
        /// </param>
        /// <param name="includeProfiles">
        /// An optional setting to indicate whether to search fields in profiles. By default, 
        /// this is false since including profile fields will significantly increase search time.
        /// </param>
        /// <param name="maxResults">
        /// The maximum number of fields to return. This defaults to 200 fields. If this is set to 
        /// 0 all the results will be returned.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<IEnumerable<Field>> SearchFieldsAsync( string databaseId, string search = null, string fieldGroupId = null,
            bool includeProfiles = false, int maxResults = 200, CallContext context = null )
        {
            return CallApiTask( api => api.SearchDatabaseFields( databaseId, search, fieldGroupId, includeProfiles, maxResults, context ) );
        }

        /// <summary>
        /// Returns all the fields matching the specified search options.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the database containing fields to return.
        /// </param>
        /// <param name="search">
        /// An optional field name search string used to match fields to return.
        /// </param>
        /// <param name="fieldGroupId">
        /// The optional parent field group ID whose contents to search.
        /// </param>
        /// <param name="includeProfiles">
        /// An optional setting to indicate whether to search fields in profiles. By default, 
        /// this is false since including profile fields will significantly increase search time.
        /// </param>
        /// <param name="maxResults">
        /// The maximum number of fields to return. This defaults to 200 fields. If this is set to 
        /// 0 all the results will be returned.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual IEnumerable<Field> SearchFields( string databaseId, string search = null, string fieldGroupId = null,
            bool includeProfiles = false, int maxResults = 200, CallContext context = null )
        {
            return SafeAccessEnumerableTask( SearchFieldsAsync(
                databaseId, search, fieldGroupId, includeProfiles, maxResults, context ) );
        }

        /// <summary>
        /// Queries a database for information, composing the query with information provided in the 
        /// specified query structure. This query uses a single request and as such cannot be used
        /// with large data sets.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to query.
        /// </param>
        /// <param name="query">
        /// The information used to construct a query for which results are returned.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<DatabaseQueryResult> SimpleQueryAsync( string databaseId, DatabaseQuery query, CallContext context = null )
        {
            return CallApiTask( api => api.QueryDatabase( databaseId, query.Raw, context ) ).ContinueWith( task =>
            {
                // Convert to our result format.
                DbQueryResult queryResult = task.Result;
                var result = new DatabaseQueryResult( queryResult.Header );
                result.AddRows( query, queryResult.Rows );
                return result;
            } );
        }

        /// <summary>
        /// Queries a database for information, composing the query with information provided in the 
        /// specified query structure. This query uses a single request and as such cannot be used
        /// with large data sets.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to query.
        /// </param>
        /// <param name="query">
        /// The information used to construct a query for which results are returned.
        /// </param>
        /// <param name="callback">
        /// A callback to execute for each row of data received.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        [System.Obsolete( "Database queries using RowCallback will be removed in future versions." )]
        public virtual Task SimpleQueryAsync( string databaseId, DatabaseQuery query, RowCallback callback, CallContext context = null )
        {
            return CallApiTask( api => api.QueryDatabase( databaseId, query.Raw, context ) ).ContinueWith( task =>
            {
                // Callback for each row.
                DbQueryResult queryResult = task.Result;
                var result = new DatabaseQueryResult( queryResult.Header );
                result.CallbackRows( query, queryResult.Rows, callback );
            } );
        }

        /// <summary>
        /// Queries a database for information, composing the query with information provided in the 
        /// specified query structure. This query uses a single request and as such cannot be used
        /// with large data sets.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to query.
        /// </param>
        /// <param name="query">
        /// The information used to construct a query for which results are returned.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual DatabaseQueryResult SimpleQuery( string databaseId, DatabaseQuery query, CallContext context = null )
        {
            return AccessTaskResult( SimpleQueryAsync( databaseId, query, context ) );
        }

        /// <summary>
        /// Queries a database for information, composing the query with information provided in the 
        /// specified query structure. This query uses a single request and as such cannot be used
        /// with large data sets.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to query.
        /// </param>
        /// <param name="query">
        /// The information used to construct a query for which results are returned.
        /// </param>
        /// <param name="callback">
        /// A callback to execute for each row of data received.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        [System.Obsolete( "Database queries using RowCallback will be removed in future versions." )]
        public virtual void SimpleQuery( string databaseId, DatabaseQuery query, RowCallback callback, CallContext context = null )
        {
            SimpleQueryAsync( databaseId, query, callback, context ).Wait();
        }

        /// <summary>
        /// Queries the database for information, composing the query with information provided in the
        /// specified query structure. This method is a convenience wrapper around the async-query routes
        /// to handle pagination, and it also converts the results into a more accessible structure.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to query.
        /// </param>
        /// <param name="query">
        /// The information used to construct a query for which results are returned.
        /// </param>
        /// <param name="rowsPerCall">
        /// The number of rows to read for each API call.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual async Task<DatabaseQueryResult> QueryAsync( string databaseId, DatabaseQuery query, int rowsPerCall = DefaultRowsPerPage, CallContext context = null )
        {
            AsyncQueryInfo info = null;
            try
            {
                info = await StartQueryAsync( databaseId, query, context );
                var result = new DatabaseQueryResult( info.Header );

                int startingRow = 0;
                bool moreToRead = true;
                while( moreToRead )
                {
                    AsyncQueryData data = await ReadQueryAsync( databaseId, info.Id, startingRow, startingRow + rowsPerCall - 1, context );
                    result.AddRows( query, data.Rows );
                    startingRow += rowsPerCall;
                    moreToRead = data.HasMoreRows;
                }

                return result;
            }
            finally
            {
                if( info != null )
                    await StopQueryAsync( databaseId, info.Id, context );
            }
        }

        /// <summary>
        /// Queries the database for information, composing the query with information provided in the
        /// specified query structure. This method is a convenience wrapper around the async-query routes
        /// to handle pagination, and it also executes a callback function for each row of data.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to query.
        /// </param>
        /// <param name="query">
        /// The information used to construct a query for which results are returned.
        /// </param>
        /// <param name="callback">
        /// A callback to execute for each row of data received.
        /// </param>
        /// <param name="rowsPerCall">
        /// The number of rows to read for each API call.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        [System.Obsolete( "Database queries using RowCallback will be removed in future versions." )]
        public virtual async Task QueryAsync( string databaseId, DatabaseQuery query, RowCallback callback, int rowsPerCall = DefaultRowsPerPage, CallContext context = null )
        {
            AsyncQueryInfo info = null;
            try
            {
                info = await StartQueryAsync( databaseId, query, context );
                var result = new DatabaseQueryResult( info.Header );

                int startingRow = 0;
                bool moreToRead = true;
                while( moreToRead )
                {
                    AsyncQueryData data = await ReadQueryAsync( databaseId, info.Id, startingRow, startingRow + rowsPerCall - 1, context );
                    result.CallbackRows( query, data.Rows, callback );
                    startingRow += rowsPerCall;
                    moreToRead = data.HasMoreRows;
                }
            }
            finally
            {
                if( info != null )
                    await StopQueryAsync( databaseId, info.Id, context );
            }
        }

        /// <summary>
        /// Queries the database for information, composing the query with information provided in the
        /// specified query structure. This method is a convenience wrapper around the async-query routes
        /// to handle pagination, and it also converts the results into a more accessible structure.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to query.
        /// </param>
        /// <param name="query">
        /// The information used to construct a query for which results are returned.
        /// </param>
        /// <param name="rowsPerCall">
        /// The number of rows to read for each API call.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual DatabaseQueryResult Query( string databaseId, DatabaseQuery query, int rowsPerCall = DefaultRowsPerPage, CallContext context = null )
        {
            return AccessTaskResult( QueryAsync( databaseId, query, rowsPerCall, context ) );
        }

        /// <summary>
        /// Queries the database for information, composing the query with information provided in the
        /// specified query structure. This method is a convenience wrapper around the async-query routes
        /// to handle pagination, and it also executes a callback function for each row of data.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to query.
        /// </param>
        /// <param name="query">
        /// The information used to construct a query for which results are returned.
        /// </param>
        /// <param name="callback">
        /// A callback to execute for each row of data received.
        /// </param>
        /// <param name="rowsPerCall">
        /// The number of rows to read for each API call.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        [System.Obsolete( "Database queries using RowCallback will be removed in future versions." )]
        public virtual void Query( string databaseId, DatabaseQuery query, RowCallback callback, int rowsPerCall = DefaultRowsPerPage, CallContext context = null )
        {
            QueryAsync( databaseId, query, callback, rowsPerCall, context ).Wait();
        }

        /// <summary>
        /// Queries the database for information, composing the query with information provided in the
        /// specified query structure. This method returns an async enumerable where each item is a
        /// read only dictionary that represents a single row. The keys in the returned dictionary
        /// are the field ids, and the values are the values for each field represented by the field id.
        /// This will throw an <see cref="System.ArgumentException"/> if a field is specified more than
        /// once in the query due to attempting to add duplicate dictionary keys and QueryValuesAsync
        /// should be used instead. This is a convenience wrapper that calls both StartQueryAsync
        /// and StopQueryAsync after completing the read, but if you need access to the <see cref="AsyncQueryInfo"/>
        /// those methods need to be called manually with <see cref="ReadQueryDictionaryAsync(string, AsyncQueryInfo, int, CallContext)"/>
        /// to read the data.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to query.
        /// </param>
        /// <param name="query">
        /// The information used to construct a query for which results are returned.
        /// </param>
        /// <param name="rowsPerCall">
        /// The number of rows to read for each API call. Setting this to a lower value will return
        /// async enumerable values more quickly but may be slower overall due to requiring more API
        /// requests due to smaller pages.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual async IAsyncEnumerable<IReadOnlyDictionary<string, object>> QueryDictionaryAsync( string databaseId, DatabaseQuery query, int rowsPerCall = DefaultRowsPerPage, CallContext context = null )
        {
            AsyncQueryInfo info = null;
            try
            {
                info = await StartQueryAsync( databaseId, query, context );
                string[] orderedIds = GetOrderedFieldIds( info );

                int startingRow = 0;
                bool moreToRead = true;
                while( moreToRead )
                {
                    AsyncQueryData data = await ReadQueryAsync( databaseId, info.Id, startingRow, startingRow + rowsPerCall - 1, context );
                    foreach( ICollection<object> row in data.Rows )
                    {
                        int i = 0;
                        var result = new Dictionary<string, object>();
                        foreach( object value in row )
                        {
                            result.Add( orderedIds[i], ScrubDatabaseValue( value ) );
                            i++;
                        }

                        yield return result;
                    }
                    startingRow += rowsPerCall;
                    moreToRead = data.HasMoreRows;
                }
            }
            finally
            {
                if( info != null )
                    await StopQueryAsync( databaseId, info.Id, context );
            }
        }

        /// <summary>
        /// Reads all of the values for an async database query and returns them as an async enumerable of dictionaries.
        /// The keys in the returned dictionary are the field ids, and the values are the values for each field represented
        /// by the field id. This will throw an <see cref="System.ArgumentException"/> if a field is specified more than
        /// once in the query due to attempting to add duplicate dictionary keys and QueryValuesAsync should be used instead.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database being queried.
        /// </param>
        /// <param name="info">
        /// The asynchronous query info, as retrieved from <see cref="StartQueryAsync(string, DatabaseQuery, CallContext)"/>.
        /// </param>
        /// <param name="rowsPerCall">
        /// The number of rows to read for each API call.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual async IAsyncEnumerable<IReadOnlyDictionary<string, object>> ReadQueryDictionaryAsync( string databaseId, AsyncQueryInfo info, int rowsPerCall = DefaultRowsPerPage, CallContext context = null )
        {
            string[] orderedIds = GetOrderedFieldIds( info );

            int startingRow = 0;
            bool moreToRead = true;
            while( moreToRead )
            {
                AsyncQueryData data = await ReadQueryAsync( databaseId, info.Id, startingRow, startingRow + rowsPerCall - 1, context );
                foreach( ICollection<object> row in data.Rows )
                {
                    int i = 0;
                    var result = new Dictionary<string, object>();
                    foreach( object value in row )
                    {
                        result.Add( orderedIds[i], ScrubDatabaseValue( value ) );
                        i++;
                    }

                    yield return result;
                }
                startingRow += rowsPerCall;
                moreToRead = data.HasMoreRows;
            }
        }

        /// <summary>
        /// Returns the field ids from the query info in order.
        /// </summary>
        private string[] GetOrderedFieldIds( AsyncQueryInfo info )
        {
            string[] orderedIds = new string[info.Header.Count];
            for( int i = 0; i < orderedIds.Length; ++i )
                orderedIds[i] = info.Header[i].Id;

            return orderedIds;
        }

        /// <summary>
        /// Queries the database for information, composing the query with information provided in the
        /// specified query structure. This method returns an async enumerable where each item is a
        /// collection of the values for a single row. The values will be in the same order that the
        /// fields are specified in the query. This is a convenience wrapper that calls both StartQueryAsync
        /// and StopQueryAsync after completing the read, but if you need access to the <see cref="AsyncQueryInfo"/>
        /// those methods need to be called manually instead of using this one. This method does not trim the raw string
        /// values like QueryAsync and QueryDictionaryAsync do.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to query.
        /// </param>
        /// <param name="query">
        /// The information used to construct a query for which results are returned.
        /// </param>
        /// <param name="rowsPerCall">
        /// The number of rows to read for each API call.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual async IAsyncEnumerable<IList<object>> QueryValuesAsync( string databaseId, DatabaseQuery query, int rowsPerCall = DefaultRowsPerPage, CallContext context = null )
        {
            AsyncQueryInfo info = null;
            try
            {
                info = await StartQueryAsync( databaseId, query, context );

                // Note: This is intentionally duplicated with the body of ReadQueryValuesAsync below because we cannot directly return
                // the result of that method here. We need to use the async keyword (for start / stop) and that causes the method to
                // require a yield.
                int startingRow = 0;
                bool moreToRead = true;
                while( moreToRead )
                {
                    AsyncQueryData data = await ReadQueryAsync( databaseId, info.Id, startingRow, startingRow + rowsPerCall - 1, context );
                    foreach( IList<object> row in data.Rows )
                        yield return row;

                    startingRow += rowsPerCall;
                    moreToRead = data.HasMoreRows;
                }
            }
            finally
            {
                if( info != null )
                    await StopQueryAsync( databaseId, info.Id, context );
            }
        }

        /// <summary>
        /// Reads all of the raw values for an async database query and returns them as an async enumerable, where
        /// each item represents one row.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database being queried.
        /// </param>
        /// <param name="info">
        /// The asynchronous query info, as retrieved from <see cref="StartQueryAsync(string, DatabaseQuery, CallContext)"/>.
        /// </param>
        /// <param name="rowsPerCall">
        /// The number of rows to read for each API call.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual async IAsyncEnumerable<IList<object>> ReadQueryValuesAsync( string databaseId, AsyncQueryInfo info, int rowsPerCall = DefaultRowsPerPage, CallContext context = null )
        {
            int startingRow = 0;
            bool moreToRead = true;
            while( moreToRead )
            {
                AsyncQueryData data = await ReadQueryAsync( databaseId, info.Id, startingRow, startingRow + rowsPerCall - 1, context );
                foreach( IList<object> row in data.Rows )
                    yield return row;

                startingRow += rowsPerCall;
                moreToRead = data.HasMoreRows;
            }
        }

        /// <summary>
        /// Creates a query on a database using the provided query structure and returns an ID that 
        /// can be used to fetch result data through other async-query routes.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to query.
        /// </param>
        /// <param name="query">
        /// The information used to construct a query for which results are returned.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<AsyncQueryInfo> StartQueryAsync( string databaseId, DatabaseQuery query, CallContext context = null )
        {
            return CallApiTask( api => api.StartAsyncDatabaseQuery( databaseId, query.Raw, context ) );
        }

        /// <summary>
        /// Creates a query on a database using the provided query structure and returns an ID that 
        /// can be used to fetch result data through other async-query routes.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to query.
        /// </param>
        /// <param name="query">
        /// The information used to construct a query for which results are returned.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual AsyncQueryInfo StartQuery( string databaseId, DatabaseQuery query, CallContext context = null )
        {
            return AccessTaskResult( StartQueryAsync( databaseId, query, context ) );
        }

        /// <summary>
        /// Returns rows between (inclusive) the start and end indexes from the async query with the given ID.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to query.
        /// </param>
        /// <param name="queryId">
        /// The unique identifier of the query created by the API.
        /// </param>
        /// <param name="start">
        /// The zero-based index of the first row to return.
        /// </param>
        /// <param name="end">
        /// The zero-based index of the last row to return.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<AsyncQueryData> ReadQueryAsync( string databaseId, string queryId, int start, int end, CallContext context = null )
        {
            return CallApiTask( api => api.ReadAsyncDatabaseQuery( databaseId, queryId, start, end, context ) );
        }

        /// <summary>
        /// Returns rows between (inclusive) the start and end indexes from the async query with the given ID. If the query has not been processed
        /// by the server then the API request will be subsequently retried until data is available.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to query.
        /// </param>
        /// <param name="queryId">
        /// The unique identifier of the query created by the API.
        /// </param>
        /// <param name="start">
        /// The zero-based index of the first row to return.
        /// </param>
        /// <param name="end">
        /// The zero-based index of the last row to return.
        /// </param>
        /// <param name="initialDelay">
        /// The amount to delay after the first request if the query has not been processed yet. This has no effect if waitIfNotReady is set to true.
        /// </param>
        /// <param name="backoffFactor">
        /// This number used to multiply the time between API requests relative to the initialDelay value. For example if this is set to 1.0 then the
        /// initial delay will be elapsed between each subsequent API request. If this is set to 2.0 then the initial delay will be doubled for the second
        /// delay, and that will again be doubled for the next request where the data is not yet ready. This has no effect if waitIfNotReady is set to true.
        /// </param>
        /// <param name="cancel">
        /// A cancellation token to use to cancel the wait operation. This can also be used to implement a max timeout by using a <seealso cref="CancellationTokenSource"/>
        /// with a delay timeout set.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual async Task<AsyncQueryData> ReadQueryWhenReadyAsync( string databaseId, string queryId, int start, int end,
            TimeSpan initialDelay, float backoffFactor = 1.25f, System.Threading.CancellationToken cancel = default, CallContext context = null )
        {
            TimeSpan delay = initialDelay;
            for( int i = 0; ; ++i )
            {
                (bool dataIsReady, AsyncQueryData data) = await TryReadQueryAsync( databaseId, queryId, start, end, context );
                if( dataIsReady )
                    return data;

                await Task.Delay( delay, cancel );
                delay = TimeSpan.FromSeconds( delay.TotalSeconds * backoffFactor );
            }
        }

        /// <summary>
        /// Returns rows between (inclusive) the start and end indexes from the async query with the given ID. If the query has not been processed
        /// by the server then the dataIsReady tuple value will be false, and the data tuple value will be null. 
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to query.
        /// </param>
        /// <param name="queryId">
        /// The unique identifier of the query created by the API.
        /// </param>
        /// <param name="start">
        /// The zero-based index of the first row to return.
        /// </param>
        /// <param name="end">
        /// The zero-based index of the last row to return.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual async Task<(bool dataIsReady, AsyncQueryData data)> TryReadQueryAsync( string databaseId, string queryId, int start, int end, CallContext context = null )
        {
            HttpResponseMessage response = await CallApiTask( api => api.ReadAsyncDatabaseQuery( databaseId, queryId, start, end, waitIfNotReady: false, context: context ) );
            if( response.StatusCode == System.Net.HttpStatusCode.Accepted )
                return (false, null);

            if( !response.IsSuccessStatusCode )
            {
                // This should be our standard error dto from the API.
                string message = $"The async query returned non-success status code {response.StatusCode}";
                string rawResult = await response.Content?.ReadAsStringAsync();
                if( rawResult != null )
                {
                    var result = JObject.Parse( rawResult );
                    string description = result.GetValue( "error_description" )?.ToString();
                    if( description != null )
                        message = $"{message}: {description}";
                }

                await CallApiTask( _ => throw new EmsApiException( message ) );
                return (true, new AsyncQueryData
                {
                    HasMoreRows = false
                });
            }

            var data = AsyncQueryData.FromJson( await response.Content.ReadAsStringAsync() );
            return (true, data);
        }

        /// <summary>
        /// Returns rows between (inclusive) the start and end indexes from the async query with the given ID.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to query.
        /// </param>
        /// <param name="queryId">
        /// The unique identifier of the query created by the API.
        /// </param>
        /// <param name="start">
        /// The zero-based index of the first row to return.
        /// </param>
        /// <param name="end">
        /// The zero-based index of the last row to return.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual AsyncQueryData ReadQuery( string databaseId, string queryId, int start, int end, CallContext context = null )
        {
            return AccessTaskResult( ReadQueryAsync( databaseId, queryId, start, end, context ) );
        }

        /// <summary>
        /// Stops the async query with the given ID.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to query.
        /// </param>
        /// <param name="queryId">
        /// The unique identifier of the query created by the API.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task StopQueryAsync( string databaseId, string queryId, CallContext context = null )
        {
            return CallApiTask( api => api.StopAsyncDatabaseQuery( databaseId, queryId, context ) );
        }

        /// <summary>
        /// Stops the async query with the given ID.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to query.
        /// </param>
        /// <param name="queryId">
        /// The unique identifier of the query created by the API.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual void StopQuery( string databaseId, string queryId, CallContext context = null )
        {
            StopQueryAsync( databaseId, queryId, context ).Wait();
        }

        /// <summary>
        /// Applies one of a set of transformational changes to an EMS database query and returns the modified query result.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to query.
        /// </param>
        /// <param name="query">
        /// The query to be transformed.
        /// </param>
        /// <param name="type">
        /// The type of transformation to apply.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<DatabaseQuery> TransformQueryAsync( string databaseId, DatabaseQuery query, QueryTransformType type, CallContext context = null )
        {
            var request = new QueryTransform
            {
                InputQuery = query.Raw,
                Type = type
            };

            return CallApiTask( api => api.TransformDatabaseQuery( databaseId, request, context ) ).ContinueWith( task =>
            {
                // Convert to our result format.
                var transformedQuery = new DatabaseQuery
                {
                    Raw = task.Result
                };
                return transformedQuery;
            } );
        }

        /// <summary>
        /// Rolls back a specific tracked query that occurred in the past.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database that the tracked query uses.
        /// </param>
        /// <param name="rollback">
        /// The information used to rollback the query.
        /// </param>
        public virtual Task RollbackTrackedQueryAsync( string databaseId, TrackedQueryRollback rollback, CallContext context = null )
        {
            return CallApiTask( api => api.RollbackTrackedQuery( databaseId, rollback, context ) );
        }

        /// <summary>
        /// Rolls back a specific tracked query that occurred in the past.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database that the tracked query uses.
        /// </param>
        /// <param name="rollback">
        /// The information used to rollback the query.
        /// </param>
        public virtual void RollbackTrackedQuery( string databaseId, TrackedQueryRollback rollback, CallContext context = null )
        {
            RollbackTrackedQueryAsync( databaseId, rollback, context ).Wait();
        }

        /// <summary>
        /// Deletes a tracked query by name.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database that the tracked query uses.
        /// </param>
        /// <param name="queryName">
        /// The unique name of the tracked query.
        /// </param>
        public virtual Task DeleteTrackedQueryAsync( string databaseId, string queryName, CallContext context = null )
        {
            return CallApiTask( api => api.DeleteTrackedQuery( databaseId, queryName, context ) );
        }

        /// <summary>
        /// Deletes a tracked query by name.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database that the tracked query uses.
        /// </param>
        /// <param name="queryName">
        /// The unique name of the tracked query.
        /// </param>
        public virtual void DeleteTrackedQuery( string databaseId, string queryName, CallContext context = null )
        {
            DeleteTrackedQueryAsync( databaseId, queryName, context ).Wait();
        }

        /// <summary>
        /// Creates one or more new data entities in the database.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to add data entities to.
        /// </param>
        /// <param name="createQuery">
        /// The information used to create one or more new data entities.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<CreateResult> CreateEntityAsync( string databaseId, Create createQuery, CallContext context = null )
        {
            return CallApiTask( api => api.CreateDatabaseEntity( databaseId, createQuery, context ) );
        }

        /// <summary>
        /// Creates one or more new data entities in the database.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to add data entities to.
        /// </param>
        /// <param name="createQuery">
        /// The information used to create one or more new data entities.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual CreateResult CreateEntity( string databaseId, Create createQuery, CallContext context = null )
        {
            return AccessTaskResult( CreateEntityAsync( databaseId, createQuery, context ) );
        }

        /// <summary>
        /// Runs an update query on one or more rows of data in the database.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to update.
        /// </param>
        /// <param name="updateQuery">
        /// The information used to construct an update query.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<UpdateResult> UpdateAsync( string databaseId, Update updateQuery, CallContext context = null )
        {
            return CallApiTask( api => api.UpdateDatabase( databaseId, updateQuery, context ) );
        }

        /// <summary>
        /// Runs an update query on one or more rows of data in the database.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to update.
        /// </param>
        /// <param name="updateQuery">
        /// The information used to construct an update query.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual UpdateResult Update( string databaseId, Update updateQuery, CallContext context = null )
        {
            return AccessTaskResult( UpdateAsync( databaseId, updateQuery, context ) );
        }

        /// <summary>
        /// Deletes one or more existing data entities in the database.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to delete data entities from.
        /// </param>
        /// <param name="deleteQuery">
        /// The information used to delete one or more data entities.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<DeleteResult> DeleteEntityAsync( string databaseId, Delete deleteQuery, CallContext context = null )
        {
            return CallApiTask( api => api.DeleteDatabaseEntity( databaseId, deleteQuery, context ) );
        }

        /// <summary>
        /// Deletes one or more existing data entities in the database.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database to delete data entities from.
        /// </param>
        /// <param name="deleteQuery">
        /// The information used to delete one or more data entities.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual DeleteResult DeleteEntity( string databaseId, Delete deleteQuery, CallContext context = null )
        {
            return AccessTaskResult( DeleteEntityAsync( databaseId, deleteQuery, context ) );
        }

        /// <summary>
        /// Adds a new comment to a comment field on an specific entity.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database that the field exists on.
        /// </param>
        /// <param name="commentFieldId">
        /// The unique identifier of the EMS comment field where the comment will be added.
        /// </param>
        /// <param name="newComment">
        /// The information and context for the new comment to be added.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        /// <returns>
        /// The resulting task object. Note this is wrapped in a bool generic task
        /// for interoperability and the bool value does not indicate anything.
        /// </returns>
        public async virtual Task<bool> CreateCommentAsync( string databaseId, string commentFieldId, NewComment newComment, CallContext context = null )
        {
            // Wrap the Task in a Task<bool> for handling the response later using AccessTaskResult.
            // We don't actually care about the bool response.
            await CallApiTask( api => api.CreateComment( databaseId, commentFieldId, newComment, context ) ).ConfigureAwait( false );
            return false;
        }

        /// <summary>
        /// Adds a new comment to a comment field on an specific entity.
        /// </summary>
        /// <param name="databaseId">
        /// The unique identifier of the EMS database that the field exists on.
        /// </param>
        /// <param name="commentFieldId">
        /// The unique identifier of the EMS comment field where the comment will be added.
        /// </param>
        /// <param name="newComment">
        /// The information and context for the new comment to be added.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual void CreateComment( string databaseId, string commentFieldId, NewComment newComment, CallContext context = null )
        {
            AccessTaskResult<bool>( CreateCommentAsync( databaseId, commentFieldId, newComment, context ) );
        }

        /// <summary>
        /// Handles formatting for well-known value types.
        /// </summary>
        /// <remarks>
        /// Currently all this does is trim string values because they are often fixed width in the EMS database,
        /// and this maintains compatability with the deprecated DatabaseQueryResult.Row behavior.
        /// </remarks>
        private object ScrubDatabaseValue( object raw )
        {
            if( raw == null || !(raw is string) )
                return raw;

            return ((string)raw).Trim();
        }
    }
}
