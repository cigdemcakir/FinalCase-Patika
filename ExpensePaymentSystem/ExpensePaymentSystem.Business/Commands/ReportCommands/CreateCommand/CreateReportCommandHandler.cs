using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Commands.ReportCommands.CreateCommand;

public class CreateReportCommandHandler: IRequestHandler<CreateReportCommand, ApiResponse<ReportResponse>>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateReportCommandHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<ReportResponse>> Handle(CreateReportCommand request, CancellationToken cancellationToken)
    {
        var startDate = request.Model.StartDate;
        var endDate = request.Model.EndDate;
        var userId = request.Model.UserId;

        var query = _dbContext.Expenses.AsQueryable();
        
        if (userId.HasValue)
            query = query.Where(e => e.UserId == userId.Value);

        query = query.Where(e => e.Date >= startDate && e.Date <= endDate);

        var expensesInReport = await query.ToListAsync(cancellationToken);

        var totalExpense = await query.SumAsync(e => e.Amount, cancellationToken);

        var totalPayments = await query.Where(e => e.PaymentId.HasValue)
            .SumAsync(e => e.Amount, cancellationToken);

        var report = new Report
        {
            StartDate = startDate,
            EndDate = endDate,
            TotalExpense = totalExpense,
            TotalPayment = totalPayments, 
            UserId = userId,
            Expenses = expensesInReport
        };

        _dbContext.Reports.Add(report);
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        var response = _mapper.Map<ReportResponse>(report);
        
        return new ApiResponse<ReportResponse>(response);
    }
}