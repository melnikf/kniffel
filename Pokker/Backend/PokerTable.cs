﻿using Pokker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Pokker.Backend
{
    public class PokerTable
    {
        private Random rnd;

        private PokerSettings stg;

        private Deck deck;                      // Колода.
        private List<PokerPlayer> players;      // Игроки.
        private List<Card> ocards;              // Открытые карты.

        /*
         * 0 - ready-to-game
         * 1 - pre-flop
         * 2 - flop
         * 3 - turn
         * 4 - river
         * 5 - showdown
         */
        private int round = -1;
        private int aplayer = -1;
        private int button = -1;

        public event EventHandler PlayerJoined;
        public event EventHandler<ReadyEventArgs> Ready;
        public event EventHandler<BettingEventArgs> Betting;
        public event EventHandler Showdown;

        public PokerSettings Settings
        {
            get { return this.stg; }
        }

        public int PlayersInGame
        {
            get { return players.Count; }
        }

        public int Round
        {
            get { return round; }
        }

        public int ActivePlayer
        {
            get { return aplayer; }
        }

        public bool GameStarted
        {
            get { return round > 0; }
        }

        public PokerTable()
        {
            rnd = new Random();
            stg = new PokerSettings();
            deck = new Deck();
            players = new List<PokerPlayer>(stg.PlayersMax);
            ocards = new List<Card>();
        }

        public int GetPlayerBet(int n)
        {
            return (int)players[n].BetTotal;
        }

        public string GetPlayerName(int n)
        {
            return players[n].Name;
        }

        public int Join(string name)
        {
            PokerPlayer tpl;
            Player pl;

            int delay;
            int id = -1;

            if (players.Count != stg.PlayersMax && round < 1)
            {
                using (PokkerDbContext ctx = new PokkerDbContext())
                    pl = ctx.Players.FirstOrDefault(p => p.Name == name);

                if (pl != null)
                {
                    id = rnd.Next(1000, 10000);

                    tpl = new PokerPlayer((uint)id, pl.PlayerId, pl.Name, (uint)pl.Cash);
                    players.Add(tpl);
                    
                    this.PlayerJoinedInvoke();

                    // Запускаем игру, если гроков достаточно.
                    if (players.Count >= stg.PlayersMin && round < 0)
                    {
                        round = 0;
                        delay = players.Count == stg.PlayersMax ? 0 : stg.StartDelay;
                        this.ReadyInvoke((uint)delay);
                        Task.Run(async () => 
                            {
                                await Task.Delay(delay);
                                this.StartGame();
                            });
                    }
                }
            }

            return id;
        }

        public bool Fold(uint id)
        {
            bool completed = false;
            var tpl = players.FirstOrDefault(p => p.Id == id);

            if(tpl != null && tpl.Id == aplayer)
            {
                if (!tpl.Folded)
                {
                    tpl.Fold();
                    completed = true;
                }
            }

            return completed;
        }

        public bool Bet(uint id, uint amount)
        {
            bool completed = false;
            int i = players.FindIndex(p => p.Id == id);

            uint cur;
            uint max;

            if (i != -1 && i == aplayer)
            {
                max = this.MaxBet();
                cur = players[i].BetTotal;

                if (cur + amount >= max && players[i].CanBet(amount))
                {
                    players[i].Bet(amount);
                    completed = true;
                }
            }

            return completed;
        }

        public Card[] OpenCards()
        {
            return ocards.ToArray();
        }

        public Card[] ShowHand(uint id)
        {
            Card[] res = null;
            var tpl = players.FirstOrDefault(p => p.Id == id);

            if (tpl != null)
            {
                res = tpl.ShowHand();
            }

            return res;
        }

        public uint MaxBet()
        {
            uint max = 0;
            foreach(PokerPlayer tpl in players)
            {
                if (tpl.BetTotal > max)
                    max = tpl.BetTotal;
            }
            return max;
        }

        public bool BetsEqual()
        {
            int i;
            bool eq = true;
            for(i = 1; i < players.Count; i++)
            {
                if (players[i].BetTotal != players[i - 1].BetTotal)
                    eq = false;
            }
            return eq;
        }

        private void StartGame()
        {
            button = 0;

            // Раунд 1. Раздаем по две карты. Круг торговли.
            // 
            round = 1;
            ShuffleDeck();
            DealCards(2);
            BettingRound();
            MoveButton();

            // Раунд 2. Кладем три карты. Круг торговли.
            // 
            round = 2;
            ShuffleDeck();
            BettingRound();
            MoveButton();

            // Раунд 3. Кладем одну карту. Круг торговли.
            // 
            round = 3;
            ShuffleDeck();
            BettingRound();
            MoveButton();

            // Раунд 4. Кладем одну карту. Последний круг торговли.
            // 
            round = 4;
            ShuffleDeck();
            BettingRound();
            MoveButton();

            // Раунд 5. Игроки выбирают комбинации, результат.
            //
            round = 5;
        }

        private void DealCards(int q)
        {
            int i;

            foreach(PokerPlayer tpl in players)
            {
                for (i = 0; i < q; i++)
                    tpl.AddCard(deck.GetCard());
            }
        }

        private void BettingRound()
        {
            do
            {
                this.BettingPart(button);
            } while (!this.BetsEqual());
        }

        private void BettingPart(int offset)
        {
            Task tsk;
            int total;
            int n;

            total = players.Count;
            n = offset + 1;
            for(int i = 0; i < total; i++)
            {
                if (n == total) n = 0;
                if(!players[n].Folded)
                {
                    aplayer = n;
                    tsk = new Task(() => this.WaitPlayer(n));
                    this.BettingInvoke(players[n].Id, (uint)stg.WaitTimeout);
                    tsk.Start();

                    if (!tsk.Wait(stg.WaitTimeout))
                        players[n].Fold();
                }
                n++;
            }
        }

        private void MoveButton()
        {
            button++;
            if (button == players.Count)
                button = 0;
        }

        private void ShuffleDeck()
        {
            deck.Shuffle(this.rnd);
        }

        private void BettingInvoke(uint player, uint timeout)
        {
            EventHandler<BettingEventArgs> eh = this.Betting;
            if (eh != null)
            {
                BettingEventArgs ea = new BettingEventArgs();
                ea.Player = player;
                ea.Timeout = timeout;

                eh(this, ea);
            }
        }

        private void PlayerJoinedInvoke()
        {
            EventHandler eh = this.PlayerJoined;
            if (eh != null)
            {
                eh(this, EventArgs.Empty);
            }
        }

        private void ReadyInvoke(uint delay)
        {
            EventHandler<ReadyEventArgs> eh = this.Ready;
            if (eh != null)
            {
                eh(this, new ReadyEventArgs(delay));
            }
        }

        private void WaitPlayer(int pn)
        {
            players[pn].NeedAction();
            while(true)
            {
                if (!players[pn].Sleeping)
                    return;
            }
        }
    }
}