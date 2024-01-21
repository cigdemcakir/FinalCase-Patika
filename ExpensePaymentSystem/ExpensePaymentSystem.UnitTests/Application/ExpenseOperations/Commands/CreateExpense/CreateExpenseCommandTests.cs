using AutoMapper;
using ExpensePaymentSystem.Business.Commands.ExpenseCommands.CreateExpense;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Schema;
using ExpensePaymentSystem.UnitTests.TestsSetup;
using FluentAssertions;

namespace ExpensePaymentSystem.UnitTests.Application.ExpenseOperations.Commands.CreateExpense
{
    public class CreateExpenseCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly ExpensePaymentSystemDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateExpenseCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        
        [Fact]
        public async Task Handle_GivenValidExpenseRequest_ShouldCreateExpenseAndReturnResponse()
        {
            //Arrange
            var expenseRequest = new ExpenseRequest
            {
                UserId = 1,
                Amount = 100,
                Description = "Test Expense",
                Category = "Travel",
            };

            var command = new CreateExpenseCommand(expenseRequest);
            var handler = new CreateExpenseCommandHandler(_dbContext, _mapper);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Response.Should().NotBeNull();
            result.Response.UserId.Should().Be(expenseRequest.UserId);
            
            var createdExpense = await _dbContext.Expenses.FindAsync(result.Response.ExpenseId);
            createdExpense.Should().NotBeNull();
            createdExpense.Description.Should().Be(expenseRequest.Description);

        }
    }
}
