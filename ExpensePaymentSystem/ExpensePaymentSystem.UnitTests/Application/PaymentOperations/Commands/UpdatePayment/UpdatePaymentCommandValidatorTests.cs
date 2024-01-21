using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Business.Commands.PaymentCommands.UpdatePayment;
using ExpensePaymentSystem.Schema;
using FluentValidation.TestHelper;

namespace ExpensePaymentSystem.UnitTests.Application.PaymentOperations.Commands.UpdatePayment
{
    public class UpdatePaymentCommandValidatorTests
    {
        private readonly UpdatePaymentCommandValidator _validator;

        public UpdatePaymentCommandValidatorTests()
        {
            _validator = new UpdatePaymentCommandValidator();
        }
        
        [Theory]
        [InlineData(0, PaymentMethod.BankTransfer, false)] // Invalid ID: 0
        [InlineData(-1, PaymentMethod.Cash, false)] // Invalid ID: -1
        public void Validator_ShouldReturnExpectedResult(int id, PaymentMethod paymentMethod, bool expectedIsValid)
        {
            // Arrange
            var command = new UpdatePaymentCommand(id, new PaymentRequest { PaymentMethod = paymentMethod });

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id)
                .When(x => expectedIsValid == false)
                .WithErrorMessage("Id must be a positive number.");
        }
    }
}
