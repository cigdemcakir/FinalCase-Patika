using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Queries.ExpenseQueries.GetExpenseById;

public class GetExpenseByIdQueryHandler : IRequestHandler<GetExpenseByIdQuery, ApiResponse<ExpenseResponse>>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetExpenseByIdQueryHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<ExpenseResponse>> Handle(GetExpenseByIdQuery query, CancellationToken cancellationToken)
    {
        var entity =  await _dbContext.Set<Expense>()
            .Include(x => x.Payment)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.ExpenseId == query.Id, cancellationToken);

        if (entity == null)
            return new ApiResponse<ExpenseResponse>("Record not found");

        var mapped = _mapper.Map<Expense, ExpenseResponse>(entity);
        
        return new ApiResponse<ExpenseResponse>(mapped);
    }
}