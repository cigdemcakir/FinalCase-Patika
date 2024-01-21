using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using FluentValidation;

namespace ExpensePaymentSystem.Business.Validators;

public class PaymentRequestValidator : AbstractValidator<PaymentRequest>
{
    public PaymentRequestValidator()
    {
         RuleFor(payment => payment.ExpenseId)
            .GreaterThan(0).WithMessage("Expense ID must be greater than zero.");
         
         RuleFor(x => x.PaymentMethod)
             .IsInEnum().WithMessage("Payment method is not valid.");
    }
}