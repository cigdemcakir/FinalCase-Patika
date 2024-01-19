using ExpensePaymentSystem.Base.Response;
using MediatR;

namespace ExpensePaymentSystem.Business.Commands.UserCommands.DeleteUser;

public class DeleteUserCommand : IRequest<ApiResponse>
{
    public int Id { get; }

    public DeleteUserCommand(int id)
    {
        Id = id;
    }
}