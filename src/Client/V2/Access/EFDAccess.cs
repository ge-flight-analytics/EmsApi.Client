using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    public class EfdAccess : RouteAccess
    {
        /// <summary>
        /// Returns the blob SAS URL for the given operator.
        /// </summary>
        /// <param name="operatorId">
        /// The unique identifier of the operator.
        /// </param>
        public virtual Task<string> GetOperatorBlobSasUrlAsync( string operatorId, CallContext context = null )
        {
            return CallApiTask( api => api.GetOperatorBlobSasUrl( operatorId, context ) );
        }

        /// <summary>
        /// Returns the operators authorized for EFD access on the given EMS system.
        /// </summary>
        /// <param name="context">Optional call context.</param>
        public virtual Task<IEnumerable<Operator>> GetAuthorizedOperatorsAsync( CallContext context = null )
        {
            return CallApiTask( api => api.GetAuthorizedOperators( context ) );
        }

        /// <summary> Sets the blob SAS URL for the given operator.
        /// </summary> <param name="operatorId"> The unique identifier of the operator. </param>
        /// <param name="blobSasUrl">The blob SAS URL to set.</param>
        public virtual Task SetOperatorBlobSasUrlAsync( string operatorId, string blobSasUrl, CallContext context = null )
        {
            var content = new StringContent( blobSasUrl ?? string.Empty, Encoding.UTF8, "text/plain" );
            return CallApiTask( api => api.SetOperatorBlobSasUrl( operatorId, content, context ) );
        }

        /// <summary>
        /// Returns the config JSON for the given operator.
        /// </summary>
        /// <param name="operatorId">The operator identifier.</param>
        /// <param name="context">Optional call context.</param>
        public virtual Task<object> GetOperatorConfigJsonAsync( string operatorId, CallContext context = null )
        {
            return CallApiTask( api => api.GetOperatorConfigJson( operatorId, context ) );
        }

        /// <summary>
        /// Returns the EFD artifact PAT (Personal Access Token) for the specified EMS system.
        /// </summary>
        /// <param name="context">Optional call context.</param>
        public virtual Task<string> GetArtifactPatAsync( CallContext context = null )
        {
            return CallApiTask( api => api.GetEfdArtifactPat( context ) );
        }

        /// <summary>
        /// Returns members of the configured fleet AD group for the given operator.
        /// </summary>
        /// <param name="operatorId">The operator identifier.</param>
        /// <param name="context">Optional call context.</param>
        public virtual Task<EfdFleetGroupMembersResponse> GetFleetGroupMembersAsync( string operatorId, CallContext context = null )
        {
            return CallApiTask( api => api.GetFleetGroupMembers( operatorId, context ) );
        }

        /// <summary>
        /// Sets the config JSON for the given operator.
        /// </summary>
        /// <param name="operatorId">The operator identifier.</param>
        /// <param name="jsonText">The raw JSON text to set.</param>
        /// <param name="context">Optional call context.</param>
        public virtual Task SetOperatorConfigJsonAsync( string operatorId, string jsonText, CallContext context = null )
        {
            var jsonContent = new StringContent( jsonText ?? string.Empty, Encoding.UTF8, "application/json" );
            return CallApiTask( api => api.SetOperatorConfigJson( operatorId, jsonContent, context ) );
        }
    }
}
