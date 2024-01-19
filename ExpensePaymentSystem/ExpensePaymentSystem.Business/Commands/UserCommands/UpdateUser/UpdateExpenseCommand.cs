using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Commands.UserCommands.UpdateUser;

public class UpdateUserCommand : IRequest<ApiResponse<UserResponse>>
{
    public int Id { get; }
    public UserRequest Model { get; }

    public UpdateUserCommand(int id, UserRequest model)
    {
        Id = id;
        Model = model;
    }
}