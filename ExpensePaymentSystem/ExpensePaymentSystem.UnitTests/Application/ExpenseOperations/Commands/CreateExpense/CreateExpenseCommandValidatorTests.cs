using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Business.Validators;
using ExpensePaymentSystem.Schema;

namespace ExpensePaymentSystem.UnitTests.Application.ExpenseOperations.Commands.CreateExpense
{
    public class CreateExpenseCommandValidatorTests
    {
        private readonly ExpenseRequestValidator _validator;

        public CreateExpenseCommandValidatorTests()
        {
            _validator = new ExpenseRequestValidator();
        }

        [Theory]
        [InlineData(0, 100, "Description", "Category", false)] // Invalid: UserId is 0
        [InlineData(1, 0, "Description", "Category", false)] // Invalid: Amount is 0
        [InlineData(1, 100, "", "Category", false)] // Invalid: Description is empty
        [InlineData(1, 100, "Description", "", false)] // Invalid: Category is empty
        public void Validate_ShouldReturnExpectedResult(int userId, decimal amount, string description, string category,
            bool expectedResult)
        {
            // Arrange
            var request = new ExpenseRequest
            {
                UserId = userId,
                Amount = amount,
                Date = DateTime.Now,
                Description = description,
                Category = category,
                Status = ExpenseStatus.Pending // Fixed status for the test
            };

            // Act
            var result = _validator.Validate(request);

            // Assert
            Assert.Equal(expectedResult, result.IsValid);
        }
    } 
}


