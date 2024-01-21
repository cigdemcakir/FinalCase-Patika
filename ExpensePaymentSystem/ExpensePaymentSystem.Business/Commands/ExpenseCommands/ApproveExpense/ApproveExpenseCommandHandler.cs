using AutoMapper;
using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Commands.ExpenseCommands.ApproveExpense;

public class ApproveExpenseCommandHandler : IRequestHandler<ApproveExpenseCommand, ApiResponse>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public ApproveExpenseCommandHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse> Handle(ApproveExpenseCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await _dbContext.Set<Expense>().Where(x => x.ExpenseId == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (fromdb == null)
            return new ApiResponse("Record not found");

        fromdb.Status = ExpenseStatus.Approved; 

        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return new ApiResponse();
    }
}