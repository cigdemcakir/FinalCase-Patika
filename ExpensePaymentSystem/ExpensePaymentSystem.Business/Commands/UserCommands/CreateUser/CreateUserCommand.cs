using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Commands.UserCommands.CreateUser;

public class CreateUserCommand : IRequest<ApiResponse<UserResponse>>
{
    public UserRequest Model { get; set; }

    public CreateUserCommand(UserRequest model)
    {
        Model = model;
    }
}