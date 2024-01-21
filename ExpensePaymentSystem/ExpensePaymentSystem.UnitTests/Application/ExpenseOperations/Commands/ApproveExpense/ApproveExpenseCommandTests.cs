using AutoMapper;
using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Business.Commands.ExpenseCommands.ApproveExpense;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.UnitTests.TestsSetup;
using FluentAssertions;

namespace ExpensePaymentSystem.UnitTests.Application.ExpenseOperations.Commands.ApproveExpense;

public class ApproveExpenseCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public ApproveExpenseCommandTests(CommonTestFixture fixture)
    {
        _dbContext = fixture.Context;
        _mapper = fixture.Mapper;
    }

    [Fact]
    public async Task GivenValidId_ExpenseStatusShouldBeApproved()
    {
        // Arrange
        var command = new ApproveExpenseCommand(1); // Assuming there's an Expense with Id 1 in the database
        var handler = new ApproveExpenseCommandHandler(_dbContext, _mapper);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var expense = await _dbContext.Expenses.FindAsync(1);
        expense.Should().NotBeNull();
        expense.Status.Should().Be(ExpenseStatus.Approved);
    }
}