
namespace EmsApi.Client.V2
{
    /// <summary>
    /// The result of attempting to authenticate.
    /// </summary>
    internal class AuthResult
    {
        public AuthResult( AuthToken token )
        {
            Success = true;
            TokenData = token;
        }

        public AuthResult( string error )
        {
            Success = false;
            Error = error;
        }

        /// <summary>
        /// Whether or not we were able to authenticate.
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// The acquired authentication token. This will be null if <seealso cref="Success"/> is false.
        /// </summary>
        public AuthToken TokenData { get; private set; }

        /// <summary>
        /// The error information if <seealso cref="Success"/> is false; null otherwise.
        /// </summary>
        public string Error { get; private set; }
    }
}
