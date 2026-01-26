using System;
using System.Collections.Generic;
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
    }
}
