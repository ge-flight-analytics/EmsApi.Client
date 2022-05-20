using System;
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
        /// The default timeout for HTTP requests.
        /// </summary>
        /// <remarks>
        /// The HttpClient default of 100 seconds is not long enough for some of our longer EMS API calls. For
        /// instance a gnarly database query can take longer than that to return on query creation or first result
        /// extraction time, especially if the SQL server is already busy.
        /// The value we opted for here is the value used for timeouts at the application gateway level and
        /// therefore seems like a reasonable default to use.
        /// </remarks>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes( 10 );

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
