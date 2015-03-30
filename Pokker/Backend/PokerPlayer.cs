using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokker.Backend
{
    public class PokerPlayer
    {
        public enum PlayerAction { None, Fold, Check, Call, Raise };

        private uint id;                // Ид. в игре.
        private int db_id;              // Ид. в базе.
        private string name;            // Имя.
        private uint cash;              // Кэш общий.
        private uint betTotal;          // Сколько поставил всего (не больше кэша).

        private bool folded = false;
        private bool sleep = false;

        private List<Card> hand;

        public uint Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        public uint BetTotal
        {
            get { return betTotal; }
        }

        public uint Cash
        {
            get { return cash; }
        }

        public bool Folded
        {
            get { return folded; }
        }

        public bool Sleeping
        {
            get { return sleep; }
        }


        public PokerPlayer(uint id, int db_id, string name, uint cash)
        {
            this.id = id;
            this.db_id = db_id;
            this.name = name;
            this.cash = cash;
            this.betTotal = 0;

            this.hand = new List<Card>();
        }

        public void NeedAction()
        {
            sleep = true;
        }

        public Card[] ShowHand()
        {
            return hand.ToArray();
        }

        public void AddCard(Card card)
        {
            this.hand.Add(card);
        }

        public void Fold()
        {
            sleep = false;
            folded = true;
        }

        public bool CanBet(uint amount)
        {
            return betTotal + amount <= cash && !folded;
        }

        public void Bet(uint amount)
        {
            if(!CanBet(amount))
                throw new Exception("Bet error");

            sleep = false;
            betTotal += amount;
        }
    }
}