using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Commands.TokenCommands.CreateToken;
using ExpensePaymentSystem.Schema;
using ExpensePaymentSystem.WebApi.Filter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpensePaymentSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IMediator _mediator;

    public TokenController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
            
    [TypeFilter(typeof(LogResourceFilter))]
    [TypeFilter(typeof(LogActionFilter))]
    [TypeFilter(typeof(LogAuthorizationFilter))]
    [TypeFilter(typeof(LogResourceFilter))]
    [TypeFilter(typeof(LogExceptionFilter))]
    [HttpPost]
    public async Task<ApiResponse<TokenResponse>> Post([FromBody] TokenRequest request)
    {
        var operation = new CreateTokenCommand(request);
        var result = await _mediator.Send(operation);
        return result;
    }
}