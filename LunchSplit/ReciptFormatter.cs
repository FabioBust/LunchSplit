/*Converts resulting lists to printable receipts. Exposes Format(Bill bill, 
 * List<Attendee> attendees, List<Share> roundedShares) -> string. The output 
 * string must include your student name, current date, and creation timestamp.*/

using System;
using System.Collections.Generic;
using System.Text;

namespace LunchSplit
{
    public class ReceiptFormatter
    {
        public string Format(
            Bill bill,
            List<Attendee> attendees,
            List<Share> shares)
        {
            StringBuilder receipt = new StringBuilder();

            receipt.AppendLine("LunchSplit Receipt");
            receipt.AppendLine("Student: Fabio Bustamante Romillo");
            receipt.AppendLine("Date: " + DateTime.Now.ToString("yyyy-MM-dd"));
            receipt.AppendLine("Created: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            receipt.AppendLine();

            receipt.AppendLine("Subtotal: $" + bill.Subtotal.ToString("0.00"));
            receipt.AppendLine("Tax: $" + bill.Tax.ToString("0.00"));
            receipt.AppendLine();

            receipt.AppendLine("Shares:");

            foreach (Share share in shares)
            {
                receipt.AppendLine(
                    share.Name + ": $" + share.Amount.ToString("0.00"));
            }

            return receipt.ToString();
        }
    }
}