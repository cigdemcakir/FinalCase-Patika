using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Business.Commands.ExpenseCommands.UpdateExpense;
using ExpensePaymentSystem.Business.Commands.ReportCommands.UpdateReport;
using ExpensePaymentSystem.Schema;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace ExpensePaymentSystem.UnitTests.Application.ReportOperations.Commands.UpdateReport
{
    public class UpdateReportCommandValidatorTests
    {
        private readonly UpdateReportCommandValidator _validator;

        public UpdateReportCommandValidatorTests()
        {
            _validator = new UpdateReportCommandValidator();
        }

        [Fact]
        public void Validate_ShouldReturnTrueForValidModel()
        {
            // Arrange
            var validModel = new UpdateReportCommand(1, new ReportRequest
            {
                UserId = 1,
                StartDate = new DateTime(2024, 01, 01),
                EndDate = new DateTime(2024, 02, 01)
            });

            // Act & Assert
            var result = _validator.TestValidate(validModel);
            result.ShouldNotHaveValidationErrorFor(c => c.Id);
            result.ShouldNotHaveValidationErrorFor(c => c.Model.StartDate);
            result.ShouldNotHaveValidationErrorFor(c => c.Model.EndDate);
            result.ShouldNotHaveValidationErrorFor(c => c.Model.UserId);
            result.IsValid.Should().BeTrue();
        }
    }
    
}
