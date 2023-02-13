using System.Collections.Generic;

namespace EmsApi.Dto.V2
{
    public partial class Field
    {
        /// <summary>The display name for the field</summary>
        [Newtonsoft.Json.JsonProperty( "discreteValues", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore )]
        public IDictionary<long, string> DiscreteValues { get; set; }
    }
}
