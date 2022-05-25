using System;
using System.Collections.Generic;
using System.Net.Http;
using EmsApi.Client.V2.Access;
using Microsoft.Extensions.Http;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Extensions.Http;
using Refit;

namespace EmsApi.Client.V2
{
    /// <summary>
    /// The client side representation of the EMS API.
    /// </summary>
    /// <remarks>
    /// This assembly targets .NET standard, which is supported on all
    /// flavors of .NET (Framework, Core, Xamarin, etc).
    /// </remarks>
    public class EmsApiService : IDisposable
    {
        /// <summary>
        /// Provides access to the EMS API. The <seealso cref="ServiceConfig"/> property
        /// must be set on this class before any API methods can be used.
        /// </summary>
        public EmsApiService()
        {
            Initialize( httpClientConfig: null );
            // As noted above ServiceConfig must be set before making any API calls.
        }

        /// <summary>
        /// Provides access to the EMS API using the provided configuration settings.
        /// The first and last handlers, if provided, will be added to the standard HttpMessageHandler
        /// stack used. This can be useful for tracing or testing purposes.
        /// </summary>
        public EmsApiService( EmsApiServiceConfiguration config, EmsApiServiceHttpClientConfiguration httpClientConfig = null )
        {
            Initialize( httpClientConfig );
            ServiceConfig = config;
        }

        /// <summary>
        /// Access to the swagger specification.
        /// </summary>
        public SwaggerAccess Swagger { get; set; }

        /// <summary>
        /// Access to ems-system routes.
        /// </summary>
        public EmsSystemAccess EmsSystem { get; set; }

        /// <summary>
        /// Access to securables routes.
        /// </summary>
        public EmsSecurablesAccess EmsSecurables { get; set; }

        /// <summary>
        /// Access to the admin securables routes.
        /// </summary>
        public AdminEmsSecurablesAccess AdminEmsSecurables { get; set; }

        /// <summary>
        /// Access to assets routes.
        /// </summary>
        public AssetsAccess Assets { get; set; }

        /// <summary>
        /// Access to trajectory routes.
        /// </summary>
        public TrajectoriesAccess Trajectories { get; set; }

        /// <summary>
        /// Access to APM profile routes.
        /// </summary>
        public ProfilesAccess Profiles { get; set; }

        /// <summary>
        /// Access to parameter-sets routes
        /// </summary>
        public ParameterSetsAccess ParameterSets { get; set; }

        /// <summary>
        /// Access to analytics routes.
        /// </summary>
        public AnalyticsAccess Analytics { get; set; }

        /// <summary>
        /// Access to the analytic sets routes.
        /// </summary>
        public AnalyticSetAccess AnalyticSets { get; set; }

        /// <summary>
        /// Access to database routes.
        /// </summary>
        public DatabaseAccess Databases { get; set; }

        /// <summary>
        /// Access to transfer (uploads) routes.
        /// </summary>
        public TransfersAccess Transfers { get; set; }

        /// <summary>
        /// Access to admin user routes.
        /// </summary>
        public AdminUserAccess AdminUser { get; set; }

        /// <summary>
        /// Access to flight identification information.
        /// </summary>
        public IdentificationAccess Identification { get; set; }

        /// <summary>
        /// The raw refit interface. This is internal and private set so that the
        /// access classes can use it without having to hold their own references,
        /// because this can change when the endpoint changes.
        /// </summary>
        internal IEmsApi RefitApi
        {
            get; private set;
        }

        private void InitializeHttpClient( EmsApiServiceHttpClientConfiguration httpClientConfig )
        {
            // If no client configuration was provided then generate a new one to get the default settings.
            httpClientConfig = httpClientConfig ?? new EmsApiServiceHttpClientConfiguration();

            // This builds up our message handler stack from back to front.
            // The nextHandler variable is what to assign to the InnerHandler of the latest handler created.
            HttpMessageHandler nextHandler = new HttpClientHandler
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip
            };

            if( httpClientConfig.LastHandler != null )
            {
                httpClientConfig.LastHandler.InnerHandler = nextHandler;
                nextHandler = httpClientConfig.LastHandler;
            }

