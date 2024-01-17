using ExpensePaymentSystem.Data.Entity;
using FluentValidation;

namespace ExpensePaymentSystem.Business.Validators;

public class ReportValidator : AbstractValidator<Report>
{
    public ReportValidator()
    {
        RuleFor(report => report.StartDate)
            .NotEmpty().WithMessage("Start date is required.")
            .LessThanOrEqualTo(r => r.EndDate).WithMessage("Start date must be before or equal to the end date.");

        RuleFor(report => report.EndDate)
            .NotEmpty().WithMessage("End date is required.")
            .GreaterThanOrEqualTo(r => r.StartDate).WithMessage("End date must be after or equal to the start date.");

        RuleFor(report => report.UserId)
            .GreaterThan(0).WithMessage("User ID must be greater than zero.");
    }
}