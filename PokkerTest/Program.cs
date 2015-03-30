using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pokker.Game;
using Pokker.Models;

namespace PokkerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Table tbl = new Table();

            tbl.Join("player1");
            tbl.Join("player2");
            tbl.Join("player3");
            tbl.Join("player4");

            Console.ReadKey();
        }
    }
}
