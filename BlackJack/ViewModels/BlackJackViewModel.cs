using BlackJack.Common;
using BlackJack.Events;
using BlackJack.Modelle;
using Prism.Events;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
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
        private bool _einsatzButtonsSichtbar;
        

        private Deck _deck;

        private List<Karten> _spielerHand;
        private List<Karten> _dealerHand;
        private string _dealerKartenAufdecken;
       

        public bool EinsatzButtonsSichtbar
        {
            get
            {
                return _einsatzButtonsSichtbar;
            }
            set
            {
                _einsatzButtonsSichtbar = value;
                OnPropertyChanged(nameof(EinsatzButtonsSichtbar));
            }
        }
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

        public ICommand Plus1Command { get; private set; }
        public ICommand Plus5Command { get; private set; }
        public ICommand Plus20Command { get; private set; }
        public ICommand Plus100Command { get; private set; }
        public ICommand Plus250Command { get; private set; }
        public ICommand Plus1000Command { get; private set; }
        public ICommand Plus5000Command { get; private set; }
        public ICommand Plus10000Command { get; private set; }

        public ICommand Minus1Command { get; private set; }
        public ICommand Minus5Command { get; private set; }
        public ICommand Minus20Command { get; private set; }
        public ICommand Minus100Command { get; private set; }
        public ICommand Minus250Command { get; private set; }
        public ICommand Minus1000Command { get; private set; }
        public ICommand Minus5000Command { get; private set; }
        public ICommand Minus10000Command { get; private set; }

        public ICommand EinsatzMenueCommand { get; private set; }

        public ICommand SpielInfoCommand {get; private set;}

        public ICommand DoubleDownCommand{get; private set;}
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

            Plus1Command = new ActionCommand(Plus1Execute, Plus1CanExecute);
            Plus5Command = new ActionCommand(Plus5Execute, Plus5CanExecute);
            Plus20Command = new ActionCommand(Plus20Execute, Plus20CanExecute);
            Plus100Command = new ActionCommand(Plus100Execute, Plus100CanExecute);
            Plus250Command = new ActionCommand(Plus250Execute, Plus250CanExecute);
            Plus1000Command = new ActionCommand(Plus1000Execute, Plus1000CanExecute);
            Plus5000Command = new ActionCommand(Plus5000Execute, Plus5000CanExecute);
            Plus10000Command = new ActionCommand(Plus10000Execute, Plus10000CanExecute);

            Minus1Command = new ActionCommand(Minus1Execute, Minus1CanExecute);
            Minus5Command = new ActionCommand(Minus5Execute, Minus5CanExecute);
            Minus20Command = new ActionCommand(Minus20Execute, Minus20CanExecute);
            Minus100Command = new ActionCommand(Minus100Execute, Minus100CanExecute);
            Minus250Command = new ActionCommand(Minus250Execute, Minus250CanExecute);
            Minus1000Command = new ActionCommand(Minus1000Execute, Minus1000CanExecute);
            Minus5000Command = new ActionCommand(Minus5000Execute, Minus5000CanExecute);
            Minus10000Command = new ActionCommand(Minus10000Execute, Minus10000CanExecute);

            EinsatzMenueCommand = new ActionCommand(EinsatzMenueExecute, EinsatzMenueCanExecute);
            DoubleDownCommand =new ActionCommand(DoubleDownExecute, DoubleDownCanExecute);
            
            SpielInfoCommand = new ActionCommand(SpielInfoExecute, SpielInfoCanExecute);
        }
        private bool SpielInfoCanExecute(object parameter)
        {
            return true;
        }

        private bool HitCanExecute(object parameter)
        {
            return !_spielBeendet && _deck != null && Guthaben > 0;
        }

        private bool StandCanExecute(object parameter)
        {
            return !_spielBeendet && _deck != null && Guthaben > 0;
        }
        private bool DoubleDownCanExecute(object parameter)
        {
            return !_spielBeendet && _deck != null && Guthaben > 0;
        }
        private bool BackCanExecute(object parameter)
        {
            return true;
        }
        private bool EinsatzMenueCanExecute(object parameter)
        {
            return true;
        }
        private bool NeueRundeCanExecute(object parameter)
        {
            return Guthaben > 0;
        }
        private void NeueRundeExecute(object parameter) 
        { 
            NeueRundeStarten(); 
        }
        private bool Plus1CanExecute(object parameter) { return true; }
        private bool Plus5CanExecute(object parameter) { return true; }
        private bool Plus20CanExecute(object parameter) { return true; }
        private bool Plus100CanExecute(object parameter) { return true; }
        private bool Plus250CanExecute(object parameter) { return true; }
        private bool Plus1000CanExecute(object parameter) { return true; }
        private bool Plus5000CanExecute(object parameter) { return true; }
        private bool Plus10000CanExecute(object parameter) { return true; }

        private bool Minus1CanExecute(object parameter) { return true; }
        private bool Minus5CanExecute(object parameter) { return true; }
        private bool Minus20CanExecute(object parameter) { return true; }
        private bool Minus100CanExecute(object parameter) { return true; }
        private bool Minus250CanExecute(object parameter) { return true; }
        private bool Minus1000CanExecute(object parameter) { return true; }
        private bool Minus5000CanExecute(object parameter) { return true; }
        private bool Minus10000CanExecute(object parameter) { return true; }
        

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
            if (IstBlackJack())
            {
                GewinnerRechner();
            }
        }

        private void HitExecute(object parameter)
        {
            if (_spielBeendet)
            {
                return;
            }
            if (_deck == null)
            {
                SpielStatus = "Keine aktive Runde!";
                return;
            }
            HausverbotPruefen();
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
            if (IstBlackJack())
            {
                Guthaben += Einsatz * 3;

                CurrentUser.User.Balance = Guthaben;

                SpielSpeichern(Einsatz * 3, true);

                SpielStatus = "BLACKJACK! Einsatz x3 gewonnen!";

                return;
            }
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
                HausverbotPruefen();
                
                SpielSpeichern(Einsatz, false);

                SpielStatus = "Dealer gewinnt!";
            }
            else
            {
                SpielStatus = "Unentschieden!";
            }
        }
        private bool IstBlackJack()
        {
            return _spielerHand.Count == 2 && SpielerPunkte == 21;
        }

        private void SpielSpeichern(decimal gewinnVerlust, bool gewonnen)
        {
            if (CurrentUser.User == null)
            {
                return;
            }
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

        private void EinsatzMenueExecute(object parameter)
        {
            EinsatzButtonsSichtbar = !EinsatzButtonsSichtbar;
        }
        private void Plus1Execute(object parameter) { Einsatz += 1; }
        private void Plus5Execute(object parameter) { Einsatz += 5; }
        private void Plus20Execute(object parameter) { Einsatz += 20; }
        private void Plus100Execute(object parameter) { Einsatz += 100; }
        private void Plus250Execute(object parameter) { Einsatz += 250; }
        private void Plus1000Execute(object parameter) { Einsatz += 1000; }
        private void Plus5000Execute(object parameter) { Einsatz += 5000; }
        private void Plus10000Execute(object parameter) { Einsatz += 10000; }

        private void Minus1Execute(object parameter)
        {
            if (Einsatz - 1 >= 1)
            {
                Einsatz -= 1;
            }
        }

        private void Minus5Execute(object parameter)
        {
            if (Einsatz - 5 >= 1)
            {
                Einsatz -= 5;
            }
        }

        private void Minus20Execute(object parameter)
        {
            if (Einsatz - 20 >= 1)
            {
                Einsatz -= 20;
            }
        }

        private void Minus100Execute(object parameter)
        {
            if (Einsatz - 100 >= 1)
            {
                Einsatz -= 100;
            }
        }

        private void Minus250Execute(object parameter)
        {
            if (Einsatz - 250 >= 1)
            {
                Einsatz -= 250;
            }
        }

        private void Minus1000Execute(object parameter)
        {
            if (Einsatz - 1000 >= 1)
            {
                Einsatz -= 1000;
            }
        }

        private void Minus5000Execute(object parameter)
        {
            if (Einsatz - 5000 >= 1)
            {
                Einsatz -= 5000;
            }
        }

        private void Minus10000Execute(object parameter)
        {
            if (Einsatz - 10000 >= 1)
            {
                Einsatz -= 10000;
            }
        }
        private void DoubleDownExecute(object parameter)
        {
            if (_spielBeendet)
            {
                return;
            }

            Einsatz = Einsatz * 2;

            _spielerHand.Add(_deck.ZieheKarte());

            AktualisiereAnzeige();

            if (SpielerPunkte > 21)
            {
                _spielBeendet = true;

                Guthaben -= Einsatz;

                CurrentUser.User.Balance = Guthaben;
                HausverbotPruefen();
                SpielSpeichern(-Einsatz, false);

                SpielStatus = "Bust! Double Down verloren!";

                return;
            }

            StandExecute(null);
        }
        private void SpielInfoExecute(object parameter)
        {
            MessageBox.Show(

            "Hit:\nKarte ziehen\n\n" +

            "Stand:\nZug beenden\n\n" +

            "Double Down:\nEinsatz verdoppeln + eine neue Karte\n\n" +
    
            "Einsatz:\nEinsatz erhöhen oder verringern\n\n" +

            "Neue Runde:\nNeues Spiel starten\n\n" +

            "Gewinn/Verlust:\nWin; Einsatz*2 | Lose; -Einsatz\n\n" +

            "Dealer:\nDie zweite Karte bleibt verdeckt",

            "Spielhilfe");
        }
        private void HausverbotPruefen()
        {
            if (Guthaben < 0)
            {
                using (BlackJackDB_Context db = new BlackJackDB_Context())
                {
                    User user = db.Users.FirstOrDefault(u => u.Id == CurrentUser.User.Id);

                    if (user != null)
                    {
                        user.IsBanned = true;
                        db.SaveChanges();
                    }
                }

                MessageBox.Show("Sie haben Hausverbot!\nDer Zugriff wurde gesperrt.", "Hausverbot", MessageBoxButton.OK, MessageBoxImage.Stop);

                CurrentUser.User = null;

                EventAggregator.GetEvent<LogoutEvent>().Publish();
            }
        }
    }
}