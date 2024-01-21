using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Commands.ExpenseCommands.UpdateExpense;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Commands.ExpenseCommands.UpdateExpense;

public class UpdateExpenseCommandHandler : IRequestHandler<UpdateExpenseCommand, ApiResponse<ExpenseResponse>>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdateExpenseCommandHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<ExpenseResponse>> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await _dbContext.Set<Expense>().Where(x => x.ExpenseId == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (fromdb == null)
            return new ApiResponse<ExpenseResponse>("Record not found");

        fromdb.Status = request.Model.Status;
        fromdb.Category = request.Model.Category;
        fromdb.Date = request.Model.Date;
        fromdb.Amount = request.Model.Amount;

        var response = _mapper.Map<ExpenseResponse>(fromdb);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return new ApiResponse<ExpenseResponse>(response);
    }
}