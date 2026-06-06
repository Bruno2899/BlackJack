using BlackJack.Common;
using BlackJack.Events;
using BlackJack.Modelle;
using Prism.Events;
using System.Collections.Generic;
using System.Linq;
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
        private bool _spielBeendet;
        private decimal _einsatz;

        private Deck _deck;

        private List<Karten> _spielerHand;
        private List<Karten> _dealerHand;
        private string _dealerKartenAufdecken;
        public decimal Einsatz
        {
            get
            {
                return _einsatz;
            }
            set
            {
                _einsatz = value;
                OnPropertyChanged(nameof(Einsatz));
            }
        }
        public string DealerKartenAufdecken
        {
            get
            {
                return _dealerKartenAufdecken;
            }
            set
            {
                _dealerKartenAufdecken = value;
                OnPropertyChanged(nameof(DealerKartenAufdecken));
            }
        }
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

        public ICommand Plus1Command
        {
            get;
            private set;
        }

        public ICommand Minus1Command
        {
            get;
            private set;
        }

        public BlackJackViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            
            _spielerHand = new List<Karten>();
            _dealerHand = new List<Karten>();

            if (CurrentUser.User != null)
            {
                Guthaben = CurrentUser.User.Balance;
            }

            NeueRundeStarten();

            HitCommand = new ActionCommand(HitExecute, HitCanExecute);

            StandCommand = new ActionCommand(StandExecute, StandCanExecute);

            BackCommand = new ActionCommand(BackExecute, BackCanExecute);

            NeueRundeCommand = new ActionCommand(NeueRundeExecute, NeueRundeCanExecute);

            Plus1Command =
    new ActionCommand(
        Plus1Execute,
        Plus1CanExecute);

            Minus1Command =
                new ActionCommand(
                    Minus1Execute,
                    Minus1CanExecute);
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
        private bool Plus1CanExecute(
    object parameter)
        {
            return true;
        }

        private bool Minus1CanExecute(
            object parameter)
        {
            return true;
        }

        private void NeueRundeExecute(object parameter)
        {
            NeueRundeStarten();
        }

        private void NeueRundeStarten()
        {
            
            if (Guthaben < Einsatz)
            {
                SpielStatus = "Zu wenig Guthaben";
                return;
            }
            if (Einsatz < 1)
            {
                Einsatz = 1;
            }
            _spielBeendet = false;
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
            if (_spielBeendet)
            {
                return;
            }
            _spielerHand.Add(_deck.ZieheKarte());

            AktualisiereAnzeige();

            if (SpielerPunkte > 21)
            {
                _spielBeendet = true;

                Guthaben -= Einsatz;

                CurrentUser.User.Balance = Guthaben;

                SpielSpeichern(Einsatz, false);

                SpielStatus = "Bust! Über 21!";
            }
        }

        private void StandExecute(object parameter)
        {
            if (_spielBeendet)
            {
                return;
            }

            _spielBeendet = true;

            AktualisiereAnzeige();

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

            for (int i = 0; i < _dealerHand.Count; i++)
            {
                Karten karte = _dealerHand[i];

                if (!_spielBeendet)
                {
                    if (i == 0)
                    {
                        DealerKarten += karte.Beschreibung + " ";
                    }
                    else
                    {
                        DealerKarten += "? ";
                    }
                }
                else
                {
                    DealerKarten += karte.Beschreibung + " ";
                }

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

            if (_spielBeendet)
            {
                DealerKartenAufdecken = Convert.ToString(DealerPunkte);
            }
            else
            {
                DealerKartenAufdecken = "?";
            }
        }

        private void GewinnerRechner()
        {
            _spielBeendet = true;
            if (DealerPunkte > 21)
            {
                Guthaben += Einsatz;

                CurrentUser.User.Balance = Guthaben;

                SpielSpeichern(Einsatz, true);

                SpielStatus = "Dealer hat verloren!";
                return;
            }

            if (SpielerPunkte > DealerPunkte)
            {
                Guthaben += Einsatz;

                CurrentUser.User.Balance = Guthaben;

                SpielSpeichern(Einsatz, true);

                SpielStatus = "Du hast gewonnen!";
            }
            else if (SpielerPunkte < DealerPunkte)
            {
                Guthaben -= Einsatz;

                CurrentUser.User.Balance = Guthaben;

                SpielSpeichern(Einsatz, false);

                SpielStatus = "Dealer gewinnt!";
            }
            else
            {
                SpielStatus = "Unentschieden!";
            }
        }

        private void Plus1Execute(
    object parameter)
        {
            Einsatz += 1;
        }

        private void Minus1Execute(
            object parameter)
        {
            if (Einsatz > 1)
            {
                Einsatz -= 1;
            }
        }

        private void SpielSpeichern(decimal gewinnVerlust, bool gewonnen)
        {
            using (BlackJackDB_Context db = new BlackJackDB_Context())
            {
                User user = db.Users.FirstOrDefault(u => u.Id == CurrentUser.User.Id);

                if (user != null)
                {
                    user.Balance = Guthaben;
                    
                    db.SpielStatistiken.Add(new SpielStatistik
                    {
                        UserId = user.Id,
                        GewinnVerlust = gewinnVerlust,
                        Gewonnen = gewonnen
                    });
                    db.SaveChanges();
                }
            }
        }
    }
}