using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kniffel.Models
{
    public class Player
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public uint Cash { get; set; }
    }
}