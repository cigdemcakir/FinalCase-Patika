using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Commands.ReportCommands.CreateCommand;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Commands.ReportCommands.DeleteReport;

public class DeleteReportCommandHandler: IRequestHandler<DeleteReportCommand, ApiResponse>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public DeleteReportCommandHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await _dbContext.Set<Report>().Where(x => x.ReportId == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (fromdb == null)
            return new ApiResponse("Record not found");

        _dbContext.Reports.Remove(fromdb);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return new ApiResponse();
    }
}