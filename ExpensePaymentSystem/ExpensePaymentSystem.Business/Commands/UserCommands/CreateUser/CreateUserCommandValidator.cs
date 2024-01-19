using ExpensePaymentSystem.Business.Commands.ExpenseCommands.CreateExpense;
using FluentValidation;

namespace ExpensePaymentSystem.Business.Commands.UserCommands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        /*RuleFor(expense => expense.Model.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");

        RuleFor(expense => expense.Model.Date)
            .NotEmpty().WithMessage("Date is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Date cannot be in the future.");

        RuleFor(expense => expense.Model.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(100).WithMessage("Description cannot be more than 100 characters.");

        RuleFor(expense => expense.Model.Category)
            .NotEmpty().WithMessage("Category is required.")
            .MaximumLength(50).WithMessage("Category cannot be more than 50 characters.");
            */

        // RuleFor(expense => expense.Model.Status)
        //     .IsInEnum().WithMessage("Status is not valid.");
        //
        // When(expense => expense.Model.Status == ExpenseStatus.Rejected, () =>
        // {
        //     RuleFor(expense => expense.RejectionReason)
        //         .NotEmpty().WithMessage("Rejection reason is required when the expense is rejected.")
        //         .MaximumLength(100).WithMessage("Rejection reason cannot be more than 100 characters.");
        // });

        //todo
        // When(expense => expense.Model.PaymentId.HasValue, () =>
        // {
        //     RuleFor(expense => expense.PaymentId.Value)
        //         .GreaterThan(0).WithMessage("Payment ID must be greater than zero.");
        // });
        //
        // When(expense => expense.Model.ReportId.HasValue, () =>
        // {
        //     RuleFor(expense => expense.ReportId.Value)
        //         .GreaterThan(0).WithMessage("Report ID must be greater than zero.");
        // });
    }
}