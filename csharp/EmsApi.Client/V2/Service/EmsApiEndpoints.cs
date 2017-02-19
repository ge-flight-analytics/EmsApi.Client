
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
        public const string Default = Efoqa;

        /// <summary>
        /// The primary production endpoint.
        /// </summary>
        public const string Efoqa = "https://ems.efoqa.com/api";

        /// <summary>
        /// The beta endpoint.
        /// </summary>
        public const string Beta = "https://emsapibeta.ausdig.com/api";
    }
}
