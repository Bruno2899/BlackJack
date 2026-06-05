using System.Windows;
using System.Windows.Controls;

namespace BlackJack.Views
{
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();

            Loaded += DashboardView_Loaded;
        }

        private void DashboardView_Loaded(
            object sender,
            RoutedEventArgs e)
        {
            MessageBox.Show(
                DataContext == null
                ? "Dashboard DataContext NULL"
                : "Dashboard DataContext OK");
        }
    }
}