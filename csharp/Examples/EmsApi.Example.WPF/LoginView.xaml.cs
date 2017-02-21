using System.Windows;

namespace EmsApi.Example.WPF
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView( LoginViewModel viewModel )
        {
            m_vm = viewModel;
            DataContext = m_vm;
            InitializeComponent();
            m_password.Password = viewModel.Password;
        }

        private LoginViewModel m_vm;

        private void LoginButton_Click( object sender, RoutedEventArgs e )
        {
            // You can't easily data bind to a password box.
            m_vm.Password = m_password.Password;
            DialogResult = true;
            Close();
        }
    }
}
