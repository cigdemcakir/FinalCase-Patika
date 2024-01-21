using FluentValidation;

namespace ExpensePaymentSystem.Business.Commands.PaymentCommands.DeletePayment;

public class DeletePaymentCommandValidator : AbstractValidator<DeletePaymentCommand>
{
    public DeletePaymentCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be a positive number.");
    }
}