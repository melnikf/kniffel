using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokker.Backend
{
    public class PokerSettings
    {
        private int plmax = 10;           // Макс. игроков.
        private int plmin = 2;            // Мин. игроков.

        private int wtime = 360 * 1000;   // Время ожидания действий игрока.
        private int dtime = 1 * 1000;     // Время перед началом игры.

        private int cash = 0;             // Количество фишек на столе.
        private int betLimit = 0;         // Лимит ставки.
        private int ante = 0;             // Анте - минимальная ставка для входа в игру.
        private int buyIn = 0;            // Минимальное количество фишек для входа в игру.

        public int PlayersMax
        {
            get { return plmax; }
        }

        public int PlayersMin
        {
            get { return plmin; }
        }

        public int WaitTimeout
        {
            get { return wtime; }
        }

        public int StartDelay
        {
            get { return dtime; }
        }
    }
}