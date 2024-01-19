using ExpensePaymentSystem.Business.Commands.ExpenseCommands.DeleteExpense;
using FluentValidation;

namespace ExpensePaymentSystem.Business.Commands.UserCommands.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        
    }
}