using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    public class FieldSetAccess : RouteAccess
    {
        public virtual Task<FieldSetGroup> GetFieldSetGroupsAsync( CallContext context = null )
        {
            return CallApiTask( api => api.GetFieldSetGroups( context: context ).ContinueWith( t => t.Result ) );

        }
        public virtual FieldSetGroup GetFieldSetGroups( CallContext context = null )
        {
            return AccessTaskResult( GetFieldSetGroupsAsync( context ) );
        }

        public virtual Task<FieldSetGroup> GetFieldSetGroupAsync( string groupId, CallContext context = null )
        {
            return CallApiTask( api => api.GetFieldSetGroup( groupId, context: context ).ContinueWith( t => t.Result ) );

        }
        public virtual FieldSetGroup GetFieldSetGroup( string groupId, CallContext context = null )
        {
            return AccessTaskResult( GetFieldSetGroupAsync( groupId, context ) );
        }

        public virtual Task<FieldSet> GetFieldSetAsync( string groupId, string fieldSetName, CallContext context = null )
        {
            return CallApiTask( api => api.GetFieldSet( groupId, fieldSetName, context ) );
        }

        public virtual FieldSet GetFieldSet( string groupId, string fieldSetName, CallContext context = null )
        {
            return AccessTaskResult( GetFieldSetAsync( groupId, fieldSetName, context ) );
        }
    }
}
