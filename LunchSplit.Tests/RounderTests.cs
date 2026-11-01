using System;
using System.Collections.Generic;
using System.Text;

namespace LunchSplit.Tests
{
    [TestClass]
    public class RounderTests
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
            // Arrange
            Rounder rounder = new Rounder();

            decimal rawAmount = 10.00m / 3;

            List<Share> rawShares = new List<Share>
        {
            new Share("Fabio", rawAmount),
            new Share("Carlos", rawAmount),
            new Share("Javier", rawAmount)
        };

            RoundingMode mode = RoundingMode.Bankers;

            // Act
            List<Share> roundedShares = rounder.RoundShares(rawShares, mode);

            // Assert
            Assert.AreEqual(3.34m, roundedShares[0].Amount);
            Assert.AreEqual(3.33m, roundedShares[1].Amount);
            Assert.AreEqual(3.33m, roundedShares[2].Amount);

        }

        // M2-11 Passing empty share lists returns an empty collection
        [TestMethod]
        public void RoundShares_EmptyShareCollection_ReturnsEmpty()
        {

            // Arrange
            Rounder rounder = new Rounder();

            List<Share> rawShares = new List<Share>(); // Empty list

            RoundingMode mode = RoundingMode.Bankers;

            // Act
            List<Share> roundedShares = rounder.RoundShares(rawShares, mode);

            // Assert
            Assert.AreEqual(0, roundedShares.Count);

        }
    }
}
