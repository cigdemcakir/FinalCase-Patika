using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Queries.ExpenseQueries.GetAllExpenses;

public class GetAllExpensesQuery : IRequest<ApiResponse<List<ExpenseResponse>>>
{
}