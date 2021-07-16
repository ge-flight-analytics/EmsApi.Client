using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

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
        /// <param name="useEnvVars">
        /// When true, system environment variables will be used to substitute certain
        /// parameters when the configuration is first constructed.
        /// </param>
        public EmsApiServiceConfiguration( bool useEnvVars = true )
        {
            UseCompression = true;
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
        /// The API Client id to use for trusted authentication. This may be substituted by the "EmsApiClientId"
        /// environment variable.
        /// 
        /// You will need to also set ApiClientSecret and a TrustedAuthName and TrustedAuthValue in the CallContext
        /// (although TrustedAuthName can be set here in config if it is the same for this service configuration).
        ///
        /// You can set BOTH UserName+Password AND ApiClientId+ApiClientSecret. If you do that then any CallContexts
        /// which do NOT include TrustedAuthValue being set will use the Password Authentication path (and the
        /// username and password). Any CallContexts which DO include TrustedAuthValue being set will use the Trusted
        /// Authentication path.
        /// </summary>
        public string ApiClientId { get; set; }

        /// <summary>
        /// The API Client secret to use for trusted authentication. This may be substituted by the "EmsApiClientSecret"
        /// environment variable, which should contain a base64 encoded version of the client secret.
        /// See <seealso cref="ApiClientId"/> for other trusted authentication requirements.
        /// </summary>
        public string ApiClientSecret { get; set; }

        /// <summary>
        /// The property name to search in EFOQA classic AD as part of trusted authentication. This may be substituted
        /// by the "EmsApiTrustedAuthName" environment variable.
        /// This can instead be set in the <seealso cref="CallContext.TrustedAuthName"/> if you wish to make it a
        /// per-call setting. That setting will override whatever is set here.
        /// </summary>
        public string TrustedAuthName { get; set; }

        /// <summary>
        /// The application name to pass along to the EMS API. This is used for logging on the
        /// server side.
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// The user agent header to pass along to the EMS API. 
        /// </summary>
        public string UserAgent
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                return $"EmsApi.Client v{version.Major}.{version.Minor}.{version.Build}";
            }
        }

        /// <summary>
        /// When true, gzip compression will be used for responses on routes that support it.
        /// This is enabled by default. Responses are automatically decompressed by the library,
        /// so there's no advantage to disabling this unless you are running in a CPU constrained
        /// scenario.
        /// </summary>
        public bool UseCompression { get; set; }

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
        /// Any customer headers that should be appended to a request. These are appended
        /// at the time of making the request so they can be altered on a per request basis.
        /// This is a good place to set the "X-Adi-Client-Username" and "X-Adi-Correlation-Id"
        /// headers. It's up to the application performing the requests to add and remove these
        /// headers.
        /// </summary>
        public Dictionary<string, string> CustomHeaders { get; set; }

        /// <summary>
        /// Adds the default headers into the given header collection.
        /// </summary>
        public void AddDefaultRequestHeaders( HttpRequestHeaders headerCollection )
        {
            headerCollection.Add( HttpHeaderNames.UserAgent, UserAgent );

            // Optional application name.
            if( !string.IsNullOrEmpty( ApplicationName ) )
                headerCollection.Add( HttpHeaderNames.ApplicationName, ApplicationName );

            // Optional compression header.
            if( UseCompression )
                headerCollection.Add( HttpHeaderNames.AcceptEncoding, "gzip" );
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

            // Make sure if we have a username we have a password.
            bool usernameSet = !string.IsNullOrEmpty( UserName );
            if( usernameSet )
            {
                if( string.IsNullOrEmpty( Password ) )
                {
                    error = "A password was not provided for the given username.";
                    return false;
                }
            }

            // Make sure if we have a client id that we have a client secret.
            bool clientIdSet = !string.IsNullOrEmpty( ApiClientId );
            if( clientIdSet )
            {
                if( string.IsNullOrEmpty( ApiClientSecret ) )
                {
                    error = "An API client secret was not provided for the given API client id.";
                    return false;
                }
            }

            // Validate we have some form of authentication specified.
            if( !usernameSet && !clientIdSet )
            {
                error = "Either a username and password or API client id and secret must be provided.";
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
            string clientId = Environment.GetEnvironmentVariable( "EmsApiClientId" );
            string base64ClientSecret = Environment.GetEnvironmentVariable( "EmsApiClientSecret" );
            string clientTrustedAuthName = Environment.GetEnvironmentVariable( "EmsApiTrustedAuthName" );

            if( !string.IsNullOrWhiteSpace( endpoint ) )
                Endpoint = endpoint.Trim();

            if( !string.IsNullOrWhiteSpace( user ) )
                UserName = user.Trim();

            if( !string.IsNullOrWhiteSpace( base64pass ) )
            {
                byte[] passBytes = Convert.FromBase64String( base64pass.Trim() );
                Password = System.Text.Encoding.UTF8.GetString( passBytes );
            }

            if( !string.IsNullOrWhiteSpace( clientId ) )
                ApiClientId = user.Trim();

            if( !string.IsNullOrWhiteSpace( base64ClientSecret ) )
            {
                byte[] secretBytes = Convert.FromBase64String( base64ClientSecret.Trim() );
                ApiClientSecret = System.Text.Encoding.UTF8.GetString( secretBytes );
            }

            if( !string.IsNullOrWhiteSpace( clientTrustedAuthName ) )
                TrustedAuthName = clientTrustedAuthName.Trim();
        }

        /// <summary>
        /// Retrun a copy of the configuration.
        /// </summary>
        public EmsApiServiceConfiguration Clone()
        {
            return new EmsApiServiceConfiguration
            {
                Endpoint = Endpoint,
                UserName = UserName,
                Password = Password,
                ApiClientId = ApiClientId,
                ApiClientSecret = ApiClientSecret,
                TrustedAuthName = TrustedAuthName,
                ApplicationName = ApplicationName,
                ThrowExceptionOnApiFailure = ThrowExceptionOnApiFailure,
                ThrowExceptionOnAuthFailure = ThrowExceptionOnAuthFailure,
                CustomHeaders = CustomHeaders
            };
        }

    }
}
