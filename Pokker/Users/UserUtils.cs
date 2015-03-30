using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pokker.Models;

namespace Pokker.Users
{
    public static class UserUtils
    {
        public static bool StringValid(string str, int min)
        {
            if (str == null) return false;
            if (str.Trim().Length < min) return false;
            return true;
        }

        public static bool MailValid(string mail)
        {
            // ОЧЕНЬ простая проверка адреса :)
            if (mail == null) return false;
            if (mail.Trim().Length < 3) return false;
            if (!mail.Contains('@')) return false;
            return true;
        }

        public static bool LoginSuccess (string name, string pass)
        {
            using (PokkerDbContext ctx = new PokkerDbContext())
            {
                var player = ctx.Players.FirstOrDefault(p => p.Name == name && p.Pass == pass);
                if (player == null) return false;
            }

            return true;
        }

        public static bool NameExists(string name)
        {
            using (PokkerDbContext ctx = new PokkerDbContext())
            {
                var player = ctx.Players.FirstOrDefault(p => p.Name == name);
                if (player == null) return false;
            }

            return true;
        }

        public static bool EmailExists(string email)
        {
            using (PokkerDbContext ctx = new PokkerDbContext())
            {
                var player = ctx.Players.FirstOrDefault(p => p.Email == email);
                if (player == null) return false;
            }

            return true;
        }

        public static void SignUpUser(string name, string email, string pass)
        {
            // TODO: Добавить проверку входящих данных.

            using (PokkerDbContext ctx = new PokkerDbContext())
            {
                Player player = new Player();

                player.PlayerId = -1;
                player.Name = name;
                player.Email = email;
                player.Pass = pass;
                player.Cash = 100;
                player.RegDate = DateTime.Now.ToString();

                ctx.Players.Add(player);
                ctx.SaveChanges();
            }
        }
    }
}