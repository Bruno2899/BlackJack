using System.Windows.Controls;
using BlackJack.ViewModels;
namespace BlackJack.Views
{
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }
        private void PasswortBox_PasswordChanged(object sender,System.Windows.RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel login_passwort)
            {
                login_passwort.Password = PasswortBox.Password;
            }
        }
    }
}