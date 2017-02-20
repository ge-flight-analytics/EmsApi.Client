using System;
using System.Collections.Generic;
using System.Net.Http;
using Refit;

using EmsApi.Client.V2.Wrappers;


namespace EmsApi.Client.V2
{
	using AuthCallbackDictionary = Dictionary<Action<string>, EventHandler<AuthenticationFailedEventArgs>>;
	using ExceptionCallbackDictionary = Dictionary<Action<string>, EventHandler<ApiExceptionEventArgs>>;

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
		/// Sets up our API interface and wrapper classes.
		/// </summary>
		private void Initialize()
		{
			// Set up our event maps.
			m_authCallbacks = new AuthCallbackDictionary();
			m_exceptionCallbacks = new ExceptionCallbackDictionary();

			// Set up authentication.
			m_authHandler = new Authentication.EmsApiTokenHandler( m_config );
			m_apiClient = new HttpClient( m_authHandler );
			m_apiClient.BaseAddress = new Uri( m_config.Endpoint );

			// Set up the API client abstraction.
			m_api = RestService.For<IEmsApi>( m_apiClient );

			// Set up wrapper properties for the interface.
			EmsSystems = new EmsSystemWrapper( m_api );

			// Subscribe to failure events.
			EmsApiRouteWrapper.ApiMethodFailedEvent += ApiExceptionHandler;
			m_authHandler.AuthenticationFailedEvent += AuthenticationFailedHandler;
		}

		/// <summary>
		/// Gets or sets the current service configuration. If the endpoint or authorization
		/// properties change, <seealso cref="RequestAuthentication"/> should be called.
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
        /// Manually request authentication. Normally authentication is performed on the first
        /// API request or whenever the current token times out. Authentication will automatically
		/// be handled on the next request, but this may be used to force it and wait for a failure
		/// callback.
        /// </summary>
        public bool RequestAuthentication()
        {
            return m_authHandler.Authenticate();
        }

        /// <summary>
        /// Access to EmsSystem routes.
        /// </summary>
        public EmsSystemWrapper EmsSystems { get; private set; }

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
			var handler = RegisterCallbackHandler( callback, m_authCallbacks );
			m_authHandler.AuthenticationFailedEvent += handler;
        }

		/// <summary>
		/// Unregisters a callback for API authentication failed notifications.
		/// </summary>
		public void UnregisterAuthFailedCallback( Action<string> callback )
		{
			var handler = UnregisterCallbackHandler( callback, m_authCallbacks );
			m_authHandler.AuthenticationFailedEvent -= handler;
		}

		/// <summary>
		/// Registers a callback to be notified when an API exception occurs. All callbacks will be automatically
		/// unregistered when the service is disposed, or they may
		/// </summary>
		public void RegisterApiExceptionCallback( Action<string> callback )
		{
			var handler = RegisterCallbackHandler( callback, m_exceptionCallbacks );
			EmsApiRouteWrapper.ApiMethodFailedEvent += handler;
		}

		/// <summary>
		/// Unregisters a callback for API authentication failed notifications.
		/// </summary>
		public void UnregisterApiExceptionCallback( Action<string> callback )
		{
			var handler = UnregisterCallbackHandler( callback, m_exceptionCallbacks );
			EmsApiRouteWrapper.ApiMethodFailedEvent -= handler;
		}

		/// <summary>
		/// Generates an event handler to connect an event from some other member of this class
		/// to the given callback function. Callbacks and handlers are stored in a dictionary
		/// so that they may be gracefully unregistered later.
		/// </summary>
		private EventHandler<TEventArgs> RegisterCallbackHandler<TEventArgs>( Action<string> callback, 
			Dictionary<Action<string>, EventHandler<TEventArgs>> callbackMap ) where TEventArgs : ApiEventArgs
		{
			var handler = new EventHandler<TEventArgs>( ( s, e ) => callback( e.Message ) );
			callbackMap.Add( callback, handler );
			return handler;
		}

		/// <summary>
		/// Removes a callback from our internal dictionaries, and returns the remaining event handler.
		/// </summary>
		private EventHandler<TEventArgs> UnregisterCallbackHandler<TEventArgs>( Action<string> callback,
			Dictionary<Action<string>, EventHandler<TEventArgs>> callbackMap ) where TEventArgs : ApiEventArgs
		{
			EventHandler<TEventArgs> handler;
			if( !callbackMap.TryGetValue( callback, out handler ) )
			{
				System.Diagnostics.Debug.Assert( false, "Could not unregister EMS API callback." );
				return null;
			}

			callbackMap.Remove( callback );
			return handler;
		}

		/// <summary>
		/// Free up used resources.
		/// </summary>
        public void Dispose()
        {
            if( m_apiClient != null )
                m_apiClient.Dispose();

			// Unregister authentication failed events.
			foreach( var callback in m_authCallbacks.Keys )
				UnregisterAuthFailedCallback( callback );

			// Unregister API exception events.
			foreach( var callback in m_exceptionCallbacks.Keys )
				UnregisterApiExceptionCallback( callback );

			// Unregister our own handler for API exceptions.
			m_authHandler.AuthenticationFailedEvent += AuthenticationFailedHandler;
			EmsApiRouteWrapper.ApiMethodFailedEvent -= ApiExceptionHandler;

			if( m_authHandler != null )
                m_authHandler.Dispose();
        }

		/// <summary>
		/// Executed when an underlying API exception occurs.
		/// </summary>
		private void ApiExceptionHandler( object sender, ApiExceptionEventArgs args )
		{
			if( m_config.ThrowExceptionOnApiFailure )
			{
				throw new EmsApiServiceException( "An EMS API access exception occured, and the ThrowExceptionOnApiFailure setting is true.",
					args.Exception );
			}

			if( args.ApiException != null )
			{
				System.Diagnostics.Debug.WriteLine( "EMS API client encountered Refit.ApiException ({0}): {1}",
					args.ApiException.ReasonPhrase, args.ApiException.Message );
			}

			System.Diagnostics.Debug.Assert( false, "API access exception." );
		}

		/// <summary>
		/// Executed when a API authentication fails.
		/// </summary>
		private void AuthenticationFailedHandler( object sender, AuthenticationFailedEventArgs args )
		{
			if( m_config.ThrowExceptionOnAuthFailure )
			{
				throw new EmsApiServiceException( string.Format(
					"An EMS API authentication exception occured, and the ThrowExceptionOnAuthFailure setting is true: {0}",
					args.Message ) );
			}

			System.Diagnostics.Debug.WriteLine( "EMS API client encountered authentication failure: {0}", args.Message );
			System.Diagnostics.Debug.Assert( false, "API authentication failure." );
		}

		/// <summary>
		/// Throws an exception if the given service configuration cannot be validated.
		/// </summary>
		private void ValidateConfigOrThrow( EmsApiServiceConfiguration config )
		{
			string error;
			if( !config.Validate( out error ) )
				throw new InvalidApiConfigurationException( error );
		}

		/// <summary>
		/// The authentication failed callbacks that have ben registered through 
		/// <seealso cref="RegisterAuthFailedCallback( Action{ string } )"/>.
		/// This dictionary is keyed by the function the user passes in as the callback,
		/// and the value is an event handler wrapped around the callback function.
		/// </summary>
		private AuthCallbackDictionary m_authCallbacks;

		/// <summary>
		/// The API exception callbacks.
		/// </summary>
		private ExceptionCallbackDictionary m_exceptionCallbacks;

		private IEmsApi m_api;
        private EmsApiServiceConfiguration m_config;
        private Authentication.EmsApiTokenHandler m_authHandler;
        private HttpClient m_apiClient;
	}
}
