using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace EmsApi.Client.V2
{
    /// <summary>
    /// Handles authentication and compression for API calls. This class will handle gzip headers
    /// and decompression, as well as requesting authentication tokens when necessary.
    /// </summary>
    /// <remarks>
    /// Because authentication is not attempted until the first time the service is accessed,
    /// we provide a callback for authentication errors instead of throwing an exception, since
    /// it can come at an unexpected time for the client.
    /// </remarks>
    internal class EmsApiClientHandler : HttpClientHandler, IDisposable
    {
        public EmsApiClientHandler()
        {
            AutomaticDecompression = System.Net.DecompressionMethods.GZip;

            m_endpoint = string.Empty;
            m_userName = string.Empty;
            m_pass = string.Empty;
        }

        private string m_authToken;
        private DateTime m_tokenExpiration;
        private EmsApiServiceConfiguration m_serviceConfig;
        private string m_endpoint, m_userName, m_pass;

        /// <summary>
        /// Returns true if the client is currently authenticated.
        /// </summary>
        public bool Authenticated { get; private set; }

        /// <summary>
        /// Sets the current service configuration, causing the authentication
        /// to become invalid if the endpoint, username, or password changed.
        /// </summary>
        public EmsApiServiceConfiguration ServiceConfig
        {
            set
            {
                bool deAuth = false;
                if( value.Endpoint != m_endpoint )
                {
                    m_endpoint = value.Endpoint;
                    deAuth = true;
                }
                if( value.UserName != m_userName )
                {
                    m_userName = value.UserName;
                    deAuth = true;
                }
                if( value.Password != m_pass )
                {
                    m_pass = value.Password;
                    deAuth = true;
                }

                if( deAuth )
                    InvalidateAuthentication();

                m_serviceConfig = value;
            }
        }

        /// <summary>
        /// Fired to signal that authentication has failed for the current request.
        /// </summary>
        public event EventHandler<AuthenticationFailedEventArgs> AuthenticationFailedEvent;

        /// <summary>
        /// Requests a new authentication token immediately.
        /// </summary>
        public bool Authenticate( CancellationToken? cancel = null )
        {
            if( GetNewBearerToken( out string error ) )
            {
                Authenticated = true;
                return true;
            }

            // Notify listerners of authentication failure.
            Authenticated = false;
            OnAuthenticationFailed( new AuthenticationFailedEventArgs( error ) );
            return false;
        }

        protected override Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken )
        {
            // Todo: How do we account for race conditions when retrieving a token?

            // Even if we fail to authenticate, we need to send the request or other code might
            // be stuck awaiting the send.
            if( !IsTokenValid() && !Authenticate( cancellationToken ) )
                return base.SendAsync( request, cancellationToken );

            // Apply our auth token to the header.
            request.Headers.Authorization = new AuthenticationHeaderValue( SecurityConstants.Scheme, m_authToken );
            return base.SendAsync( request, cancellationToken );
        }

        private void InvalidateAuthentication()
        {
            Authenticated = false;
            m_authToken = string.Empty;
            m_tokenExpiration = DateTime.MinValue;
        }

        private bool IsTokenValid()
        {
            return DateTime.UtcNow < m_tokenExpiration;
        }

        private bool GetNewBearerToken( out string error, CancellationToken? cancel = null )
        {
            error = null;

            var request = new HttpRequestMessage( HttpMethod.Post, string.Format( "{0}/token", m_serviceConfig.Endpoint ) );
            m_serviceConfig.AddDefaultRequestHeaders( request.Headers );

            request.Content = new FormUrlEncodedContent( new Dictionary<string, string>
            {
                { "grant_type", SecurityConstants.GrantTypePassword },
                { "username", m_serviceConfig.UserName },
                { "password", m_serviceConfig.Password }
            } );

            CancellationToken cancelToken = cancel ?? new CancellationToken();
            HttpResponseMessage response = base.SendAsync( request, cancelToken ).Result;

            // Regardless of if we succeed or fail the call, the returned structure will be a chunk of JSON.
            string rawResult = response.Content.ReadAsStringAsync().Result;
            var result = JObject.Parse( rawResult );

            if( !response.IsSuccessStatusCode )
            {
                string description = result.GetValue( "error_description" ).ToString();
                error = string.Format( "Unable to retrieve EMS API bearer token: {0}", description );
                return false;
            }

            string token = result.GetValue( "access_token" ).ToString();
            int expiresIn = result.GetValue( "expires_in" ).ToObject<int>();

            // Stash the new token and keep track of when we expire.
            m_authToken = token;
            m_tokenExpiration = DateTime.UtcNow.AddSeconds( expiresIn );
            return true;
        }

        private void OnAuthenticationFailed( AuthenticationFailedEventArgs e )
        {
            AuthenticationFailedEvent?.Invoke( this, e );
        }

        protected override void Dispose( bool disposing )
        {
            base.Dispose( disposing );
        }
    }

    internal class SecurityConstants
    {
        public const string GrantTypePassword = "password";
        public const string GrantTypeTrusted = "trusted";
        public const string Scheme = "Bearer";
    }

