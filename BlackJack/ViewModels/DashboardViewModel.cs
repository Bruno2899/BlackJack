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
        }

        public ICommand StartGameCommand
        {
            get;
            private set;
        }
        public ICommand StatistikCommand
        {
            get;
            private set;
        }

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
    }
}