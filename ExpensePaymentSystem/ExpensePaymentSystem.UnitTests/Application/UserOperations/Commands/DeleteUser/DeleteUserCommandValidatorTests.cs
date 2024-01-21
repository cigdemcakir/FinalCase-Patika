using ExpensePaymentSystem.Business.Commands.ReportCommands.DeleteReport;
using ExpensePaymentSystem.Business.Commands.UserCommands.DeleteUser;
using FluentValidation;

namespace ExpensePaymentSystem.UnitTests.Application.UserOperations.Commands.DeleteUser;

public class DeleteUserCommandValidatorTests : AbstractValidator<DeleteUserCommandTests>
{
    public class DeleteReportCommandValidatorTests
    {
        private readonly DeleteUserCommandValidator _validator;

        public DeleteReportCommandValidatorTests()
        {
            _validator = new DeleteUserCommandValidator();
        }

        [Theory]
        [InlineData(1, true)]   // Valid ID
        [InlineData(0, false)]  // Invalid ID: 0
        [InlineData(-1, false)] // Invalid ID: Negative number
        public void Validator_ShouldReturnExpectedResult(int id, bool expectedIsValid)
        {
            // Arrange
            var command = new DeleteUserCommand(id);

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.Equal(expectedIsValid, result.IsValid);
        }
    } 
}