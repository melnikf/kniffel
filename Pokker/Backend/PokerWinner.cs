using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokker.Backend
{
    public class PokerWinner
    {
        public string Name
        {
            get; private set;
        }

        public int WinTotal
        {
            get; private set;
        }

        public int Combination
        {
            get; private set;
        }

        public PokerWinner(string name, int winTotal, int comb)
        {
            this.Name = name;
            this.WinTotal = winTotal;
            this.Combination = comb;
        }
    }
}