using EmsApi.Client.V2.Model;

namespace EmsApi.Example.WPF
{
    /// <summary>
    /// The ViewModel for an EMS system accessed via the API.
    /// </summary>
    public class EmsSystemViewModel
    {
        public EmsSystemViewModel( EmsSystem system, EmsSystemInfo serverInfo )
        {
            m_system = system;
            m_serverInfo = serverInfo;
        }

        private EmsSystem m_system;
        private EmsSystemInfo m_serverInfo;

        public int Id
        {
            get { return m_system.Id; }
        }

        public string Name
        {
            get { return m_system.Name; }
        }

        public string Description
        {
            get { return m_system.Description; }
        }

        public string Client
        {
            get { return m_serverInfo.ClientAbbreviation; }
        }

        public string Version
        {
            get { return m_serverInfo.ServerVersion.ToString(); }
        }
    }
}
