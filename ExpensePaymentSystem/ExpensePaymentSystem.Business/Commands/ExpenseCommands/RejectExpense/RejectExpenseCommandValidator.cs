using ExpensePaymentSystem.Business.Commands.ExpenseCommands.DeleteExpense;
using FluentValidation;

namespace ExpensePaymentSystem.Business.Commands.ExpenseCommands.RejectExpense;

public class RejectExpenseCommandValidator : AbstractValidator<RejectExpenseCommand>
{
    public RejectExpenseCommandValidator()
    {
        RuleFor(command => command.Id)
            .GreaterThan(0).WithMessage("Id must be a positive number.");

        RuleFor(command => command.Reason)
            .NotEmpty().WithMessage("Rejection reason is required.")
            .Length(1, 500).WithMessage("Rejection reason must be between 1 and 500 characters.");
    }
}