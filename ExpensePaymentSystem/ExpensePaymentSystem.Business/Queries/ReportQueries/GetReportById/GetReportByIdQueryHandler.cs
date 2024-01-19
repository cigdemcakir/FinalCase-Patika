using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Queries.PaymentQueries.GetPaymentById;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Queries.ReportQueries.GetReportById;

public class GetReportByIdQueryHandler : IRequestHandler<GetReportByIdQuery, ApiResponse<ReportResponse>>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetReportByIdQueryHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<ReportResponse>> Handle(GetReportByIdQuery request, CancellationToken cancellationToken)
    {
        var entity =  await _dbContext.Set<Report>()
            .Include(x => x.Expenses)
            .FirstOrDefaultAsync(x => x.ReportId == request.Id, cancellationToken);

        if (entity == null)
        {
            return new ApiResponse<ReportResponse>("Record not found");
        }
        
        var mapped = _mapper.Map<Report, ReportResponse>(entity);
        return new ApiResponse<ReportResponse>(mapped);
    }
}