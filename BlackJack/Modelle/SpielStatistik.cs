namespace BlackJack.Modelle
{
    public class SpielStatistik
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal GewinnVerlust { get; set; }

        public bool Gewonnen { get; set; }
    }
}