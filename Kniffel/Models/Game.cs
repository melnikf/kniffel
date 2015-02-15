using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kniffel.Models
{
    public class Game
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public uint PlayerId { get; set; }
        public byte Result { get; set; } // 0 - поражение, 1 - победа, 2 - ничья
        public uint BetAmount { get; set; } // сколько поставил
        public uint WinAmount { get; set; } // сколько выиграл
    }
}