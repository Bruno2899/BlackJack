using System.Windows;

namespace BlackJack.Views
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string userpasswort = PasswortBox.Password;

            if (username == "Bruno" && userpasswort == "Bruno")
            {
                MessageBox.Show("Login erfolgreich!");

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show("Falscher Benutzername oder Passwort!");
            }
        }
    }
}