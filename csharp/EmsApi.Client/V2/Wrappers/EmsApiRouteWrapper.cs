
namespace EmsApi.Client.V2.Wrappers
{
    /// <summary>
    /// An API route wrapper is intended to convert the raw IEmsApi interface calls
    /// to something that is a little more natural to work with inside .NET applications.
    /// </summary>
    public class EmsApiRouteWrapper
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
    }
}
