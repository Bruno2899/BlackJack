using Prism.Events;
using System.Windows.Controls;
using BlackJack.Events;
using BlackJack.Views;

namespace BlackJack.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private UserControl _currentView;

        private LoginView _loginView;
        private DashboardView _dashboardView;
        private BlackjackView _blackjackView;

        public MainViewModel(IEventAggregator eventAggregator)
            : base(eventAggregator)
        {
            _loginView = new LoginView();
            _loginView.DataContext =
                new LoginViewModel(EventAggregator);

            _dashboardView = new DashboardView();
            _dashboardView.DataContext =
                new DashboardViewModel(EventAggregator);
            ////////////////////////////////////////////
           System.Windows.MessageBox.Show(
            _dashboardView.DataContext == null
            ? "Dashboard DC NULL"
            : "Dashboard DC OK");

            _blackjackView = new BlackjackView();
            _blackjackView.DataContext =
                new BlackJackViewModel(EventAggregator);

            CurrentView = _loginView;

            System.Windows.MessageBox.Show(
    CurrentView == null
    ? "CurrentView NULL"
    : CurrentView.GetType().Name);

            EventAggregator
                .GetEvent<LoginSuccessEvent>()
                .Subscribe(OpenDashboard);

            EventAggregator
                .GetEvent<GameStartEvent>()
                .Subscribe(OpenGame);

            EventAggregator
                .GetEvent<BackToDashboardEvent>()
                .Subscribe(OpenDashboard);
        }

        public UserControl CurrentView
        {
            get
            {
                return _currentView;
            }
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        private void OpenDashboard()
        {
            System.Windows.MessageBox.Show(
                "Dashboard Event angekommen");

            CurrentView = _dashboardView;
        }

        private void OpenGame()
        {
            System.Windows.MessageBox.Show(
                "Blackjack Event angekommen");

            CurrentView = _blackjackView;
        }
    }
}