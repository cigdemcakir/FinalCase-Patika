using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using FluentValidation;

namespace ExpensePaymentSystem.Business.Validators;

public class ReportRequestValidator : AbstractValidator<ReportRequest>
{
    public ReportRequestValidator()
    {
        RuleFor(report => report.StartDate)
            .NotEmpty().WithMessage("Start date is required.")
            .LessThanOrEqualTo(r => r.EndDate).WithMessage("Start date must be before or equal to the end date.");

        RuleFor(report => report.EndDate)
            .NotEmpty().WithMessage("End date is required.")
            .GreaterThanOrEqualTo(r => r.StartDate).WithMessage("End date must be after or equal to the start date.");

        When(x => x.UserId.HasValue, () =>
        {
            RuleFor(x => x.UserId.Value)
                .GreaterThan(0).WithMessage("User ID must be a positive number.");
        });

    }
}