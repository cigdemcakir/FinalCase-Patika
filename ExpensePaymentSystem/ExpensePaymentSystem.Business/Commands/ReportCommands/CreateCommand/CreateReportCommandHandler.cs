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

        var expenses = await _dbContext.Expenses
            .Where(e => e.Date >= startDate && e.Date <= endDate)
            .GroupBy(e => e.Category)
            .Select(group => new 
            {
                Category = group.Key,
                TotalAmount = group.Sum(e => e.Amount)
            })
            .ToListAsync(cancellationToken);

        var report = new Report
        {
            StartDate = startDate,
            EndDate = endDate,
            TotalAmount = expenses.Sum(e => e.TotalAmount),
        };

        _dbContext.Reports.Add(report);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var response = _mapper.Map<ReportResponse>(report);
        return new ApiResponse<ReportResponse>(response);
    }
}