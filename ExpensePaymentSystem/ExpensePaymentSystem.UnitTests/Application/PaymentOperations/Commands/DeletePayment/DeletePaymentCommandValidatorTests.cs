using ExpensePaymentSystem.Business.Commands.ExpenseCommands.DeleteExpense;
using ExpensePaymentSystem.Business.Commands.PaymentCommands.DeletePayment;

namespace ExpensePaymentSystem.UnitTests.Application.PaymentOperations.Commands.DeletePayment
{
    public class DeleteExpenseCommandValidatorTests
    {
        private readonly DeletePaymentCommandValidator _validator;

        public DeleteExpenseCommandValidatorTests()
        {
            _validator = new DeletePaymentCommandValidator();
        }

        [Theory]
        [InlineData(1, true)]   // Valid ID
        [InlineData(0, false)]  // Invalid ID: 0
        [InlineData(-1, false)] // Invalid ID: Negative number
        public void Validator_ShouldReturnExpectedResult(int id, bool expectedIsValid)
        {
            // Arrange
            var command = new DeletePaymentCommand(id);

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.Equal(expectedIsValid, result.IsValid);
        }
    }
}
