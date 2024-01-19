using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Queries.PaymentQueries.GetPaymentById;

public class GetPaymentByIdQuery : IRequest<ApiResponse<PaymentResponse>>
{
    public int Id { get; }

    public GetPaymentByIdQuery(int id)
    {
        Id = id;
    }
}