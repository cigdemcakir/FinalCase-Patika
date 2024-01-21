using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Queries.ExpenseQueries.GetPastExpenseClaims;

public class GetPastExpenseClaimsQuery : IRequest<ApiResponse<List<ExpenseResponse>>>
{
    
}