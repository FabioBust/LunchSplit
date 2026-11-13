
using System.Net.Http;

namespace LunchSplit
{
    public class PaymentManager
    {
        private readonly IPaymentProcessor _paymentProcessor;

        // The payment processor is provided through the constructor.
        public PaymentManager(IPaymentProcessor paymentProcessor)
        {
            _paymentProcessor = paymentProcessor;
        }

        // Processes a payment using the provided payment processor.
        public async Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request)
        {
            // Prevent payment requests with no valid amount.
            if (request.Amount <= 0)
            {
                return new PaymentResult
                {
                    IsSuccess = false,
                    ErrorCode = "INVALID_AMOUNT"
                };
            }

            try
            {
                // Delegate the payment to the processor.
                return await _paymentProcessor.ProcessPaymentAsync(request);
            }
            catch (HttpRequestException ex)
            {
                // Convert network failures into a payment gateway exception.
                throw new PaymentGatewayTimeoutException(
                    "The payment gateway could not be reached.", ex);
            }
        }
    }
}
