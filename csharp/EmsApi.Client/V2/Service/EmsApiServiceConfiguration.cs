
namespace EmsApi.Client.V2
{
	/// <summary>
	/// The configuration to use when talking to the EMS API. This may be
	/// modified after the <seealso cref="EmsApiService"/> has been created
	/// by setting the <seealso cref="EmsApiService.ServiceConfig"/> property.
	/// </summary>
    public class EmsApiServiceConfiguration
    {
		/// <summary>
		/// Creates a new instance of the configuration with the given endpoint.
		/// </summary>
		/// <param name="apiEndpoint">
		/// The API endpoint to connect to. If this is not specified, a default
		/// value will be used.
		/// </param>
        public EmsApiServiceConfiguration( string apiEndpoint = EmsApiEndpoints.Default )
        {
            Endpoint = apiEndpoint;
        }

        /// <summary>
        /// The API endpoint to connect to.
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        /// The user name to use for authentication.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The password to use for authentication.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The trusted token to use for authentication.
        /// </summary>
        /// <remarks>
        /// This will always be overridden by the username / password.
        /// </remarks>
        public string TrustedToken { get; set; }

        /// <summary>
        /// Returns true if authentication should use the trusted token, false otherwise.
        /// </summary>
        public bool UseTrustedToken()
        {
            return string.IsNullOrEmpty( UserName );
        }

        /// <summary>
        /// Returns true if the configuration is valid, or false if not.
        /// </summary>
        /// <param name="error">
        /// The reason the configuration is invalid.
        /// </param>
        public bool Validate( out string error )
        {
            error = null;

            // Make sure the user did not set the endpoint back to null.
            if( string.IsNullOrEmpty( Endpoint ) )
            {
                error = "The API endpoint is not set.";
                return false;
            }

            // Make sure we have a username / password OR token.
            if( !string.IsNullOrEmpty( UserName ) )
            {
                if( string.IsNullOrEmpty( Password ) )
                {
                    error = "A password was not provided for the given username.";
                    return false;
                }

                return true;
            }

            if( string.IsNullOrEmpty( TrustedToken ) )
            {
                error = "Either a username and password or a trusted token must be provided.";
                return false;
            }

            return true;
        }
    }
}
