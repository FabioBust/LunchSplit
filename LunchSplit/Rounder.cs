/*Applies decimal precision rules. Exposes RoundShares(List<Share> rawShares, 
 * RoundingMode mode) -> List<Share>. Must implement the Penny Reconciliation 
 * algorithm.*/

namespace LunchSplit
{
    public class Rounder
    {
        public List<Share> RoundShares(List<Share> rawShares, RoundingMode mode)
        {
            List<Share> roundedShares = new List<Share>();

            decimal rawTotal = 0.00m;
            decimal roundedTotal = 0.00m;

            // Round each individual share
            foreach (Share share in rawShares)
            {
                rawTotal += share.Amount;

                decimal roundedAmount = RoundAmount(share.Amount, mode);
                roundedTotal += roundedAmount;

                roundedShares.Add(new Share(share.Name, roundedAmount));
            }

            // Penny reconciliation is only needed when splitting between multiple people
            if (roundedShares.Count <= 1)
            {
                return roundedShares;
            }

            decimal targetTotal = Math.Round(rawTotal, 2, MidpointRounding.ToEven);

            decimal difference = targetTotal - roundedTotal;

            int index = 0;

            // Adjust shares by one cent until the rounded total matches
            while (difference != 0)
            {
                decimal adjustment;

                if (difference > 0)
                {
                    adjustment = 0.01m;
                }
                else
                {
                    adjustment = -0.01m;
                }

                decimal newAmount = roundedShares[index].Amount + adjustment;

                roundedShares[index] = new Share(roundedShares[index].Name, newAmount);

                difference -= adjustment;

                index++;

                if (index == roundedShares.Count)
                {
                    index = 0;
                }
            }

            return roundedShares;
        }

        // Apply the selected rounding rule
        private decimal RoundAmount(decimal amount, RoundingMode mode)
        {
            switch (mode)
            {
                case RoundingMode.None:
                    return amount;

                case RoundingMode.Bankers:
                    return Math.Round(amount, 2, MidpointRounding.ToEven);

                case RoundingMode.Ceil:
                    return Math.Ceiling(amount * 100) / 100;

                case RoundingMode.Floor:
                    return Math.Floor(amount * 100) / 100;

                default:
                    throw new ArgumentOutOfRangeException(nameof(mode));
            }
        }
    }
}
