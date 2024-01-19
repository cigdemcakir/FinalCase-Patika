using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Queries.PaymentQueries.GetPaymentsByParameter;

public class GetPaymentsByParameterQuery : IRequest<ApiResponse<List<PaymentResponse>>>
{
    public decimal? Amount { get; set; }
    
    public DateTime? PaymentDate { get; set; }
    
    public string? PaymentMethod { get; set; }

    public GetPaymentsByParameterQuery(decimal? amount = null, DateTime? paymentDate = null, string? paymentMethod = null)
    {
        PaymentDate = paymentDate;
        Amount = amount;
        PaymentMethod = paymentMethod;
    }
}