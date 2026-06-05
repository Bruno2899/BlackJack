using System.Windows.Input;
using Prism.Events;
using BlackJack.Common;
using BlackJack.Events;

namespace BlackJack.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        private string _password;

        public LoginViewModel(IEventAggregator eventAggregator)
            : base(eventAggregator)
        {
            System.Windows.MessageBox.Show("LoginViewModel erstellt");

            LoginCommand = new ActionCommand(
                LoginCommandExecute,
                LoginCommandCanExecute);
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

        private bool LoginCommandCanExecute(object parameter)
        {
            return true;
        }

        private void LoginCommandExecute(object parameter)
        {
            System.Windows.MessageBox.Show("Login Button");

            EventAggregator
                .GetEvent<LoginSuccessEvent>()
                .Publish();
        }
    }
}