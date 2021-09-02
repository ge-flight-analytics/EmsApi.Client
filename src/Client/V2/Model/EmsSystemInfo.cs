using System;
using Newtonsoft.Json;

namespace EmsApi.Dto.V2
{
    /// <summary>
    /// Additional server information about an EMS system connected to the API.
    /// </summary>
    /// <remarks>
    /// This isn't described in the swagger spec, so it's not in the EmsApi.Dto
    /// namespace already.
    /// </remarks>
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

        /// <summary>
        /// The UTC start of the next maintenance window, or when the current maintenance window started if we are in the
        /// middle of one now, or null if there is no scheduled next maintenance window.
        /// </summary>
        [JsonProperty]
        public DateTime? NextMaintenanceWindowStart { get; set; }

        /// <summary>
        /// The UTC end of the next maintenance window or null if there is no scheduled next maintenance window.
        /// </summary>
        [JsonProperty]
        public DateTime? NextMaintenanceWindowEnd { get; set; }
    }
}
