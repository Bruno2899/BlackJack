namespace BlackJack.Modelle
{
    public class Karten
    {
        public int Id { get; set; }

        // Herz, Karo, Pik, Kreuz
        public string Art { get; set; }

        // Ass, König, Dame, Bube, 2-10
        public string Beschreibung { get; set; }

        public int Wert { get; set; }
    }
}