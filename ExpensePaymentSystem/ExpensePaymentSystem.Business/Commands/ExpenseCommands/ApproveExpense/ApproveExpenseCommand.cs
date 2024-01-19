using ExpensePaymentSystem.Base.Response;
using MediatR;

namespace ExpensePaymentSystem.Business.Commands.ExpenseCommands.ApproveExpense;

public class ApproveExpenseCommand : IRequest<ApiResponse>
{
    public int Id { get; }

    public ApproveExpenseCommand(int id)
    {
        Id = id;
    }
}