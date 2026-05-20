using Microsoft.EntityFrameworkCore;

namespace BlackJack.Modelle
{
    public class BlackJackDB_Context : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Karten> Karten { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=db54854.public.databaseasp.net;Database=db54854;User Id=db54854;Password=WerdasWeiß;TrustServerCertificate=True;");
        }
    }
}