using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Data.Entity;
using FluentValidation;

namespace ExpensePaymentSystem.Business.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.UserName)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(50).WithMessage("Username cannot be more than 50 characters.");

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .MaximumLength(250).WithMessage("Password cannot be more than 250 characters.");

        RuleFor(user => user.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name cannot be more than 50 characters.");

        RuleFor(user => user.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name cannot be more than 50 characters.");

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.")
            .MaximumLength(50).WithMessage("Email cannot be more than 50 characters.");

        RuleFor(user => user.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .MaximumLength(20).WithMessage("Phone number cannot be more than 20 characters.");

        When(user => user.Role == UserRole.Employee, () =>
        {
            RuleFor(user => user.IBAN)
                .NotEmpty().WithMessage("IBAN is required for employees.")
                .Length(34).WithMessage("IBAN must be 34 characters long.");
        });

        RuleFor(user => user.LastActivityDate)
            .NotEmpty().WithMessage("Last activity date is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Last activity date cannot be in the future.");

        RuleFor(user => user.PasswordRetryCount)
            .GreaterThanOrEqualTo(0).WithMessage("Password retry count cannot be negative.");

        RuleFor(user => user.IsActive)
            .NotNull().WithMessage("IsActive flag is required.");
    }
}