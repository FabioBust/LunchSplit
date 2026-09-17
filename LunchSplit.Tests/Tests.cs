namespace LunchSplit.Tests
{
    [TestClass]
    public sealed class TipTests
    {
        // M1-1 Assert returned tip is exactly $0.00
        [TestMethod]
        public void ComputeTip_NoTipMode_ReturnsZero()
        {
            // Arrange
            Splitter splitter = new Splitter();
            decimal subtotal = 100m;
            TipMode mode = TipMode.None;
            decimal tipInput = 0m;

            // Act
            decimal tip = splitter.ComputeTip(subtotal, mode, tipInput);

            // Assert
            Assert.AreEqual(0m, tip);
        }

        // M1-2 Subtotal $120.00 with 15% tip returns $18.00
        [TestMethod]
        public void ComputeTip_PercentTipMode_ReturnsCorrectPercent()
        {
            // Arrange
            Splitter splitter = new Splitter();
            decimal subtotal = 120.00m;
            TipMode mode = TipMode.Percent;
            decimal tipInput = 15m;

            // Act
            decimal tip = splitter.ComputeTip(subtotal, mode, tipInput);

            // Assert
            Assert.AreEqual(18.0m, tip);
        }

        // M1-3 Subtotal $100.00 with $15.00 fixed returns $15.00
        [TestMethod]
        public void ComputeTip_FixedTipMode_ReturnsFixedAmount()
        {
            // Arrange
            Splitter splitter = new Splitter();
            decimal subtotal = 100.0m;
            TipMode mode = TipMode.Fixed;
            decimal tipInput = 15.0m;

            // Act
            decimal tip = splitter.ComputeTip(subtotal, mode, tipInput);

            // Assert
            Assert.AreEqual(15.0m, tip);
        }

        // M1-4 Passing subtotal < 0 throws ArgumentOutOfRangeException
        [TestMethod]
        public void ComputeTip_NegativeSubtotal_ThrowsException()
        {
            // Arrange
            Splitter splitter = new Splitter();
            decimal subtotal = -1.0m;
            TipMode mode = TipMode.None;
            decimal tipInput = 0m;

            // Act and Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => splitter.ComputeTip(subtotal, mode, tipInput));

        }

        // M1-5 Passing fixed tip < 0 throws ArgumentOutOfRangeException
        [TestMethod]
        public void ComputeTip_NegativeFixedTip_ThrowsException()
        {
            // Arrange
            Splitter splitter = new Splitter();
            decimal subtotal = 100.0m;
            TipMode mode = TipMode.Fixed;
            decimal tipInput = -1.0m;

            // Act and Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => splitter.ComputeTip(subtotal, mode, tipInput));
        }
    }

    [TestClass]
    public sealed class RoundingTests
    {
        // M2-6 Ensure raw share decimals are preserved without modification
        public void RoundShares_NoRounding_PreservesRawDecimals()
        {

        }

        // M2-7 IEEE 754: $10.325 -> $10.32; $10.335 -> $10.34
        public void RoundShares_BankersRounding_RoundsToNearestEven()
        {

        }

        // M2-8 Ceil-rounding shifts any fractional cent upwards ($10.331 -> $10.34)
        public void RoundShares_RoundUp_AppliesCeilingToCents()
        {

        }

        // M2-9 Floor-rounding truncates fractional cents ($10.339 -> $10.33).
        public void RoundShares_RoundDown_AppliesFloorToCents()
        {

        }
    }

}
