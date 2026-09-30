/*Applies decimal precision rules. Exposes RoundShares(List<Share> rawShares, 
 * RoundingMode mode) -> List<Share>. Must implement the Penny Reconciliation 
 * algorithm.*/

using System;
using System.Collections.Generic;
using System.Text;
using LunchSplit;
using static LunchSplit.RoundingMode;
using static LunchSplit.Share;

namespace LunchSplit
{
    public class Rounder
    {
        public List <Share> RoundShares(List<Share> rawShares, RoundingMode mode)
        {
            List<Share> roundedShares = new List<Share>();
            for (int i = 0; i < rawShares.Count; i++)
            {
                decimal roundedAmount;

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

                Share roundedShare = new Share(rawShares[i].Name, roundedAmount);

                roundedShares.Add(roundedShare);
            }
            return roundedShares;
        }
    }
}
