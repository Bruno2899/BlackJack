using BlackJack.Common;
using BlackJack.Events;
using Prism.Events;
using System.Windows.Input;
using BlackJack.Modelle;
using System.Collections.Generic;

namespace BlackJack.ViewModels
{
    public class BlackJackViewModel : ViewModelBase
    {
        private string _spielerKarten;
        private string _dealerKarten;
        private int _spielerPunkte;
        private int _dealerPunkte; 
        private Deck _deck;

        private List<Karten> _spielerHand;

        private List<Karten> _dealerHand;
        public string SpielerKarten
        {
            get
            {
                return _spielerKarten;
            }
            set
            {
                _spielerKarten = value;
                OnPropertyChanged(nameof(SpielerKarten));
            }
        }
        public string DealerKarten
        {
            get
            {
                return _dealerKarten;
            }
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
            get
            {
                return _dealerPunkte;
            }
            set
            {
                _dealerPunkte = value;
                OnPropertyChanged(nameof(DealerPunkte));
            }
        }

        public ICommand HitCommand
        {
            get;
            private set;
        }

        public ICommand StandCommand
        {
            get;
            private set;
        }

        public ICommand BackCommand
        {
            get;
            private set;
        }

        public BlackJackViewModel(IEventAggregator eventAggregator): base(eventAggregator)
        {
            _deck = new Deck();

            _spielerHand = new List<Karten>();

            _dealerHand = new List<Karten>();

            _spielerHand.Add(_deck.ZieheKarte());
            _spielerHand.Add(_deck.ZieheKarte());

            _dealerHand.Add(_deck.ZieheKarte());
            _dealerHand.Add(_deck.ZieheKarte());

            AktualisiereAnzeige();

            HitCommand = new ActionCommand(HitExecute,HitCanExecute);

            StandCommand = new ActionCommand(StandExecute,StandCanExecute);

            BackCommand = new ActionCommand(BackExecute,BackCanExecute);
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

        private void HitExecute(object parameter)
        {
            _spielerHand.Add(_deck.ZieheKarte());

            AktualisiereAnzeige();

            if (SpielerPunkte > 21)
            {
                System.Windows.MessageBox.Show("Du hast verloren!");
            }
        }
        private void StandExecute(object parameter)
        {
            AktualisiereAnzeige();
        }

        private void BackExecute(object parameter)
        {
            EventAggregator.GetEvent<BackToDashboardEvent>().Publish();
        }
        private void AktualisiereAnzeige()
        {
            SpielerKarten = "";
            SpielerPunkte = 0;
            DealerKarten = "";
            DealerPunkte = 0;

            foreach (Karten karte in _spielerHand)
            {   
                SpielerKarten +=karte.Beschreibung + " ";
                SpielerPunkte += karte.Wert;
            }

            foreach (Karten karte in _dealerHand)
            {
                DealerKarten += karte.Beschreibung + " ";
                DealerPunkte += karte.Wert;
            }
        }
    }
}