/*Applies decimal precision rules. Exposes RoundShares(List<Share> rawShares, 
 * RoundingMode mode) -> List<Share>. Must implement the Penny Reconciliation 
 * algorithm.*/

using System;
using System.Collections.Generic;

namespace LunchSplit
{
    public class Rounder
    {
        public List <Share> RoundShares(List<Share> rawShares, RoundingMode mode)
        {
            List<Share> roundedShares = new List<Share>();

            decimal rawTotal = 0.00m;
            decimal roundedTotal = 0.00m;

            for (int i = 0; i < rawShares.Count; i++)
            {
                decimal roundedAmount;
                rawTotal += rawShares[i].Amount;

                switch (mode)
                {
                    case RoundingMode.None:
                        roundedAmount = rawShares[i].Amount;
                        break;

                    case RoundingMode.Bankers:
                        roundedAmount = Math.Round(rawShares[i].Amount, 2, MidpointRounding.ToEven);
                        break;

                    case RoundingMode.Ceil:
                        roundedAmount = Math.Ceiling(rawShares[i].Amount * 100) / 100;
                        break;

                    case RoundingMode.Floor:
                        roundedAmount = Math.Floor(rawShares[i].Amount * 100) / 100;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(mode));
                }

                roundedTotal += roundedAmount;
                Share roundedShare = new Share(rawShares[i].Name, roundedAmount);

                roundedShares.Add(roundedShare);
            }

            // Penny reconciliation
            if (roundedShares.Count <= 1)
            {
                return roundedShares;
            }

            decimal targetTotal = Math.Round(rawTotal, 2, MidpointRounding.ToEven);

            decimal difference = targetTotal - roundedTotal;

            int index = 0;

            while (difference != 0 && roundedShares.Count > 0)
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
    }
}
