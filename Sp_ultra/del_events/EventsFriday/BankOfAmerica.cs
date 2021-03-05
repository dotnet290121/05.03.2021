using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsFriday
{
    public class BankOfAmerica
    {
        public delegate void notify_delegate(double maded, string msg);
        public notify_delegate NotifyMeWhenStocksChanged;

        public void StockChanged()
        {
            double madad = 100 + 1.1 * 1.5435;

            if (NotifyMeWhenStocksChanged != null)
            {
                NotifyMeWhenStocksChanged(madad, "changed by one stock!");
            }
        }
    }
}
