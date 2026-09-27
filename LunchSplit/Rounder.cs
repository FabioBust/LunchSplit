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
            List<Share> roundedShares;
            for (int i = 0; i <= rawShares.Count; i++)
            {
                switch (mode)
                {
                    case RoundingMode.None:
                        roundedShares = rawShares;
                        break;
                    case RoundingMode.Bankers:
                        roundedShares.Amount[i] = Math.Round(rawShares.Amount[i], 2, MidpointRounding.ToEven);
                        break;
                    case RoundingMode.Ceil:
                        roundedShares.Amount[i] = Math.Ceiling(rawShares.Amount[i] * 100) / 100;
                        break;
                    case RoundingMode.Floor:
                        roundedShares.Amount[i] = Math.Floor(rawShares.Amount[i] * 100) / 100;
                        break;
                }
            }
            return roundedShares;
        }
    }
}
