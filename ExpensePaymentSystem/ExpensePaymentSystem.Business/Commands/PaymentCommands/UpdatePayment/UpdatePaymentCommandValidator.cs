using ExpensePaymentSystem.Business.Commands.ExpenseCommands.UpdateExpense;
using ExpensePaymentSystem.Business.Validators;
using FluentValidation;

namespace ExpensePaymentSystem.Business.Commands.PaymentCommands.UpdatePayment;

public class UpdatePaymentCommandValidator : AbstractValidator<UpdatePaymentCommand>
{
    public UpdatePaymentCommandValidator()
    {
        RuleFor(command => command.Id)
            .GreaterThan(0).WithMessage("Id must be a positive number.");

        RuleFor(command => command.Model)
            .SetValidator(new PaymentRequestValidator());
    }
}