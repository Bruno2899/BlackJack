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

        public MainViewModel(IEventAggregator eventAggregator): base(eventAggregator)
        {
            _loginView = new LoginView();
            _loginView.DataContext = new LoginViewModel(EventAggregator);

            _dashboardView = new DashboardView();
            _dashboardView.DataContext = new DashboardViewModel(EventAggregator);
       
            _blackjackView = new BlackjackView();
            _blackjackView.DataContext = new BlackJackViewModel(EventAggregator);

            CurrentView = _loginView;

            EventAggregator.GetEvent<LoginSuccessEvent>().Subscribe(OpenDashboard);

            EventAggregator.GetEvent<GameStartEvent>().Subscribe(OpenGame);

            EventAggregator.GetEvent<BackToDashboardEvent>().Subscribe(OpenDashboard);
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
            CurrentView = _dashboardView;
        }

        private void OpenGame()
        {
            CurrentView = _blackjackView;
        }
    }
}