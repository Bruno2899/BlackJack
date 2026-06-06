using Prism.Events;
using System.Windows.Input;
using BlackJack.Common;
using BlackJack.Events;

namespace BlackJack.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        public DashboardViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            StartGameCommand = new ActionCommand(StartGameExecute, StartGameCanExecute);
            StatistikCommand = new ActionCommand(StatistikExecute, StatistikCanExecute);
            
            LogoutCommand =new ActionCommand(LogoutExecute, LogoutCanExecute);
        }

        public ICommand StartGameCommand{get; private set;}
        public ICommand StatistikCommand{get; private set;}
        public ICommand LogoutCommand{get; private set;}

        private bool StartGameCanExecute(object parameter)
        {
            return true;
        }

        private void StartGameExecute(object parameter)
        {
            EventAggregator.GetEvent<GameStartEvent>().Publish();
        }
        private bool StatistikCanExecute(object parameter)
        {
            return true;
        }

        private void StatistikExecute(
            object parameter)
        {
            EventAggregator.GetEvent<OpenStatistikEvent>().Publish();
        }
        private bool LogoutCanExecute(object parameter)
        {
            return true;
        }

        private void LogoutExecute(object parameter)
        {
            CurrentUser.User = null;
            EventAggregator.GetEvent<LogoutEvent>().Publish();
        }
    }
}