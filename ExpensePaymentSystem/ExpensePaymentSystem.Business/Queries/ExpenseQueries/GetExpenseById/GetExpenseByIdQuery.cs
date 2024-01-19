using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Queries.ExpenseQueries.GetExpenseById;

public class GetExpenseByIdQuery : IRequest<ApiResponse<ExpenseResponse>>
{
    public int Id { get; }

    public GetExpenseByIdQuery(int id)
    {
        Id = id;
    }
}