using System;
using System.Collections.Generic;
using System.Text;

namespace LunchSplit.Tests
{
    [TestClass]
    public class FutureFeatureTests
    {
        // TDD-26: Future CSV receipt exporter (F5)
        // This test intentionally remains RED until F5 is implemented.

        [TestMethod]
        public void Format_CsvReceiptExporter_MatchesSchema()
        {
            // Arrange
            string expectedHeader = "Name,Amount";

            // Act
            string csvOutput = string.Empty; // CSV export is not implemented yet
            bool hasCorrectHeader = csvOutput.StartsWith(expectedHeader);

            // Assert
            Assert.IsTrue(hasCorrectHeader,"RED/PENDING: CSV receipt exporter is not implemented yet.");
        }

        // TDD-27: Future Stripe payment integration (F6)
        // This test intentionally remains RED until F6 is implemented.56
        [TestMethod]
        public void CalculateShares_PaymentRequest_ValidatesStripeMock()
        {
            // Arrange
            Splitter splitter = new Splitter();
            Bill bill = new Bill(100.00m, 0m, TipMode.None, 0m);

            List<Attendee> attendees = new List<Attendee>
            {
                new Attendee("Fabio", 1, true)
            };

            RoundingMode mode = RoundingMode.Bankers;

            // Act
            List<Share> shares = splitter.CalculateShares(bill, attendees, mode);

            // Assert
            Assert.Fail("RED/PENDING: Stripe payment integration is not implemented yet.");
        }
    }
}
