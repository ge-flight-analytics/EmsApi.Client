using System.Net.Http;

namespace EmsApi.Client.V2
{
    /// <summary>
    /// Encapsulates the configuration options for the internal HttpClient which the EMS API SDK
    /// utilizes.
    /// </summary>
    /// <remarks>
    /// As we are currently maintaining .NET Standard 2.0 compatibility we can't utilize
    /// IHttpClientFactory and other bits
    /// </remarks>
    public class EmsApiServiceHttpClientConfiguration
    {
        /// <summary>
        /// Whether or not to retry transient failures.
        /// </summary>
        /// <remarks>
        /// This utilizes the Polly policy and HTTP extensions for detecting transient failures.
        /// This will retry and 408 (request timeout), 5XX (server error), or network failures (HttpRequestException).
        /// Both the token acquisition and the normal EMS API calls will be retried.
        ///
        /// This defaults to true as we believe that is the more common/desired use case.
        /// </remarks>
        public bool RetryTransientFailures { get; set; } = true;

        /// <summary>
        /// A delegating handler added to the front of the HTTP message stack.
        /// </summary>
        /// <remarks>
        /// This will be the first handler called in an outgoing HTTP send.
        /// This can be useful for tracing or testing purposes.
        /// </remarks>
        public DelegatingHandler FirstHandler { get; set; }

        /// <summary>
        /// A delegating handler added to the back of the HTTP message stack.
        /// </summary>
        /// <remarks>
        /// This will be the last handler called in an outgoing HTTP send.
        /// This can be useful for tracing or testing purposes.
        /// </remarks>
        public DelegatingHandler LastHandler { get; set; }
    }
}
