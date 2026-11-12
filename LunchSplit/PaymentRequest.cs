
namespace LunchSplit
{
    public class PaymentRequest
    {
        // Amount to charge in dollars.
        public decimal Amount { get; set; }

        // Currency used for the payment (e.g., CAD).
        public string Currency { get; set; } = "CAD";

        // Unique identifier to prevent duplicate charges.
        public string IdempotencyKey { get; set; } = string.Empty;

        // Square Sandbox card nonce used for testing.
        public string CardNonce { get; set; } = string.Empty;
    }
}
