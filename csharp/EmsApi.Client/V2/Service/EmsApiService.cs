using System;
using System.Collections.Generic;
using System.Net.Http;
using Refit;

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
        /// Sets up our API interface and access properties.
        /// </summary>
        private void Initialize()
        {
            m_cleanup = new List<Action>();
            m_authCallbacks = new List<Action<string>>();
            m_exceptionCallbacks = new List<Action<string>>();

            // Set up authentication.
            m_authHandler = new Authentication.EmsApiTokenHandler( m_config );
            m_apiClient = new HttpClient( m_authHandler );
            m_apiClient.BaseAddress = new Uri( m_config.Endpoint );

            // Set up the API client abstraction.
            m_api = RestService.For<IEmsApi>( m_apiClient );

            // Set up access properties.
            InitializeAccessProperties();

            // Subscribe to authentication failure events.
            m_authHandler.AuthenticationFailedEvent += AuthenticationFailedHandler;
            m_cleanup.Add( () => m_authHandler.AuthenticationFailedEvent -= AuthenticationFailedHandler );
        }

        /// <summary>
        /// Initializes all the known access classes, hooks their API method failed event,
        /// and adds a cleanup function to remove the event handler on shutdown.
        /// </summary>
        private void InitializeAccessProperties()
        {
            EmsSystems = InitializeAccessClass<EmsSystemsAccess>();
        }

        private TAccess InitializeAccessClass<TAccess>() where TAccess : EmsApiRouteAccess, new()
        {
            EmsApiRouteAccess access = new TAccess();
            access.SetInterface( m_api );
            access.ApiMethodFailedEvent += ApiExceptionHandler;
            m_cleanup.Add( () => access.ApiMethodFailedEvent -= ApiExceptionHandler );
            return (TAccess)access;
        }

        /// <summary>
        /// Gets or sets the current service configuration. If the endpoint or authorization
        /// properties change, <seealso cref="Authenticate"/> should be called.
        /// </summary>
        public EmsApiServiceConfiguration ServiceConfig
        {
            get { return m_config; }
            set
            {
                ValidateConfigOrThrow( value );
                m_config = value;
                m_apiClient.BaseAddress = new Uri( m_config.Endpoint );
                m_authHandler.UpdateConfiguration( m_config );
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
        /// Access to ems-system routes.
        /// </summary>
        public EmsSystemsAccess EmsSystems { get; private set; }

        /// <summary>
        /// Provides raw access the the API interface. Typically you would want
        /// to avoid using this directly, unless the functionality you need is
        /// not exposed by one of the specific properties on this class.
        /// </summary>
        public IEmsApi Raw
        {
            get { return m_api; }
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

            if( m_apiClient != null )
                m_apiClient.Dispose();

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

            if( m_config.ThrowExceptionOnApiFailure )
            {
                throw new EmsApiException( "An EMS API access exception occured, and the ThrowExceptionOnApiFailure setting is true.",
                    args.Exception );
            }

            if( args.ApiException != null )
            {
                System.Diagnostics.Debug.WriteLine( "EMS API client encountered Refit.ApiException ({0}): {1}",
                    args.ApiException.ReasonPhrase, args.ApiException.Message );
            }
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
            {
                throw new EmsApiAuthenticationException( string.Format(
                    "An EMS API authentication exceptionoc ured, and the ThrowExceptionOnAuthFailure setting is true: {0}",
                    args.Message ) );
            }

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

        private IEmsApi m_api;
        private EmsApiServiceConfiguration m_config;
        private Authentication.EmsApiTokenHandler m_authHandler;
        private HttpClient m_apiClient;
    }
}
