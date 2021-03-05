using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsFriday
{
    public class StocksBoard
    {
        private void RedrawBoard(double maded, string msg)
        {
            Console.WriteLine($"Redrawring board with data: {maded} {msg}");
        }

        public StocksBoard(BankOfAmerica boa)
        {
            boa.NotifyMeWhenStocksChanged += RedrawBoard;
        }
    }
}
