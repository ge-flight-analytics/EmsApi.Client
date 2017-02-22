using System;
using Newtonsoft.Json;

namespace EmsApi.Client.V2.Model
{
    /// <summary>
    /// Contains class definitions for models that share common properties.
    /// </summary>
    public class CommonModels
    {
        /// <summary>
        /// Base class for models that have an Id property.
        /// </summary>
        public abstract class IdModel
        {
            /// <summary>
            /// The primary key identifier for this instance.
            /// </summary>
            [JsonProperty]
            public int Id { get; set; }
        }

        /// <summary>
        /// Base class for models that have an Id and Description property.
        /// </summary>
        public abstract class IdAndDescriptionModel : IdModel
        {
            /// <summary>
            /// The description of the object.
            /// </summary>
            [JsonProperty]
            public string Description { get; set; }
        }
    }
}
