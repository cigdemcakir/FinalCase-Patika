using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Commands.ExpenseCommands.UpdateExpense;

public class UpdateExpenseCommand : IRequest<ApiResponse<ExpenseResponse>>
{
    public int Id { get; }
    public ExpenseRequest Model { get; }

    public UpdateExpenseCommand(int id, ExpenseRequest model)
    {
        Id = id;
        Model = model;
    }
}