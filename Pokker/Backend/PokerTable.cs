using Pokker.Models;
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
        private List<PokerWinner> winners;      // Победители.
        private List<Card> board;               // Открытые карты.

        private int[,] combs = 
        {
            {0,1,2,3,4},
            {0,1,2,3,5},
            {0,1,2,3,6},
            {0,1,2,4,5},
            {0,1,2,4,6},
            {0,1,2,5,6},
            {0,1,3,4,5},
            {0,1,3,4,6},
            {0,1,3,5,6},
            {0,1,4,5,6},
            {0,2,3,4,5},
            {0,2,3,4,6},
            {0,2,3,5,6},
            {0,2,4,5,6},
            {0,3,4,5,6},
            {1,2,3,4,5},
            {1,2,3,4,6},
            {1,2,3,5,6},
            {1,2,4,5,6},
            {1,3,4,5,6},
            {2,3,4,5,6}
        };

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

        private uint bank = 0;

        private string gameName = "";

        public event EventHandler Updated;
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

        public int Bank
        {
            get { return (int)bank; }
        }

        public bool Finished
        {
            get { return round == 5; }
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
            winners = new List<PokerWinner>(stg.PlayersMax);
            board = new List<Card>();
        }

        public PokerWinner[] GetWinners()
        {
            return winners.ToArray();
        }

        public int GetPlayerBet(int n)
        {
            return (int)players[n].BetTotal;
        }

        public string GetPlayerName(int n)
        {
            return players[n].Name;
        }

        public int MyId(string name)
        {
            PokerPlayer tpl;
            int id = -1;

            if(players != null)
            {
                tpl = players.FirstOrDefault(p => p.Name == name);
                if(tpl != null)
                    id = (int)tpl.Id;
            }

            return id;
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

                    // Запускаем игру, если гроков достаточно.
                    if (players.Count >= stg.PlayersMin && round < 0)
                    {
                        round = 0;
                        delay = players.Count == stg.PlayersMax ? 0 : stg.StartDelay;
                        Task.Run(async () => 
                            {
                                await Task.Delay(delay);
                                this.StartGame();
                            });
                    }

                    this.UpdatedInvoke();
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
                    bank += amount;
                    completed = true;
                }
            }

            return completed;
        }

        public Card[] ShowBoard()
        {
            return board.ToArray();
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
            gameName = DateTime.Now.ToFileTimeUtc().ToString();

            this.UpdatedInvoke();

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
            OpenCards(3);
            BettingRound();
            MoveButton();

            // Раунд 3. Кладем одну карту. Круг торговли.
            // 
            round = 3;
            ShuffleDeck();
            OpenCards(1);
            BettingRound();
            MoveButton();

            // Раунд 4. Кладем одну карту. Последний круг торговли.
            // 
            round = 4;
            ShuffleDeck();
            OpenCards(1);
            BettingRound();
            MoveButton();

            // Раунд 5. Игроки выбирают комбинации, результат.
            //
            round = 5;
            ShowDown();
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

        private void OpenCards(int q)
        {
            int i;

            for (i = 0; i < q; i++)
                board.Add(deck.GetCard());
        }

        private int FindBestCards(int n)
        {
            int i, c, nc;

            Card[] targ = new Card[7];

            Combination cmb;

            Array.Copy(players[n].ShowHand(), targ, 2);
            Array.Copy(board.ToArray(), targ, 5);

            c = 0;
            for(i = 0; i < combs.GetLength(0); i++)
            {
                cmb = new Combination(new Card[5] 
                { 
                    targ[combs[i, 0]], 
                    targ[combs[i, 1]], 
                    targ[combs[i, 2]], 
                    targ[combs[i, 3]], 
                    targ[combs[i, 4]] 
                });

                nc = cmb.FindBest();
                if (nc > c) c = nc;
            }

            return c;
        }

        private void FindWinners()
        {
            List<PokerPlayer> tmp = new List<PokerPlayer>(players.Count);
            int[] bestCards = new int[players.Count];
            int bestComb, i, win;

            for(i = 0; i < players.Count; i++)
            {
                bestCards[i] = this.FindBestCards(i);
            }

            bestComb = bestCards.Max();

            for (i = 0; i < players.Count; i++)
            {
                if (bestCards[i] == bestComb)
                {
                    tmp.Add(players[i]);
                }
            }

            win = (int)bank / tmp.Count;

            for (i = 0; i < tmp.Count; i++)
            {
                tmp[i].AddWin((uint)win);
                winners.Add(new PokerWinner(tmp[i].Name, (int)tmp[i].WinTotal, bestComb));
            }
        }

        private void SaveResult()
        {
            foreach(PokerPlayer pl in players)
            {
                pl.Save(gameName);
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
                    this.UpdatedInvoke();
                    tsk.Start();

                    if (!tsk.Wait(stg.WaitTimeout))
                        players[n].Fold();
                }
                n++;
            }
        }

        private void ShowDown()
        {
            Task tsk;

            tsk = new Task(() => 
                {
                    this.FindWinners();
                    this.SaveResult();
                });
            tsk.Start();
            tsk.Wait();
            this.UpdatedInvoke();
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

        private void UpdatedInvoke()
        {
            EventHandler eh = this.Updated;
            if (eh != null)
            {
                eh(this, EventArgs.Empty);
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