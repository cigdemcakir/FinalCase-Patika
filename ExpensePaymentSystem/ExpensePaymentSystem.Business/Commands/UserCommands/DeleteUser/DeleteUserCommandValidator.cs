using ExpensePaymentSystem.Business.Commands.ExpenseCommands.DeleteExpense;
using FluentValidation;

namespace ExpensePaymentSystem.Business.Commands.UserCommands.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be a positive number.");
    }
}