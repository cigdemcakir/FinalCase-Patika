using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Queries.ExpenseQueries.GetActiveExpenseClaims;

public class GetActiveExpenseClaimsQuery: IRequest<ApiResponse<List<ExpenseResponse>>>
{
}
