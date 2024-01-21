using AutoMapper;
using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Business.Commands.ExpenseCommands.UpdateExpense;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using ExpensePaymentSystem.UnitTests.TestsSetup;
using FluentAssertions;

namespace ExpensePaymentSystem.UnitTests.Application.ExpenseOperations.Commands.UpdateExpense
{
    public class UpdateExpenseCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly ExpensePaymentSystemDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateExpenseCommandTests(CommonTestFixture fixture)
        {
            _dbContext = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GivenValidInputs_ExpenseShouldBeUpdated()
        {
            // Arrange
            var testExpense = new Expense
            {
                UserId = 1, 
                Amount = 100m, 
                Date = DateTime.Now.AddDays(-10), 
                Description = "Original Description", 
                Category = "Travel", 
                Status = ExpenseStatus.Pending
            };
            await _dbContext.Expenses.AddAsync(testExpense);
            await _dbContext.SaveChangesAsync();

            var updateRequest = new ExpenseRequest 
            {
                Amount = 200m, 
                Category = "Food",
                Status = ExpenseStatus.Approved
            };

            var command = new UpdateExpenseCommand(testExpense.ExpenseId, updateRequest);
            var handler = new UpdateExpenseCommandHandler(_dbContext, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Response.Should().NotBeNull();

            var updatedExpense = await _dbContext.Expenses.FindAsync(testExpense.ExpenseId);
            updatedExpense.Should().NotBeNull();
            updatedExpense.Amount.Should().Be(updateRequest.Amount);
            updatedExpense.Category.Should().Be(updateRequest.Category);
            updatedExpense.Status.Should().Be(updateRequest.Status);
        }
    }
}
