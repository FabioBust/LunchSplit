
using LunchSplit;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LunchSplit.Tests
{
    [TestClass]
    public class PaymentProcessorMoqTests
    {
        // M3-04: Verify the correct payment amount is sent.
        [TestMethod]
        public async Task ProcessPaymentAsync_CorrectAmount()
        {
            // Arrange
            PaymentRequest request = new PaymentRequest();
            request.Amount = 25.00m;

            Mock<IPaymentProcessor> mock = new Mock<IPaymentProcessor>();

            mock.Setup(p => p.ProcessPaymentAsync(request)).ReturnsAsync(new PaymentResult { IsSuccess = true });

            PaymentManager manager = new PaymentManager(mock.Object);

            // Act
            await manager.ProcessPaymentAsync(request);

            // Assert
            mock.Verify(p => p.ProcessPaymentAsync(It.Is<PaymentRequest>(r => r.Amount == 25.00m)), Times.Once());
        }
    }
}
