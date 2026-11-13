
using LunchSplit;
using LunchSplit.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LunchSplit.Tests
{
    [TestClass]
    public class SquareSandboxTests
    {
        // M3-01: Verify that Square Sandbox successfully processes a payment.
        [TestMethod]
        public async Task ProcessPaymentAsync_ValidCard_Succeeds()
        {
            // Arrange
            SquarePaymentService service = new SquarePaymentService();

            PaymentRequest request = new PaymentRequest
            {
                Amount = 25.00m,
                Currency = "CAD",
                CardNonce = "cnon:card-nonce-ok",
                IdempotencyKey = Guid.NewGuid().ToString()
            };

            // Act
            PaymentResult result = await service.ProcessPaymentAsync(request);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.TransactionId));
        }

        // M3-02: Verify that Square rejects a declined card.
        [TestMethod]
        public async Task ProcessPaymentAsync_DeclinedCard_ReturnsError()
        {
            // Arrange
            SquarePaymentService service = new SquarePaymentService();

            PaymentRequest request = new PaymentRequest
            {
                Amount = 25.00m,
                Currency = "CAD",
                CardNonce = "cnon:card-nonce-declined",
                IdempotencyKey = Guid.NewGuid().ToString()
            };

            // Act
            PaymentResult result = await service.ProcessPaymentAsync(request);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("CARD_DECLINED", result.ErrorCode);
        }

    }
}
