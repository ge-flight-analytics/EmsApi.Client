using System.Collections.Generic;
using Newtonsoft.Json;

namespace EmsApi.Dto.V2
{
    /// <summary>
    /// Response for Wasabi registration audit.
    /// </summary>
    public class WasabiRegistrationAuditResponse
    {
        /// <summary>
        /// The registration audit entries.
        /// </summary>
        [JsonProperty("registrations", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public List<WasabiRegistrationAuditEntity> Registrations { get; set; }
    }

    /// <summary>
    /// Represents a single Wasabi registration audit record.
    /// </summary>
    public class WasabiRegistrationAuditEntity
    {
        [JsonProperty("username", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }

        [JsonProperty("machineName", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string MachineName { get; set; }

        [JsonProperty("machineGuid", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string MachineGuid { get; set; }

        // UTC timestamp string, as provided by server.
        [JsonProperty("registrationTimeUtc", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string RegistrationTimeUtc { get; set; }
    }
}
