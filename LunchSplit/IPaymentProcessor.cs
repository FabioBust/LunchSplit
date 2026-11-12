
namespace LunchSplit
{
    public interface IPaymentProcessor
    {
        // Processes a payment through a payment gateway.
        Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request);

        // Retrieves the status of an existing transaction.
        Task<PaymentStatus> GetTransactionStatusAsync(string transactionId);
    }
}
