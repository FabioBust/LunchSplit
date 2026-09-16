namespace LunchSplit.Tests
{
    [TestClass]
    public sealed class TipTests
    {
        // M1-1 Assert returned tip is exactly $0.00
        [TestMethod]
        public void ComputeTip_NoTipMode_ReturnsZero()
        {
        }

        // M1-2 Subtotal $120.00 with 15% tip returns $18.00
        [TestMethod]
        public void ComputeTip_PercentTipMode_ReturnsCorrectPercent()
        {
        }

        // M1-3 Subtotal $100.00 with $15.00 fixed returns $15.00
        [TestMethod]
        public void ComputeTip_FixedTipMode_ReturnsFixedAmount()
        {
        }

        // M1-4 Passing subtotal < 0 throws ArgumentOutOfRangeException
        [TestMethod]
        public void ComputeTip_NegativeSubtotal_ThrowsException()
        {
        }

        // M1-5 Passing fixed tip < 0 throws ArgumentOutOfRangeException
        [TestMethod]
        public void ComputeTip_NegativeFixedTip_ThrowsException()
        {
        }
    }
}
