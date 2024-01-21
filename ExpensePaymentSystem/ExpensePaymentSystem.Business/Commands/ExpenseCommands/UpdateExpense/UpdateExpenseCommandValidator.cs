using ExpensePaymentSystem.Business.Commands.ExpenseCommands.UpdateExpense;
using ExpensePaymentSystem.Business.Validators;
using FluentValidation;

namespace ExpensePaymentSystem.Business.Commands.ExpenseCommands.UpdateExpense;

public class UpdateExpenseCommandValidator : AbstractValidator<UpdateExpenseCommand>
{
    public UpdateExpenseCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be a positive number.");

        RuleFor(command => command.Model)
            .SetValidator(new ExpenseRequestValidator());
    }
}