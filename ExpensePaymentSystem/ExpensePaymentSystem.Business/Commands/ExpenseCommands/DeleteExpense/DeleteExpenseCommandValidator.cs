using ExpensePaymentSystem.Business.Commands.ExpenseCommands.DeleteExpense;
using FluentValidation;

namespace ExpensePaymentSystem.Business.Commands.ExpenseCommands.DeleteExpense;

public class DeleteExpenseCommandValidator : AbstractValidator<DeleteExpenseCommand>
{
    public DeleteExpenseCommandValidator()
    {
        
    }
}