            if( httpClientConfig.RetryTransientFailures )
            {
                // If we are configured to retry transient failures set that up now.
                // We add this *after* the MessageHandler in the stack so that we get retries on
                // both token request calls as well as normal API calls.
                // This will retry and 408 (request timeout), 5XX (server error), or network failures (HttpRequestException).
                var policyBuilder = HttpPolicyExtensions.HandleTransientHttpError();
                var policy = policyBuilder.WaitAndRetryAsync( new[]
                {
                    // Retry 3 times with slightly longer retry periods each time.
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10)
                } );
                nextHandler = new PolicyHttpMessageHandler( policy )
                {
                    InnerHandler = nextHandler
                };
            }

            nextHandler = m_messageHandler = new MessageHandler
            {
                InnerHandler = nextHandler
            };

            if( httpClientConfig.FirstHandler != null )
            {
                httpClientConfig.FirstHandler.InnerHandler = nextHandler;
                nextHandler = httpClientConfig.FirstHandler;
            }

            m_httpClient = new HttpClient( nextHandler )
            {
                Timeout = httpClientConfig.Timeout
            };
        }

        /// <summary>
        /// Sets up our API interface and access properties.
        /// </summary>
        private void Initialize( EmsApiServiceHttpClientConfiguration httpClientConfig )
        {
            m_cleanup = new List<Action>();
            m_authCallbacks = new List<Action<string>>();
            m_exceptionCallbacks = new List<Action<string>>();

            // Set up the HTTP client. This includes enabling automatic GZip decompression as the
            // EMS API will use that if requested.
            InitializeHttpClient( httpClientConfig );

            // Set up access properties for external clients to use.
            InitializeAccessProperties();

            // Subscribe to authentication failure events.
            m_messageHandler.AuthenticationFailedEvent += AuthenticationFailedHandler;
            m_cleanup.Add( () => m_messageHandler.AuthenticationFailedEvent -= AuthenticationFailedHandler );
        }

        /// <summary>
        /// Initializes all the known access classes, hooks their API method failed event,
        /// and adds a cleanup function to remove the event handler on shutdown.
        /// </summary>
        private void InitializeAccessProperties()
        {
            Swagger = InitializeAccessClass<SwaggerAccess>();
            EmsSystem = InitializeAccessClass<EmsSystemAccess>();
            EmsSecurables = InitializeAccessClass<EmsSecurablesAccess>();
            Assets = InitializeAccessClass<AssetsAccess>();
            Trajectories = InitializeAccessClass<TrajectoriesAccess>();
            Profiles = InitializeAccessClass<ProfilesAccess>();
            ParameterSets = InitializeAccessClass<ParameterSetsAccess>();
            Analytics = InitializeAccessClass<AnalyticsAccess>();
            AnalyticSets = InitializeAccessClass<AnalyticSetAccess>();
            Databases = InitializeAccessClass<DatabaseAccess>();
            Transfers = InitializeAccessClass<TransfersAccess>();
            AdminUser = InitializeAccessClass<AdminUserAccess>();
            AdminEmsSecurables = InitializeAccessClass<AdminEmsSecurablesAccess>();
            Identification = InitializeAccessClass<IdentificationAccess>();
        }

        private TAccess InitializeAccessClass<TAccess>() where TAccess : RouteAccess, new()
        {
            RouteAccess access = new TAccess();
            access.SetService( this );
            access.ApiMethodFailedEvent += ApiExceptionHandler;
            m_cleanup.Add( () => access.ApiMethodFailedEvent -= ApiExceptionHandler );

            return (TAccess)access;
        }

        /// <summary>
        /// Gets or sets the current service configuration. When setting this value, everything
        /// including the base <seealso cref="HttpClient"/> is re-configured, since the endpoint
        /// or authentication properties may have changed.
        /// </summary>
        public EmsApiServiceConfiguration ServiceConfig
        {
            get { return m_config; }
            set
            {
                ValidateConfigOrThrow( value );
                SetServiceConfigInternal( value );
            }
        }

        /// <summary>
        /// Internally sets the current service configuration without validation. This is used
        /// with the default constructor so that the user can specify a configuration using the
        /// ServiceConfig property without forcing a validation (because the default config is
        /// invalid without a credential).
        /// </summary>
        private void SetServiceConfigInternal( EmsApiServiceConfiguration config )
        {
            m_config = config;
            m_messageHandler.ServiceConfig = config;

            // Reset the default headers, they may have changed with the config.
            m_httpClient.DefaultRequestHeaders.Clear();
            m_config.AddDefaultRequestHeaders( m_httpClient.DefaultRequestHeaders );

            // See if the endpoint has changed.
            if( m_config.Endpoint != m_endpoint )
            {
                m_endpoint = m_config.Endpoint;

                // Reset the BaseAddress, and create a new refit service stub.
                // It's bound to the HttpClient's base address when it's constructed.
                m_httpClient.BaseAddress = new Uri( m_config.Endpoint );
                var refitSettings = new RefitSettings
                {
                    // In Refit v6 they switched to using System.Text.Json by default. We did not
                    // want to go through the hassle of converting our DTOs to that so we are
                    // going to still use Newtonsoft.
                    ContentSerializer = new NewtonsoftJsonContentSerializer()
                };
                RefitApi = RestService.For<IEmsApi>( m_httpClient, refitSettings );
            }
        }

        /// <summary>
        /// Returns true if the current service is authenticated for password authentication. If
        /// the service is not currently password authenticated, but has performed trusted
        /// authentication, this will return false. This is by design as this property was intended
        /// for use with password authentication only. Re-authentication may still be required if
        /// this returns true, as the token may have timed out or may time out by the time the next
        /// EMS API request is made.
        /// </summary>
        public bool Authenticated
        {
            get { return m_messageHandler.HasAuthenticatedWithPassword(); }
        }

        /// <summary>
        /// Returns true if the specified trusted name/value pair has authenticated.
        /// </summary>
        public bool HasAuthenticatedWithTrusted( string authName, string authValue )
        {
            return m_messageHandler.HasAuthenticatedWithTrusted( authName, authValue );
        }

        /// <summary>
        /// Manually initiate password authentication. Returns true if authentication succeeded,
        /// or false otherwise. Normally authentication is performed on the first API
        /// request or on the next request after the current token has timed out.
        /// </summary>
        public bool Authenticate()
        {
            return m_messageHandler.AuthenticateWithPassword();
        }

        /// <summary>
        /// Clear out the authentication token cache.
        /// </summary>
        public void ClearAuthenticationCache()
        {
            m_messageHandler.ClearAuthenticationCache();
        }

        /// <summary>
        /// Expires the entries in the autnetication cache.
        /// </summary>
        /// <remarks>
        /// This is intended for testing purposes. If you want to clear the authentication cache
        /// use <seealso cref="ClearAuthenticationCache"/>.
        /// </remarks>
        public void ExpireAuthenticationCacheEntries()
        {
            m_messageHandler.ExpireAuthenticationCacheEntries();
        }

        /// <summary>
        /// Registers a callback to be notified when API authentication fails. The callback will
        /// be executed for every authentication failure, even subsequent failures in rapid succession.
        /// All callbacks will be automatically unregistered when the service is disposed, or they may
        /// be manually unregistered with <seealso cref="UnregisterAuthFailedCallback( Action{ string } )"/>
        /// </summary>
        public void RegisterAuthFailedCallback( Action<string> callback )
        {
            m_authCallbacks.Add( callback );
        }

        /// <summary>
        /// Unregisters a callback for API authentication failed notifications.
        /// </summary>
        public void UnregisterAuthFailedCallback( Action<string> callback )
        {
            m_authCallbacks.Remove( callback );
        }

        /// <summary>
        /// Registers a callback to be notified when an API exception occurs. All callbacks will be automatically
        /// unregistered when the service is disposed, or they may
        /// </summary>
        public void RegisterApiExceptionCallback( Action<string> callback )
        {
            m_exceptionCallbacks.Add( callback );
        }

        /// <summary>
        /// Unregisters a callback for API authentication failed notifications.
        /// </summary>
        public void UnregisterApiExceptionCallback( Action<string> callback )
        {
            m_exceptionCallbacks.Remove( callback );
        }

        /// <summary>
        /// Free up used resources.
        /// </summary>
        public void Dispose()
        {
            // Perform cleanup actions.
            if( m_cleanup != null )
            {
                foreach( Action action in m_cleanup )
                    action();
            }

            if( m_messageHandler != null )
                m_messageHandler.Dispose();
        }

        /// <summary>
        /// Executed when an underlying API exception occurs.
        /// </summary>
        private void ApiExceptionHandler( object sender, ApiExceptionEventArgs args )
        {
            // Execute our callbacks. We don't handle exceptions since this is client code anyway.
            foreach( var callback in m_exceptionCallbacks )
                callback( args.Message );

            if( !m_config.ThrowExceptionOnApiFailure )
                return;

            if( !(args.Exception is ApiException apiEx) )
            {
                // If we got an EmsApiException already, then we should just rethrow that here.
                if( args.Exception is EmsApiException )
                    throw args.Exception;

                throw new EmsApiException( args.Exception.Message, args.Exception );
            }

            // Note: This object is a Dto.V2.Error, but in that class the messageDetail
            // field is marked as required, so it will not deserialize if the details
            // are not there. In many cases the details are empty, so we parse the json
            // manually instead.
            JObject details = null;
            try
            {
                details = JObject.Parse( apiEx.Content );
            }
            catch( Exception ) { }

            // We want the details if available.
            string message = null;
            if( details != null )
            {
                message = details.GetValue( "messageDetail" )?.ToString();
                if( string.IsNullOrEmpty( message ) )
                    message = details.GetValue( "message" )?.ToString();
            }

            if( string.IsNullOrEmpty( message ) )
                message = apiEx.Message;

            System.Diagnostics.Debug.WriteLine( "EMS API client encountered Refit.ApiException ({0}): {1}",
                args.ApiException.ReasonPhrase, message );

            throw new EmsApiException( message, args.Exception );
        }

        /// <summary>
        /// Executed when a API authentication fails.
        /// </summary>
        private void AuthenticationFailedHandler( object sender, AuthenticationFailedEventArgs args )
        {
            // Execute our callbacks. We don't handle exceptions since this is client code anyway.
            foreach( var callback in m_authCallbacks )
                callback( args.Message );

            if( m_config.ThrowExceptionOnAuthFailure )
                throw new EmsApiAuthenticationException( args.Message );

            System.Diagnostics.Debug.WriteLine( "EMS API client encountered authentication failure: {0}", args.Message );
        }

        /// <summary>
        /// Throws an exception if the given service configuration cannot be validated.
        /// </summary>
        private void ValidateConfigOrThrow( EmsApiServiceConfiguration config )
        {
            if( !config.Validate( out string error ) )
                throw new EmsApiConfigurationException( error );
        }

        /// <summary>
        /// The callbacks to execute when authentication fails.
        /// </summary>
        private List<Action<string>> m_authCallbacks;

        /// <summary>
        /// The callbacks to execute when a low level API exception occurs.
        /// </summary>
        private List<Action<string>> m_exceptionCallbacks;

        /// <summary>
        /// A list of actions to perform when the service is disposed.
        /// </summary>
        private List<Action> m_cleanup;

        /// <summary>
        /// The configuration for the service.
        /// </summary>
        private EmsApiServiceConfiguration m_config;

        /// <summary>
        /// The message handler, which handles authentication and other needs.
        /// </summary>
        private MessageHandler m_messageHandler;

        /// <summary>
        /// The current http client.
        /// </summary>
        private HttpClient m_httpClient;

        /// <summary>
        /// The last API endpoint specified. This is used to track when the
        /// endpoint changes, since we need to do a more thorough reset when
        /// that happens, due to the fact that the refit implementation gets
        /// bound to the endpoint url.
        /// </summary>
        private string m_endpoint = string.Empty;
    }
}
