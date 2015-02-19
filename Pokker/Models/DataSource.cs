using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokker.Models
{
    public class DataSource
    {
        private PokkerDbContext ctx = new PokkerDbContext();

        public IEnumerable<Player> Players
        {
            get { return ctx.Players; }
        }

        public IEnumerable<Game> Games
        {
            get { return ctx.Games; }
        }
    }
}