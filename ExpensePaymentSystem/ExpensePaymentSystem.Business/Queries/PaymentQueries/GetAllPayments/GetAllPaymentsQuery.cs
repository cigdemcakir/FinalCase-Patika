using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Queries.PaymentQueries.GetAllPayments;

public class GetAllPaymentsQuery : IRequest<ApiResponse<List<PaymentResponse>>>
{
}