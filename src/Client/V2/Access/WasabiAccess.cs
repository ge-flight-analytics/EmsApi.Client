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
        /// Returns the list of fleets the user has access to in their security context.
        /// </summary>
        /// <param name="context">
        /// The optional call context to include.
        /// </param>
        public virtual Task<object> GetOperatorAuthJsonAsync( string operatorId, CallContext context = null )
        {
            return CallApiTask( api => api.GetOperatorAuthJson( operatorId, context ) );
        }

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
            // fire-and-wait to propagate errors
            SetOperatorAuthJsonAsync( operatorId, auth, context ).Wait();
        }

        public virtual object GetOperatorConfigJson( string operatorId, CallContext context = null )
        {
            return AccessTaskResult( GetOperatorConfigJsonAsync( operatorId, context ) );
        }

        public virtual void SetOperatorConfigJson( string operatorId, string jsonText, CallContext context = null )
        {
            // fire-and-wait to propagate errors
            SetOperatorConfigJsonAsync( operatorId, jsonText, context ).Wait();
        }
    }
}
