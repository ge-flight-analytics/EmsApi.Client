using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    /// <summary>
    /// Provides access to the EMS API Analytic Set routes.
    /// These routes replace the Parameter Set routes.
    /// </summary>
    public class AnalyticSetAccess : RouteAccess
    {
        #region AnalyticSetGroup
        
        /// <summary>
        /// Returns the root analytic group.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<AnalyticSetGroup> GetAnalyticSetGroupsAsync( CallContext context = null )
        {
            return CallApiTask( api => api.GetAnalyticSetGroups( context: context ).ContinueWith( t => t.Result ) );
        }

        /// <summary>
        /// Returns the root analytic group.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual AnalyticSetGroup GetAnalyticSetGroups( CallContext context = null )
        {
            return AccessTaskResult( GetAnalyticSetGroupsAsync( context ) );
        }

        /// <summary>
        /// Returns the specified analytic group.
        /// </summary>
        /// <param name="analyticGroupId">
        /// The ID of the group to retrieve.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<AnalyticSetGroup> GetAnalyticSetGroupAsync( string analyticGroupId, CallContext context = null )
        {
            return CallApiTask( api => api.GetAnalyticSetGroup( analyticGroupId, context: context ).ContinueWith( t => t.Result ) );
        }

        /// <summary>
        /// Returns the specified analytic group.
        /// </summary>
        /// <param name="analyticGroupId">
        /// The ID of the group to retrieve.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual AnalyticSetGroup GetAnalyticSetGroup( string analyticGroupId, CallContext context = null )
        {
            return AccessTaskResult( GetAnalyticSetGroupAsync( analyticGroupId, context ) );
        }

        /// <summary>
        /// Creates an analytic set group.
        /// </summary>
        /// <param name="newAnalyticSetGroup">
        /// The information to create a new analytic set group.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<AnalyticSetGroupCreated> CreateAnalyticSetGroupAsync( NewAnalyticSetGroup newAnalyticSetGroup, CallContext context = null )
        {
            return CallApiTask( api => api.CreateAnalyticSetGroup( newAnalyticSetGroup, context ) );
        }

        /// <summary>
        /// Creates an analytic set group.
        /// </summary>
        /// <param name="newAnalyticSetGroup">
        /// The information to create a new analytic set group.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual AnalyticSetGroupCreated CreateAnalyticSetGroup( NewAnalyticSetGroup newAnalyticSetGroup, CallContext context = null )
        {
            return AccessTaskResult( CreateAnalyticSetGroupAsync( newAnalyticSetGroup, context ) );
        }

        /// <summary>
        /// Updates an analytic set group.
        /// </summary>
        /// <param name="analyticGroupId">
        /// The group to update.
        /// </param>
        /// <param name="updateAnalyticSetGroup">
        /// The information to update the group with.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<AnalyticSetGroupUpdated> UpdateAnalyticSetGroupAsync( string analyticGroupId, UpdateAnalyticSetGroup updateAnalyticSetGroup, CallContext context = null )
        {
            return CallApiTask( api => api.UpdateAnalyticSetGroup( analyticGroupId, updateAnalyticSetGroup, context ) );
        }

        /// <summary>
        /// Updates an analytic set group.
        /// </summary>
        /// <param name="analyticGroupId">
        /// The group to update.
        /// </param>
        /// <param name="updateAnalyticSetGroup">
        /// The information to update the group with.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual AnalyticSetGroupUpdated UpdateAnalyticSetGroup( string analyticGroupId, UpdateAnalyticSetGroup updateAnalyticSetGroup, CallContext context = null )
        {
            return AccessTaskResult( UpdateAnalyticSetGroupAsync( analyticGroupId, updateAnalyticSetGroup, context ) );
        }

        /// <summary>
        /// Deletes an analytic set group.
        /// </summary>
        /// <param name="analyticGroupId">
        /// The ID of the group to delete.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<object> DeleteAnalyticSetGroupAsync( string analyticGroupId, CallContext context = null )
        {
            return CallApiTask( api => api.DeleteAnalyticSetGroup( analyticGroupId, context ) );
        }

        /// <summary>
        /// Deletes an analytic set group.
        /// </summary>
        /// <param name="analyticGroupId">
        /// The ID of the group to delete.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual void DeleteAnalyticSetGroup( string analyticGroupId, CallContext context = null )
        {
            AccessTaskResult<object>( DeleteAnalyticSetGroupAsync( analyticGroupId, context ) );
        }

        #endregion

        #region AnalyticSet

        /// <summary>
        /// Gets a specific analytic set.
        /// </summary>
        /// <param name="analyticGroupId">
        /// The ID of the analytic set's parent group.
        /// </param>
        /// <param name="analyticSetName">
        /// The name of the analytic set.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<AnalyticSet> GetAnalyticSetAsync( string analyticGroupId, string analyticSetName, CallContext context = null )
        {
            return CallApiTask( api => api.GetAnalyticSet( analyticGroupId, analyticSetName, context ) );
        }

        /// <summary>
        /// Gets a specific analytic set.
        /// </summary>
        /// <param name="analyticGroupId">
        /// The ID of the analytic set's parent group.
        /// </param>
        /// <param name="analyticSetName">
        /// The name of the analytic set.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual AnalyticSet GetAnalyticSet( string analyticGroupId, string analyticSetName, CallContext context = null )
        {
            return AccessTaskResult( GetAnalyticSetAsync( analyticGroupId, analyticSetName, context ) );
        }

        /// <summary>
        /// Creates an analytic set.
        /// </summary>
        /// <param name="analyticGroupId">
        /// The ID of the group where the new set will be created.
        /// </param>
        /// <param name="newAnalyticSet">
        /// The information to create the new set.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<AnalyticSetCreated> CreateAnalyticSetAsync(string analyticGroupId, NewAnalyticSet newAnalyticSet, CallContext context = null )
        {
            return CallApiTask( api => api.CreateAnalyticSet( analyticGroupId, newAnalyticSet, context ) );
        }

        /// <summary>
        /// Creates an analytic set.
        /// </summary>
        /// <param name="analyticGroupId">
        /// The ID of the group where the new set will be created.
        /// </param>
        /// <param name="newAnalyticSet">
        /// The information to create the new set.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual AnalyticSetCreated CreateAnalyticSet( string analyticGroupId, NewAnalyticSet newAnalyticSet, CallContext context = null )
        {
            return AccessTaskResult( CreateAnalyticSetAsync( analyticGroupId, newAnalyticSet, context ) );
        }

        /// <summary>
        /// Updates an analytic set.
        /// </summary>
        /// <param name="analyticGroupId">
        /// The ID of the parent group of the analytic set to update.
        /// </param>
        /// <param name="analyticSetName">
        /// The name of the analytic set to update.
        /// </param>
        /// <param name="updateAnalyticSet">
        /// The information to update the analytic set with.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<object> UpdateAnalyticSetAsync( string analyticGroupId, string analyticSetName, UpdateAnalyticSet updateAnalyticSet, CallContext context = null )
        {
            return CallApiTask( api => api.UpdateAnalyticSet( analyticGroupId, analyticSetName, updateAnalyticSet, context ) );
        }

        /// <summary>
        /// Updates an analytic set.
        /// </summary>
        /// <param name="analyticGroupId">
        /// The ID of the parent group of the analytic set to update.
        /// </param>
        /// <param name="analyticSetName">
        /// The name of the analytic set to update.
        /// </param>
        /// <param name="updateAnalyticSet">
        /// The information to update the analytic set with.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual void UpdateAnalyticSet( string analyticGroupId, string analyticSetName, UpdateAnalyticSet updateAnalyticSet, CallContext context = null )
        {
             AccessTaskResult<object>( UpdateAnalyticSetAsync( analyticGroupId, analyticSetName, updateAnalyticSet, context ) );
        }

        /// <summary>
        /// Deletes an analytic set.
        /// </summary>
        /// <param name="analyticGroupId">
        /// The ID of the parent group of the analytic set to delete.
        /// </param>
        /// <param name="analyticSetName">
        /// The name of the analytic set to delete.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<object> DeleteAnalyticSetAsync( string analyticGroupId, string analyticSetName, CallContext context = null )
        {
            return CallApiTask( api => api.DeleteAnalyticSet( analyticGroupId, analyticSetName, context ) );
        }

        /// <summary>
        /// Deletes an analytic set.
        /// </summary>
        /// <param name="analyticGroupId">
        /// The ID of the parent group of the analytic set to delete.
        /// </param>
        /// <param name="analyticSetName">
        /// The name of the analytic set to delete.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual void DeleteAnalyticSet( string analyticGroupId, string analyticSetName, CallContext context = null )
        {
            AccessTaskResult<object>( DeleteAnalyticSetAsync( analyticGroupId, analyticSetName, context ) );
        }

        #endregion

        #region AnalyticCollection

        /// <summary>
        /// Gets an analytic collection.
        /// </summary>
        /// <param name="analyticGroupId">
        /// The ID of the parent group of the collection.
        /// </param>
        /// <param name="analyticCollectionName">
        /// The name of the analytic collection.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<AnalyticCollection> GetAnalyticCollectionAsync( string analyticGroupId, string analyticCollectionName, CallContext context = null )
        {
            return CallApiTask( api => api.GetAnalyticCollection( analyticGroupId, analyticCollectionName, context ) );
        }

        /// <summary>
        /// Gets an analytic collection.
        /// </summary>
        /// <param name="analyticGroupId">
        /// The ID of the parent group of the collection.
        /// </param>
        /// <param name="analyticCollectionName">
        /// The name of the analytic collection.</param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual AnalyticCollection GetAnalyticCollection( string analyticGroupId, string analyticCollectionName, CallContext context = null )
        {
            return AccessTaskResult( GetAnalyticCollectionAsync( analyticGroupId, analyticCollectionName, context ) );
        }

        /// <summary>
        /// Creates an analytic collection.
        /// </summary>
        /// <param name="analyticGroupId">
        /// The ID of the group where the new collection will be created.
        /// </param>
        /// <param name="newAnalyticCollection">
        /// The information needed to create the new collection.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<AnalyticCollectionCreated> CreateAnalyticCollectionAsync( string analyticGroupId, NewAnalyticCollection newAnalyticCollection, CallContext context = null )
        {
            return CallApiTask( api => api.CreateAnalyticCollection( analyticGroupId, newAnalyticCollection, context ) );
        }

        /// <summary>
        /// Creates an analytic collection.
        /// </summary>
        /// <param name="analyticGroupId">
        /// The ID of the group where the new collection will be created.
        /// </param>
        /// <param name="newAnalyticCollection">
        /// The information needed to create the new collection.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual AnalyticCollectionCreated CreateAnalyticCollection( string analyticGroupId, NewAnalyticCollection newAnalyticCollection, CallContext context = null )
        {
            return AccessTaskResult( CreateAnalyticCollectionAsync( analyticGroupId, newAnalyticCollection, context ) );
        }

        /// <summary>
        /// Updates an analytic collection.
        /// </summary>
        /// <param name="analyticGroupId">
        /// The ID of the parent group of the collection to update.
        /// </param>
        /// <param name="analyticCollectionName">
        /// The name of the collection to update.
        /// </param>
        /// <param name="updateAnalyticCollection">
        /// The information to update the collection with.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<object> UpdateAnalyticCollectionAsync( string analyticGroupId, string analyticCollectionName, UpdateAnalyticCollection updateAnalyticCollection, CallContext context = null )
        {
            return CallApiTask( api => api.UpdateAnalyticCollection( analyticGroupId, analyticCollectionName, updateAnalyticCollection, context ) );
        }

        /// <summary>
        /// Updates an analytic collection.
        /// </summary>
        /// <param name="analyticGroupId">
        /// The ID of the parent group of the collection to update.
        /// </param>
        /// <param name="analyticCollectionName">
        /// The name of the collection to update.
        /// </param>
        /// <param name="updateAnalyticCollection">
        /// The information to update the collection with.
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual void UpdateAnalyticCollection( string analyticGroupId, string analyticCollectionName, UpdateAnalyticCollection updateAnalyticCollection, CallContext context = null )
        {
            AccessTaskResult<object>( UpdateAnalyticCollectionAsync( analyticGroupId, analyticCollectionName, updateAnalyticCollection, context ) );
        }

        /// <summary>
        /// Deletes an analytic collection.
        /// </summary>
        /// <param name="analyticGroupId">
        /// The ID of the parent group of the collection to delete.
        /// </param>
        /// <param name="analyticCollectionName">
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<object> DeleteAnalyticCollectionAsync( string analyticGroupId, string analyticCollectionName, CallContext context = null )
        {
            return CallApiTask( api => api.DeleteAnalyticCollection( analyticGroupId, analyticCollectionName, context ) );
        }

        /// <summary>
        /// Deletes an analytic collection.
        /// </summary>
        /// <param name="analyticGroupId">
        /// The ID of the parent group of the collection to delete.
        /// </param>
        /// <param name="analyticCollectionName">
        /// </param>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual void DeleteAnalyticCollection( string analyticGroupId, string analyticCollectionName, CallContext context = null )
        {
            AccessTaskResult<object>( DeleteAnalyticCollectionAsync( analyticGroupId, analyticCollectionName, context ) );
        }

        #endregion
    }
}
