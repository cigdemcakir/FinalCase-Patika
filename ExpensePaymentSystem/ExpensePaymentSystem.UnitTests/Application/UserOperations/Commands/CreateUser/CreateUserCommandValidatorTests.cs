using ExpensePaymentSystem.Business.Validators;
using ExpensePaymentSystem.Schema;
using FluentAssertions;

namespace ExpensePaymentSystem.UnitTests.Application.UserOperations.Commands.CreateUser
{
    public class UpdateUserCommandValidatorTests
    {
        private readonly UserRequestValidator _validator;

        public UpdateUserCommandValidatorTests()
        {
            _validator = new UserRequestValidator();
        }

        [Fact]
        public void Validate_ShouldReturnTrueForValidModel()
        {
            // Arrange
            var validModel = new UserRequest
            {
                UserName = "testuser",
                Password = "testpassword",
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890"
            };

            // Act
            var validationResult = _validator.Validate(validModel);

            // Assert
            validationResult.IsValid.Should().BeTrue();
        }
    }
}
