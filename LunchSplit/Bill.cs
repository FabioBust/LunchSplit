/* An immutable C# record/class representing the raw bill metadata. Contains 
 * decimal Subtotal, decimal Tax, TipMode (None, Percent, or Fixed), and 
 * decimal TipInput.*/

namespace LunchSplit
{
    public class Bill
    {
        public decimal Subtotal { get; }
        public decimal Tax { get; }
        public TipMode Mode { get; }
        public decimal TipInput { get; }

        public Bill(decimal subtotal, decimal tax, TipMode mode, decimal tipInput)
        {
            Subtotal = subtotal;
            Tax = tax;
            Mode = mode;
            TipInput = tipInput;
        }
    }
}
