using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace EmsApi.Tests
{
    /// <summary>
    /// A helper class for capturing HTTP traffic.
    /// </summary>
    public class TestMessageHandler : DelegatingHandler
    {
        public List<HttpRequestMessage> RequestMessages { get; private set; } = new List<HttpRequestMessage>();
        public HttpRequestMessage LastRequest { get; private set; }

        public List<HttpResponseMessage> ResponseMessages { get; private set; } = new List<HttpResponseMessage>();
        public HttpResponseMessage LastResponse { get; private set; }

        public int CallCount { get; private set; }

        protected override async Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken )
        {
            ++CallCount;
            LastRequest = request;
            RequestMessages.Add( request );
            var response = await base.SendAsync( request, cancellationToken );
            LastResponse = response;
            ResponseMessages.Add( response );
            return response;
        }
    }
}
