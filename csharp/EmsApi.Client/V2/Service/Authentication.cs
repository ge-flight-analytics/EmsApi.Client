using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace EmsApi.Client.V2
{
    internal class Authentication
    {
        /// <summary>
        /// Handles authentication requests for API calls. This class will request authentication
        /// tokens when necessary, and attach the token to the header of every other request.
        /// </summary>
        /// <remarks>
        /// Because authentication is not attempted until the first time the service is accessed,
        /// we provide a callback for authentication errors instead of throwing an exception, since
        /// it can come at an unexpected time for the client.
        /// </remarks>
        internal class EmsApiTokenHandler : HttpClientHandler, IDisposable
        {
            public EmsApiTokenHandler( EmsApiServiceConfiguration serviceConfig )
            {
                UpdateConfiguration( serviceConfig );
            }

            private string m_authUrl;
            private HttpClient m_authClient;
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
            public bool Authenticate()
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

            public void UpdateConfiguration( EmsApiServiceConfiguration config )
            {
                m_serviceConfig = config;
                m_authUrl = string.Format( "{0}/token", m_serviceConfig.Endpoint );

                // Set the token to invalid in case we need to use different authentication now.
                m_tokenExpiration = DateTime.UtcNow;
            }

            protected override Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken )
            {
                // Todo: How do we account for race conditions when retrieving a token?

                // Even if we fail to authenticate, we need to send the request or other code might
                // be stuck awaiting the send.
                if( !IsTokenValid() && !Authenticate() )
                    return base.SendAsync( request, cancellationToken );

                // Apply our auth token to the header.
                request.Headers.Authorization = new AuthenticationHeaderValue( SecurityConstants.Scheme, m_authToken );
                return base.SendAsync( request, cancellationToken );
            }

            private bool IsTokenValid()
            {
                return DateTime.UtcNow < m_tokenExpiration;
            }

            private bool GetNewBearerToken( out string error )
            {
                error = null;

                // We use a separate http client for authentication requests that should only
                // be loaded once.
                if( m_authClient == null )
                    m_authClient = new HttpClient();

                var content = new FormUrlEncodedContent( new Dictionary<string, string>
                {
                    { "grant_type", SecurityConstants.GrantTypePassword },
                    { "username", m_serviceConfig.UserName },
                    { "password", m_serviceConfig.Password }
                } );

                // Regardless of if we succeed or fail the call, the returned structure will be a chunk of JSON.
                HttpResponseMessage response = m_authClient.PostAsync( m_authUrl, content ).Result;
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
                if( m_authClient != null )
                    m_authClient.Dispose();

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
}
