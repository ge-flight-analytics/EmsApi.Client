using System;
using System.Net.Http;
using Refit;

using EmsApi.Client.V2.Wrappers;

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
        /// Provides access to the EMS API using the provided configuration settings.
        /// At a minimum, a username and password must be specified.
        /// </summary>
		public EmsApiService( EmsApiServiceConfiguration config )
        {
            m_config = config;

            string error;
            if( !m_config.Validate( out error ) )
                throw new InvalidApiConfigurationException( error );

            Initialize();
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

        public void Dispose()
        {
            if( m_apiClient != null )
                m_apiClient.Dispose();

            if( m_authHandler != null )
                m_authHandler.Dispose();
        }

        private IEmsApi m_api;
        private EmsApiServiceConfiguration m_config;
        private HttpClientHandler m_authHandler;
        private HttpClient m_apiClient;
    }
}
