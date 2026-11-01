using System;
using System.Collections.Generic;
using System.Text;

namespace LunchSplit.Tests
{
    [TestClass]
    public class ReceiptFormatterTests
    {
        // M5-24 Assert formatted receipt contains exact itemized attendee values
        [TestMethod]
        public void Format_DefaultBillReceipt_ContainsLineItems()
        {
            // Arrange
            ReceiptFormatter formatter = new ReceiptFormatter();

            Bill bill = new Bill(50.00m, 0m, TipMode.None, 0m);

            List<Attendee> attendees = new List<Attendee>
            {
                new Attendee("Mario", 1, true),
                new Attendee("Fabio", 1, true)
            };

            List<Share> shares = new List<Share>
            {
                new Share("Mario", 20.00m),
                new Share("Fabio", 30.00m)
            };

            // Act
            string receipt = formatter.Format(bill, attendees, shares);

            // Assert
            StringAssert.Contains(receipt, "Mario");
            StringAssert.Contains(receipt, "20.00");

            StringAssert.Contains(receipt, "Fabio");
            StringAssert.Contains(receipt, "30.00");
        }

        // M5-25 Confirm output has Student Name, Date, and creation timestamp
        [TestMethod]
        public void Format_ReceiptOutput_ContainsRequiredMetadata()
        {
            // Arrange
            ReceiptFormatter formatter = new ReceiptFormatter();

            Bill bill = new Bill(90.00m, 0m, TipMode.None, 0m);

            List<Attendee> attendees = new List<Attendee>
            {
                new Attendee("Fabio", 1, true)
            };

            List<Share> shares = new List<Share>
            {
                new Share("Fabio", 90.00m)
            };

            // Act
            string receipt = formatter.Format(bill, attendees, shares);

            // Assert
            StringAssert.Contains(receipt, "Fabio Bustamante Romillo");
            StringAssert.Contains(receipt, DateTime.Now.ToString("yyyy-MM-dd"));
            StringAssert.Contains(receipt, "Created:");
        }
    }
}
