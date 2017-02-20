using System;

namespace EmsApi.Client.V2
{
    /// <summary>
    /// A generic exception that is thrown when there is an error in the API client.
    /// </summary>
    public class EmsApiServiceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmsApiServiceException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public EmsApiServiceException( string message )
            : base( message ) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmsApiServiceException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference 
        /// if no inner exception is specified.</param>
        public EmsApiServiceException( string message, Exception innerException )
            : base( message, innerException ) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmsApiServiceException"/> class.
        /// </summary>
        public EmsApiServiceException( string format, params object[] args )
            : base( string.Format( format, args ) ) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmsApiServiceException"/> class.
        /// </summary>
        public EmsApiServiceException( Exception innerException, string format, params object[] args )
            : base( string.Format( format, args ), innerException ) { }
    }

    /// <summary>
    /// Thrown when the API configuration is not valid.
    /// </summary>
    public class InvalidApiConfigurationException : EmsApiServiceException
    {
		/// <summary>
		/// Thrown when the <seealso cref="EmsApiServiceConfiguration"/> is invalid.
		/// </summary>
		/// <param name="why">
		/// The reason why the configuration is invalid.
		/// </param>
        public InvalidApiConfigurationException( string why )
            : base( string.Format( "The EMS API service configuration is not valid: {0}", why ), innerException: null ) { }
    }
}
