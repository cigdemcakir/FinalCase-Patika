using ExpensePaymentSystem.Business.Commands.PaymentCommands.DeletePayment;
using ExpensePaymentSystem.Business.Commands.ReportCommands.DeleteReport;

namespace ExpensePaymentSystem.UnitTests.Application.ReportOperations.Commands.DeleteReport
{
    public class DeleteReportCommandValidatorTests
    {
        private readonly DeleteReportCommandValidator _validator;

        public DeleteReportCommandValidatorTests()
        {
            _validator = new DeleteReportCommandValidator();
        }

        [Theory]
        [InlineData(1, true)]   // Valid ID
        [InlineData(0, false)]  // Invalid ID: 0
        [InlineData(-1, false)] // Invalid ID: Negative number
        public void Validator_ShouldReturnExpectedResult(int id, bool expectedIsValid)
        {
            // Arrange
            var command = new DeleteReportCommand(id);

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.Equal(expectedIsValid, result.IsValid);
        }
    } 
}
