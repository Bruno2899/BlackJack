using BlackJack.Common;
using BlackJack.Events;
using BlackJack.Modelle;
using Prism.Events;
using System.Linq;
using System.Windows;
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
        private string _ranking;


        public StatistikViewModel(IEventAggregator eventAggregator): base(eventAggregator)
        {
            StatistikLaden();
            BackCommand = new ActionCommand(BackExecute, BackCanExecute);
            StatistikInfoCommand = new ActionCommand(StatistikInfoExecute, StatistikInfoCanExecute);
            InfoCommand =new ActionCommand(InfoExecute, InfoCanExecute);
        }
        public string Ranking
        {
            get
            {
                return _ranking;
            }
            set
            {
                _ranking = value;
                OnPropertyChanged(nameof(Ranking));
            }
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
        public ICommand BackCommand{get; private set; } 
        public ICommand StatistikInfoCommand{get; private set; }
        public ICommand InfoCommand{get; private set;}
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


                Ranking = "";

                var top5 = db.Users.OrderByDescending(u => u.Balance).Take(5).ToList();

                for (int i = 0; i < top5.Count; i++)
                {
                    Ranking += i+1 + ". "+ top5[i].Username+ " besitzt: " + top5[i].Balance + " €\n";
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
        private bool StatistikInfoCanExecute(object parameter)
        {
            return true;
        }
        private bool InfoCanExecute(object parameter)
        {
            return true;
        }

        private void StatistikInfoExecute(object parameter)
        {
            MessageBox.Show(

        "Spiele:\nAnzahl aller gespielten Runden des Users\n\n" +

        "Gesamtgewinn:\nSumme aller Gewinne und Verluste all time\n\n" +

        "Win Rate:\nProzent der gewonnenen Spiele\n\n" +

        "Durchschnitt:\nDurchschnittlicher Gewinn pro Spiel\n\n" +

        "Größter Gewinn:\nHöchster Gewinn in einer Runde\n\n" +

        " Leaderboard \nder Reichsten Spieler Konten");
        }

        private void InfoExecute(object parameter)
        {
            MessageBox.Show(

        "BLACKJACK\n\n" +

        "Ziel:\n" +

        "Der User muss möglich nah an 21 Punkte kommen.\n\n" +

        "Hit:\nNeue Karte ziehen.\n\n" +

        "Stand:\nKeine Karte ziehen.\n\n" +

        "Double Down:\nEinsatz verdoppeln und genau eine Karte erhalten.\n\n" +

        "Dealer:\nMuss bis mindestens 17 Punkte  weitere Karten ziehen.",

        "Spielanleitung");
        }
    }
}