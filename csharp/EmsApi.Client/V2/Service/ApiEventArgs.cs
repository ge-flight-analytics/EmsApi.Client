using System;

namespace EmsApi.Client.V2
{
    /// <summary>
    /// Common class for event arguments used inside the API client.
    /// </summary>
    internal class ApiEventArgs : EventArgs
    {
        public ApiEventArgs( string message )
        {
            Message = message;
        }

        public string Message { get; private set; }
    }

    /// <summary>
    /// Arguments for an event fired whenever API authentication fails.
    /// </summary>
    internal class AuthenticationFailedEventArgs : ApiEventArgs
    {
        public AuthenticationFailedEventArgs( string message ) : base( message ) { }
    }

    /// <summary>
    /// Arguments for an event fired whenever a low level API exception occurs.
    /// </summary>
    internal class ApiExceptionEventArgs : ApiEventArgs
    {
        public ApiExceptionEventArgs( Exception exception ) : base( exception.Message )
        {
            Exception = exception;
        }

        public Exception Exception
        {
            get; private set;
        }

        public Refit.ApiException ApiException
        {
            get { return Exception as Refit.ApiException; }
        }
    }
}
