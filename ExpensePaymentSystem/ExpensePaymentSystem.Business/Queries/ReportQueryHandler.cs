using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Queries;

public class ReportQueryHandler:
    IRequestHandler<GetAllReportQuery, ApiResponse<List<ReportResponse>>>,
    IRequestHandler<GetReportByIdQuery, ApiResponse<ReportResponse>>,
    IRequestHandler<GetReportByParameterQuery, ApiResponse<List<ReportResponse>>>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public ReportQueryHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<ReportResponse>>> Handle(GetAllReportQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Report>()
            .Include(x => x.User)
            .Include(x => x.Expenses)
            .ToListAsync(cancellationToken);
        
        var mappedList = mapper.Map<List<Report>, List<ReportResponse>>(list);
         return new ApiResponse<List<ReportResponse>>(mappedList);
    }

    public async Task<ApiResponse<ReportResponse>> Handle(GetReportByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity =  await dbContext.Set<Report>()
            .Include(x => x.User)
            .Include(x => x.Expenses)
            .FirstOrDefaultAsync(x => x.ReportId == request.Id, cancellationToken);

        if (entity == null)
        {
            return new ApiResponse<ReportResponse>("Record not found");
        }
        
        var mapped = mapper.Map<Report, ReportResponse>(entity);
        return new ApiResponse<ReportResponse>(mapped);
    }

    public async Task<ApiResponse<List<ReportResponse>>> Handle(GetReportByParameterQuery request,
        CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Report>(true);
        
        var list =  await dbContext.Set<Report>()
            .Include(x => x.User)
            .Include(x => x.Expenses)
            .Where(predicate).ToListAsync(cancellationToken);
        
        var mappedList = mapper.Map<List<Report>, List<ReportResponse>>(list);
        return new ApiResponse<List<ReportResponse>>(mappedList);
    }
}