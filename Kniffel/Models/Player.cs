using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kniffel.Models
{
    public class Player
    {
        public uint PlayerId { get; set; }
        public string RegDate { get; set; }
        public string Name { get; set; }
        public string Pass { get; set; }
        public string Email { get; set; }
        public uint Balance { get; set; }
    }
}