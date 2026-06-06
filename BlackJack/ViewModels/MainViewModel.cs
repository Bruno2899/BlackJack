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
        
        public MainViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            _loginView = new LoginView();
            _loginView.DataContext = new LoginViewModel(EventAggregator);

            _dashboardView = new DashboardView();
            _dashboardView.DataContext = new DashboardViewModel(EventAggregator);


            CurrentView = _loginView;

            EventAggregator.GetEvent<LoginSuccessEvent>().Subscribe(OpenDashboard);

            EventAggregator.GetEvent<GameStartEvent>().Subscribe(OpenGame);

            EventAggregator.GetEvent<BackToDashboardEvent>().Subscribe(OpenDashboard);

            EventAggregator.GetEvent<OpenStatistikEvent>().Subscribe(OpenStatistik);

            EventAggregator.GetEvent<BackFromStatistikEvent>().Subscribe(OpenDashboard);
            EventAggregator.GetEvent<LogoutEvent>().Subscribe(OpenLogin);
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
        private void OpenLogin()
        {
            CurrentView = _loginView;
        }

        private void OpenGame()
        {
            BlackjackView blackjackView =new BlackjackView();

            blackjackView.DataContext =new BlackJackViewModel(EventAggregator);

            CurrentView = blackjackView;
        }

        private void OpenStatistik()
        {
            StatistikView statistikView =new StatistikView();

            statistikView.DataContext =new StatistikViewModel(EventAggregator);

            CurrentView = statistikView;
        }
    }
}