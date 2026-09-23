using Mono.Cecil.Cil;
using System.Diagnostics.Metrics;
using System.Runtime.Intrinsics.X86;

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
        [TestMethod]
        public void RoundShares_NoRounding_PreservesRawDecimals()
        {
            // Arrange
            Rounder rounder = new Rounder();
            List<Share> rawShares = new List<Share>
            {
                new Share("Fabio", 10.325m),
                new Share("Carlos", 20.675m)
            };

            RoundingMode mode = RoundingMode.None;

            // Act
            List<Share> roundedShares = rounder.RoundShares(rawShares, mode);

            // Assert
            Assert.AreEqual(10.325m, roundedShares[0].Amount);
            Assert.AreEqual(20.675m, roundedShares[1].Amount);
        }

        // M2-7 IEEE 754: $10.325 -> $10.32; $10.335 -> $10.34
        [TestMethod]
        public void RoundShares_BankersRounding_RoundsToNearestEven()
        {
            // Arrange
            Rounder rounder = new Rounder();
            List<Share> rawShares = new List<Share>
            {
                new Share("Fabio", 10.325m),
                new Share("Carlos", 10.335m)
            };

            RoundingMode mode = RoundingMode.Bankers;

            // Act
            List<Share> roundedShares = rounder.RoundShares(rawShares, mode);

            // Assert
            Assert.AreEqual(10.32m, roundedShares[0].Amount);
            Assert.AreEqual(10.34m, roundedShares[1].Amount);
        }

        // M2-8 Ceil-rounding shifts any fractional cent upwards ($10.331 -> $10.34)
        [TestMethod]
        public void RoundShares_RoundUp_AppliesCeilingToCents()
        {
            // Arrange
            Rounder rounder = new Rounder();
            List<Share> rawShares = new List<Share>
            {
                new Share("Fabio", 10.331m)
            };

            RoundingMode mode = RoundingMode.Ceil;

            // Act
            List<Share> roundedShares = rounder.RoundShares(rawShares, mode);

            // Assert
            Assert.AreEqual(10.34m, roundedShares[0].Amount);
        }

        // M2-9 Floor-rounding truncates fractional cents ($10.339 -> $10.33)
        [TestMethod]
        public void RoundShares_RoundDown_AppliesFloorToCents()
        {
            // Arrange
            Rounder rounder = new Rounder();
            List<Share> rawShares = new List<Share>
            {
                new Share("Fabio", 10.339m)
            };

            RoundingMode mode = RoundingMode.Floor;

            // Act
            List<Share> roundedShares = rounder.RoundShares(rawShares, mode);

            // Assert
            Assert.AreEqual(10.33m, roundedShares[0].Amount);
        }

        // M2-10 Reconcile a $10.00 bill split among 3 attendees ($3.34, $3.33, $3.33)
        [TestMethod]
        public void RoundShares_UnevenSplit_ReconcilesRemainder()
        {

        }

        // M2-11 Passing empty share lists returns an empty collection
        [TestMethod]
        public void RoundShares_EmptyShareCollection_ReturnsEmpty()
        {

        }

    }

    [TestClass]
    public sealed class BillValidatorTests
    {
        // M3-12 Verify that valid subtotals, non-negative taxes, and users pass
        [TestMethod]
        public void Validate_CompleteBillDetails_ReturnsOk()
        {

        }

        // M3-13 Flag zero-attendee collections as invalid with error msg
        [TestMethod]
        public void Validate_EmptyAttendeeCollection_ReturnsFail()
        {

        }

        // M3-14 Verify negative subtotals fail with student-friendly error
        [TestMethod]
        public void Validate_NegativeSubtotal_ReturnsFail()
        {

        }

        // M3-15 Ensure negative tax values fail validation
        [TestMethod]
        public void Validate_NegativeTax_ReturnsFail()
        {

        }

        // M3-16 Assert that having exactly 0 attendees fails validation
        [TestMethod]
        public void Validate_ZeroAttendees_ReturnsFail()
        {

        }
    }

    [TestClass]
    public sealed class CalculateSharesTests
    {
        // M4-17 Split $90.00 among 3 users equally, assert $30.00 each
        [TestMethod]
        public void CalculateShares_EqualSplit_ApportionsEvenly()
        {

        }

        // M4-18 Split $90.00 with weights 2 and 1, assert $60.00 and $30.00
        [TestMethod]
        public void CalculateShares_ProportionalSplit_ApportionsWeighted()
        {

        }

        // M4-19 Assert excluded attendee gets $0.00; total split among active
        [TestMethod]
        public void CalculateShares_ExcludedAttendee_ApportionsZero()
        {

        }

        // M4-20 Split $100.00 + $13.00 tax + 15% tip. Grand Total: $128.00
        [TestMethod]
        public void CalculateShares_TaxAndPercentTip_ApportionsTotal()
        {

        }

        // M4-21 Split $100.00 + $13.00 tax + $20.00 fixed tip. Grand Total: $133.00
        [TestMethod]
        public void CalculateShares_TaxAndFixedTip_ApportionsTotal()
        {

        }

        // M4-22 Attempting calculations when all users are excluded throws exception
        [TestMethod]
        public void CalculateShares_AllAttendeesExcluded_ThrowsException()
        {

        }

        // M4-23 All weights set to 0 throws division-by-zero prevention exception
        [TestMethod]
        public void CalculateShares_TotalWeightsZero_ThrowsException()
        {

        }
    }

    [TestClass]
    public sealed class ReciptFormatterTests
    {
        // M5-24 Assert formatted receipt contains exact itemized attendee values
        [TestMethod]
        public void Format_DefaultBillReceipt_ContainsLineItems()
        {

        }

        // M5-25 Confirm output has Student Name, Date, and creation timestamp
        [TestMethod]
        public void Format_ReceiptOutput_ContainsRequiredMetadata()
        {

        }
    }
}