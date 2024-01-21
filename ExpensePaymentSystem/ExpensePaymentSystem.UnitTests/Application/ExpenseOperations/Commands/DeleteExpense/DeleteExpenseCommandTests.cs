using AutoMapper;
using ExpensePaymentSystem.Business.Commands.ExpenseCommands.CreateExpense;
using ExpensePaymentSystem.Business.Commands.ExpenseCommands.DeleteExpense;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using ExpensePaymentSystem.UnitTests.TestsSetup;
using FluentAssertions;

namespace ExpensePaymentSystem.UnitTests.Application.ExpenseOperations.Commands.DeleteExpense
{
    public class DeleteExpenseCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly ExpensePaymentSystemDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeleteExpenseCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        
        [Fact]
        public async Task Handle_WithInvalidId_ShouldReturnRecordNotFound()
        {
            // Arrange
            var invalidExpenseId = 999; 
            var command = new DeleteExpenseCommand(invalidExpenseId);
            var handler = new DeleteExpenseCommandHandler(_dbContext, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Message.Should().Be("Record not found");
        }
    }
}

