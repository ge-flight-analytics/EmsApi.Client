using System;

namespace EmsApi.Dto.V2
{
    /// <summary>
    /// A date+time range that defines an EMS System maintenance window.
    /// </summary>
    public class MaintenanceWindow
    {
        /// <summary>
        /// The start of the next maintenance window or the start of the current maintenance window if we are currently
        /// within one.
        /// </summary>
        public DateTime StartUtc { get; set; }

        /// <summary>
        /// The end of the next maintenance window.
        /// </summary>
        public DateTime EndUtc { get; set; }
    }
}
