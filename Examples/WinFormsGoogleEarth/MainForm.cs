using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using EmsApi.Client.V2;

namespace EmsApi.Example.GoogleEarth
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            Icon = Icon.FromHandle( Properties.Resources.pushpin.GetHicon() );
            InitializeComponent();
            this.Load += MainForm_Load;
        }

        private void MainForm_Load( object sender, EventArgs e )
        {
            m_api = new EmsApiService();
            CredentialsDialog login = new CredentialsDialog( m_api );
            if( login.ShowDialog() != DialogResult.OK )
            {
                Close();
                return;
            }

            m_emsSystemDropdown.ApiService = m_api;
            m_trajectoryTypeDropdown.ApiService = m_api;
        }

        private int m_flightId;
        private int m_emsSystemId;
        private string m_trajectoryConfigurationId;

        private void m_emsSystemDropdown_SelectedIndexChanged( object sender, EventArgs e )
        {
            m_emsSystemId = (int)m_emsSystemDropdown.SelectedValue;
            m_api.CachedEmsSystem = m_emsSystemId;
        }

        private void m_trajectoryTypeDropdown_SelectedIndexChanged( object sender, EventArgs e )
        {
            m_trajectoryConfigurationId = (string)m_trajectoryTypeDropdown.SelectedValue;
        }

        private void m_flightIdBox_TextChanged( object sender, EventArgs e )
        {
            // Evaluate if we can open the KML yet.
            m_openKmlButton.Enabled = m_api.Authenticated
                && int.TryParse( m_flightIdBox.Text, out m_flightId );
        }

        private void m_openKmlButton_Click( object sender, EventArgs e )
        {
            string kml = m_api.Trajectories.GetTrajectoryKml( m_flightId, m_trajectoryConfigurationId, m_emsSystemId );

            string temp = null;
            try
            {

                temp = Path.GetTempFileName();
                temp = Path.ChangeExtension( temp, "kml" );
                File.WriteAllText( temp, kml );

                try
                {
                    Process p = Process.Start( temp );
                    p.WaitForExit();
                }
                catch( Exception )
                {
                    MessageBox.Show( "Could not open Google Earth process, maybe it's not installed?", "Google Earth Not Found",
                        MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }
            finally
            {
                if( temp != null )
                    File.Delete( temp );
            }
            
        }

        private EmsApiService m_api;
    }
}
