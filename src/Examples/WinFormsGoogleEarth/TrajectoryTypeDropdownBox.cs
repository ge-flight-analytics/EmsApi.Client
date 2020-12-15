using System.Windows.Forms;
using EmsApi.Client.V2;

namespace EmsApi.Example.GoogleEarth
{
    class TrajectoryTypeDropdownBox : ComboBox
    {
        public TrajectoryTypeDropdownBox() : base()
        {
            const string idMember = "TrajectoryId";
            DisplayMember = idMember;
            ValueMember = idMember;
            DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// The API service to retrieve EMS systems from. This must be
        /// set prior to using the dropdown box in the code for the form
        /// that owns the dropdown.
        /// </summary>
        internal EmsApiService ApiService
        {
            get
            {
                return m_api;
            }
            set
            {
                m_api = value;
                ReloadOptions();
            }
        }

        /// <summary>
        /// Reloads the dropdown options from the API.
        /// </summary>
        public void ReloadOptions()
        {
            DataSource = m_api.Trajectories.GetAllConfigurations();
            RefreshItems();
        }

        private EmsApiService m_api;
    }
}
