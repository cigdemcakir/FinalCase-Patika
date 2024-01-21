using AutoMapper;
using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Business.Commands.ExpenseCommands.UpdateExpense;
using ExpensePaymentSystem.Business.Commands.PaymentCommands.UpdatePayment;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using ExpensePaymentSystem.UnitTests.TestsSetup;
using FluentAssertions;

namespace ExpensePaymentSystem.UnitTests.Application.PaymentOperations.Commands.UpdatePayment
{
    public class UpdatePaymentCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly ExpensePaymentSystemDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdatePaymentCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        
        [Fact]
        public async Task GivenValidInputs_PaymentShouldBeUpdated()
        {
            // Arrange
            var testExpense = new Payment
            {
                ExpenseId = 1,
                Amount = 100,
                PaymentDate = DateTime.Now.AddDays(-10),
                PaymentMethod = PaymentMethod.BankTransfer
            };
            
            await _dbContext.Payments.AddAsync(testExpense);
            await _dbContext.SaveChangesAsync();

            var updateRequest = new PaymentRequest
            {
                PaymentMethod = PaymentMethod.CreditCard
            };
            
            var command = new UpdatePaymentCommand(testExpense.PaymentId, updateRequest);
            var handler = new UpdatePaymentCommandHandler(_dbContext, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Response.Should().NotBeNull();

            var updatedPayment = await _dbContext.Payments.FindAsync(testExpense.PaymentId);
            updatedPayment.Should().NotBeNull();
            updatedPayment.PaymentMethod.Should().Be(updateRequest.PaymentMethod);
        }
    }
}
