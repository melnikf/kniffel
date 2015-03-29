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
                chips = player.Chips;
            }
            return chips;
        }

        public static int TakeGames(string login)
        {
            int id_player;
            int tmp;
            using (PokkerDbContext ctx = new PokkerDbContext())
            {
                var player = ctx.Players.FirstOrDefault(p => p.Name == login);
                id_player = player.PlayerId;
                IEnumerable<Game> games = ctx.Games.Where(p => p.PlayerId == id_player);
                tmp = games.Count();
            }
            return tmp;
        }
    }
}