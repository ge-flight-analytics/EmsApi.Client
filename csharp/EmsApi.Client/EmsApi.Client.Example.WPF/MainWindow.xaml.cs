using System.Collections.Generic;
using System.Windows;

using EmsApi.Client.V2.Model;

namespace EmsApi.Client.Example.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // In a large application you would not want to access these things here,
            // instead it would make more sense to use a DI container and load these
            // up in a ViewModel class.
            var api = App.EmsApi;

            // Get each ems system and extended server information, and assign the
            // list of them as the data context.
            var systemVms = new List<EmsSystemViewModel>();
            foreach( EmsSystem ems in App.EmsApi.EmsSystems.GetAll() )
            {
                EmsSystemInfo server = App.EmsApi.EmsSystems.GetSystemInfo( ems.Id );
                systemVms.Add( new EmsSystemViewModel( ems, server ) );
            }

            DataContext = systemVms;
            InitializeComponent();
        }
    }
}