#if DEBUG
    internal class DebugClientHandler : EmsApiClientHandler, IDisposable
    {
        protected override async Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken )
        {
            DateTime start = DateTime.Now;
            var resp = await base.SendAsync( request, cancellationToken );
            DateTime end = DateTime.Now;

            string id;
            var adiRequestId = resp.Headers.Where( h => h.Key == "X-Adi-Unique-Id" );
            if( adiRequestId.Count() == 1 )
                id = adiRequestId.First().Value.First();
            else
                id = Guid.NewGuid().ToString();

            var req = request;
            var msg = $"[{id} - Request]";

            Debug.WriteLine( $"{msg}========Start==========" );
            Debug.WriteLine( $"{msg} {req.Method} {req.RequestUri.PathAndQuery} {req.RequestUri.Scheme}/{req.Version}" );
            Debug.WriteLine( $"{msg} Host: {req.RequestUri.Scheme}://{req.RequestUri.Host}" );

            foreach( var header in req.Headers )
                Debug.WriteLine( $"{msg} {header.Key}: {string.Join( ", ", header.Value )}" );

            if( req.Content != null )
            {
                foreach( var header in req.Content.Headers )
                    Debug.WriteLine( $"{msg} {header.Key}: {string.Join( ", ", header.Value )}" );

                if( req.Content is StringContent || IsTextBasedContentType( req.Headers ) || IsTextBasedContentType( req.Content.Headers ) )
                {
                    var result = await req.Content.ReadAsStringAsync();
                    Debug.WriteLine( $"{msg} Content:" );
                    Debug.WriteLine( $"{msg} {string.Join( "", result.Cast<char>().Take( 255 ) )}..." );

                }
            }

            Debug.WriteLine( $"{msg} Duration: {end - start}" );
            Debug.WriteLine( $"{msg}==========End==========" );

            msg = $"[{id} - Response]";
            Debug.WriteLine( $"{msg}=========Start=========" );

            Debug.WriteLine( $"{msg} {req.RequestUri.Scheme.ToUpper()}/{resp.Version} {(int)resp.StatusCode} {resp.ReasonPhrase}" );

            foreach( var header in resp.Headers )
                Debug.WriteLine( $"{msg} {header.Key}: {string.Join( ", ", header.Value )}" );

            if( resp.Content != null )
            {
                foreach( var header in resp.Content.Headers )
                    Debug.WriteLine( $"{msg} {header.Key}: {string.Join( ", ", header.Value )}" );

                if( resp.Content is StringContent || IsTextBasedContentType( resp.Headers ) || IsTextBasedContentType( resp.Content.Headers ) )
                {
                    start = DateTime.Now;
                    var result = await resp.Content.ReadAsStringAsync();
                    end = DateTime.Now;

                    Debug.WriteLine( $"{msg} Content:" );
                    Debug.WriteLine( $"{msg} {string.Join( "", result.Cast<char>().Take( 255 ) )}..." );
                    Debug.WriteLine( $"{msg} Duration: {end - start}" );
                }
            }

            Debug.WriteLine( $"{msg}==========End==========" );
            return resp;
        }

        private readonly string[] m_types = new[] { "html", "text", "xml", "json", "txt", "x-www-form-urlencoded" };

        private bool IsTextBasedContentType( HttpHeaders headers )
        {
            if( !headers.TryGetValues( "Content-Type", out IEnumerable<string> values ) )
                return false;

            var header = string.Join( " ", values ).ToLowerInvariant();
            return m_types.Any( t => header.Contains( t ) );
        }
    }
#endif

}
