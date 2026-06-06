using System.Windows;
using Prism.Events;
using BlackJack.ViewModels;

namespace BlackJack
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            IEventAggregator eventAggregator = new EventAggregator();

            DataContext = new MainViewModel(eventAggregator);

        }
    }
}