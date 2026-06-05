using Prism.Events;

namespace BlackJack.ViewModels
{
    public class BlackJackViewModel : ViewModelBase
    {
        public BlackJackViewModel(
            IEventAggregator eventAggregator)
            : base(eventAggregator)
        {
        }
    }
}