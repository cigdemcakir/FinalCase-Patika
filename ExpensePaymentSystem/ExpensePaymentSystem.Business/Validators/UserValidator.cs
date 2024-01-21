using System.Text.RegularExpressions;
using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using FluentValidation;

namespace ExpensePaymentSystem.Business.Validators;

public class UserRequestValidator : AbstractValidator<UserRequest>
{
    public UserRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username is required.")
            .Length(3, 50).WithMessage("Username must be between 3 and 50 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Invalid role.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .Length(1, 50).WithMessage("First name must be between 1 and 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .Length(1, 50).WithMessage("Last name must be between 1 and 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(new Regex(@"^\+?\d{10,15}$")).WithMessage("Invalid phone number.");

        When(x => x.IBAN != null, () =>
        {
            RuleFor(x => x.IBAN)
                .Matches(new Regex(@"^TR\d{2}[a-zA-Z0-9]{16,26}$")).WithMessage("Invalid IBAN.");
        });
    }
}