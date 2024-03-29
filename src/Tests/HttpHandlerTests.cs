using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EmsApi.Client.V2;
using FluentAssertions;
using Xunit;

namespace EmsApi.Tests
{
    public class HttpHandlerTests : TestBase
    {
        [Fact( DisplayName = "Test retry" )]
        public void Test_Retry()
        {
            var lastHandler = new FlakyMessageHandler( new HttpStatusCode[]
            {
                HttpStatusCode.RequestTimeout,
                HttpStatusCode.OK,
                HttpStatusCode.InternalServerError,
                HttpStatusCode.OK
            });

            using var api = NewService( new EmsApiServiceHttpClientConfiguration { LastHandler = lastHandler } );

            api.EmsSystem.Get();

            lastHandler.TotalCallCount.Should().Be( 4 ); // Two failures and two success: one of each for the token call, and one of each for the real call.
            lastHandler.SuccessCallCount.Should().Be( 2 );
        }

        [Fact( DisplayName = "Test multiple fails which recover" )]
        public void Test_Multiple_Fails()
        {
            var lastHandler = new FlakyMessageHandler( new HttpStatusCode[]
            {
                HttpStatusCode.InternalServerError,
                HttpStatusCode.ServiceUnavailable,
                HttpStatusCode.InternalServerError
            } );

            using var api = NewService( new EmsApiServiceHttpClientConfiguration { LastHandler = lastHandler } );

            api.EmsSystem.Get();

            lastHandler.TotalCallCount.Should().Be( 5 ); // Three failures on the API token and then a token and a normal API success.
            lastHandler.SuccessCallCount.Should().Be( 2 );
        }

        [Fact( DisplayName = "Test timeout", Skip = "Test needs more work" )]
        public async void Test_Http_Timeout()
        {
            var lastHandler = new FlakyMessageHandler( new HttpStatusCode[0] );
            var config = new EmsApiServiceHttpClientConfiguration
            {
                Timeout = TimeSpan.FromMilliseconds( 1 ),
                LastHandler = lastHandler
            };

            using var api = NewService( config );
            try
            {
                await api.EmsSystem.GetAsync();
            }
            catch( AggregateException )
            {

            }

            lastHandler.TotalCallCount.Should().Be( 1 );
        }
    }

    public class FlakyMessageHandler : DelegatingHandler
    {
        public int TotalCallCount { get; private set; }

        public int SuccessCallCount { get; private set; }

        private readonly HttpStatusCode[] m_results;

        public FlakyMessageHandler( params HttpStatusCode[] results )
        {
            m_results = results;
        }

        protected override async Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken )
        {
            HttpStatusCode result = TotalCallCount >= m_results.Length ? HttpStatusCode.OK : m_results[TotalCallCount];
            ++TotalCallCount;

            // Every even request we will fail with a server error or request timeout
            if( result != HttpStatusCode.OK )
                return new HttpResponseMessage( result );

            ++SuccessCallCount;
            return await base.SendAsync( request, cancellationToken );
        }
    }
}
