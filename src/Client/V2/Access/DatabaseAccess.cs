using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using EmsApi.Dto.V2;

using RowCallback = System.Action<EmsApi.Dto.V2.DatabaseQueryResult.Row>;

namespace EmsApi.Client.V2.Access
{
    /// <summary>
    /// Provides access to EMS API "databases" routes.
    /// </summary>
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
        public Task<DatabaseQueryResult> SimpleQueryAsync( string databaseId, DatabaseQuery query, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.QueryDatabase( ems, databaseId, query.Raw ) ).ContinueWith( task =>
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public Task SimpleQueryAsync( string databaseId, DatabaseQuery query, RowCallback callback, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.QueryDatabase( ems, databaseId, query.Raw ) ).ContinueWith( task =>
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public DatabaseQueryResult SimpleQuery( string databaseId, DatabaseQuery query, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( SimpleQueryAsync( databaseId, query, emsSystem ) );
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public void SimpleQuery( string databaseId, DatabaseQuery query, RowCallback callback, int emsSystem = NoEmsServerSpecified )
        {
            SimpleQueryAsync( databaseId, query, callback, emsSystem ).Wait();
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
        public async Task<DatabaseQueryResult> QueryAsync( string databaseId, DatabaseQuery query, int rowsPerCall = 20000, int emsSystem = NoEmsServerSpecified )
        {
            AsyncQueryInfo info = await StartQueryAsync( databaseId, query, emsSystem );
            var result = new DatabaseQueryResult( info.Header );

            int startingRow = 0;
            bool moreToRead = true;
            while( moreToRead )
            {
                AsyncQueryData data = await ReadQueryAsync( databaseId, info.Id, startingRow, startingRow + rowsPerCall - 1, emsSystem );
                result.AddRows( query, data.Rows );
                startingRow = startingRow + rowsPerCall;
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
        /// <param name="callback">
        /// A callback to execute for each row of data received.
        /// </param>
        /// <param name="rowsPerCall">
        /// The number of rows to read for each API call.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public async Task QueryAsync( string databaseId, DatabaseQuery query, RowCallback callback, int rowsPerCall = 20000, int emsSystem = NoEmsServerSpecified )
        {
            AsyncQueryInfo info = await StartQueryAsync( databaseId, query, emsSystem );
            var result = new DatabaseQueryResult( info.Header );

            int startingRow = 0;
            bool moreToRead = true;
            while( moreToRead )
            {
                AsyncQueryData data = await ReadQueryAsync( databaseId, info.Id, startingRow, startingRow + rowsPerCall - 1, emsSystem );
                result.CallbackRows( query, data.Rows, callback );
                startingRow = startingRow + rowsPerCall;
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
        public DatabaseQueryResult Query( string databaseId, DatabaseQuery query, int rowsPerCall = 20000, int emsSystem = NoEmsServerSpecified )
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
        /// <param name="callback">
        /// A callback to execute for each row of data received.
        /// </param>
        /// <param name="rowsPerCall">
        /// The number of rows to read for each API call.
        /// </param>
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public void Query( string databaseId, DatabaseQuery query, RowCallback callback, int rowsPerCall = 20000, int emsSystem = NoEmsServerSpecified )
        {
            QueryAsync( databaseId, query, callback, rowsPerCall, emsSystem ).Wait();
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
        public Task<AsyncQueryInfo> StartQueryAsync( string databaseId, DatabaseQuery query, int emsSystem = NoEmsServerSpecified )
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
        public AsyncQueryInfo StartQuery( string databaseId, DatabaseQuery query, int emsSystem = NoEmsServerSpecified )
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
            StopQueryAsync( databaseId, queryId, emsSystem ).Wait();
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public Task<CreateResult> CreateEntityAsync( string databaseId, Create createQuery, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.CreateDatabaseEntity( ems, databaseId, createQuery ) );
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public CreateResult CreateEntity( string databaseId, Create createQuery, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( CreateEntityAsync( databaseId, createQuery, emsSystem ) );
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public Task<UpdateResult> UpdateAsync( string databaseId, Update updateQuery, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.UpdateDatabase( ems, databaseId, updateQuery ) );
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public UpdateResult Update( string databaseId, Update updateQuery, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( UpdateAsync( databaseId, updateQuery, emsSystem ) );
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public Task<DeleteResult> DeleteEntityAsync( string databaseId, Delete deleteQuery, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );
            return CallApiTask( api => api.DeleteDatabaseEntity( ems, databaseId, deleteQuery ) );
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public DeleteResult DeleteEntity( string databaseId, Delete deleteQuery, int emsSystem = NoEmsServerSpecified )
        {
            return AccessTaskResult( DeleteEntityAsync( databaseId, deleteQuery, emsSystem ) );
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        /// <returns>
        /// The resulting task object. Note this is wrapped in a bool generic task
        /// for interoperability and the bool value does not indicate anything.
        /// </returns>
        public async Task<bool> CreateCommentAsync( string databaseId, string commentFieldId, NewComment newComment, int emsSystem = NoEmsServerSpecified )
        {
            int ems = GetEmsSystemForMethodCall( emsSystem );

            // Wrap the Task in a Task<bool> for handling the response later using AccessTaskResult.
            // We don't actually care about the bool response.
            await CallApiTask( api => api.CreateComment( ems, databaseId, commentFieldId, newComment ) ).ConfigureAwait( false );
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
        /// <param name="emsSystem">
        /// The unique identifier of the system containing the EMS data.
        /// </param>
        public void CreateComment( string databaseId, string commentFieldId, NewComment newComment, int emsSystem = NoEmsServerSpecified )
        {
            AccessTaskResult<bool>( CreateCommentAsync( databaseId, commentFieldId, newComment, emsSystem ) );
        }
    }
}
