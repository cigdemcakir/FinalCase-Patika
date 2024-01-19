using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Commands.ExpenseCommands.CreateExpense;

public class CreateExpenseCommand : IRequest<ApiResponse<ExpenseResponse>>
{
    public ExpenseRequest Model { get; set; }

    public CreateExpenseCommand(ExpenseRequest model)
    {
        Model = model;
    }
}