
namespace LunchSplit
{
    public class PaymentResult
    {
        // Indicates whether the payment was successful.
        public bool IsSuccess { get; set; }

        // Unique transaction ID returned by the payment gateway.
        public string? TransactionId { get; set; }

        // Error code returned if the payment fails.
        public string? ErrorCode { get; set; }

        // HTTP status code returned by the gateway.
        public int HttpStatusCode { get; set; }
    }
}
