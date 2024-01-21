using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using FluentValidation;

namespace ExpensePaymentSystem.Business.Validators;

public class ExpenseRequestValidator : AbstractValidator<ExpenseRequest>
{
    public ExpenseRequestValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("User ID must be a positive number.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Date must not be in the future.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .Length(1, 500).WithMessage("Description must be between 1 and 500 characters.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid status.");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required.")
            .Length(1, 100).WithMessage("Category must be between 1 and 100 characters.");

    }

     
}
