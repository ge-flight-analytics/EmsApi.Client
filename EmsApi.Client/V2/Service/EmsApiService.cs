using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Refit;
using Newtonsoft.Json.Linq;

using EmsApi.Client.V2.Access;

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
            m_config = new EmsApiServiceConfiguration();
            Initialize();
        }

        /// <summary>
        /// Provides access to the EMS API using the provided configuration settings.
        /// At a minimum, a username and password must be specified.
        /// </summary>
        public EmsApiService( EmsApiServiceConfiguration config )
        {
            ValidateConfigOrThrow( config );
            m_config = config;
            Initialize();
        }

        /// <summary>
        /// Access to the swagger specification.
        /// </summary>
        public SwaggerAccess Swagger { get; private set; }

        /// <summary>
        /// Access to ems-system routes.
        /// </summary>
        public EmsSystemsAccess EmsSystems { get; private set; }

        /// <summary>
        /// Access to assets routes.
        /// </summary>
        public AssetsAccess Assets { get; private set; }

        /// <summary>
        /// Access to trajectory routes.
        /// </summary>
        public TrajectoriesAccess Trajectories { get; private set; }

        /// <summary>
        /// Access to APM profile routes.
        /// </summary>
        public ProfilesAccess Profiles { get; private set; }

        /// <summary>
        /// Access to parameter-sets routes
        /// </summary>
        public ParameterSetsAccess ParameterSets { get; private set; }

        /// <summary>
        /// Access to analytics routes.
        /// </summary>
        public AnalyticsAccess Analytics { get; private set; }

        /// <summary>
        /// Access to database routes.
        /// </summary>
        public DatabaseAccess Databases { get; private set; }

        /// <summary>
        /// The current EMS system that the service is operating on. This value may
        /// be set to exclude it from access methods that need an EMS system specified.
        /// </summary>
        public int CachedEmsSystem
        {
            get { return m_cachedEmsSystemId; }
            set { SetCachedEmsSystem( value ); }
        }

        /// <summary>
        /// The HTTP client used by the API. This is normally not access directly, but
        /// can be used to let the library handle headers and authentication while sending
        /// your own requests manually.
        /// </summary>
        public HttpClient HttpClient { get; private set; }

        /// <summary>
        /// Sets up our API interface and access properties.
        /// </summary>
        private void Initialize()
        {
            m_cleanup = new List<Action>();
            m_authCallbacks = new List<Action<string>>();
            m_exceptionCallbacks = new List<Action<string>>();

            // Set up all the client services we need to use.
            AllocateClients();

            // Set up access properties for extenal clients to use.
            InitializeAccessProperties();

            // Subscribe to authentication failure events.
            m_authHandler.AuthenticationFailedEvent += AuthenticationFailedHandler;
            m_cleanup.Add( () => m_authHandler.AuthenticationFailedEvent -= AuthenticationFailedHandler );
        }

        /// <summary>
        /// Allocates all of the background clients and services needed to function. Thsi will instantiate
        /// a new authentication handler, use that to create a new HttpClient, set some default headers, and
        /// then allocate a new Refit interface implementation.
        /// </summary>
        private void AllocateClients()
        {
            m_authHandler = new Authentication.EmsApiTokenHandler( m_config );
            HttpClient = new HttpClient( m_authHandler );
            HttpClient.BaseAddress = new Uri( m_config.Endpoint );
            m_config.AddDefaultRequestHeaders( HttpClient.DefaultRequestHeaders );
            m_api = RestService.For<IEmsApi>( HttpClient );
        }

        /// <summary>
        /// Initializes all the known access classes, hooks their API method failed event,
        /// and adds a cleanup function to remove the event handler on shutdown.
        /// </summary>
        private void InitializeAccessProperties()
        {
            m_accessors = new List<EmsApiRouteAccess>();
            Swagger = InitializeAccessClass<SwaggerAccess>();
            EmsSystems = InitializeAccessClass<EmsSystemsAccess>();
            Assets = InitializeAccessClass<AssetsAccess>();
            Trajectories = InitializeAccessClass<TrajectoriesAccess>();
            Profiles = InitializeAccessClass<ProfilesAccess>();
            ParameterSets = InitializeAccessClass<ParameterSetsAccess>();
            Analytics = InitializeAccessClass<AnalyticsAccess>();
            Databases = InitializeAccessClass<DatabaseAccess>();
        }

        private TAccess InitializeAccessClass<TAccess>() where TAccess : EmsApiRouteAccess, new()
        {
            EmsApiRouteAccess access = new TAccess();
            access.SetInterface( m_api );
            access.ApiMethodFailedEvent += ApiExceptionHandler;
            m_cleanup.Add( () => access.ApiMethodFailedEvent -= ApiExceptionHandler );
            m_accessors.Add( access );

            return (TAccess)access;
        }

        private void SetCachedEmsSystem( int newId )
        {
            if( newId <= 0 )
                throw new EmsApiException( "The cached EMS system id must be greater than 0." );

            m_cachedEmsSystemId = newId;
            m_accessors.OfType<CachedEmsIdRouteAccess>().ToList()
                .ForEach( a => a.SetEmsSystemId( m_cachedEmsSystemId ) );
        }

        /// <summary>
        /// Gets or sets the current service configuration. When setting this value, everything
        /// including the base <seealso cref="HttpClient"/> is re-allocated, since the endpoint
        /// or authentication properties may have changed.
        /// </summary>
        public EmsApiServiceConfiguration ServiceConfig
        {
            get { return m_config; }
            set
            {
                ValidateConfigOrThrow( value );
                m_config = value;
                AllocateClients();
            }
        }

        /// <summary>
        /// Returns true if the current service is authenticated.
        /// </summary>
        public bool Authenticated
        {
            get { return m_authHandler.Authenticated; }
        }

        /// <summary>
        /// Manually initiate authentication. Returns true if authentication succeeded,
        /// or false otherwise. Normally authentication is performed on the first API
        /// request or on the next request whenever the current token times out.
        /// </summary>
        public bool Authenticate()
        {
            return m_authHandler.Authenticate();
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

            if( m_authHandler != null )
                m_authHandler.Dispose();
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

            var apiEx = args.Exception as ApiException;
            if( apiEx == null )
                throw new EmsApiException( args.Exception.Message, args.Exception );

            JObject details = JObject.Parse( apiEx.Content );

            // We want the details if available.
            string message = details.GetValue( "messageDetail" )?.ToString();

            if( message == null )
                message = details.GetValue( "message" )?.ToString();

            if( message == null )
                message = "An unknown API exception occurred.";

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
            string error;
            if( !config.Validate( out error ) )
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
        /// References to the Access classes exposed by this service, so that
        /// we can perform operations on them iteratively.
        /// </summary>
        private List<EmsApiRouteAccess> m_accessors;

        /// <summary>
        /// The current value for the cached ems system id.
        /// </summary>
        private int m_cachedEmsSystemId;

        private IEmsApi m_api;
        private EmsApiServiceConfiguration m_config;
        private Authentication.EmsApiTokenHandler m_authHandler;
    }
}
