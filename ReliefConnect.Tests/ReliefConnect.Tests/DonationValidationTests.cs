using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReliefConnect.Models;
using Xunit;

namespace ReliefConnect.Tests.Unit
{
    public class DonationValidationTests
    {
        [Fact]
        public void Amount_Must_Be_Positive()
        {
            var d = new Donation
            {
                Amount = 0m,                 // invalid
                PaymentMethod = "Cash"
            };

            var results = new List<ValidationResult>();
            var ok = Validator.TryValidateObject(d, new ValidationContext(d), results, true);

            Assert.False(ok);
        }

        [Fact]
        public void Valid_Donation_Passes()
        {
            var d = new Donation
            {
                Amount = 200.00m,
                PaymentMethod = "Cash"
            };

            var results = new List<ValidationResult>();
            var ok = Validator.TryValidateObject(d, new ValidationContext(d), results, true);

            Assert.True(ok);
        }

        [Fact]
        public void PaymentMethod_Required()
        {
            var d = new Donation
            {
                Amount = 100.00m,
                PaymentMethod = "" // invalid
            };

            var results = new List<ValidationResult>();
            var ok = Validator.TryValidateObject(d, new ValidationContext(d), results, true);

            Assert.False(ok);
        }
    }
}
