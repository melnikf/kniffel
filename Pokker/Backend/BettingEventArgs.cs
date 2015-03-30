using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokker.Backend
{
    public class BettingEventArgs : EventArgs
    {
        public uint Player { get; set; }
        public uint Timeout { get; set; }
    }
}