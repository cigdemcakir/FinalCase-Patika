using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Commands.ExpenseCommands.DeleteExpense;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Commands.ExpenseCommands.DeleteExpense;

public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand, ApiResponse>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public DeleteExpenseCommandHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await _dbContext.Set<Expense>().Where(x => x.ExpenseId == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (fromdb == null)
            return new ApiResponse("Record not found");

        _dbContext.Expenses.Remove(fromdb);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return new ApiResponse();
    }
}