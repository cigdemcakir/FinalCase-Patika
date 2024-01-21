using ExpensePaymentSystem.Business.Commands.ExpenseCommands.RejectExpense;
using FluentValidation;

namespace ExpensePaymentSystem.UnitTests.Application.ExpenseOperations.Commands.RejectExpense;

public class RejectExpenseCommandValidatorTests
{
    private readonly RejectExpenseCommandValidator _validator;

    public RejectExpenseCommandValidatorTests()
    {
        _validator = new RejectExpenseCommandValidator();
    }

    [Theory]
    [InlineData(1, "Valid reason", true)]  // Valid ID and Reason
    [InlineData(0, "Valid reason", false)] // Invalid ID: 0
    [InlineData(1, "", false)]             // Invalid Reason: Empty
    [InlineData(1, null, false)]           // Invalid Reason: Null
    public void Validator_ShouldReturnExpectedResult(int id, string reason, bool expectedIsValid)
    {
        // Arrange
        var command = new RejectExpenseCommand(id, reason);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.Equal(expectedIsValid, result.IsValid);
    }
}
