using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    public class AnalysisAccess : RouteAccess
    {
        public virtual Task<AnalysisGroup> GetAnalysisGroupsAsync( CallContext context = null )
        {
            return CallApiTask( api => api.GetAnalysisGroups( context: context ).ContinueWith( t => t.Result ) );

        }
        public virtual AnalysisGroup GetAnalysisGroups( CallContext context = null )
        {
            return AccessTaskResult( GetAnalysisGroupsAsync( context ) );
        }

        public virtual Task<AnalysisGroup> GetAnalysisGroupAsync( string groupId, CallContext context = null )
        {
            return CallApiTask( api => api.GetAnalysisGroup( groupId, context: context ).ContinueWith( t => t.Result ) );

        }
        public virtual AnalysisGroup GetAnalysisGroup( string groupId, CallContext context = null )
        {
            return AccessTaskResult( GetAnalysisGroupAsync( groupId, context ) );
        }

        public virtual Task<Analysis> GetAnalysisAsync( string groupId, string analysisName, CallContext context = null )
        {
            return CallApiTask( api => api.GetAnalysis( groupId, analysisName, context ) );
        }

        public virtual Analysis GetAnalysis( string groupId, string analysisName, CallContext context = null )
        {
            return AccessTaskResult( GetAnalysisAsync( groupId, analysisName, context ) );
        }
    }
}
