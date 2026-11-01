using System;
using System.Collections.Generic;
using System.Text;

namespace LunchSplit.Tests
{
    [TestClass]
    public class BillValidatorTests
    {
        // M3-12 Verify that valid subtotals, non-negative taxes, and users pass
        [TestMethod]
        public void Validate_CompleteBillDetails_ReturnsOk()
        {
            // Arrange
            BillValidator validator = new BillValidator();

            decimal subtotal = 10.00m;
            decimal tax = 1.30m;
            TipMode mode = TipMode.Fixed;
            decimal tipInput = 2m;

            Bill bill = new Bill(subtotal, tax, mode, tipInput);

            List<Attendee> attendees = new List<Attendee>
            {
                new Attendee("Fabio", 1, true)
            };

            // Act
            ValidationResult result = validator.Validate(bill, attendees);

            // Assert
            Assert.IsTrue(result.IsValid);
        }

        // M3-13 Flag zero-attendee collections as invalid with error msg
        [TestMethod]
        public void Validate_EmptyAttendeeCollection_ReturnsFail()
        {
            // Arrange
            BillValidator validator = new BillValidator();

            decimal subtotal = 10.00m;
            decimal tax = 1.30m;
            TipMode mode = TipMode.Fixed;
            decimal tipInput = 2m;

            Bill bill = new Bill(subtotal, tax, mode, tipInput);

            List<Attendee> attendees = new List<Attendee>();

            // Act
            ValidationResult result = validator.Validate(bill, attendees);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(string.IsNullOrEmpty(result.ErrorMessage));
        }

        // M3-14 Verify negative subtotals fail with student-friendly error
        [TestMethod]
        public void Validate_NegativeSubtotal_ReturnsFail()
        {
            // Arrange
            BillValidator validator = new BillValidator();

            decimal subtotal = -10.00m;
            decimal tax = 1.30m;
            TipMode mode = TipMode.Fixed;
            decimal tipInput = 2m;

            Bill bill = new Bill(subtotal, tax, mode, tipInput);

            List<Attendee> attendees = new List<Attendee>
            {
                new Attendee("Fabio", 1, true)
            };

            // Act
            ValidationResult result = validator.Validate(bill, attendees);

            // Assert
            Assert.IsFalse(result.IsValid);
        }

        // M3-15 Ensure negative tax values fail validation
        [TestMethod]
        public void Validate_NegativeTax_ReturnsFail()
        {
            // Arrange
            BillValidator validator = new BillValidator();

            decimal subtotal = 10.00m;
            decimal tax = -1.30m;
            TipMode mode = TipMode.Fixed;
            decimal tipInput = 2m;

            Bill bill = new Bill(subtotal, tax, mode, tipInput);

            List<Attendee> attendees = new List<Attendee>
            {
                new Attendee("Fabio", 1, true)
            };

            // Act
            ValidationResult result = validator.Validate(bill, attendees);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(string.IsNullOrEmpty(result.ErrorMessage));
        }

        // M3-16 Assert that having exactly 0 attendees fails validation
        [TestMethod]
        public void Validate_ZeroAttendees_ReturnsFail()
        {
            // Arrange
            BillValidator validator = new BillValidator();

            decimal subtotal = 10.00m;
            decimal tax = 1.30m;
            TipMode mode = TipMode.Fixed;
            decimal tipInput = 2m;

            Bill bill = new Bill(subtotal, tax, mode, tipInput);

            List<Attendee> attendees = new List<Attendee>();

            // Act
            ValidationResult result = validator.Validate(bill, attendees);

            // Assert
            Assert.IsFalse(result.IsValid);
        }
    }
}
