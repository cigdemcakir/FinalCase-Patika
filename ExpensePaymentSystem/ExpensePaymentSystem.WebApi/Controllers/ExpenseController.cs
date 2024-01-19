using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Commands.ExpenseCommands.ApproveExpense;
using ExpensePaymentSystem.Business.Commands.ExpenseCommands.CreateExpense;
using ExpensePaymentSystem.Business.Commands.ExpenseCommands.DeleteExpense;
using ExpensePaymentSystem.Business.Commands.ExpenseCommands.RejectExpense;
using ExpensePaymentSystem.Business.Commands.ExpenseCommands.UpdateExpense;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Business.Queries.ExpenseQueries.GetAllExpenses;
using ExpensePaymentSystem.Business.Queries.ExpenseQueries.GetExpenseById;
using ExpensePaymentSystem.Business.Queries.ExpenseQueries.GetExpensesByParameter;
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

    // GetAllExpenses
    [HttpGet]
    public async Task<ApiResponse<List<ExpenseResponse>>> GetAll()
    {
        var operation = new GetAllExpensesQuery();
        var result = await _mediator.Send(operation);
        return result;
    }
    
    // // GetExpensesByUser
    // [HttpGet("user/{userId}")]
    // public async Task<ApiResponse<List<ExpenseResponse>>> GetExpensesByUser(int userId)
    // {
    //     var query = new GetExpensesByUserQuery(userId);
    //     var result = await _mediator.Send(query);
    //     return result;
    // }
    
    // // GetExpensesByStatus
    // [HttpGet("status/{status}")]
    // public async Task<ApiResponse<List<ExpenseResponse>>> GetExpensesByStatus(ExpenseStatus status)
    // {
    //     var query = new GetExpensesByStatusQuery(status);
    //     var result = await _mediator.Send(query);
    //
    //     return result;
    // }

    // GetExpensesById
    [HttpGet("{id}")]
    public async Task<ApiResponse<ExpenseResponse>> GetById(int id)
    {
        var operation = new GetExpenseByIdQuery(id);
        var result = await _mediator.Send(operation);
        return result;
    }
    
    [HttpGet("parameter")]
    public async Task<ApiResponse<List<ExpenseResponse>>> GetByParameter([FromQuery] int? userId, [FromQuery] ExpenseStatus? status, [FromQuery] decimal? amount, [FromQuery] DateTime? date, [FromQuery] string? category)
    {
        var query = new GetExpensesByParameterQuery(userId, status, amount, date, category);
        var result = await _mediator.Send(query);

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
    public async Task<ApiResponse<ExpenseResponse>> Put(int id, [FromBody] ExpenseRequest Expense)
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
    
    // ApproveExpense
    [HttpPost("{id}/approve")]
    public async Task<ApiResponse> ApproveExpense(int id)
    {
        var command = new ApproveExpenseCommand(id);
        var result = await _mediator.Send(command);

        return result;
    }

    // RejectExpense
    [HttpPost("{id}/reject")]
    public async Task<ApiResponse> RejectExpense(int id, [FromBody] string reason)
    {
        var command = new RejectExpenseCommand(id, reason);
        var result = await _mediator.Send(command);

        return result;
    }
}