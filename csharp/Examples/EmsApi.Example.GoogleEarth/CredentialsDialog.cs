using System;
using System.Windows.Forms;

using EmsApi.Client.V2;

namespace EmsApi.Example.GoogleEarth
{
    public partial class CredentialsDialog : Form
    {
        public CredentialsDialog( EmsApiService api )
        {
            m_api = api;
            Icon = System.Drawing.Icon.FromHandle( Properties.Resources.pushpin.GetHicon() );
            InitializeComponent();

            // Note: These will already contain values if the environment
            // variables are set (EmsApiEndpoint, EmsApiUsername, EmsApiPassword).
            m_endpointBox.Text = api.ServiceConfig.Endpoint;
            m_userBox.Text = api.ServiceConfig.UserName;
            m_passwordBox.Text = api.ServiceConfig.Password;
        }

        private void m_loginButton_Click( object sender, EventArgs e )
        {
            // Already authenticated.
            if( m_api.Authenticated )
                return;

            m_api.ServiceConfig.Endpoint = m_endpointBox.Text;
            m_api.ServiceConfig.UserName = m_userBox.Text;
            m_api.ServiceConfig.Password = m_passwordBox.Text;

            try
            {
                m_api.Authenticate();
            }
            catch( EmsApiAuthenticationException ex )
            {
                MessageBox.Show( string.Format( "Failed to authenticate with the EMS API: {0}", ex.Message ), "Authentication Failed", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error );
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            // With a valid login, we set the result to OK and close.
            DialogResult = DialogResult.OK;
            Close();
        }

        private EmsApiService m_api;
    }
}
