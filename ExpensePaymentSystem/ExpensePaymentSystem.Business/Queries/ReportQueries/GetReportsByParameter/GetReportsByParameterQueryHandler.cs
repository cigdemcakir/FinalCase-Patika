using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Queries.PaymentQueries.GetPaymentsByParameter;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Queries.ReportQueries.GetReportsByParameter;

public class GetReportsByParameterQueryHandler : IRequestHandler<GetReportsByParameterQuery, ApiResponse<List<ReportResponse>>>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetReportsByParameterQueryHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<ReportResponse>>> Handle(GetReportsByParameterQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Report>(true);

        if (request.StartDate.HasValue)
            predicate = predicate.And(x => x.StartDate.Date== request.StartDate.Value.Date);

        if (request.EndDate.HasValue)
            predicate = predicate.And(x => x.EndDate.Date== request.EndDate.Value.Date);

        var list =  await _dbContext.Set<Report>()
            .Include(x=>x.Expenses)
            .Where(predicate).ToListAsync(cancellationToken);

        var mappedList = _mapper.Map<List<Report>, List<ReportResponse>>(list);
        
        return new ApiResponse<List<ReportResponse>>(mappedList);
    }
}