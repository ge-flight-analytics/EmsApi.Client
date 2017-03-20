
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
    public abstract class EmsApiRouteAccess
    {
        /// <summary>
        /// Creates a new instance of an access class.
        /// </summary>
        internal EmsApiRouteAccess() { }

        /// <summary>
        /// Sets the interface instance used by the access class. This must
        /// be called before accessing methods on the class.
        /// </summary>
        /// <param name="api">
        /// The API interface to call into.
        /// </param>
        /// <remarks>
        /// This is here so that the service class can use the default constructor,
        /// then set the interface afterwards.
        /// </remarks>
        internal void SetInterface( IEmsApi api )
        {
            m_api = api;
        }

        /// The reference to the raw api interface. This is private get so that derived classes
        /// are required to call <seealso cref="CallApiTask{TRet}(Func{IEmsApi, Task{TRet}})"/>
        /// in order to access the interface.
        private IEmsApi m_api;

        /// <summary>
        /// Event that is fired whenever a low level API exception occurs.
        /// </summary>
        /// <remarks>
        /// This is static so that this same event will fire for all derived wrapper classes.
        /// </remarks>
        internal event EventHandler<ApiExceptionEventArgs> ApiMethodFailedEvent;

        /// <summary>
        /// Converts an enum value into a string suitable for an API method call.
        /// </summary>
        protected static string MakeEnumString<TEnum>( Enum value )
        {
            return Enum.GetName( typeof( TEnum ), value ).ToLower();
        }

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

    /// <summary>
    /// Adds some additional functions for setting and accessing a cached EMS server id.
    /// </summary>
    public abstract class CachedEmsIdRouteAccess : EmsApiRouteAccess
    {
        /// <summary>
        /// A special id to use when an EMS system ID is not being provided for a method call.
        /// </summary>
        public const int NoEmsServerSpecified = -1;

        /// <summary>
        /// Allows the library user to set a particular EMS system id and have all subsequent
        /// method calls refer to that system.
        /// </summary>
        /// <param name="emsSystemId">
        /// The unique identifier of the EMS system to cache.
        /// </param>
        internal void SetEmsSystemId( int emsSystemId )
        {
            m_emsId = emsSystemId;
        }

        /// <summary>
        /// Returns the proper EMS system to access for the method call.
        /// </summary>
        /// <param name="inputArg">
        /// The value for the emsSystemId parameter that was passed to the method originally.
        /// </param>
        /// <returns></returns>
        protected int GetEmsSystemForMethodCall( int inputArg )
        {
            if( inputArg == NoEmsServerSpecified )
            {
                // Make sure we have a cached value or throw an exception.
                if( m_emsId == NoEmsServerSpecified )
                    throw new EmsApiException( "No EMS server was specified for the method call, and a cached server has not been set." );

                return m_emsId;
            }

            return inputArg;
        }

        private int m_emsId = NoEmsServerSpecified;
    }
}
