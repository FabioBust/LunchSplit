using System;
using System.Collections.Generic;
using System.Text;

namespace LunchSplit.Tests
{
    [TestClass]
    public class TipTests
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
            Assert.Throws<ArgumentOutOfRangeException>(() => splitter.ComputeTip(subtotal, mode, tipInput));

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
            Assert.Throws<ArgumentOutOfRangeException>(() => splitter.ComputeTip(subtotal, mode, tipInput));
        }
    }
}
