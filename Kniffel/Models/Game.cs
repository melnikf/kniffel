using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kniffel.Models
{
    public class Game
    {
        public uint GameId { get; set; }
        public string Name { get; set; }
        public uint PlayerId { get; set; }
        public byte Result { get; set; } // 0 - поражение, 1 - победа, 2 - ничья
        public uint ChipsLost { get; set; } // сколько поставил
        public uint ChipsWon { get; set; } // сколько выиграл
    }
}