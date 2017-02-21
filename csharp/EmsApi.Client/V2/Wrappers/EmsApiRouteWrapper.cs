
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmsApi.Client.V2.Wrappers
{
    /// <summary>
    /// An API route wrapper is intended to convert the raw IEmsApi interface calls
    /// to something that is a little more natural to work with inside .NET applications.
    /// </summary>
    public abstract class EmsApiRouteWrapper
    {
        /// <summary>
        /// Creates a new instance of a route wrapper.
        /// </summary>
        /// <param name="api">
        /// The raw API interface to make calls to.
        /// </param>
        public EmsApiRouteWrapper( IEmsApi api )
        {
            m_api = api;
        }

        /// <summary>
        /// The reference to the raw api interface.
        /// </summary>
        protected IEmsApi m_api;

        /// <summary>
        /// Event that is fired whenever a low level API exception occurs.
        /// </summary>
        /// <remarks>
        /// This is static so that this same event will fire for all derived wrapper classes.
        /// </remarks>
        internal static event EventHandler<ApiExceptionEventArgs> ApiMethodFailedEvent;

        /// <summary>
        /// Checks the input enumerable for null, and returns an empty enumerable instead
        /// if it is null.
        /// </summary>
        /// <remarks>
        /// This is intended to help handle cases where an async call might return null
        /// when it fails.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of value that the enumerable returns.
        /// </typeparam>
        /// <param name="input">
        /// The enumerable to check.
        /// </param>
        protected static IEnumerable<T> SafeAccessEnumerable<T>( IEnumerable<T> input )
        {
            return input == null ? Enumerable.Empty<T>() : input;
        }

        /// <summary>
        /// Unpackages the result and turns any exceptions into events that may or may
        /// not throw exceptions (based on the settings).
        /// </summary>
        protected TRet ForwardAggregateExceptions<TRet>( Task<TRet> task )
        {
            try
            {
                return task.Result;
            }
            catch( AggregateException ex )
            {
                foreach( var inner in ex.InnerExceptions )
                {
                    if( inner is TaskCanceledException )
                        throw inner;

                    // Rethrow if it's already EmsApiException, or wrap it otherwise.
                    OnApiMethodFailed( new ApiExceptionEventArgs( ex ) );
                }
            }

            return default( TRet );
        }

        private void OnApiMethodFailed( ApiExceptionEventArgs args )
        {
            if( ApiMethodFailedEvent != null )
                ApiMethodFailedEvent( this, args );
        }
    }
}
