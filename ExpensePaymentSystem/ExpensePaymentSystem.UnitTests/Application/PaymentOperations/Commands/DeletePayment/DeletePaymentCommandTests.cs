using AutoMapper;
using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Business.Commands.ExpenseCommands.DeleteExpense;
using ExpensePaymentSystem.Business.Commands.PaymentCommands.DeletePayment;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.UnitTests.TestsSetup;
using FluentAssertions;

namespace ExpensePaymentSystem.UnitTests.Application.PaymentOperations.Commands.DeletePayment
{
    public class DeletePaymentCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly ExpensePaymentSystemDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeletePaymentCommandTests(CommonTestFixture fixture)
        {
            _dbContext = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_WithInvalidId_ShouldReturnRecordNotFound()
        {
            // Arrange
            var invalidExpenseId = 999; 
            var command = new DeletePaymentCommand(invalidExpenseId);
            var handler = new DeletePaymentCommandHandler(_dbContext, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Message.Should().Be("Record not found");
        }
    }
}
