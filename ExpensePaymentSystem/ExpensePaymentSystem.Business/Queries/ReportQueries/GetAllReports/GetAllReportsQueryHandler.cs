using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Queries.PaymentQueries.GetAllPayments;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Queries.ReportQueries.GetAllReports;

public class GetAllReportsQueryHandler : IRequestHandler<GetAllReportsQuery, ApiResponse<List<ReportResponse>>>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllReportsQueryHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<ReportResponse>>> Handle(GetAllReportsQuery request, CancellationToken cancellationToken)
    {
        var list = await _dbContext.Set<Report>()
            .Include(x=>x.Expenses)
            .ToListAsync(cancellationToken);
        
        var mappedList = _mapper.Map<List<Report>, List<ReportResponse>>(list);
        
        return new ApiResponse<List<ReportResponse>>(mappedList);
    }
}