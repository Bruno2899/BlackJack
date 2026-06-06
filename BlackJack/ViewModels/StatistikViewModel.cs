using BlackJack.Common;
using BlackJack.Modelle;
using Prism.Events;
using System.Linq;
using BlackJack.Events;
using System.Windows.Input;

namespace BlackJack.ViewModels
{
    public class StatistikViewModel : ViewModelBase
    {
        private int _spieleAnzahl;
        private decimal _gesamtGewinn;
        private double _winRate;
        private decimal _durchschnittGewinn;
        private decimal _groessterGewinn;

        public StatistikViewModel(
            IEventAggregator eventAggregator): base(eventAggregator)
        {
            StatistikLaden();
            BackCommand = new ActionCommand(BackExecute, BackCanExecute);
        }

        public int SpieleAnzahl
        {
            get { return _spieleAnzahl; }
            set
            {
                _spieleAnzahl = value;
                OnPropertyChanged(nameof(SpieleAnzahl));
            }
        }

        public decimal GesamtGewinn
        {
            get { return _gesamtGewinn; }
            set
            {
                _gesamtGewinn = value;
                OnPropertyChanged(nameof(GesamtGewinn));
            }
        }

        public double WinRate
        {
            get { return _winRate; }
            set
            {
                _winRate = value;
                OnPropertyChanged(nameof(WinRate));
            }
        }

        public decimal DurchschnittGewinn
        {
            get { return _durchschnittGewinn; }
            set
            {
                _durchschnittGewinn = value;
                OnPropertyChanged(nameof(DurchschnittGewinn));
            }
        }

        public decimal GroessterGewinn
        {
            get { return _groessterGewinn; }
            set
            {
                _groessterGewinn = value;
                OnPropertyChanged(nameof(GroessterGewinn));
            }
        }
        public ICommand BackCommand
        {
            get;
            private set;
        }
        private void StatistikLaden()
        {
            if (CurrentUser.User == null)
            {
                return;
            }
            using (BlackJackDB_Context db = new BlackJackDB_Context())
            {
                int userId = CurrentUser.User.Id;

                var spiele = db.SpielStatistiken.Where(s => s.UserId == userId).ToList();

                SpieleAnzahl = spiele.Count;

                GesamtGewinn = spiele.Sum(s => s.GewinnVerlust);

                if (spiele.Count > 0)
                {
                    DurchschnittGewinn = spiele.Average(s => s.GewinnVerlust);

                    GroessterGewinn = spiele.Max(s => s.GewinnVerlust);

                    WinRate = spiele.Count(s => s.Gewonnen) * 100.0 / spiele.Count;
                }
            }
        }
        private bool BackCanExecute(object parameter)
        {
            return true;
        }

        private void BackExecute(object parameter)
        {
            EventAggregator.GetEvent<BackFromStatistikEvent>().Publish();
        }
    }
}