
namespace EmsApi.Client.V2
{
    /// <summary>
    /// Well-known API endpoints.
    /// </summary>
    public static class EmsApiEndpoints
    {
        /// <summary>
        /// The default endpoint.
        /// </summary>
        public const string Default = Production;

        /// <summary>
        /// The production endpoint.
        /// </summary>
        public const string Production = "https://ems.efoqa.com/api";

        /// <summary>
        /// The stable endpoint.
        /// </summary>
        public const string Stable = "https://emsapi.ausdig.com/api";

        /// <summary>
        /// The beta build endpoint.
        /// </summary>
        public const string Beta = "https://emsapibeta.ausdig.com/api";

        /// <summary>
        /// The nightly build endpoint.
        /// </summary>
        public const string Nightly = "https://emsapitest.ausdig.com/api";
    }
}
