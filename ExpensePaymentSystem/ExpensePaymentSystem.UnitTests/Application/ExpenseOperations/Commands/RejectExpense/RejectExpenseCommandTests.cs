using AutoMapper;
using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Commands.ExpenseCommands.RejectExpense;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.UnitTests.TestsSetup;
using FluentAssertions;
using MediatR;

namespace ExpensePaymentSystem.UnitTests.Application.ExpenseOperations.Commands.RejectExpense;

public class RejectExpenseCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public RejectExpenseCommandTests(CommonTestFixture fixture)
    {
        _dbContext = fixture.Context;
        _mapper = fixture.Mapper;
    }

    [Fact]
    public async Task GivenValidInputs_ExpenseShouldBeRejected()
    {
        // Arrange
        var testExpense = new Expense
        {
            UserId = 1, 
            Amount = 100m, 
            Date = DateTime.Now.AddDays(-10), 
            Description = "Test Expense", 
            Category = "Travel", 
            Status = ExpenseStatus.Pending
        };
        
        await _dbContext.Expenses.AddAsync(testExpense);
        await _dbContext.SaveChangesAsync();

        var command = new RejectExpenseCommand(testExpense.ExpenseId, "Not valid for reimbursement");
        var handler = new RejectExpenseCommandHandler(_dbContext, _mapper);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var rejectedExpense = await _dbContext.Expenses.FindAsync(testExpense.ExpenseId);
        rejectedExpense.Should().NotBeNull();
        rejectedExpense.Status.Should().Be(ExpenseStatus.Rejected);
        rejectedExpense.RejectionReason.Should().Be("Not valid for reimbursement");
    }
}
