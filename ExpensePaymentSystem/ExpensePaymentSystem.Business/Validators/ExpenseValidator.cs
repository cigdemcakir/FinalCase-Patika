using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Data.Entity;
using FluentValidation;

namespace ExpensePaymentSystem.Business.Validators;

public class ExpenseValidator : AbstractValidator<Expense>
{
    public ExpenseValidator()
    {
        RuleFor(expense => expense.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");

        RuleFor(expense => expense.Date)
            .NotEmpty().WithMessage("Date is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Date cannot be in the future.");

        RuleFor(expense => expense.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(100).WithMessage("Description cannot be more than 100 characters.");

        RuleFor(expense => expense.Category)
            .NotEmpty().WithMessage("Category is required.")
            .MaximumLength(50).WithMessage("Category cannot be more than 50 characters.");

        RuleFor(expense => expense.Status)
            .IsInEnum().WithMessage("Status is not valid.");

        When(expense => expense.Status == ExpenseStatus.Rejected, () =>
        {
            RuleFor(expense => expense.RejectionReason)
                .NotEmpty().WithMessage("Rejection reason is required when the expense is rejected.")
                .MaximumLength(100).WithMessage("Rejection reason cannot be more than 100 characters.");
        });

        When(expense => expense.PaymentId.HasValue, () =>
        {
            RuleFor(expense => expense.PaymentId.Value)
                .GreaterThan(0).WithMessage("Payment ID must be greater than zero.");
        });

        When(expense => expense.ReportId.HasValue, () =>
        {
            RuleFor(expense => expense.ReportId.Value)
                .GreaterThan(0).WithMessage("Report ID must be greater than zero.");
        });
    }

     
}
