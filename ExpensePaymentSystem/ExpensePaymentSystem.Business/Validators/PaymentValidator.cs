using ExpensePaymentSystem.Data.Entity;
using FluentValidation;

namespace ExpensePaymentSystem.Business.Validators;

public class PaymentValidator : AbstractValidator<Payment>
{
    public PaymentValidator()
    {
        RuleFor(payment => payment.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");

        RuleFor(payment => payment.PaymentDate)
            .NotEmpty().WithMessage("Payment date is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Payment date cannot be in the future.");

        RuleFor(payment => payment.PaymentMethod)
            .NotEmpty().WithMessage("Payment method is required.")
            .MaximumLength(50).WithMessage("Payment method cannot be more than 50 characters.");

        RuleFor(payment => payment.ExpenseId)
            .GreaterThan(0).WithMessage("Expense ID must be greater than zero.");
    }
}