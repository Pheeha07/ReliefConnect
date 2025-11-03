using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReliefConnect.Models;
using Xunit;

namespace ReliefConnect.Tests.Unit
{
    public class IncidentReportValidationTests
    {
        [Fact]
        public void Missing_Title_Fails()
        {
            var m = new IncidentReport
            {
                Title = "",
                Description = "desc",
                Location = "Zone A",
                Severity = "High"
            };

            var results = new List<ValidationResult>();
            var ok = Validator.TryValidateObject(m, new ValidationContext(m), results, true);

            Assert.False(ok);
        }

        [Fact]
        public void With_All_Required_Fields_Passes()
        {
            var m = new IncidentReport
            {
                Title = "Flood near bridge",
                Description = "Water level rising rapidly",
                Location = "Zone A",
                Severity = "High"
            };

            var results = new List<ValidationResult>();
            var ok = Validator.TryValidateObject(m, new ValidationContext(m), results, true);

            Assert.True(ok);
        }

        [Fact]
        public void Empty_Location_Fails()
        {
            var m = new IncidentReport
            {
                Title = "Road blocked",
                Description = "Debris on road",
                Location = "",
                Severity = "Low"
            };

            var results = new List<ValidationResult>();
            var ok = Validator.TryValidateObject(m, new ValidationContext(m), results, true);

            Assert.False(ok);
        }
    }
}
