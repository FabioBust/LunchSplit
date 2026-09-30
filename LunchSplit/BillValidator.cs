/*Guarantees data integrity before execution. Exposes Validate(Bill bill, 
 * List<Attendee> attendees) -> ValidationResult. Blocks negative subtotals, 
 * zero/empty attendees, and negative weights.*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static LunchSplit.ValidationResult;

namespace LunchSplit
{
    public class BillValidator
    {
        public ValidationResult Validate(Bill bill, List<Attendee> attendees)
        {
            if (bill.Subtotal < 0)
            {
                return new ValidationResult(false, "Subtotal cannot be negative.");
            }

            if (bill.Tax < 0)
            {
                return new ValidationResult(false, "Tax cannot be negative.");
            }

            if (attendees == null || attendees.Count == 0)
            {
                return new ValidationResult(false, "At least one attendee is required.");
            }

            foreach (Attendee attendee in attendees)
            {
                if (attendee.Weight < 0)
                {
                    return new ValidationResult(false, "Attendee weight cannot be negative.");
                }
            }

            return new ValidationResult(true, "");
        }
    }
}