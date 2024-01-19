using ExpensePaymentSystem.Schema;
using FluentValidation;

namespace ExpensePaymentSystem.Business.Commands.TokenCommands.CreateToken;

public class CreateTokenCommandValidator : AbstractValidator<TokenRequest>
{
    public CreateTokenCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().MinimumLength(5).MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(5).MaximumLength(10);
    }
}