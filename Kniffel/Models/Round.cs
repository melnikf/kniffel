using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kniffel.Models
{
    public class Round
    {
        public uint Id { get; set; }
        public uint GameId { get; set; }
        public ushort Result { get; set; }
        public ushort Bet { get; set; }
    }
}