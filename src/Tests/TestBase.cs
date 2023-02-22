using System;

using EmsApi.Client.V2;

//
// We expose some synchronous APIs which we test. This can lead to deadlocks in XUnit given the way
// it was designed (to help catch this sort of thing). Instead we are going to let it start as many
// threads as it wants. We could also solve this by setting `DisableParallelization` to be true, but
// then tests would take longer to run, and we don't want that.
//
// For a write-up of some of the gory details in this XUnit choice see this: https://github.com/xunit/xunit/issues/864
//
// This value CAN be overridden by test runner configuration, so be careful. Full details on that
// can be found here: https://xunit.net/docs/running-tests-in-parallel
//
[assembly: Xunit.CollectionBehavior( MaxParallelThreads = -1 )]

namespace EmsApi.Tests
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

            string apiClientId = Environment.GetEnvironmentVariable( "EmsApiTestClientId" );
            if( string.IsNullOrEmpty( apiClientId ) )
               throw new InvalidOperationException( "Test API Client Id not set" );

            string apiClientSecret = Environment.GetEnvironmentVariable( "EmsApiTestClientSecret" );
            if( string.IsNullOrEmpty( apiClientSecret ) )
               throw new InvalidOperationException( "Test API Client Secret not set" );

            m_config = new EmsApiServiceConfiguration( useEnvVars: false )
            {
                Endpoint = endpoint,
                UserName = user,
                Password = pass,
                ApiClientId = apiClientId,
                ApiClientSecret = apiClientSecret
            };
        }

        /// <summary>
        /// Returns a new instance of the EMS API service with a valid configuration
        /// (set by the EmsApiTest* environment variables) and a valid cached ems system
        /// id.
        /// </summary>
        protected static EmsApiService NewService( EmsApiServiceHttpClientConfiguration httpClientConfig = null )
        {
            return new EmsApiService( m_config.Clone(), httpClientConfig );
        }

        /// <summary>
        /// Returns a new instance of the EMS API service with a valid configuration
        /// that does not throw exceptions.
        /// </summary>
        protected static EmsApiService NewNoThrowService()
        {
            var config = m_config.Clone();
            config.ThrowExceptionOnApiFailure = false;
            config.ThrowExceptionOnAuthFailure = false;
            return new EmsApiService( config );
        }

        /// <summary>
        /// Returns a new instance of the EMS API service with an invalid login.
        /// </summary>
        protected static EmsApiService NewInvalidLoginService()
        {
            return new EmsApiService( GetInvalidLoginConfig() );
        }

        /// <summary>
        /// Returns a new instance of the EMS API service with a valid configuration
        /// that does not throw exceptions.
        /// </summary>
        protected static EmsApiService NewNoThrowInvalidLoginService()
        {
            var config = GetInvalidLoginConfig();
            config.ThrowExceptionOnApiFailure = false;
            config.ThrowExceptionOnAuthFailure = false;
            return new EmsApiService( config );
        }

        private static EmsApiServiceConfiguration GetInvalidLoginConfig()
        {
            EmsApiServiceConfiguration config = m_config.Clone();
            config.UserName = string.Format( "Inv@lidUser{0}", Guid.NewGuid() );
            return config;
        }

        protected static readonly EmsApiServiceConfiguration m_config;
    }
}
