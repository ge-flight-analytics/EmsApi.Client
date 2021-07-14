
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmsApi.Client.V2.Access
{
    /// <summary>
    /// A route access class is intended to be the primary access method for an API
    /// route in the library. The three major goals of these classes are:
    /// 1) To provide access methods that are simple for a client of the library to use.
    /// 2) To provide both synchronous and asynchronous access methods.
    /// 3) To provide unified error handling for both asynchronous and synchronous methods.
    /// </summary>
    public abstract class RouteAccess
    {
        /// <summary>
        /// Creates a new instance of an access class.
        /// </summary>
        internal RouteAccess() { }

        /// <summary>
        /// Sets the service that this access class is working for. This must
        /// be called before accessing methods on the class.
        /// </summary>
        /// <param name="service">
        /// The service through which we call the raw API.
        /// </param>
        /// <remarks>
        /// This is here so we don't have to implement a constructor for each
        /// dervied access class.
        /// </remarks>
        internal void SetService( EmsApiService service )
        {
            m_service = service;
        }

        /// <summary>
        /// The service instance we are working for, which exposes the raw refit
        /// interface used to send requests.
        /// </summary>
        private EmsApiService m_service;

        /// <summary>
        /// Event that is fired whenever a low level API exception occurs.
        /// </summary>
        internal event EventHandler<ApiExceptionEventArgs> ApiMethodFailedEvent;

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
            return input ?? Enumerable.Empty<T>();
        }

        /// <summary>
        /// Unpackages the return value of the task as an enumerable that cannot be null.
        /// </summary>
        protected static IEnumerable<TEnum> SafeAccessEnumerableTask<TEnum>( Task<IEnumerable<TEnum>> task )
        {
            return SafeAccessEnumerable( AccessTaskResult( task ) );
        }

        /// <summary>
        /// Must be used by derived classes to access methods on the <seealso cref="IEmsApi"/>
        /// interface. This method adds continuation onto the task that automatically handles API
        /// interface exceptions. The exceptions are handled by forwarding all exceptions as events
        /// to the <seealso cref="ApiMethodFailedEvent"/>. From non-asynchronous code, these tasks
        /// should be accessed via <seealso cref="AccessTaskResult{TRet}(Task{TRet})"/> so that aggregate
        /// errors are converted to our own exception type.
        /// </summary>
        /// <param name="apiFunc">
        /// The delegate that needs to be run with exception safety.
        /// </param>
        /// <remarks>
        /// The point of this is to make sure all tasks from Refit automatically convert exceptions
        /// into the API failed event when they are evaluated.
        /// </remarks>
        protected Task<TRet> CallApiTask<TRet>( Func<IEmsApi, Task<TRet>> apiFunc )
        {
            return apiFunc( m_service.RefitApi ).ContinueWith( HandleApiException );
        }

        /// <summary>
        /// Must be used by derived classes to access methods on the <seealso cref="IEmsApi"/>
        /// interface. This method adds continuation onto the task that automatically handles API
        /// interface exceptions. The exceptions are handled by forwarding all exceptions as events
        /// to the <seealso cref="ApiMethodFailedEvent"/>.
        /// </summary>
        /// <param name="apiFunc">
        /// The delegate that needs to be run with exception safety.
        /// </param>
        /// <remarks>
        /// The point of this is to make sure all tasks from Refit automatically convert exceptions
        /// into the API failed event when they are evaluated.
        /// </remarks>
        protected Task CallApiTask( Func<IEmsApi, Task> apiFunc )
        {
            return apiFunc( m_service.RefitApi ).ContinueWith( HandleApiException );
        }

        /// <summary>
        /// Accesses the result of the task, rethrowing inner exceptions for aggregate
        /// exceptions that occur during task execution. Since all inner exceptions are
        /// handled by the <seealso cref="HandleApiException"/> callback, we should only
        /// ever see exceptions thrown as a result of <seealso cref="OnApiMethodFailed(ApiExceptionEventArgs)"/>
        /// calls, and task cancellation exceptions.
        /// </summary>
        protected static TRet AccessTaskResult<TRet>( Task<TRet> task )
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

            task.Exception.Handle( HandleTaskException );

            // This will normally return null, the caller can handle null if it wants.
            return default( TRet );
        }

        private void HandleApiException( Task task )
        {
            if( task.Exception == null )
                return;

            task.Exception.Handle( HandleTaskException );
        }

        private bool HandleTaskException( Exception ex )
        {
            // We don't handle task cancelled.
            if( ex is TaskCanceledException )
                return false;

            // We handle all other exceptions by firing the API exception event here. Depending
            // on the configuration, the EmsApiService class might swallow it, or it might get
            // unpackaged and thrown.
            OnApiMethodFailed( new ApiExceptionEventArgs( ex ) );
            return true;
        }

        private void OnApiMethodFailed( ApiExceptionEventArgs args )
        {
            ApiMethodFailedEvent?.Invoke( this, args );
        }
    }
}
