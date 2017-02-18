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
        internal class SecurityConstants
        {
            public const string GrantTypePassword = "password";
            public const string GrantTypeTrusted = "trusted";
            public const string Scheme = "Bearer";
        }

        /// <summary>
        /// Handles authentication requests for API calls. This class will request authentication
        /// tokens when necessary, and attach the token to the header of every other request.
        /// </summary>
        internal class EmsApiTokenHandler : HttpClientHandler, IDisposable
        {
            public EmsApiTokenHandler( EmsApiServiceConfiguration serviceConfig )
            {
                m_serviceConfig = serviceConfig;
                m_tokenExpiration = DateTime.UtcNow;
                m_authUrl = string.Format( "{0}/token", m_serviceConfig.Endpoint );
            }

            private string m_authUrl;
            private HttpClient m_authClient;
            private string m_authToken;
            private DateTime m_tokenExpiration;
            private EmsApiServiceConfiguration m_serviceConfig;

            protected override async Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken )
            {
                // Todo: How do we account for race conditions when retrieving a token?
                if( !IsTokenValid() )
                    await GetNewBearerToken().ConfigureAwait( false );

                // Apply our token to the header.
                request.Headers.Authorization = new AuthenticationHeaderValue( SecurityConstants.Scheme, m_authToken );
                return await base.SendAsync( request, cancellationToken ).ConfigureAwait( false );
            }

            private bool IsTokenValid()
            {
                return DateTime.UtcNow < m_tokenExpiration;
            }

            private async Task GetNewBearerToken()
            {
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

                HttpResponseMessage response = await m_authClient.PostAsync( m_authUrl, content );
                if( !response.IsSuccessStatusCode )
                {
                    throw new UnauthorizedAccessException( string.Format( "Unable to retrieve EMS API token. Status: {0}, Response: {1}",
                        response.StatusCode, response.Content.ReadAsStringAsync().Result ) );
                }

                string rawResult = await response.Content.ReadAsStringAsync();

                JObject result = JObject.Parse( rawResult );
                string token = result.GetValue( "access_token" ).ToString();
                int expiresIn = result.GetValue( "expires_in" ).ToObject<int>();

                // Stash the new token and keep track of when we expire.
                m_authToken = token;
                m_tokenExpiration = DateTime.UtcNow.AddSeconds( expiresIn );
            }

            protected override void Dispose( bool disposing )
            {
                if( m_authClient != null )
                    m_authClient.Dispose();

                base.Dispose( disposing );
            }
        }
    }
}
