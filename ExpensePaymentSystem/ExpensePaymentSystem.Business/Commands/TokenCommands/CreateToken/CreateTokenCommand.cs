using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Commands.TokenCommands.CreateToken;


public class CreateTokenCommand : IRequest<ApiResponse<TokenResponse>>
{
    public TokenRequest Model { get; set; }

    public CreateTokenCommand(TokenRequest model)
    {
        Model = model;
    }
}