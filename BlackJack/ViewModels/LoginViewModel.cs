using System.Windows.Input;
using Prism.Events;
using BlackJack.Common;
using BlackJack.Events;
using BlackJack.Modelle;
using System.Linq;

namespace BlackJack.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        private string _password;

        public LoginViewModel(IEventAggregator eventAggregator): base(eventAggregator)
        {
            System.Windows.MessageBox.Show("LoginViewModel erstellt");

            LoginCommand = new ActionCommand(LoginCommandExecute,LoginCommandCanExecute);
            RegisterCommand = new ActionCommand(RegisterCommandExecute,RegisterCommandCanExecute);
        }

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand
        {
            get;
            private set;
        }
        public ICommand RegisterCommand
        {
            get;
            private set;
        }
        private bool LoginCommandCanExecute(object parameter)
        {
            return true;
        }

        private void LoginCommandExecute(object parameter)
        {
            

            using (BlackJackDB_Context db =
                new BlackJackDB_Context())
            {
                
                User user = db.Users.FirstOrDefault(u => u.Username == Username && u.PasswordHash == Password);

                if (user != null)
                {
                    CurrentUser.User = user;

                    System.Windows.MessageBox.Show(
                        "Willkommen " + user.Username);

                    EventAggregator
                        .GetEvent<LoginSuccessEvent>()
                        .Publish();
                }
                else
                {
                    System.Windows.MessageBox.Show(
                        "Falscher Benutzername oder Passwort");
                }
            }
        }
        private bool RegisterCommandCanExecute(object parameter)
        {
            return true;
        }
        private void RegisterCommandExecute(object parameter)
        {
            using (BlackJackDB_Context db =
                new BlackJackDB_Context())
            {
                User vorhandenerUser = db.Users.FirstOrDefault(u => u.Username == Username);

                if (vorhandenerUser != null)
                {
                    System.Windows.MessageBox.Show("Benutzer existiert bereits");

                    return;
                }

                User neuerUser = new User
                {
                    Username = Username,
                    PasswordHash = Password,
                    Balance = 1000
                };

                db.Users.Add(neuerUser);

                db.SaveChanges();

                System.Windows.MessageBox.Show(
                    "Registrierung erfolgreich");
                CurrentUser.User = neuerUser;

                EventAggregator.GetEvent<LoginSuccessEvent>().Publish();
            }
        }
    }
}