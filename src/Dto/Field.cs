using System;
using System.Collections.Generic;
using System.Text;

namespace EmsApi.Dto.V2
{
    public partial class Field
    {
        /// <summary>The display name for the field</summary>
        [Newtonsoft.Json.JsonProperty( "discreteValues", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore )]
        public IDictionary<int, string> DiscreteValues { get; set; }
    }
}
