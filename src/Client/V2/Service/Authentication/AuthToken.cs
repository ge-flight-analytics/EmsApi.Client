using System;

namespace EmsApi.Client.V2
{
    /// <summary>
    /// An authentication token and related information.
    /// </summary>
    internal class AuthToken
    {
        public AuthToken( string token, int secondsUntilExpiration )
        {
            Token = token;

            // Generate a timepoint for when this token expires. We subtract a minute to attempt to
            // account for some time having elapsed from when the server returned the token until
            // we compute the expiration time.
            ExpiresOn = DateTime.UtcNow.AddSeconds( secondsUntilExpiration - 60 );
        }

        /// <summary>
        /// The token itself, which can be passed to the EMS API
        /// </summary>
        public string Token { get; private set; }

        /// <summary>
        /// When this token will expire.
        /// </summary>
        public DateTime ExpiresOn { get; private set; }

        /// <summary>
        /// Returns true if the token is valid (unexpired), false otherwise.
        /// </summary>
        public bool Valid
        {
            get
            {
                return DateTime.UtcNow < ExpiresOn;
            }
        }

        /// <summary>
        /// Mark the token as being expired.
        /// </summary>
        /// <remarks>
        /// This is intended for testing purposes.
        /// </remarks>
        public void Expire()
        {
            ExpiresOn = DateTime.UtcNow;
        }
    }
}
