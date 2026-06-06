using System;
using System.Collections.Generic;

namespace BlackJack.Modelle
{
    public class Deck
    {
        public List<Karten> KartenDeck
        {
            get;
            set;
        }

        private Random random = new Random();

        public Deck()
        {
            KartenDeck = new List<Karten>();

            DeckErstellen();
        }

        private void DeckErstellen()
        {
            string[] farben =
            {
                "Herz",
                "Karo",
                "Pik",
                "Kreuz"
            };

            foreach (string farbe in farben)
            {
                for (int i = 2; i <= 10; i++)
                {
                    KartenDeck.Add(new Karten
                    {
                        Art = farbe,
                        Beschreibung = Convert.ToString(i),
                        Wert = i
                    });
                }

                KartenDeck.Add(new Karten
                {
                    Art = farbe,
                    Beschreibung = "Bube",
                    Wert = 10
                });

                KartenDeck.Add(new Karten
                {
                    Art = farbe,
                    Beschreibung = "Dame",
                    Wert = 10
                });

                KartenDeck.Add(new Karten
                {
                    Art = farbe,
                    Beschreibung = "König",
                    Wert = 10
                });

                KartenDeck.Add(new Karten
                {
                    Art = farbe,
                    Beschreibung = "Ass",
                    Wert = 11
                });
            }
        }

        public Karten ZieheKarte()
        {
            int index =random.Next(KartenDeck.Count);

            Karten karte =KartenDeck[index];

            KartenDeck.RemoveAt(index);

            return karte;
        }
    }
}