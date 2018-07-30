using System.Data.Entity;

namespace DatabaseMethods
{
    public class GamesDb : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
