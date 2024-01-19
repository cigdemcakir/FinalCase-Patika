using ExpensePaymentSystem.Base.Response;
using MediatR;

namespace ExpensePaymentSystem.Business.Commands.ExpenseCommands.RejectExpense;

public class RejectExpenseCommand : IRequest<ApiResponse>
{
    public int Id { get; }
    public string Reason { get; } 

    public RejectExpenseCommand(int id, string reason)
    {
        Id = id;
        Reason = reason;
    }
}
