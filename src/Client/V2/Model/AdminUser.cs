using System;
using Newtonsoft.Json;

namespace EmsApi.Dto.V2
{
    /// <summary>
    /// Additional server information about an EMS system connected to the API.
    /// </summary>
    /// <remarks>
    /// We are not generating our DTO objects from the form of the Swagger doc with the Admin routes included.
    /// </remarks>
    public class AdminUser
    {
        /// <summary>
        /// The unique integer identifier of the user.
        /// </summary>
        [JsonProperty]
        public int Id { get; set; }

        /// <summary>
        /// The Active Directory username of the user, typically in the form of "domain\user.name". If not in an environment with multiple domains, the domain name is not needed.
        /// </summary>
        [JsonProperty]
        public string Username { get; set; }

        /// <summary>
        /// Indicates whether the account lockout policy applies to this user, causing them to be locked out of the application for a configured amount of time after failed login attempts.
        /// </summary>
        [JsonProperty]
        public bool LockoutPolicyEnabled { get; set; }

        /// <summary>
        /// The list of roles to which the user is a member.
        /// </summary>
        [JsonProperty]
        public string[] Roles { get; set; }

        /// <summary>
        /// The Tableau username to which this user is linked. This user name is only needed if different than the user name.
        /// </summary>
        [JsonProperty]
        public string TableauUsername { get; set; }

        /// <summary>
        /// A read-only value indicating the last time the user logged into the application (in UTC). Null if the user has never logged in.
        /// </summary>
        [JsonProperty]
        public DateTime? LastLogin { get; set; }

        /// <summary>
        /// A read-only value indicating the time that the user will no longer be locked out. Null if the user is not currently locked out.
        /// </summary>
        [JsonProperty]
        public DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        /// A read-only value indicating the number of times the user failed to login.
        /// </summary>
        [JsonProperty]
        public int AccessFailedCount { get; set; }
    }
}
