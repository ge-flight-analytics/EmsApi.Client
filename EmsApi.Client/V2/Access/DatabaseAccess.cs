using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    public class DatabaseAccess : CachedEmsIdRouteAccess
    {
        /// <summary>
        /// Returns a database group with a matching ID containing only its immediate children 
        /// in a hierarchical tree used to organize databases.
        /// </summary>
        /// <param name="groupId">
        /// The unique identifier of the EMS database group whose contents to return. If not specified, 
        /// the contents of the root group are returned.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public Task<DatabaseGroup> GetDatabaseGroupAsync( string groupId = null, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetDatabaseGroup( ems, groupId ) );
        }

        /// <summary>
        /// Returns a database group with a matching ID containing only its immediate children 
        /// in a hierarchical tree used to organize databases.
        /// </summary>
        /// <param name="groupId">
        /// The unique identifier of the EMS database group whose contents to return. If not specified, 
        /// the contents of the root group are returned.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public DatabaseGroup GetDatabaseGroup( string groupId = null, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetDatabaseGroupAsync( groupId, emsSystem ) );
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public Task<FieldGroup> GetFieldGroupAsync( string databaseId, string groupId = null, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetDatabaseFieldGroup( ems, databaseId, groupId ) );
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public FieldGroup GetFieldGroup( string databaseId, string groupId = null, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetFieldGroupAsync( databaseId, groupId, emsSystem ) );
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public Task<Field> GetFieldAsync( string databaseId, string fieldId, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.GetDatabaseFieldDefinition( ems, databaseId, fieldId ) );
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public Field GetField( string databaseId, string fieldId, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( GetFieldAsync( databaseId, fieldId, emsSystem ) );
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public Task<IEnumerable<Field>> SearchFieldsAsync( string databaseId, string search = null, string fieldGroupId = null,
            bool includeProfiles = false, int maxResults = 200, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.SearchDatabaseFields( ems, databaseId, search, fieldGroupId, includeProfiles, maxResults ) );
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public IEnumerable<Field> SearchFields( string databaseId, string search = null, string fieldGroupId = null,
            bool includeProfiles = false, int maxResults = 200, int emsSystem = NoEmsServerSpecified )
        {
            return SafeAccessEnumerableTask( SearchFieldsAsync( 
                databaseId, search, fieldGroupId, includeProfiles, maxResults, emsSystem ) );
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public Task<DatabaseQueryResult> SimpleQueryAsync( string databaseId, QueryWrapper query, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.QueryDatabase( ems, databaseId, query.Raw ) ).ContinueWith( task =>
            {
                // Convert to our result format.
                QueryResult2 queryResult = task.Result;
                var result = new DatabaseQueryResult( queryResult.Header );
                result.AddRows( query.Raw, queryResult.Rows );
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public DatabaseQueryResult SimpleQuery( string databaseId, QueryWrapper query, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( SimpleQueryAsync( databaseId, query, emsSystem ) );
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public async Task<DatabaseQueryResult> QueryAsync( string databaseId, QueryWrapper query, int rowsPerCall = 20000, int emsSystem = NoEmsServerSpecified )
        {
            AsyncQueryInfo info = await StartQueryAsync( databaseId, query, emsSystem );
            var result = new DatabaseQueryResult( info.Header );

            int startingRow = 0;
            bool moreToRead = true;
            while( moreToRead )
            {
                AsyncQueryData data = await ReadQueryAsync( databaseId, info.Id, startingRow, startingRow + rowsPerCall - 1, emsSystem );
                result.AddRows( query.Raw, data.Rows );
                moreToRead = data.HasMoreRows;
            }

            await StopQueryAsync( databaseId, info.Id, emsSystem );
            return result;
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
        /// <param name="rowCallback">
        /// A callback to execute for each row of data received.
        /// </param>
        /// <param name="rowsPerCall">
        /// The number of rows to read for each API call.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public async Task QueryAsync( string databaseId, QueryWrapper query, Action<DatabaseQueryResult.Row> rowCallback, int rowsPerCall = 20000, int emsSystem = NoEmsServerSpecified )
        {
            AsyncQueryInfo info = await StartQueryAsync( databaseId, query, emsSystem );
            var result = new DatabaseQueryResult( info.Header );

            int startingRow = 0;
            bool moreToRead = true;
            while( moreToRead )
            {
                AsyncQueryData data = await ReadQueryAsync( databaseId, info.Id, startingRow, startingRow + rowsPerCall - 1, emsSystem );
                result.AddRows( query.Raw, data.Rows );
                moreToRead = data.HasMoreRows;
            }

            await StopQueryAsync( databaseId, info.Id, emsSystem );
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public DatabaseQueryResult Query( string databaseId, QueryWrapper query, int rowsPerCall = 20000, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( QueryAsync( databaseId, query, rowsPerCall, emsSystem ) );
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
        /// <param name="rowCallback">
        /// A callback to execute for each row of data received.
        /// </param>
        /// <param name="rowsPerCall">
        /// The number of rows to read for each API call.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public void Query( string databaseId, QueryWrapper query, Action<DatabaseQueryResult.Row> rowCallback, int rowsPerCall = 20000, int emsSystem = NoEmsServerSpecified )
        {
            QueryAsync( databaseId, query, rowCallback, rowsPerCall, emsSystem ).RunSynchronously();
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public Task<AsyncQueryInfo> StartQueryAsync( string databaseId, QueryWrapper query, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.StartAsyncDatabaseQuery( ems, databaseId, query.Raw ) );
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public AsyncQueryInfo StartQuery( string databaseId, QueryWrapper query, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( StartQueryAsync( databaseId, query, emsSystem ) );
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public Task<AsyncQueryData> ReadQueryAsync( string databaseId, string queryId, int start, int end, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.ReadAsyncDatabaseQuery( ems, databaseId, queryId, start, end ) );
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public AsyncQueryData ReadQuery( string databaseId, string queryId, int start, int end, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( ReadQueryAsync( databaseId, queryId, start, end, emsSystem ) );
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public Task StopQueryAsync( string databaseId, string queryId, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.StopAsyncDatabaseQuery( ems, databaseId, queryId ) );
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public void StopQuery( string databaseId, string queryId, int emsSystem = NoEmsServerSpecified )
        {
            StopQueryAsync( databaseId, queryId, emsSystem ).RunSynchronously();
        }
    }
}
