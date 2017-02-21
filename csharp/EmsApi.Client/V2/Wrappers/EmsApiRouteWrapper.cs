
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
        /// The reference to the raw api interface. This is private so that derived classes 
        /// are required to call <seealso cref="ContinueWithExceptionSafety{TRet}(Func{IEmsApi, Task{TRet}})"/>
        /// in order to access the interface.
        /// </summary>
        private IEmsApi m_api;

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
        /// Adds a continuation onto the task that automatically handles API interface exceptions.
        /// The exceptions are handled by forwarding all exceptions as events to the 
        /// <seealso cref="ApiMethodFailedEvent"/>. From non-asynchronous code, these tasks should
        /// be accessed via <seealso cref="AccessTaskResult{TRet}(Task{TRet})"/> so that aggregate
        /// errors are converted to our own exception type.
        /// </summary>
        /// <param name="apiFunc">
        /// The delegate that needs to be run with exception safety.
        /// </param>
        /// <remarks>
        /// The point of this is to make sure all tasks from Refit automatically convert exceptions 
        /// into the API failed event when they are evaluated. 
        /// </remarks>
        protected Task<TRet> ContinueWithExceptionSafety<TRet>( Func<IEmsApi, Task<TRet>> apiFunc )
        {
            var api = apiFunc( m_api );
            var safeTask = api.ContinueWith( HandleApiException );
            return safeTask;
        }

        /// <summary>
        /// Accesses the result of the task, rethrowing inner exceptions for aggregate
        /// exceptions that occur during task execution. Since all inner exceptions are 
        /// handled by the <seealso cref="HandleApiException"/> callback, we should only 
        /// ever see exceptions thrown as a result of <seealso cref="OnApiMethodFailed(ApiExceptionEventArgs)"/>
        /// calls, and task cancellation exceptions.
        /// </summary>
        protected TRet AccessTaskResult<TRet>( Task<TRet> task )
        {
            try
            {
                return task.Result;
            }
            catch( AggregateException ex )
            {
                foreach( Exception inner in ex.InnerExceptions )
                    throw inner;
            }

            return default( TRet );
        }

        private TRet HandleApiException<TRet>( Task<TRet> task )
        {
            if( task.Exception == null )
                return task.Result;

            task.Exception.Handle( ex =>
            {
                // We don't handle task cancelled.
                if( ex is TaskCanceledException )
                    return false;

                // We handle all other exceptions by firing the API exception event here. Depending 
                // on the configuration, the EmsApiService class might swallow it, or it might get
                // unpackaged and thrown.
                OnApiMethodFailed( new ApiExceptionEventArgs( ex ) );
                return true;
            } );

            // This will normally return null, the caller can handle null if it wants.
            return default( TRet );
        }

        private void OnApiMethodFailed( ApiExceptionEventArgs args )
        {
            if( ApiMethodFailedEvent != null )
                ApiMethodFailedEvent( this, args );
        }
    }
}
