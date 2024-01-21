using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Business.Commands.PaymentCommands.DeletePayment;
using ExpensePaymentSystem.Schema;

namespace ExpensePaymentSystem.UnitTests.Application.ExpenseOperations.Commands.DeleteExpense
{
    public class CreateExpenseCommandValidatorTests
    {
        private readonly DeletePaymentCommandValidator _validator;

        public CreateExpenseCommandValidatorTests()
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
