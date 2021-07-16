using System.Net.Http;
using System.Collections.Generic;

namespace EmsApi.Client.V2
{
    /// <summary>
    /// The configuration to use for authentication for a single API call.
    /// </summary>
    internal class AuthConfig
    {
        public AuthConfig( string username, string password )
        {
            m_type = AuthType.Password;
            m_userName = username;
            m_password = password;
        }

        public AuthConfig( string clientId, string clientSecret, string trustedAuthName, string trustedAuthValue )
        {
            m_type = AuthType.Trusted;
            m_clientId = clientId;
            m_clientSecret = clientSecret;
            m_trustedAuthName = trustedAuthName;
            m_trustedAuthValue = trustedAuthValue;
        }

        /// <summary>
        /// The key to use for the password authentication token.
        /// </summary>
        public const string PasswordTokenKey = "**Password Auth**";

        /// <summary>
        /// Get a key to use for the specified trusted name and value pair.
        /// </summary>
        public static string GetTrustedTokenKey( string authName, string authValue )
        {
            return $"{authName}={authValue}".ToUpperInvariant();
        }

        /// <summary>
        /// The key to use for the token that would come from this authentication configuration in
        /// a cache.
        /// </summary>
        public string TokenKey
        {
            get
            {
                if( m_type == AuthType.Password )
                    return PasswordTokenKey;

                // Make the key case insensitive to avoid case sensitivity cache misses.
                return GetTrustedTokenKey( m_trustedAuthName, m_trustedAuthValue );
            }
        }

        /// <summary>
        /// Retrieve the form body to POST when requesting a token.
        /// </summary>
        public HttpContent GetTokenFormBody()
        {
            if( m_type == AuthType.Password )
            {
                return new FormUrlEncodedContent( new Dictionary<string, string>
                {
                    { "grant_type", HeaderConstants.GrantTypePassword },
                    { "username", m_userName },
                    { "password", m_password }
                } );
            }

            return new FormUrlEncodedContent( new Dictionary<string, string>
            {
                { "grant_type", HeaderConstants.GrantTypeTrusted },
                { "client_id", m_clientId },
                { "client_secret", m_clientSecret },
                { "name", m_trustedAuthName },
                { "value", m_trustedAuthValue }
            } );
        }

        private enum AuthType { Password, Trusted };

        private readonly AuthType m_type;

        // Password auth config.
        private readonly string m_userName;
        private readonly string m_password;

        // Trusted auth config.
        private readonly string m_clientId;
        private readonly string m_clientSecret;
        private readonly string m_trustedAuthName;
        private readonly string m_trustedAuthValue;
    }
}
