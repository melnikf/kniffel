using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokker.Backend
{
    public class Combination
    {
        Card[] cards;

        public Combination(Card[] cards)
        {
            if (cards == null || cards.Length != 5)
                throw new ArgumentException("Five cards!", "cards");

            this.cards = cards;
            Array.Sort(cards);
        }

        public int FindBest()
        {
            if (this.RoyalFlush()) return 9;
            if (this.StraightFlush()) return 8;
            if (this.Four()) return 7;
            if (this.FullHouse()) return 6;
            if (this.Flush()) return 5;
            if (this.Straight()) return 4;
            if (this.Three()) return 3;
            if (this.TwoPair()) return 2;
            if (this.Pair()) return 1;

            return 0;
        }

        // 9 - роял-флэш
        private bool RoyalFlush()
        {
            int i;
            int n;

            n = 9;
            for(i = 0; i < 5; i++)
            {
                if(cards[i].weight != n) return false;
                n++;
            }
            for(i = 0; i < 5; i++)
            {
                if (cards[i].suit != cards[i + 1].suit) return false;
            }
            return true;
        }

        // 8 - стрит-флэш
        private bool StraightFlush()
        {
            int i;
            int n;

            n = cards[0].weight + 1;
            for (i = 1; i < 5; i++)
            {
                if (cards[i].weight != n) return false;
                n++;
            }
            for (i = 0; i < 5; i++)
            {
                if (cards[i].suit != cards[i + 1].suit) return false;
            }
            return true;
        }

        // 7 - карэ
        private bool Four()
        {
            if (this.Kind(4, 0) > 0)
                return true;
            else
                return false;
        }

        // 6 - фул-хаус
        private bool FullHouse()
        {
            int w;
            w = this.Kind(3, 0);
            if(w > 0)
            {
                w = this.Kind(2, w);
                if (w > 0) return true;
            }

            return false;
        }

        // 5 - флэш
        private bool Flush()
        {
            int i;

            for (i = 0; i < 5; i++)
            {
                if (cards[i].suit != cards[i + 1].suit) return false;
            }

            return true;
        }

        // 4 - стрит
        private bool Straight()
        {
            int i;
            int n;

            n = cards[0].weight + 1;
            for (i = 1; i < 5; i++)
            {
                if (cards[i].weight != n) return false;
                n++;
            }

            return true;
        }

        // 3 - тройка
        private bool Three()
        {
            if (this.Kind(3, 0) > 0)
                return true;
            else
                return false;
        }

        // 2 - две пары
        private bool TwoPair()
        {
            int w;
            w = this.Kind(2, 0);
            if (w > 0)
            {
                w = this.Kind(2, w);
                if (w > 0) return true;
            }

            return false;
        }

        // 1 - пара
        private bool Pair()
        {
            if (this.Kind(2, 0) > 0)
                return true;
            else
                return false;
        }

        private int Kind(int n, int exc)
        {
            int i, j, w, c;

            for (i = 0; i < 5; i++)
            {
                w = cards[i].weight;
                c = 1;
                for (j = 0; j < 5; j++)
                {
                    if (i == j) continue;
                    if (cards[j].weight == exc) continue;
                    if (cards[j].weight == w) c++;
                    if (c == n) return w;
                }
            }

            return -1;
        }
    }
}