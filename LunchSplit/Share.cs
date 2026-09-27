/* A resulting ledger model containing string Name and decimal Amount (their 
 * calculated portion of the grand total).*/

using System;
using System.Collections.Generic;
using System.Text;

namespace LunchSplit
{
    public class Share
    {
        public string Name { get; }
        public decimal Amount { get; }

        public Share(string name, decimal amount)
        {
            Name = name;
            Amount = amount;
        }
    }
}
