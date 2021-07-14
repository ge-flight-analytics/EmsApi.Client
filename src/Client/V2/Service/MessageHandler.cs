using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace EmsApi.Client.V2
{
    /// <summary>
    /// Handles authentication and setting other EMS API specific headers. This class will handle
    /// requesting authentication tokens when necessary.
    /// </summary>
    /// <remarks>
    /// Because authentication is not attempted until the first time the service is accessed,
    /// we provide a callback for authentication errors instead of throwing an exception, since
    /// it can come at an unexpected time for the client.
    /// </remarks>
    internal class MessageHandler : DelegatingHandler
    {
        public MessageHandler( HttpMessageHandler innerHandler ) : base( innerHandler )
        { }

        private EmsApiServiceConfiguration m_serviceConfig;

        // We store copies of some service config variables to determine if a change to the service
        // configuration affects us.
        private string m_endpoint, m_userName, m_pass, m_clientId, m_clientSecret, m_trustedAuthName;

        /// <summary>
        /// The cache of tokens we have retrieved.
        /// The keys to this dictionary are defined by <seealso cref="AuthConfig"/>.
        /// Note: Tokens in the cache may have expired.
        /// </summary>
        private readonly ConcurrentDictionary<string, AuthToken> m_tokens = new ConcurrentDictionary<string, AuthToken>();

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
                if( value.ApiClientId != m_clientId )
                {
                    m_clientId = value.ApiClientId;
                    deAuth = true;
                }
                if( value.ApiClientSecret != m_clientSecret )
                {
                    m_clientSecret = value.ApiClientSecret;
                    deAuth = true;
                }
                if( value.TrustedAuthName != m_trustedAuthName )
                {
                    m_trustedAuthName = value.TrustedAuthName;
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
        /// Returns true if the client is currently authenticated with password authentication.
        /// </summary>
        public bool HasAuthenticatedWithPassword()
        {
            return m_tokens.ContainsKey( AuthConfig.PasswordTokenKey );
        }

        /// <summary>
        /// Returns true if the client is currently authenticated with trusted authentication for
        /// the specified name and value.
        /// </summary>
        public bool HasAuthenticatedWithTrusted( string authName, string authValue )
        {
            return m_tokens.ContainsKey( AuthConfig.GetTrustedTokenKey( authName, authValue ) );
        }

        /// <summary>
        /// Clears out the authentication cache.
        /// </summary>
        public void ClearAuthenticationCache()
        {
            m_tokens.Clear();
        }

        /// <summary>
        /// Expire the tokens in the authentication cache.
        /// </summary>
        public void ExpireAuthenticationCacheEntries()
        {
            foreach( var token in m_tokens )
                token.Value.Expire();
        }

        /// <summary>
        /// Requests a new authentication token immediately.
        /// </summary>
        public bool AuthenticateWithPassword( CancellationToken? cancel = null )
        {
            // Use a default call context as that will use password authentication.
            var ctx = new CallContext();
            var cancellationToken = cancel ?? new CancellationToken();
            string token = GetTokenAsync( DetermineAuthMode( ctx ), cancellationToken ).Result;
            return token != null;
        }

        protected override async Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken )
        {
            AddCustomHeaders( request.Headers );

            CallContext ctx = RetrieveCallContext( request );
            AddCallContextHeaders( request.Headers, ctx );

            var authConfig = DetermineAuthMode( ctx );
            string token = await GetTokenAsync( authConfig, cancellationToken );

            // Even if we fail to authenticate, we need to send the request or other code might
            // be stuck awaiting the send.
            if( token != null )
                request.Headers.Authorization = new AuthenticationHeaderValue( HeaderConstants.Scheme, token );
            return await base.SendAsync( request, cancellationToken );
        }

        /// <summary>
        /// Attempt to retrieve the call context for this call.
        /// </summary>
        private CallContext RetrieveCallContext( HttpRequestMessage request )
        {
            if( !request.Properties.TryGetValue( "context", out object rawCtx ) )
                return null;
            return rawCtx as CallContext;
        }

        /// <summary>
        /// Adds any custom headers configured in the <see cref="EmsApiServiceConfiguration"/> to the request.
        /// This will add the custom headers to any existing headers (not overwriting them).
        /// </summary>
        /// <param name="headers">The existing request headers to add the custom headers to.</param>
        private void AddCustomHeaders( HttpRequestHeaders headers )
        {
            if( m_serviceConfig.CustomHeaders != null )
                m_serviceConfig.CustomHeaders.ToList().ForEach( keyValue => headers.Add( keyValue.Key, keyValue.Value ) );
        }

        /// <summary>
        /// Determine the authentication mode to utilize and the config for that.
        /// This will throw if the call or service configuration is not setup correctly. The goal
        /// being to notify the user ASAP if things are incorrectly configured.
        /// </summary>
        private AuthConfig DetermineAuthMode( CallContext ctx )
        {
            if( IsValidAuthSetting( ctx?.TrustedAuthValue ) )
            {
                // The call context had a trusted value, make sure the rest of the trusted auth configuration exists.
                string authName = IsValidAuthSetting( ctx.TrustedAuthName ) ? ctx.TrustedAuthName : m_serviceConfig.TrustedAuthName;
                if( !IsValidAuthSetting( authName ) )
                    throw new EmsApiException( "Trusted authentication value was provided in the CallContext but no trusted authentication name was provided there or in the service configuration." );
                if( !IsValidAuthSetting( m_serviceConfig.ApiClientId ) )
                    throw new EmsApiException( "Trusted authentication value was provided in the CallContext but API client id was not set in the service configuration." );

                // This should not be possible due to service configuration validation but we double check it here.
                if( !IsValidAuthSetting( m_serviceConfig.ApiClientSecret ) )
                    throw new EmsApiException( "Trusted authentication value was provided in the CallContext but API client secret was not set in the service configuration." );

                return new AuthConfig( m_serviceConfig.ApiClientId, m_serviceConfig.ApiClientSecret, authName, ctx.TrustedAuthValue );
            }

            // There is no trusted value in the call context, we will use password authentication.

            if( IsValidAuthSetting( ctx?.TrustedAuthName ) )
                throw new EmsApiException( "No trusted authentication value was provided in the CallContext but a trusted authentication name was, this is unexpected." );

            // Neither of these should not be possible due to service configuration validation but we double check it here.
            if( !IsValidAuthSetting( m_serviceConfig.UserName ) )
                throw new EmsApiException( "Username not set in the service configuration." );
            if( !IsValidAuthSetting( m_serviceConfig.Password ) )
                throw new EmsApiException( "Password was not set in the service configuration." );

            return new AuthConfig( m_serviceConfig.UserName, m_serviceConfig.Password );
        }

        private static bool IsValidAuthSetting( string input )
        {
            return !string.IsNullOrWhiteSpace( input );
        }

        /// <summary>
        /// Adds any headers which the call context specifies.
        /// This will overwrite any existing headers if the headers are intended to be singular.
        /// </summary>
        private static void AddCallContextHeaders( HttpRequestHeaders headers, CallContext ctx )
        {
            if( ctx == null )
                return;

            if( !string.IsNullOrWhiteSpace( ctx.ClientUsername ) )
                OverwriteHeader( headers, HttpHeaderNames.ClientUsername, ctx.ClientUsername );
            if( !string.IsNullOrWhiteSpace( ctx.CorrelationId ) )
                OverwriteHeader( headers, HttpHeaderNames.CorrelationId, ctx.CorrelationId );
        }

        /// <summary>
        /// Remove any existing values of the specified header and add just the one value specified.
        /// </summary>
        private static void OverwriteHeader( HttpRequestHeaders headers, string name, string value )
        {
            if( headers.Contains( name ) )
                headers.Remove( name );
            headers.Add( name, value );
        }

        /// <summary>
        /// Attempts to find an existing token for the specified config or retrieve a new one.
        /// </summary>
        /// <returns>Null if it was unable to retrieve a token.</returns>
        private async Task<string> GetTokenAsync( AuthConfig config, CancellationToken cancellationToken )
        {
            string tokenKey = config.TokenKey;

            // First check the cache for a token which is still valid.
            if( m_tokens.TryGetValue( tokenKey, out var token ) && token.Valid )
            {
                return token.Token;
            }

            // Request a new token. Multiple threads can get here at the same time but we are okay
            // with this optimistic collision detection vs. blocking any threads waiting here.
            var tokenResult = await RequestNewTokenAsync( config.GetTokenFormBody(), cancellationToken );

            if( tokenResult.Success )
            {
                // We succeeded so cache this new token and return it.
                m_tokens[tokenKey] = tokenResult.TokenData;
                return tokenResult.TokenData.Token;
            }

            // We failed to authenticate so notify any interested parties and return.
            // Note: We are *not* clearing out any token in the cache for this key, this means an
            // expired token will sit in there until a valid token replaces it. But this seemed
            // nicer than the possibility that another valid token for the key may have come in at
            // the same time and we would clear that one out.
            OnAuthenticationFailed( new AuthenticationFailedEventArgs( tokenResult.Error ) );
            return null;
        }

        /// <summary>
        /// Requests a new token from the EMS API.
        /// </summary>
        private async Task<AuthResult> RequestNewTokenAsync( HttpContent tokenFormBody, CancellationToken? cancel = null )
        {
            var request = new HttpRequestMessage( HttpMethod.Post, string.Format( "{0}/token", m_serviceConfig.Endpoint ) );
            m_serviceConfig.AddDefaultRequestHeaders( request.Headers );

            request.Content = tokenFormBody;

            CancellationToken cancelToken = cancel ?? new CancellationToken();
            HttpResponseMessage response = await base.SendAsync( request, cancelToken );

            // Regardless of if we succeed or fail the call, the returned structure will be a chunk of JSON.
            string rawResult = await response.Content.ReadAsStringAsync();
            var result = JObject.Parse( rawResult );

            if( !response.IsSuccessStatusCode )
            {
                string description = result.GetValue( "error_description" ).ToString();
                return new AuthResult( string.Format( "Unable to retrieve EMS API bearer token: {0}", description ) );
            }

            string token = result.GetValue( "access_token" ).ToString();
            int expiresIn = result.GetValue( "expires_in" ).ToObject<int>();

            return new AuthResult( new AuthToken( token, expiresIn ) );
        }

        private void InvalidateAuthentication()
        {
            m_tokens.Clear();
        }

        private void OnAuthenticationFailed( AuthenticationFailedEventArgs e )
        {
            AuthenticationFailedEvent?.Invoke( this, e );
        }
    }
}
