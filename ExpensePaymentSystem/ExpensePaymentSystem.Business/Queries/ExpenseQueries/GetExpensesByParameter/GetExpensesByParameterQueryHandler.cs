using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Queries.ExpenseQueries.GetExpensesByParameter;

public class GetExpensesByParameterQueryHandler : IRequestHandler<GetExpensesByParameterQuery, ApiResponse<List<ExpenseResponse>>>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetExpensesByParameterQueryHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetExpensesByParameterQuery query, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Expense>(true);
        
        if (query.UserId.HasValue)
            predicate = predicate.And(x => x.UserId == query.UserId.Value);

        if (query.Status.HasValue)
            predicate = predicate.And(x => x.Status == query.Status.Value);
        
        if (query.Amount.HasValue)
            predicate = predicate.And(x => x.Amount == query.Amount.Value);

        if (query.Date.HasValue)
            predicate = predicate.And(x => x.Date.Date == query.Date.Value.Date);

        if (!string.IsNullOrEmpty(query.Category))
            predicate= predicate.And(x => x.Category.ToUpper().Contains(query.Category.ToUpper()));
        
        var list =  await _dbContext.Set<Expense>()
            .Where(predicate).ToListAsync(cancellationToken);
        
        var mappedList = _mapper.Map<List<Expense>, List<ExpenseResponse>>(list);
        
        return new ApiResponse<List<ExpenseResponse>>(mappedList);
    }
}