/* Contains the business calculation flow. Exposes ComputeTip(decimal subtotal, 
 * TipMode mode, decimal tipInput) -> decimal tip and CalculateShares(Bill bill, 
 * List<Attendee> attendees, RoundingMode roundingMode) -> List<Share>.*/

namespace LunchSplit
{
    public class Splitter
    {
        public decimal ComputeTip(decimal subtotal, TipMode mode, decimal tipInput)
        {

            // Validate values that cannot be negative
            if (subtotal < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(subtotal));
            }

            if (mode == TipMode.Fixed && tipInput < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(tipInput));
            }

            decimal tip;

            // Calculate the tip based on the selected mode
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

        public List<Share> CalculateShares(Bill bill, List<Attendee> attendees, RoundingMode mode)
        {
            decimal tip = ComputeTip(bill.Subtotal, bill.Mode, bill.TipInput);
            decimal total = bill.Subtotal + bill.Tax + tip;

            int totalWeight = 0;
            int includedCount = 0;

            // Calculate the total weight of included attendees
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

            // Divide the bill according to each attendee's weight
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
