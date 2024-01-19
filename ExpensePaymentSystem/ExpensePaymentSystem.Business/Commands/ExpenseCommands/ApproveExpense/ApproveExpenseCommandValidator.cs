using FluentValidation;

namespace ExpensePaymentSystem.Business.Commands.ExpenseCommands.ApproveExpense;

public class ApproveExpenseCommandValidator : AbstractValidator<DeleteExpense.DeleteExpenseCommand>
{
    public ApproveExpenseCommandValidator()
    {
        
    }
}