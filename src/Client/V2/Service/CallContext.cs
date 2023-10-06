
using System.Text;

namespace EmsApi.Client.V2
{
    /// <summary>
    /// An (optional) set of information to pass in with each API call.
    /// 
    /// This is used to convey any per-call, non-route-specific information. For instance if you wanted to fill out the
    /// "X-Adi-Correlation-Id" header and it was going to be different with each call, that would naturally go here.
    ///
    /// Values which are null or empty or whitespace will be ignored at runtime unless otherwise specified.
    ///
    /// Settings here which modify headers are applied in the call *after* any custom headers specified in the
    /// <seealso cref="EmsApiServiceConfiguration.CustomHeaders"/> property. This means they will override any common
    /// headers specified in <seealso cref="EmsApiServiceConfiguration.CustomHeaders"/>.
    /// </summary>
    public class CallContext
    {
        private string m_applicationName;
        private string m_clientUsername;
        private string m_correlationId;

        /// <summary>
        /// The value to use for the 'X-Adi-Application-Name' header.
        /// Note: assigned values will be converted to ASCII strings as required for HTTP headers.
        /// </summary>
        public string ApplicationName
        {
            get { return m_applicationName; }
            set { m_applicationName = EncodeAsAsciiString( value ); }
        }

        /// <summary>
        /// The value to use in the 'X-Adi-Client-Username' header.
        /// Note: assigned values will be converted to ASCII strings as required for HTTP headers.
        /// </summary>
        public string ClientUsername
        {
            get { return m_clientUsername; }
            set { m_clientUsername = EncodeAsAsciiString( value ); }
        }

        /// <summary>
        /// The value to use in the 'X-Adi-Correlation-Id' header.
        /// Note: assigned values will be converted to ASCII strings as required for HTTP headers.
        /// </summary>
        public string CorrelationId
        {
            get { return m_correlationId; }
            set { m_correlationId = EncodeAsAsciiString( value ); }
        }

        /// <summary>
        /// The trusted authentication name to use as part of Trusted Authentication for this call.
        /// 
        /// This is the name of the property in the EFOQA classic Active Directory to use for looking up the user to
        /// make the call on behalf of.
        ///
        /// This need not be set here, instead it can be set in the <seealso cref="EmsApiServiceConfiguration.TrustedAuthName"/>.
        /// That will be the default trusted authentication name used for all calls in that case, with this value overloading that.
        /// The common case is to not have variable trusted authentication names within one usage.
        ///
        /// For trusted authentication to work you must also set the <seealso cref="EmsApiServiceConfiguration.ApiClientId"/>
        /// and the <seealso cref="EmsApiServiceConfiguration.ApiClientSecret"/>.
        /// </summary>
        public string TrustedAuthName { get; set; }

        /// <summary>
        /// The trusted authentication value to use as part of Trusted Authentication for this call.
        ///
        /// This is the value of the property that will be searched for in the EFOQA classic Active Directory. The user
        /// found with this value will be the user that the call is made on behalf of.
        ///
        /// For trusted authentication to work you must also set the <seealso cref="EmsApiServiceConfiguration.ApiClientId"/>
        /// and the <seealso cref="EmsApiServiceConfiguration.ApiClientSecret"/>.
        /// </summary>
        public string TrustedAuthValue { get; set; }

        private string EncodeAsAsciiString( string value )
        {
            byte[] byteArray = Encoding.UTF8.GetBytes( value );
            byte[] asciiArray = Encoding.Convert( Encoding.UTF8, Encoding.ASCII, byteArray );
            return Encoding.ASCII.GetString( asciiArray );
        }
    }
}
