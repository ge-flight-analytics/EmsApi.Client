using System;
using System.Collections.Generic;
using System.Text;

using EmsApi.Client.V2;

namespace EmsApi.Client.Tests
{
    /// <summary>
    /// Base class for tests that provides a valid test API endpoint.
    /// </summary>
    public abstract class TestBase
    {
        static TestBase()
        {
            string endpoint = Environment.GetEnvironmentVariable( "EmsApiTestEndpoint" );
            if( string.IsNullOrEmpty( endpoint ) )
                throw new InvalidOperationException( "Test API Endpoint not set" );

            string user = Environment.GetEnvironmentVariable( "EmsApiTestUsername" );
            if( string.IsNullOrEmpty( user ) )
                throw new InvalidOperationException( "Test API Username not set" );

            string pass = Environment.GetEnvironmentVariable( "EmsApiTestPassword" );
            if( string.IsNullOrEmpty( pass ) )
                throw new InvalidOperationException( "Test API Password not set" );

            m_config = new EmsApiServiceConfiguration( useEnvVars: false )
            {
                Endpoint = endpoint,
                UserName = user,
                Password = pass
            };

            m_rand = new Random();
        }

        /// <summary>
        /// Returns a new instance of the EMS API service with a valid configuration
        /// (set by the EmsApiTest* environment variables).
        /// </summary>
        protected static EmsApiService NewService()
        {
            return new EmsApiService( m_config.Clone() );
        }

        /// <summary>
        /// Returns a new instance of the EMS API service with an invalid login.
        /// </summary>
        protected static EmsApiService NewInvalidLoginService()
        {
            EmsApiServiceConfiguration config = m_config.Clone();
            config.UserName = string.Format( "Inv@lidUser{0}", Guid.NewGuid() );
            return new EmsApiService( config );
        }

        private static readonly Random m_rand;
        private static readonly EmsApiServiceConfiguration m_config;
    }
}
