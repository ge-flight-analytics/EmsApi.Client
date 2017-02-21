using System.Windows;
using EmsApi.Client.V2;

namespace EmsApi.Example.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // Initialize logging and other application wide components.
            s_emsApi = new EmsApiService();
            s_emsApi.RegisterAuthFailedCallback( AuthenticationFailed );
        }

        /// <summary>
        /// The application wide EMS API service. This property is lazy loaded, and
        /// accessing it the first time will show the login dialog and set the corresponding
        /// configuration.
        /// </summary>
        public static EmsApiService EmsApi
        {
            get
            {
                // Show the connection dialog until we get authenticated or give up.
                while( !s_emsApi.Authenticated )
                {
                    var config = ShowConnectionDialog();
                    if( config == null )
                    {
                        // Shut down if the user cancelled login.
                        // Note: We do a hard exit here so whatever code is waiting on a login
                        // doesn't have to handle the fact that the API might be null.
                        if( s_emsApi != null )
                            s_emsApi.Dispose();

                        System.Environment.Exit( 0 );
                        return null;
                    }

                    try
                    {
                        // We handle failures through callbacks.
                        config.ThrowExceptionOnApiFailure = false;
                        config.ThrowExceptionOnAuthFailure = false;
                        s_emsApi.ServiceConfig = config;
                    }
                    catch( EmsApiConfigurationException ex )
                    {
                        // Notify the user that some login configuration is wrong, and retry.
                        LoginValidationFailed( ex.Message );
                        continue;
                    }

                    s_emsApi.Authenticate();
                }   

                return s_emsApi;
            }
        }

        private void AuthenticationFailed( string error )
        {
            string message = string.Format( "{0}\n\nPlease re-enter credentials, or press cancel to close the application.", error );
            MessageBox.Show( message, "EMS API Authentication Failed", MessageBoxButton.OK, MessageBoxImage.Error );
        }

        private static void LoginValidationFailed( string error )
        {
            string message = string.Format( "{0}\n\nPress OK to retry.", error );
            MessageBox.Show( message, "EMS API Configuration Validation Failed", MessageBoxButton.OK, MessageBoxImage.Error );
        }

        private static EmsApiServiceConfiguration ShowConnectionDialog()
        {
            var config = new EmsApiServiceConfiguration();
            var viewModel = new LoginViewModel
            {
                Endpoint = config.Endpoint,
                Username = config.UserName,
                Password = config.Password
            };

            var login = new LoginView( viewModel );
            bool? result = login.ShowDialog();

            if( !result.HasValue || !result.Value )
                return null;

            config.Endpoint = viewModel.Endpoint;
            config.UserName = viewModel.Username;
            config.Password = viewModel.Password;
            return config;
        }

        protected override void OnExit( ExitEventArgs e )
        {
            if( s_emsApi != null )
                s_emsApi.Dispose();

            base.OnExit( e );
        }

        private static EmsApiService s_emsApi;
    }
}
