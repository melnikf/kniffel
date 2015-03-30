using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pokker.Models;

namespace Pokker.MainPage
{
    public class MainPageUtils
    {
        public static string TakeEmail(string login)
        {
            string email;
            using (PokkerDbContext ctx = new PokkerDbContext())
            {
                var player = ctx.Players.FirstOrDefault(p => p.Name == login);
                email = player.Email;
            }
            return email;
        }

        public static int TakeChips(string login)
        {
            int chips;
            using (PokkerDbContext ctx = new PokkerDbContext())
            {
                var player = ctx.Players.FirstOrDefault(p => p.Name == login);
                chips = player.Cash;
            }
            return chips;
        }

        public static Game[] TakeGames(string login)
        {
            int id_player;
            IEnumerable<Game> tmp;
            Game[] games;
            using (PokkerDbContext ctx = new PokkerDbContext())
            {
                int i=0;
                var player = ctx.Players.FirstOrDefault(p => p.Name == login);
                id_player = player.PlayerId;
                tmp = ctx.Games.Where(p => p.PlayerId == id_player);
                games=new Game[tmp.Count()];
                foreach(Game p in tmp)
                {
                    games[i]=p;
                    i++;
                }
            }
            return games;
        }
    }
}