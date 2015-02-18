using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kniffel.Models
{
    public class DataSource
    {
        private KniffelDbContext ctx = new KniffelDbContext();

        public IEnumerable<Player> Players
        {
            get { return ctx.Players; }
        }

        public IEnumerable<Game> Games
        {
            get { return ctx.Games; }
        }

        public IEnumerable<Round> Throws
        {
            get { return ctx.Rounds; }
        }
    }
}