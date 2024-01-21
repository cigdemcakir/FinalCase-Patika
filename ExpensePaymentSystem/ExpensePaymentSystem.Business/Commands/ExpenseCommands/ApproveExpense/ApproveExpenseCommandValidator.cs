using FluentValidation;

namespace ExpensePaymentSystem.Business.Commands.ExpenseCommands.ApproveExpense;

public class ApproveExpenseCommandValidator : AbstractValidator<ApproveExpenseCommand>
{
    public ApproveExpenseCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be a positive number.");
    }
}