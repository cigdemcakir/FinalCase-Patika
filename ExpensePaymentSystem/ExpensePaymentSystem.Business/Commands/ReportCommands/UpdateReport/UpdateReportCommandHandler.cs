using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Commands.ReportCommands.CreateCommand;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Commands.ReportCommands.UpdateReport;

public class UpdateReportCommandHandler: IRequestHandler<UpdateReportCommand, ApiResponse<ReportResponse>>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdateReportCommandHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<ReportResponse>> Handle(UpdateReportCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await _dbContext.Set<Report>().Where(x => x.ReportId == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (fromdb == null)
            return new ApiResponse<ReportResponse>("Record not found");

        fromdb.StartDate = request.Model.StartDate;
        fromdb.EndDate = request.Model.EndDate;
        
        fromdb.TotalPayment = await _dbContext.Reports
            .Where(e => e.StartDate >= request.Model.StartDate && e.EndDate <= request.Model.EndDate)
            .SumAsync(e => e.TotalPayment, cancellationToken);

        var response = _mapper.Map<ReportResponse>(fromdb);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return new ApiResponse<ReportResponse>(response);
       
    }
}