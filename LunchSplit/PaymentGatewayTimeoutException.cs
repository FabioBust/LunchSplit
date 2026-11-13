
namespace LunchSplit
{
    public class PaymentGatewayTimeoutException : Exception
    {
        public PaymentGatewayTimeoutException(
            string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
