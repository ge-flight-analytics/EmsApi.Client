using System.Windows;
using EmsApi.Client.V2;

namespace EmsApi.Client.Example.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // Initialize logging and other application wide components.
        }

        protected override void OnStartup( StartupEventArgs e )
        {
            // We need to get login information before connecting to the API.
            var config = new EmsApiServiceConfiguration();
            var viewModel = new LoginViewModel { Endpoint = EmsApiEndpoints.Beta };

            var login = new LoginView( viewModel );
            bool? result = login.ShowDialog();

            // Shut down if we don't get login info.
            if( !result.HasValue && result.Value )
                Shutdown();

            // In a real application you would probably want to bind these
            // together in the viewmodel, instead of reading back here.
            config.Endpoint = viewModel.Endpoint;
            config.UserName = viewModel.Username;
            config.Password = viewModel.Password;
            
            EmsApi = new EmsApiService( config );

            base.OnStartup( e );
        }

        protected override void OnExit( ExitEventArgs e )
        {
            EmsApi.Dispose();
            base.OnExit( e );
        }

        public static EmsApiService EmsApi;
    }
}
