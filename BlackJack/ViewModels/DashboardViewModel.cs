using Prism.Events;
using System.Windows.Input;
using BlackJack.Common;
using BlackJack.Events;

namespace BlackJack.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        /*
        public DashboardViewModel(IEventAggregator eventAggregator)
            : base(eventAggregator)
        {
            StartGameCommand =
                new ActionCommand(
                    StartGameExecute,
                    StartGameCanExecute);
        }*/
        public DashboardViewModel(IEventAggregator eventAggregator)
    : base(eventAggregator)
        {
            System.Windows.MessageBox.Show("DashboardViewModel erstellt");

            StartGameCommand =
                new ActionCommand(
                    StartGameExecute,
                    StartGameCanExecute);
        }

        public ICommand StartGameCommand
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
            EventAggregator
                .GetEvent<GameStartEvent>()
                .Publish();
        }
    }
}