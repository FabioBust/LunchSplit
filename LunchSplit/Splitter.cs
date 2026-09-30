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

                default:
                    throw new ArgumentOutOfRangeException(nameof(mode));
            }   
            return tip;
        }

        public List <Share> CalculateShares(Bill bill, List<Attendee> attendees, RoundingMode mode)
        {
            decimal tip = ComputeTip(bill.Subtotal, bill.Mode, bill.TipInput);
            decimal total = bill.Subtotal + bill.Tax + tip;

            int totalWeight = 0;
            int includedCount = 0;

            foreach (Attendee attendee in attendees)
            {
                if (attendee.Included)
                {
                    includedCount++;
                    totalWeight += attendee.Weight;
                }
            }

            if (includedCount == 0)
            {
                throw new InvalidOperationException();
            }

            if (totalWeight == 0)
            {
                throw new DivideByZeroException();
            }

            List<Share> shares = new List<Share>();

            foreach (Attendee attendee in attendees)
            {
                decimal amount = 0.00m;

                if (attendee.Included)
                {
                    amount = total * attendee.Weight / totalWeight;
                }

                shares.Add(new Share(attendee.Name, amount));
            }

            Rounder rounder = new Rounder();

            return rounder.RoundShares(shares, mode);
        }
    }
}
