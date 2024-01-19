using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Commands.ExpenseCommands.DeleteExpense;

public class DeleteExpenseCommand : IRequest<ApiResponse>
{
    public int Id { get; }

    public DeleteExpenseCommand(int id)
    {
        Id = id;
    }
}