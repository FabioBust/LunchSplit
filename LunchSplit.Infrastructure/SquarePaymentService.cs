
using LunchSplit;
using Square;
using Square.Payments;

namespace LunchSplit.Infrastructure
{
    public class SquarePaymentService
    {
        private readonly SquareClient _client;
        private readonly string _locationId;

        public SquarePaymentService()
        {
            // Get credentials from Windows environment variables.
            string token = Environment.GetEnvironmentVariable(
                "SQUARE_SANDBOX_ACCESS_TOKEN")
                ?? throw new InvalidOperationException(
                    "Square access token is missing.");

            _locationId = Environment.GetEnvironmentVariable(
                "SQUARE_SANDBOX_LOCATION_ID")
                ?? throw new InvalidOperationException(
                    "Square location ID is missing.");

            // Connect to Square's testing environment.
            _client = new SquareClient(
                token,
                new ClientOptions
                {
                    BaseUrl = SquareEnvironment.Sandbox
                });
        }

        // Process a payment using Square Sandbox.
        public async Task<PaymentResult> ProcessPaymentAsync(
            PaymentRequest request)
        {
            // Convert dollars into cents.
            long amountInCents = checked(
                (long)(request.Amount * 100m));

            // Create the request Square expects.
            CreatePaymentRequest squareRequest = new CreatePaymentRequest
            {
                SourceId = request.CardNonce,
                IdempotencyKey = request.IdempotencyKey,
                LocationId = _locationId,

                AmountMoney = new Money
                {
                    Amount = amountInCents,
                    Currency = Currency.FromCustom(request.Currency)
                },

                Autocomplete = true
            };

            try
            {
                // Send the payment to Square Sandbox.
                var response = await _client.Payments.CreateAsync(
                    squareRequest,
                    new RequestOptions
                    {
                        Timeout = TimeSpan.FromSeconds(5),
                        MaxRetries = 0
                    });

                // Return successful payment information.
                return new PaymentResult
                {
                    IsSuccess = response.Payment?.Status?.ToString()
                        == "COMPLETED",
                    TransactionId = response.Payment?.Id,
                    HttpStatusCode = 200
                };
            }
            catch (SquareApiException ex)
            {
                // Get the error code returned by Square.
                string errorCode = ex.Errors.FirstOrDefault()?.Code.ToString()
                    ?? "SQUARE_ERROR";

                // Convert Square's generic decline into our application's error code.
                if (errorCode == "GENERIC_DECLINE")
                {
                    errorCode = "CARD_DECLINED";
                }

                // Return the error information.
                return new PaymentResult
                {
                    IsSuccess = false,
                    ErrorCode = errorCode,
                    HttpStatusCode = (int)ex.StatusCode
                };
            }
        }
    }
}
