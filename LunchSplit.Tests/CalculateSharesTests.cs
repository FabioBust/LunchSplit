using System;
using System.Collections.Generic;
using System.Text;

namespace LunchSplit.Tests
{
    [TestClass]
    public class CalculateSharesTests
    {
        // M4-17 Split $90.00 among 3 users equally, assert $30.00 each
        [TestMethod]
        public void CalculateShares_EqualSplit_ApportionsEvenly()
        {
            // Arrange
            Splitter splitter = new Splitter();

            decimal subtotal = 90.00m;
            decimal tax = 0m;
            TipMode tMode = TipMode.None;
            decimal tipInput = 0m;

            Bill bill = new Bill(subtotal, tax, tMode, tipInput);

            List<Attendee> attendees = new List<Attendee>
            {
                new Attendee("Mario", 1, true),
                new Attendee("Fabio", 1, true),
                new Attendee("Felix", 1, true)
            };

            RoundingMode rMode = RoundingMode.Bankers;

            // Act
            List<Share> shares = splitter.CalculateShares(bill, attendees, rMode);

            // Assert
            Assert.AreEqual(30.00m, shares[0].Amount);
            Assert.AreEqual(30.00m, shares[1].Amount);
            Assert.AreEqual(30.00m, shares[2].Amount);
        }

        // M4-18 Split $90.00 with weights 2 and 1, assert $60.00 and $30.00
        [TestMethod]
        public void CalculateShares_ProportionalSplit_ApportionsWeighted()
        {
            // Arrange
            Splitter splitter = new Splitter();

            decimal subtotal = 90.00m;
            decimal tax = 0m;
            TipMode tMode = TipMode.None;
            decimal tipInput = 0m;

            Bill bill = new Bill(subtotal, tax, tMode, tipInput);

            List<Attendee> attendees = new List<Attendee>
            {
                new Attendee("Mario", 2, true),
                new Attendee("Fabio", 1, true)
            };

            RoundingMode rMode = RoundingMode.Bankers;

            // Act
            List<Share> shares = splitter.CalculateShares(bill, attendees, rMode);

            // Assert
            Assert.AreEqual(60.00m, shares[0].Amount);
            Assert.AreEqual(30.00m, shares[1].Amount);
        }

        // M4-19 Assert excluded attendee gets $0.00; total split among active
        [TestMethod]
        public void CalculateShares_ExcludedAttendee_ApportionsZero()
        {
            // Arrange
            Splitter splitter = new Splitter();

            decimal subtotal = 90.00m;
            decimal tax = 0m;
            TipMode tMode = TipMode.None;
            decimal tipInput = 0m;

            Bill bill = new Bill(subtotal, tax, tMode, tipInput);

            List<Attendee> attendees = new List<Attendee>
            {
                new Attendee("Mario", 1, false),
                new Attendee("Fabio", 1, true)
            };

            RoundingMode rMode = RoundingMode.Bankers;

            // Act
            List<Share> shares = splitter.CalculateShares(bill, attendees, rMode);

            // Assert
            Assert.AreEqual(0.00m, shares[0].Amount);
            Assert.AreEqual(90.00m, shares[1].Amount);
        }

        // M4-20 Split $100.00 + $13.00 tax + 15% tip. Grand Total: $128.00
        [TestMethod]
        public void CalculateShares_TaxAndPercentTip_ApportionsTotal()
        {
            // Arrange
            Splitter splitter = new Splitter();

            decimal subtotal = 100.00m;
            decimal tax = 13.00m;
            TipMode tMode = TipMode.Percent;
            decimal tipInput = 15m;

            Bill bill = new Bill(subtotal, tax, tMode, tipInput);

            List<Attendee> attendees = new List<Attendee>
            {
                new Attendee("Fabio", 1, true)
            };

            RoundingMode rMode = RoundingMode.Bankers;

            // Act
            List<Share> shares = splitter.CalculateShares(bill, attendees, rMode);

            // Assert
            Assert.AreEqual(128.00m, shares[0].Amount);
        }

        // M4-21 Split $100.00 + $13.00 tax + $20.00 fixed tip. Grand Total: $133.00
        [TestMethod]
        public void CalculateShares_TaxAndFixedTip_ApportionsTotal()
        {
            // Arrange
            Splitter splitter = new Splitter();

            decimal subtotal = 100.00m;
            decimal tax = 13.00m;
            TipMode tMode = TipMode.Fixed;
            decimal tipInput = 20.00m;

            Bill bill = new Bill(subtotal, tax, tMode, tipInput);

            List<Attendee> attendees = new List<Attendee>
            {
                new Attendee("Fabio", 1, true)
            };

            RoundingMode rMode = RoundingMode.Bankers;

            // Act
            List<Share> shares = splitter.CalculateShares(bill, attendees, rMode);

            // Assert
            Assert.AreEqual(133.00m, shares[0].Amount);
        }

        // M4-22 Attempting calculations when all users are excluded throws exception
        [TestMethod]
        public void CalculateShares_AllAttendeesExcluded_ThrowsException()
        {
            // Arrange
            Splitter splitter = new Splitter();

            decimal subtotal = 90.00m;
            decimal tax = 0m;
            TipMode tMode = TipMode.None;
            decimal tipInput = 0m;

            Bill bill = new Bill(subtotal, tax, tMode, tipInput);

            List<Attendee> attendees = new List<Attendee>
            {
                new Attendee("Mario", 1, false),
                new Attendee("Fabio", 1, false),
                new Attendee("Felix", 1, false)
            };

            RoundingMode rMode = RoundingMode.Bankers;

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => splitter.CalculateShares(bill, attendees, rMode));
        }

        // M4-23 All weights set to 0 throws division-by-zero prevention exception
        [TestMethod]
        public void CalculateShares_TotalWeightsZero_ThrowsException()
        {
            // Arrange
            Splitter splitter = new Splitter();

            decimal subtotal = 90.00m;
            decimal tax = 0m;
            TipMode tMode = TipMode.None;
            decimal tipInput = 0m;

            Bill bill = new Bill(subtotal, tax, tMode, tipInput);

            List<Attendee> attendees = new List<Attendee>
            {
                new Attendee("Mario", 0, true),
                new Attendee("Fabio", 0, true),
                new Attendee("Felix", 0, true)
            };

            RoundingMode rMode = RoundingMode.Bankers;

            // Act and Assert
            Assert.Throws<DivideByZeroException>(() => splitter.CalculateShares(bill, attendees, rMode));
        }
    }
}
