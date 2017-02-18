using System;
using Newtonsoft.Json;

namespace EmsApi.Client.V2.Model
{
    public class EmsSystemInfo
    {
        [JsonProperty]
        public DateTime UtcTimeStamp { get; set; }

        [JsonProperty]
        public string ClientAbbreviation { get; set; }

        [JsonProperty]
        public string ClientDescription { get; set; }

        [JsonProperty]
        public string EmsServerName { get; set; }

        [JsonProperty]
        public Version ServerVersion { get; set; }

        [JsonProperty]
        public string SqlServerName { get; set; }

        [JsonProperty]
        public bool EmsWebEnabled { get; set; }

        [JsonProperty]
        public string AircraftListDescription { get; set; }
    }
}
