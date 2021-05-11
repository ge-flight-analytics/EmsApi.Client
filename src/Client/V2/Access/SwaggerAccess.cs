using Newtonsoft.Json.Linq;

namespace EmsApi.Client.V2.Access
{
    /// <summary>
    /// Provides access to the EMS API swagger specification.
    /// </summary>
    public class SwaggerAccess : EmsApiRouteAccess
    {
        public const string DefaultSwaggerVersion = "v2";

        /// <summary>
        /// Returns the swagger specification as a raw chunk of JSON.
        /// </summary>
        public virtual string GetSpecificationJson( string apiVersion = DefaultSwaggerVersion )
        {
            var task = CallApiTask( api => api.GetSwaggerSpecification( apiVersion ) );
            return AccessTaskResult( task );
        }

        /// <summary>
        /// Returns the swagger specification as a parsed <seealso cref="JObject"/>.
        /// </summary>
        public virtual JObject GetSpecification( string apiVersion = DefaultSwaggerVersion )
        {
            return JObject.Parse( GetSpecificationJson( apiVersion ) );
        }
    }
}
