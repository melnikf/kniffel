using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokker.Backend
{
    public class ReadyEventArgs : EventArgs
    {
        public uint Delay { get; set; }

        public ReadyEventArgs(uint delay)
        {
            this.Delay = delay;
        }
    }
}