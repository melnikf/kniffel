using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokker.Models
{
    public class Game
    {
        public uint GameId { get; set; }
        public string Name { get; set; }
        public uint PlayerId { get; set; }
        public uint Chips { get; set; }
        public byte Result { get; set; }
    }
}