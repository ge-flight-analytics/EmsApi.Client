using System.Collections.Generic;
using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    public class AlertAccess : RouteAccess
    {
        public virtual IEnumerable<Definition> GetAlertDefinitions( CallContext context = null )
        {
            return AccessTaskResult( GetAlertDefinitionsAsync( context ) );
        }

        public virtual Task<IEnumerable<Definition>> GetAlertDefinitionsAsync( CallContext context = null )
        {
            return CallApiTask( api => api.GetAlertDefinitions( context ) );
        }

        public virtual Definition CreateAlertDefinition( SetDefinition alertDefinition, CallContext context = null )
        {
            return AccessTaskResult( CreateAlertDefinitionAsync( alertDefinition, context ) );
        }

        public virtual Task<Definition> CreateAlertDefinitionAsync( SetDefinition alertDefinition, CallContext context = null )
        {
            return CallApiTask( api => api.CreateAlertDefinition( alertDefinition, context ) );
        }

        public virtual Definition GetAlertDefinition( string definitionId, CallContext context = null )
        {
            return AccessTaskResult( GetAlertDefinitionAsync( definitionId, context ) );
        }

        public virtual Task<Definition> GetAlertDefinitionAsync( string definitionId, CallContext context = null )
        {
            return CallApiTask( api => api.GetAlertDefinition( definitionId, context ) );
        }

        public virtual void UpdateAlertDefinition( string definitionId, SetDefinition alertDefinition, CallContext context = null )
        {
            UpdateAlertDefinitionAsync( definitionId, alertDefinition, context ).Wait();
        }

        public virtual Task UpdateAlertDefinitionAsync( string definitionId, SetDefinition alertDefinition, CallContext context = null )
        {
            return CallApiTask( api => api.UpdateAlertDefinition( definitionId, alertDefinition, context ) );
        }

        public virtual void DeleteAlertDefinition( string definitionId, CallContext context = null )
        {
            DeleteAlertDefinitionAsync( definitionId, context ).Wait();
        }

        public virtual Task DeleteAlertDefinitionAsync( string definitionId, CallContext context = null )
        {
            return CallApiTask( api => api.DeleteAlertDefinition( definitionId, context ) );
        }

        public virtual GetActivity GetAlertDefinitionEntityState( string definitionId, int entityRecord, int? maxRestults = null, CallContext context = null )
        {
            return AccessTaskResult( GetAlertDefinitionEntityStateAsync( definitionId, entityRecord, maxRestults, context ) );
        }

        public virtual Task<GetActivity> GetAlertDefinitionEntityStateAsync( string definitionId, int entityRecord, int? maxRestults = null, CallContext context = null )
        {
            return CallApiTask( api => api.GetAlertDefinitionEntityState( definitionId, entityRecord, maxRestults, context ) );
        }

        public virtual IEnumerable<GetActivity> GetAlertDefinitionEntityActivity( string definitionId, int entityRecord, int? maxRestults = null, CallContext context = null )
        {
            return SafeAccessEnumerableTask( GetAlertDefinitionEntityActivityAsync( definitionId, entityRecord, maxRestults, context ) );
        }

        public virtual Task<IEnumerable<GetActivity>> GetAlertDefinitionEntityActivityAsync( string definitionId, int entityRecord, int? maxRestults = null, CallContext context = null )
        {
            return CallApiTask( api => api.GetAlertDefinitionEntityActivity( definitionId, entityRecord, maxRestults, context ) );
        }

        public virtual GetActivity AddAlertDefinitionEntityActivity( string definitionId, int entityRecord, AddActivity activity, CallContext context = null )
        {
            return AccessTaskResult( AddAlertDefinitionEntityActivityASync( definitionId, entityRecord, activity, context ) );
        }

        public virtual Task<GetActivity> AddAlertDefinitionEntityActivityASync( string definitionId, int entityRecord, AddActivity activity, CallContext context = null )
        {
            return CallApiTask( api => api.AddAlertDefinitionEntityActivity( definitionId, entityRecord, activity, context ) );

        }
    }
}
