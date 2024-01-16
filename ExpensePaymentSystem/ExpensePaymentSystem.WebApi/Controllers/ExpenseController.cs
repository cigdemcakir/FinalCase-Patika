using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Business.Interfaces;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpensePaymentSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpenseController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExpenseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ApiResponse<List<ExpenseResponse>>> Get()
    {
        var operation = new GetAllExpenseQuery();
        var result = await _mediator.Send(operation);
        return result;
    }

    [HttpGet("{id}")]
    public async Task<ApiResponse<ExpenseResponse>> Get(int id)
    {
        var operation = new GetExpenseByIdQuery(id);
        var result = await _mediator.Send(operation);
        return result;
    }

    [HttpGet("ByParameters")]
    public async Task<ApiResponse<List<ExpenseResponse>>> GetByParameter(
        [FromQuery] string? FirstName,
        [FromQuery] string? LastName,
        [FromQuery] string? IdentityNumber)
    {
        var operation = new GetExpenseByParameterQuery(FirstName,LastName,IdentityNumber);
        var result = await _mediator.Send(operation);
        return result;
    }

    [HttpPost]
    public async Task<ApiResponse<ExpenseResponse>> Post([FromBody] ExpenseRequest Expense)
    {
        var operation = new CreateExpenseCommand(Expense);
        var result = await _mediator.Send(operation);
        return result;
    }

    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] ExpenseRequest Expense)
    {
        var operation = new UpdateExpenseCommand(id, Expense);
        var result = await _mediator.Send(operation);
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteExpenseCommand(id);
        var result = await _mediator.Send(operation);
        return result;
    }
}