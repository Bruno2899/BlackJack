using Prism.Events;
using BlackJack.Common;

namespace BlackJack.ViewModels
{
    public class ViewModelBase : NotifyPropertyChanged
    {
        public ViewModelBase(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
        }

        protected IEventAggregator EventAggregator
        {
            get;
        }
    }
}