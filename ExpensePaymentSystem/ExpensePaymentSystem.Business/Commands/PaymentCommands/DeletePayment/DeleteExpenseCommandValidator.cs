using FluentValidation;

namespace ExpensePaymentSystem.Business.Commands.PaymentCommands.DeletePayment;

public class DeletePaymentCommandValidator : AbstractValidator<DeletePaymentCommand>
{
    public DeletePaymentCommandValidator()
    {
        
    }
}