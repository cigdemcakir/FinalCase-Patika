using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Business.Commands.ExpenseCommands.UpdateExpense;
using ExpensePaymentSystem.Schema;

namespace ExpensePaymentSystem.UnitTests.Application.ExpenseOperations.Commands.UpdateExpense
{
    public class UpdateExpenseCommandValidatorTests
    {
        private readonly UpdateExpenseCommandValidator _validator;

        public UpdateExpenseCommandValidatorTests()
        {
            _validator = new UpdateExpenseCommandValidator();
        }

        [Theory]
        [InlineData(0, 100, "Updated Description", "Food", false)] 
        [InlineData(1, 0, "Updated Description", "Food", false)]   
        [InlineData(1, 100, "Updated Description", "", false)]     
        public void Validator_ShouldReturnExpectedResult(int id, decimal amount, string description, string category, bool expectedIsValid)
        {
            // Arrange
            var model = new ExpenseRequest
            {
                Amount = amount,
                Description = description,
                Category = category,
                Date = DateTime.Now,
                Status = ExpenseStatus.Pending 
            };

            var command = new UpdateExpenseCommand(id, model);

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.Equal(expectedIsValid, result.IsValid);
        }
    }


}
