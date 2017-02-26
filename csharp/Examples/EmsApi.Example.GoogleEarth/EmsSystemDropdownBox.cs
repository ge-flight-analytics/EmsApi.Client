using System.Windows.Forms;
using EmsApi.Client.V2;

namespace EmsApi.Example.GoogleEarth
{
    public class EmsSystemDropdownBox : ComboBox
    {
        public EmsSystemDropdownBox() : base()
        {
            // Display the EMS system name in the dropdown, and the selected value is the id.
            DisplayMember = "Name";
            ValueMember = "Id";
            DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// The API service to retrieve EMS systems from. This must be
        /// set prior to using the dropdown box in the code for the form
        /// that owns the dropdown.
        /// </summary>
        internal EmsApiService ApiService
        {
            get { return m_api; }
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
            DataSource = m_api.EmsSystems.GetAll();
            RefreshItems();
        }

        private EmsApiService m_api;
    }
}
