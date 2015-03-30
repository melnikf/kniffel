using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokker.Backend
{
    public class Deck
    {
        private const int wmax = 13;            // Кол-во весов.
        private const int smax = 4;             // Кол-во мастей.
        private const int dmax = wmax * smax;   // Кол-во карт в колоде.

        int cap = dmax - 1;

        Card[] cards;

        public bool Empty
        {
            get { return cap == -1; }
        }

        public Deck()
        {
            int i, j, n;

            cards = new Card[dmax];

            n = 0;
            for(i = 1; i <= wmax; i++)
            {
                for(j = 1; j <= smax; j++)
                {
                    cards[n].weight = i;
                    cards[n].suit = j;
                    n++;
                }
            }
        }

        public Card GetCard()
        {
            if (this.Empty) throw new Exception("Empty deck!");

            return cards[cap--];
        }

        public void Shuffle(Random rnd)
        {
            int i, swpi;
            Card tmp;

            if (rnd == null) rnd = new Random();

            for (i = cap; i > 0; i--)
            {
                swpi = rnd.Next(i + 1);
                tmp = cards[i];
                cards[i] = cards[swpi];
                cards[swpi] = tmp;
            }
        }
    }
}