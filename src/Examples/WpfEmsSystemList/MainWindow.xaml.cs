﻿using System.Collections.ObjectModel;
using System.Windows;

using EmsApi.Dto.V2;

namespace EmsApi.Example.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            m_emsSystems = new ObservableCollection<EmsSystemViewModel>();
            DataContext = m_emsSystems;
            Loaded += ( s, e ) => GetEmsSystems();
        }

        private ObservableCollection<EmsSystemViewModel> m_emsSystems;

        private void GetEmsSystems()
        {
            // Get each ems system and extended server information, and assign the
            // list of them as the data context.
            foreach( EmsSystem ems in App.EmsApi.EmsSystems.GetAll() )
            {
                EmsSystemInfo server = App.EmsApi.EmsSystems.GetSystemInfo( ems.Id.Value );
                m_emsSystems.Add( new EmsSystemViewModel( ems, server ) );
            }
        }
    }
}
