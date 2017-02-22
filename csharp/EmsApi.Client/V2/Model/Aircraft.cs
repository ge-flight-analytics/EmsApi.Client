using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace EmsApi.Client.V2.Model
{
    /// <summary>
    /// Represents a single aircraft returned by the EMS API.
    /// </summary>
    public class Aircraft : CommonModels.IdAndDescriptionModel
    {
        /// <summary>
        /// The fleet ids that the aircraft is assigned to.
        /// </summary>
        [JsonProperty]
        public int[] FleetIds { get; set; }
    }
}
