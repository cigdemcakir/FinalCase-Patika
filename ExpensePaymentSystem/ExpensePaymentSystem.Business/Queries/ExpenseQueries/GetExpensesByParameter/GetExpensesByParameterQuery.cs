using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Queries.ExpenseQueries.GetExpensesByParameter;

public class GetExpensesByParameterQuery : IRequest<ApiResponse<List<ExpenseResponse>>>
{
    public int? UserId { get; set; }
    public ExpenseStatus? Status { get; set; }
    public decimal? Amount { get; set; }
    public DateTime? Date { get; set; }
    public string? Category { get; set; }

    public GetExpensesByParameterQuery(int? userId = null, ExpenseStatus? status = null, decimal? amount = null, DateTime? date = null, string? category = null)
    {
        UserId = userId;
        Status = status;
        Amount = amount;
        Date = date;
        Category = category;
    }
}