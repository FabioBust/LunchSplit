/* Contains the business calculation flow. Exposes ComputeTip(decimal subtotal, 
 * TipMode mode, decimal tipInput) -> decimal tip and CalculateShares(Bill bill, 
 * List<Attendee> attendees, RoundingMode roundingMode) -> List<Share>.*/

using System;
using System.Collections.Generic;
using System.Text;
using LunchSplit;
using static LunchSplit.TipMode;

namespace LunchSplit
{   
    public class Splitter
    {
        public decimal ComputeTip(decimal subtotal, TipMode mode, decimal tipInput)
        {
            decimal tip;
            switch (mode)
            {
                case TipMode.None:
                    tip = 0.00m;
                    break;
                case TipMode.Percent:
                    tip = subtotal * (tipInput / 100);
                    break;
                case TipMode.Fixed:
                    tip = tipInput;
                    break;  
            }   
            return tip;
        }

        List <Share> CalculateShares(Bill bill, List<Attendee> attendees, RoundingMode mode)
        {
            decimal total = bill.Subtotal + bill.Tax + bill.TipInput;

            return shares;
        }
    }
}
