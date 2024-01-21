using System.Net.Mail;
using AutoMapper;
using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Business.Commands.PaymentCommands.CreatePayment;
using ExpensePaymentSystem.Business.Interfaces;
using ExpensePaymentSystem.Business.Services;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using ExpensePaymentSystem.UnitTests.TestsSetup;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.UnitTests.Application.PaymentOperations.Commands.CreatePayment
{
    public class CreatePaymentCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly ExpensePaymentSystemDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPaymentSimulator _paymentSimulator;
        private readonly INotificationService _notificationService;
        

        public CreatePaymentCommandTests(CommonTestFixture fixture)
        {
            _dbContext = fixture.Context;
            _mapper = fixture.Mapper;
            _paymentSimulator = new FakePaymentSimulator();
            _notificationService = new NotificationService(_dbContext, fixture.Configuration, new SmtpClient());
        }

        [Fact]
        public async Task Handle_WhenPaymentSucceeds_ShouldCreatePaymentAndUpdateExpense()
        {
            // Arrange
            var handler = new CreatePaymentCommandHandler(_dbContext, _mapper, _paymentSimulator, _notificationService);
            var command = new CreatePaymentCommand(new PaymentRequest { ExpenseId = 1, PaymentMethod = PaymentMethod.BankTransfer });

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            var payment = await _dbContext.Payments.FirstOrDefaultAsync(p => p.ExpenseId == 1);
            payment.Should().NotBeNull();
            payment.PaymentMethod.Should().Be(PaymentMethod.BankTransfer);

            var updatedExpense = await _dbContext.Expenses.FindAsync(1);
            updatedExpense.Status.Should().Be(ExpenseStatus.Approved);
        }
    }

}
