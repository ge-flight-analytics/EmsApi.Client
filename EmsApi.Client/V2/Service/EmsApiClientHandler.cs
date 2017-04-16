using System;
using System.Collections.Generic;
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
        public EmsApiClientHandler( EmsApiServiceConfiguration serviceConfig )
        {
            m_serviceConfig = serviceConfig;
            AutomaticDecompression = System.Net.DecompressionMethods.GZip;
        }

        private string m_authToken;
        private DateTime m_tokenExpiration;
        private EmsApiServiceConfiguration m_serviceConfig;

        /// <summary>
        /// Returns true if the client is currently authenticated.
        /// </summary>
        public bool Authenticated { get; private set; }

        /// <summary>
        /// Fired to signal that authentication has failed for the current request.
        /// </summary>
        public event EventHandler<AuthenticationFailedEventArgs> AuthenticationFailedEvent;

        /// <summary>
        /// Requests a new authentication token immediately.
        /// </summary>
        public bool Authenticate( CancellationToken? cancel = null )
        {
            string error;
            if( GetNewBearerToken( out error ) )
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

        private bool IsTokenValid()
        {
            return DateTime.UtcNow < m_tokenExpiration;
        }

        private bool GetNewBearerToken( out string error, CancellationToken? cancel = null )
        {
            error = null;

            HttpRequestMessage request = new HttpRequestMessage( HttpMethod.Post, string.Format( "{0}/token", m_serviceConfig.Endpoint ) );
            m_serviceConfig.AddDefaultRequestHeaders( request.Headers );

            request.Content = new FormUrlEncodedContent( new Dictionary<string, string>
            {
                { "grant_type", SecurityConstants.GrantTypePassword },
                { "username", m_serviceConfig.UserName },
                { "password", m_serviceConfig.Password }
            } );

            CancellationToken cancelToken = cancel.HasValue ? cancel.Value : new CancellationToken();
            HttpResponseMessage response = base.SendAsync( request, cancelToken ).Result;

            // Regardless of if we succeed or fail the call, the returned structure will be a chunk of JSON.
            string rawResult = response.Content.ReadAsStringAsync().Result;
            JObject result = JObject.Parse( rawResult );

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
            if( AuthenticationFailedEvent != null )
                AuthenticationFailedEvent( this, e );
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
}
