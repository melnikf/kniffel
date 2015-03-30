using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokker.Backend
{
    public struct Card : IComparable
    {
        public int weight;
        public int suit;

        public int CompareTo(object obj)
        {
            Card card = (Card)obj;

            if (this.weight > card.weight) return 1;
            else if (this.weight < card.weight) return -1;
            else
            {
                if (this.suit > card.suit) return 1;
                else if (this.suit < card.suit) return -1;
                else return 0;
            }
        }
    }
}