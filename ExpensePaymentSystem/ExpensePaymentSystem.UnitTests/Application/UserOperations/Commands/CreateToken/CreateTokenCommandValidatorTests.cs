using ExpensePaymentSystem.Business.Commands.TokenCommands.CreateToken;
using ExpensePaymentSystem.Business.Validators;
using ExpensePaymentSystem.Schema;
using FluentValidation.TestHelper;

namespace ExpensePaymentSystem.UnitTests.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommandValidatorTests
    {
        private readonly CreateTokenValidator _validator;

        public CreateTokenCommandValidatorTests()
        {
            _validator = new CreateTokenValidator();
        }

        [Fact]
        public void ShouldHaveError_WhenUserNameIsEmpty()
        {
            // Arrange
            var model = new TokenRequest
            {
                UserName = "",
                Password = "testpassword"
            };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UserName)
                .WithErrorMessage("'User Name' must not be empty.");
        }

    }
}
