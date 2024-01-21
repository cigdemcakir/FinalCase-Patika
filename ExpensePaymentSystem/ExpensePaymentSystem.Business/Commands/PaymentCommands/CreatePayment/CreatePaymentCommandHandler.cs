using AutoMapper;
using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Commands.ExpenseCommands.CreateExpense;
using ExpensePaymentSystem.Business.Interfaces;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Commands.PaymentCommands.CreatePayment;

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, ApiResponse<PaymentResponse>>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IPaymentSimulator _paymentSimulator;
    private readonly INotificationService _notificationService;

    public CreatePaymentCommandHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper, IPaymentSimulator paymentSimulator, INotificationService notificationService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _paymentSimulator = paymentSimulator;
        _notificationService = notificationService;
    }

    public async Task<ApiResponse<PaymentResponse>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var expense = await _dbContext.Expenses
            .FirstOrDefaultAsync(e => e.ExpenseId == request.Model.ExpenseId, cancellationToken);

        if (expense == null)
            return new ApiResponse<PaymentResponse>("Expense not found");

        var payment = _mapper.Map<Payment>(request.Model);
        
        payment.Amount = expense.Amount;
        payment.PaymentDate = DateTime.UtcNow;

        bool paymentResult = await _paymentSimulator.SimulatePaymentAsync(payment.Amount, expense.UserId);

        if (paymentResult)
        {
            await _dbContext.Payments.AddAsync(payment, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            expense.Status = ExpenseStatus.Approved;
            expense.PaymentId = payment.PaymentId;

            await _dbContext.SaveChangesAsync(cancellationToken);
            
            BackgroundJob.Enqueue(() => _notificationService.SendExpensePaymentNotificationAsync(expense.UserId));

            var response = _mapper.Map<PaymentResponse>(payment);
            return new ApiResponse<PaymentResponse>(response);
        }
        else
        {
            return new ApiResponse<PaymentResponse>("Payment failed"); 
        }
    }
}