using BlackJack.Common;
using BlackJack.Events;
using BlackJack.Modelle;
using Prism.Events;
using System.Collections.Generic;
using System.Windows.Input;

namespace BlackJack.ViewModels
{
    public class BlackJackViewModel : ViewModelBase
    {
        private string _spielerKarten;
        private string _dealerKarten;
        private int _spielerPunkte;
        private int _dealerPunkte;
        private string _spielStatus;
        private decimal _guthaben;

        private Deck _deck;

        private List<Karten> _spielerHand;
        private List<Karten> _dealerHand;

        public string SpielerKarten
        {
            get { return _spielerKarten; }
            set
            {
                _spielerKarten = value;
                OnPropertyChanged(nameof(SpielerKarten));
            }
        }

        public string DealerKarten
        {
            get { return _dealerKarten; }
            set
            {
                _dealerKarten = value;
                OnPropertyChanged(nameof(DealerKarten));
            }
        }

        public int SpielerPunkte
        {
            get { return _spielerPunkte; }
            set
            {
                _spielerPunkte = value;
                OnPropertyChanged(nameof(SpielerPunkte));
            }
        }

        public int DealerPunkte
        {
            get { return _dealerPunkte; }
            set
            {
                _dealerPunkte = value;
                OnPropertyChanged(nameof(DealerPunkte));
            }
        }

        public string SpielStatus
        {
            get { return _spielStatus; }
            set
            {
                _spielStatus = value;
                OnPropertyChanged(nameof(SpielStatus));
            }
        }
        public decimal Guthaben
        {
            get
            {
                return _guthaben;
            }
            set
            {
                _guthaben = value;
                OnPropertyChanged(nameof(Guthaben));
            }
        }

        public ICommand HitCommand { get; private set; }

        public ICommand StandCommand { get; private set; }

        public ICommand BackCommand { get; private set; }

        public ICommand NeueRundeCommand { get; private set; }

        public BlackJackViewModel(IEventAggregator eventAggregator): base(eventAggregator)
        {
            _spielerHand = new List<Karten>();
            _dealerHand = new List<Karten>();

            NeueRundeStarten();

            HitCommand = new ActionCommand(HitExecute,HitCanExecute);

            StandCommand = new ActionCommand(StandExecute,StandCanExecute);

            BackCommand = new ActionCommand(BackExecute,BackCanExecute);

            NeueRundeCommand = new ActionCommand(NeueRundeExecute,NeueRundeCanExecute);
        }

        private bool HitCanExecute(object parameter)
        {
            return true;
        }

        private bool StandCanExecute(object parameter)
        {
            return true;
        }

        private bool BackCanExecute(object parameter)
        {
            return true;
        }

        private bool NeueRundeCanExecute(object parameter)
        {
            return true;
        }

        private void NeueRundeExecute(object parameter)
        {
            NeueRundeStarten();
        }

        private void NeueRundeStarten()
        {
            _deck = new Deck();

            _spielerHand.Clear();
            _dealerHand.Clear();

            _spielerHand.Add(_deck.ZieheKarte());
            _spielerHand.Add(_deck.ZieheKarte());

            _dealerHand.Add(_deck.ZieheKarte());
            _dealerHand.Add(_deck.ZieheKarte());

            SpielStatus = "Spiel läuft";

            AktualisiereAnzeige();
        }

        private void HitExecute(object parameter)
        {
            _spielerHand.Add(
            _deck.ZieheKarte());

            AktualisiereAnzeige();

            if (SpielerPunkte > 21)
            {
                SpielStatus = "Bust! Über 21!";
            }
        }

        private void StandExecute(object parameter)
        {
            while (DealerPunkte < 17)
            {
                _dealerHand.Add(
                _deck.ZieheKarte());

                AktualisiereAnzeige();
            }

            GewinnerRechner();
        }

        private void BackExecute(object parameter)
        {
            EventAggregator.GetEvent<BackToDashboardEvent>().Publish();
        }

        private void AktualisiereAnzeige()
        {
            SpielerKarten = "";
            DealerKarten = "";

            SpielerPunkte = 0;
            DealerPunkte = 0;

            int anzahlAsse = 0;

            foreach (Karten karte in _spielerHand)
            {
                SpielerKarten += karte.Beschreibung + " ";

                SpielerPunkte += karte.Wert;

                if (karte.Beschreibung == "Ass")
                {
                    anzahlAsse++;
                }
            }

            while (SpielerPunkte > 21 && anzahlAsse > 0)
            {
                SpielerPunkte -= 10;
                anzahlAsse--;
            }

            int dealerAsse = 0;

            foreach (Karten karte in _dealerHand)
            {
                DealerKarten += karte.Beschreibung + " ";

                DealerPunkte += karte.Wert;

                if (karte.Beschreibung == "Ass")
                {
                    dealerAsse++;
                }
            }

            while (DealerPunkte > 21 && dealerAsse > 0)
            {
                DealerPunkte -= 10;
                dealerAsse--;
            }
        }

        private void GewinnerRechner()
        {
            if (DealerPunkte > 21)
            {
                SpielStatus = "Dealer hat verloren!";
                return;
            }

            if (SpielerPunkte > DealerPunkte)
            {
                SpielStatus = "Du hast gewonnen!";
            }
            else if (SpielerPunkte < DealerPunkte)
            {
                SpielStatus = "Dealer gewinnt!";
            }
            else
            {
                SpielStatus = "Unentschieden!";
            }
        }
    }
}