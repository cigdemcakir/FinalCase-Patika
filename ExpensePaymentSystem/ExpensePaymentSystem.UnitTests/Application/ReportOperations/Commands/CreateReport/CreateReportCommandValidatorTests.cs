using ExpensePaymentSystem.Business.Commands.ReportCommands.CreateCommand;
using ExpensePaymentSystem.Business.Validators;
using ExpensePaymentSystem.Schema;
using FluentAssertions;
using FluentValidation;

namespace ExpensePaymentSystem.UnitTests.Application.ReportOperations.Commands.CreateReport
{
    public class CreateReportCommandValidatorTests
    {
        private readonly ReportRequestValidator _validator;

        public CreateReportCommandValidatorTests()
        {
            _validator = new ReportRequestValidator();
        }

        [Theory]
        [InlineData("2024-01-01", "2024-02-01", 1, true)] 
        [InlineData("2024-02-01", "2024-01-01", 1, false)] 
        [InlineData("2024-01-01", "2024-02-01", 0, false)] 
        public void Validate_ShouldReturnExpectedResult(string startDate, string endDate, int userId, bool expectedIsValid)
        {
            // Arrange
            var model = new ReportRequest
            {
                UserId = userId,
                StartDate = DateTime.Parse(startDate), 
                EndDate = DateTime.Parse(endDate) 
            };

            // Act
            var result = _validator.Validate(model);

            // Assert
            result.IsValid.Should().Be(expectedIsValid);
        }
    }
}
