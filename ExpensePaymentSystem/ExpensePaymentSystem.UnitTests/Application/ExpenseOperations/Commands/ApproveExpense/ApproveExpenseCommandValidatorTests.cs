using ExpensePaymentSystem.Business.Commands.ExpenseCommands.ApproveExpense;
using FluentAssertions;

namespace ExpensePaymentSystem.UnitTests.Application.ExpenseOperations.Commands.ApproveExpense;

public class ApproveExpenseCommandValidatorTests
{
    private readonly ApproveExpenseCommandValidator _validator;

    public ApproveExpenseCommandValidatorTests()
    {
        _validator = new ApproveExpenseCommandValidator();
    }

    [Theory]
    [InlineData(1, true)]   // Valid ID
    [InlineData(0, false)]  // Invalid ID: 0
    [InlineData(-1, false)] // Invalid ID: Negative number
    public void Validator_ShouldValidateCommand(int id, bool expectedIsValid)
    {
        // Arrange
        var command = new ApproveExpenseCommand(id);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().Be(expectedIsValid);
    }
}