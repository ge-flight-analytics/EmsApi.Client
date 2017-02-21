using System;
using Newtonsoft.Json;

namespace EmsApi.Client.V2.Model
{
    /// <summary>
    /// Additional server information about an EMS system connected to the API.
    /// </summary>
    public class EmsSystemInfo
    {
        /// <summary>
        /// The server timestamp on the response.
        /// </summary>
        [JsonProperty]
        public DateTime UtcTimeStamp { get; set; }

        /// <summary>
        /// The client abbreviation (TLA) assigned to the system.
        /// </summary>
        [JsonProperty]
        public string ClientAbbreviation { get; set; }

        /// <summary>
        /// A description of the EMS system.
        /// </summary>
        [JsonProperty]
        public string ClientDescription { get; set; }

        /// <summary>
        /// The name of the EMS system.
        /// </summary>
        [JsonProperty]
        public string EmsServerName { get; set; }

        /// <summary>
        /// The EMS component version of the system.
        /// </summary>
        [JsonProperty]
        public Version ServerVersion { get; set; }

        /// <summary>
        /// The EMS sql server name.
        /// </summary>
        [JsonProperty]
        public string SqlServerName { get; set; }

        /// <summary>
        /// True if the EMS API is enabled on this system.
        /// </summary>
        [JsonProperty]
        public bool EmsWebEnabled { get; set; }

        /// <summary>
        /// The human-readable description of security in the aircraft list.
        /// </summary>
        [JsonProperty]
        public string AircraftListDescription { get; set; }
    }
}
