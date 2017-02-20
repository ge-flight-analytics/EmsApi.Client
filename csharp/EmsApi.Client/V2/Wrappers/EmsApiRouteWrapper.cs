
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
		/// <remarks>
		/// This is private so classes which derive from this one must use the
		/// <seealso cref="ContinueWithExceptionSafety{TRet}(Func{IEmsApi, Task{TRet}})"/>
		/// method instead.
		/// </remarks>
		private IEmsApi m_api;

		/// <summary>
		/// Event that is fired whenever a low level API exception occurs.
		/// </summary>
		/// <remarks>
		/// This is static so that this same event will fire for all derived wrapper classes.
		/// </remarks>
		internal static event EventHandler<ApiExceptionEventArgs> ApiMethodFailedEvent;

		/// <summary>
		/// Executes a delegate that returns a task which accesses the API interface.
		/// The task forwards all exceptions through the <seealso cref="ApiMethodFailedEvent"/>.
		/// </summary>
		/// <param name="apiFunc">
		/// The delegate for a function to execute.
		/// </param>
		protected Task<TRet> ContinueWithExceptionSafety<TRet>( Func<IEmsApi, Task<TRet>> apiFunc )
		{
			// All the Refit methods return tasks, so we need to handle exceptions
			// as continuations.
			return apiFunc( m_api );

			/*
			var api = apiFunc( m_api );
			var safeTask = api.ContinueWith( HandleApiException, TaskContinuationOptions.OnlyOnFaulted );
			return safeTask;
			*/
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
		protected IEnumerable<T> EnsureEnumerableNotNull<T>( IEnumerable<T> input )
		{
			return input == null ? Enumerable.Empty<T>() : input;
		}

		/// <summary>
		/// Rethrows any exceptions that were packaged as an aggregate exception
		/// as due to accessing the task's result.
		/// </summary>
		protected TRet RethrowAggregateExceptions<TRet>( Task<TRet> task )
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
						continue;

					throw inner;
				}		
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
