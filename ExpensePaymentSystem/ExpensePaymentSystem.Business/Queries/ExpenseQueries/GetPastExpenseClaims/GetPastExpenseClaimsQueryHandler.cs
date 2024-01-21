using AutoMapper;
using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Queries.ExpenseQueries.GetPastExpenseClaims;

public class GetPastExpenseClaimsQueryHandler : IRequestHandler<GetPastExpenseClaimsQuery, ApiResponse<List<ExpenseResponse>>>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPastExpenseClaimsQueryHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetPastExpenseClaimsQuery query, CancellationToken cancellationToken)
    {
        var expenses = await _dbContext.Expenses
            .Where(e => e.Status == ExpenseStatus.Approved || e.Status == ExpenseStatus.Rejected)
            .ToListAsync(cancellationToken);

        var response = _mapper.Map<List<ExpenseResponse>>(expenses);
        
        return new ApiResponse<List<ExpenseResponse>>(response);
    }
}