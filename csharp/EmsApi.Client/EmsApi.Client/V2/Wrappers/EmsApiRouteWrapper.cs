
namespace EmsApi.Client.V2.Wrappers
{
    /// <summary>
    /// An API route wrapper is intended to convert the raw IEmsApi interface calls
    /// to something that is a little more natural to work with inside .NET applications.
    /// </summary>
    public class EmsApiRouteWrapper
    {
        public EmsApiRouteWrapper( IEmsApi api )
        {
            m_api = api;
        }

        protected IEmsApi m_api;
    }
}
