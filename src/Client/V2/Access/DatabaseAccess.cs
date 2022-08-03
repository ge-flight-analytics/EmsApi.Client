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
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<FieldGroup> GetFieldGroupAsync( string databaseId, string groupId = null, CallContext context = null )
        {
            return CallApiTask( api => api.GetDatabaseFieldGroup( databaseId, groupId, context ) );
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
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual FieldGroup GetFieldGroup( string databaseId, string groupId = null, CallContext context = null )
        {
            return AccessTaskResult( GetFieldGroupAsync( databaseId, groupId, context ) );
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
        public virtual async Task<DatabaseQueryResult> QueryAsync( string databaseId, DatabaseQuery query, int rowsPerCall = 20000, CallContext context = null )
        {
            AsyncQueryInfo info = await StartQueryAsync( databaseId, query, context );
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

            await StopQueryAsync( databaseId, info.Id, context );
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
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual async Task QueryAsync( string databaseId, DatabaseQuery query, RowCallback callback, int rowsPerCall = 20000, CallContext context = null )
        {
            AsyncQueryInfo info = await StartQueryAsync( databaseId, query, context );
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

            await StopQueryAsync( databaseId, info.Id, context );
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
        public virtual DatabaseQueryResult Query( string databaseId, DatabaseQuery query, int rowsPerCall = 20000, CallContext context = null )
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
        public virtual void Query( string databaseId, DatabaseQuery query, RowCallback callback, int rowsPerCall = 20000, CallContext context = null )
        {
            QueryAsync( databaseId, query, callback, rowsPerCall, context ).Wait();
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
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        /// <param name="initialDelay">
        /// The amount to delay after the first request if the query has not been processed yet. This has no effect if waitIfNotReady is set to true.
        /// </param>
        /// <param name="backoffFactor">
        /// This number used to multiply the time between API requests relative to the initialDelay value. For example if this is set to 1.0 then the
        /// initial delay will be elapsed between each subsequent API request. If this is set to 2.0 then the initial delay will be doubled for the second
        /// delay, and that will again be doubled for the next request where the data is not yet ready. This has no effect if waitIfNotReady is set to true.
        /// </param>
        public virtual async Task<AsyncQueryData> ReadQueryWhenReadyAsync( string databaseId, string queryId, int start, int end,
            TimeSpan initialDelay, float backoffFactor = 1.25f, CancellationToken cancel = default, CallContext context = null )
        {
            TimeSpan delay = initialDelay;
            HttpResponseMessage response;
            for( int i = 0; ; ++i )
            {
                response = await CallApiTask( api => api.ReadAsyncDatabaseQuery( databaseId, queryId, start, end, waitIfNotReady: false, context: context ) );
                if( response.StatusCode != System.Net.HttpStatusCode.Accepted )
                    break;

                await Task.Delay( delay, cancel );
                delay = TimeSpan.FromSeconds( delay.TotalSeconds * backoffFactor );
            }

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
                return new AsyncQueryData
                {
                    HasMoreRows = false
                };
            }

            var data = AsyncQueryData.FromJson( await response.Content.ReadAsStringAsync() );
            return data;
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
    }
}
