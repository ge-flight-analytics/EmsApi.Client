using System;
using System.Collections.Generic;
using System.Net.Http;
using Refit;

using EmsApi.Client.V2.Wrappers;

using AuthEventArgs = EmsApi.Client.V2.Authentication.AuthenticationFailedEventArgs;

namespace EmsApi.Client.V2
{
	using CallbackDictionary = Dictionary<Action<string>, EventHandler<AuthEventArgs>>;

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
            // This intentionally does not use the property so we don't try
            // to validate.
            m_config = new EmsApiServiceConfiguration();
			m_authCallbacks = new CallbackDictionary();
            Initialize();
        }

        /// <summary>
        /// Provides access to the EMS API using the provided configuration settings.
        /// At a minimum, a username and password must be specified.
        /// </summary>
		public EmsApiService( EmsApiServiceConfiguration config )
        {
            ServiceConfig = config;
			m_authCallbacks = new CallbackDictionary();
			Initialize();
		}

		/// <summary>
		/// Sets up our API interface and wrapper classes.
		/// </summary>
		private void Initialize()
		{
			// Set up authentication.
			m_authHandler = new Authentication.EmsApiTokenHandler( m_config );
			m_apiClient = new HttpClient( m_authHandler );
			m_apiClient.BaseAddress = new Uri( m_config.Endpoint );

			// Set up the API client abstraction.
			m_api = RestService.For<IEmsApi>( m_apiClient );

			// Set up wrapper properties for the interface.
			EmsSystems = new EmsSystemWrapper( m_api );
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
                string error;
                if( !value.Validate( out error ) )
                    throw new InvalidApiConfigurationException( error );

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
		/// <seealso cref="System.Net.HttpStatusCode.Unauthorized"/> will be returned immediately for 
		/// each request that cannot authorize. All callbacks will be automatically unregistered when 
		/// the service is disposed, or they may be manually unregistered with 
		/// <seealso cref="UnregisterAuthFailedCallback( Action{ string } )"/>
		/// </summary>
		public void RegisterAuthFailedCallback( Action<string> callback )
        {
            var handler = new EventHandler<AuthEventArgs>( ( s, e ) => callback( e.Message ) );
			m_authCallbacks.Add( callback, handler );
            m_authHandler.AuthenticationFailed += handler;
        }

		/// <summary>
		/// Unregisters a callback for API authentication failed notifications.
		/// </summary>
		public void UnregisterAuthFailedCallback( Action<string> callback )
		{
			EventHandler<AuthEventArgs> handler;
			if( !m_authCallbacks.TryGetValue( callback, out handler ) )
			{
				System.Diagnostics.Debug.Assert( false, "Could not unregister EMS API authentication callback." );
				return;
			}

			m_authHandler.AuthenticationFailed -= handler;
			m_authCallbacks.Remove( callback );
		}

        public void Dispose()
        {
            if( m_apiClient != null )
                m_apiClient.Dispose();

			// Unregister remaining authentication failed callbacks.
			foreach( var handler in m_authCallbacks.Values )
				m_authHandler.AuthenticationFailed -= handler;

            if( m_authHandler != null )
                m_authHandler.Dispose();
        }

		/// <summary>
		/// The authentication failed callbacks that have ben registered through 
		/// <seealso cref="RegisterAuthFailedCallback( Action{ string } )"/>.
		/// This dictionary is keyed by the function the user passes in as the callback,
		/// and the value is an event handler wrapped around the callback function.
		/// </summary>
		private CallbackDictionary m_authCallbacks;

		private IEmsApi m_api;
        private EmsApiServiceConfiguration m_config;
        private Authentication.EmsApiTokenHandler m_authHandler;
        private HttpClient m_apiClient;
	}
}
