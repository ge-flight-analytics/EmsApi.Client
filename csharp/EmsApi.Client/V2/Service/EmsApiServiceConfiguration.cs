using System;

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
        /// <param name="endpoint">
        /// The API endpoint to connect to. If this is not specified, a default
        /// value will be used.
        /// </param>
        /// <param name="useEnvVars">
        /// When true, system environment variables will be used to substitute certain
        /// parameters when the configuration is first constructed.
        /// </param>
        /// <remarks>
        /// </remarks>
        public EmsApiServiceConfiguration( string endpoint = EmsApiEndpoints.Default, bool useEnvVars = true )
        {
            Endpoint = endpoint;
            ThrowExceptionOnAuthFailure = true;
            ThrowExceptionOnApiFailure = true;

            if( useEnvVars )
                LoadEnvironmentVariables();
        }

        /// <summary>
        /// The API endpoint to connect to. This may be substituted by the "EmsApiEndpoint"
        /// environment variable.
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        /// The user name to use for authentication. This may be substituted by the "EmsApiUsername"
        /// environment variable.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The password to use for authentication. This may be substituted by the "EmsApiPassword"
        /// environment variable, which should contain a base64 encoded version of the password.
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
        /// When true, the <seealso cref="EmsApiService"/> will throw an exception for
        /// authentication failures. This is the default behavior, because opting out 
        /// of exceptions requires implementing additional callback functions. Callbacks
        /// are always executed regardless of this setting.
        /// </summary>
        public bool ThrowExceptionOnAuthFailure { get; set; }

        /// <summary>
        /// When true, the <seealso cref="EmsApiService"/> will throw an exception for
        /// any low level API failures. This is the default behavior, because opting out
        /// of exceptions requires implementing additional callback functions. Callbacks
        /// are always executed regardless of this setting.
        /// </summary>
        public bool ThrowExceptionOnApiFailure { get; set; }

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

        /// <summary>
        /// Loads some well-known environment variables into the current configuration.
        /// </summary>
        private void LoadEnvironmentVariables()
        {
            string endpoint = Environment.GetEnvironmentVariable( "EmsApiEndpoint" );
            string user = Environment.GetEnvironmentVariable( "EmsApiUsername" );
            string base64pass = Environment.GetEnvironmentVariable( "EmsApiPassword" );

            if( !string.IsNullOrWhiteSpace( endpoint ) )
                Endpoint = endpoint.Trim();

            if( !string.IsNullOrWhiteSpace( user ) )
                UserName = user.Trim();

            if( !string.IsNullOrWhiteSpace( base64pass ) )
            {
                byte[] passBytes = Convert.FromBase64String( base64pass.Trim() );
                Password = System.Text.Encoding.UTF8.GetString( passBytes );
            }
        }

        /// <summary>
        /// Retrun a copy of the configuration.
        /// </summary>
        /// <returns></returns>
        public EmsApiServiceConfiguration Clone()
        {
            return new EmsApiServiceConfiguration
            {
                Endpoint = this.Endpoint,
                UserName = this.UserName,
                Password = this.Password,
                TrustedToken = this.TrustedToken,
                ThrowExceptionOnApiFailure = this.ThrowExceptionOnApiFailure,
                ThrowExceptionOnAuthFailure = this.ThrowExceptionOnAuthFailure
            };
        }

    }
}
