using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Queries.ExpenseQueries.GetAllExpenses;

public class GetAllExpensesQueryHandler : IRequestHandler<GetAllExpensesQuery, ApiResponse<List<ExpenseResponse>>>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllExpensesQueryHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetAllExpensesQuery query, CancellationToken cancellationToken)
    {
        var list = await _dbContext.Set<Expense>()
            .ToListAsync(cancellationToken);
        
        var mappedList = _mapper.Map<List<Expense>, List<ExpenseResponse>>(list);
        
        return new ApiResponse<List<ExpenseResponse>>(mappedList);
    }
}