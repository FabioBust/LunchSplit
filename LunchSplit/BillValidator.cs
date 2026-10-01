/*Guarantees data integrity before execution. Exposes Validate(Bill bill, 
 * List<Attendee> attendees) -> ValidationResult. Blocks negative subtotals, 
 * zero/empty attendees, and negative weights.*/

namespace LunchSplit
{
    public class BillValidator
    {
        public ValidationResult Validate(Bill bill, List<Attendee> attendees)
        {
            // Validate the bill's monetary values
            if (bill.Subtotal < 0)
            {
                return new ValidationResult(false, "Subtotal cannot be negative.");
            }

            if (bill.Tax < 0)
            {
                return new ValidationResult(false, "Tax cannot be negative.");
            }

            // Make sure there is at least one attendee
            if (attendees == null || attendees.Count == 0)
            {
                return new ValidationResult(false, "At least one attendee is required.");
            }

            // Check each attendee for an invalid weight
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