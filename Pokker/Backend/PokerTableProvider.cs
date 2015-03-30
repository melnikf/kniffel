using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokker.Backend
{
    public class PokerTableProvider
    {
        private static PokerTable table = null;

        static PokerTableProvider()
        {
            table = new PokerTable();
        }

        public static PokerTable GetTable()
        {
            return table;
        }
    }
}