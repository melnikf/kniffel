using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Pokker.Models
{
    public class PokkerDbContext : DbContext
    {
        public PokkerDbContext() : base("PokkerDbContext") 
        {
            Database.SetInitializer<PokkerDbContext>(null);
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
    }
}