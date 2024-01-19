using FluentValidation;

namespace ExpensePaymentSystem.Business.Commands.ExpenseCommands.RejectExpense;

public class RejectExpenseCommandValidator : AbstractValidator<DeleteExpense.DeleteExpenseCommand>
{
    public RejectExpenseCommandValidator()
    {
        
    }
}