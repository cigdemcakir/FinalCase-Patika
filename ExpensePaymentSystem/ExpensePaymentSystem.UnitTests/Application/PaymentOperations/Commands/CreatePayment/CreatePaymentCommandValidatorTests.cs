using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Business.Validators;
using ExpensePaymentSystem.Schema;

namespace ExpensePaymentSystem.UnitTests.Application.PaymentOperations.Commands.CreatePayment
{
    public class CreatePaymentCommandValidatorTests
    {
        private readonly PaymentRequestValidator _validator;

        public CreatePaymentCommandValidatorTests()
        {
            _validator = new PaymentRequestValidator();
        }

        [Theory]
        [InlineData(1, PaymentMethod.CreditCard, true)]  // Valid data
        [InlineData(0, PaymentMethod.CreditCard, false)] // Invalid Expense ID: 0
        [InlineData(1, (PaymentMethod)99, false)]       // Invalid Payment Method
        public void Validate_ShouldReturnExpectedResult(int expenseId, PaymentMethod paymentMethod, bool expectedIsValid)
        {
            // Arrange
            var request = new PaymentRequest
            {
                ExpenseId = expenseId,
                PaymentMethod = paymentMethod
            };

            // Act
            var result = _validator.Validate(request);

            // Assert
            Assert.Equal(expectedIsValid, result.IsValid);
        }
    }
}
