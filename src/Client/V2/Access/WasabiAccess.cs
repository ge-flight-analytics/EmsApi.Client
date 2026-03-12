using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EmsApi.Dto.V2;

namespace EmsApi.Client.V2.Access
{
    public class WasabiAccess : RouteAccess
    {
        /// <summary>
        /// Returns the the operator authentication JSON.
        /// </summary>
        /// <param name="operatorId">
        /// The unique identifier of the operator.
        /// </param>
        public virtual Task<object> GetOperatorAuthJsonAsync( string operatorId, CallContext context = null )
        {
            return CallApiTask( api => api.GetOperatorAuthJson( operatorId, context ) );
        }

        /// <summary> Sets the operator authentication JSON.
        /// </summary> <param name="operatorId"> The unique identifier of the operator. </param>
        /// <param name="auth">The authentication information to set.</param> 
        public virtual Task SetOperatorAuthJsonAsync( string operatorId, WasabiAuthRequest auth, CallContext context = null )
        {
            return CallApiTask( api => api.SetOperatorAuthJson( operatorId, auth, context ) );
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
        /// Returns registration audit entries for the given operator.
        /// </summary>
        /// <param name="operatorId">The operator identifier.</param>
        /// <param name="context">Optional call context.</param>
        public virtual Task<WasabiRegistrationAuditResponse> GetRegistrationAuditAsync( string operatorId, CallContext context = null )
        {
            return CallApiTask( api => api.GetWasabiRegistrationAudit( operatorId, context ) );
        }

        /// <summary>
        /// Returns the Wasabi artifact PAT (Personal Access Token) for the specified EMS system.
        /// </summary>
        /// <param name="context">Optional call context.</param>
        public virtual Task<string> GetArtifactPatAsync( CallContext context = null )
        {
            return CallApiTask( api => api.GetWasabiArtifactPat( context ) );
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

        // Convenience synchronous wrappers.
        public virtual object GetOperatorAuthJson( string operatorId, CallContext context = null )
        {
            return AccessTaskResult( GetOperatorAuthJsonAsync( operatorId, context ) );
        }

        public virtual void SetOperatorAuthJson( string operatorId, WasabiAuthRequest auth, CallContext context = null )
        {
            SetOperatorAuthJsonAsync( operatorId, auth, context ).Wait();
        }

        public virtual object GetOperatorConfigJson( string operatorId, CallContext context = null )
        {
            return AccessTaskResult( GetOperatorConfigJsonAsync( operatorId, context ) );
        }

        public virtual WasabiRegistrationAuditResponse GetRegistrationAudit( string operatorId, CallContext context = null )
        {
            return AccessTaskResult( GetRegistrationAuditAsync( operatorId, context ) );
        }

        public virtual string GetArtifactPat( CallContext context = null )
        {
            return AccessTaskResult( GetArtifactPatAsync( context ) );
        }

        public virtual void SetOperatorConfigJson( string operatorId, string jsonText, CallContext context = null )
        {
            SetOperatorConfigJsonAsync( operatorId, jsonText, context ).Wait();
        }
    }
}
