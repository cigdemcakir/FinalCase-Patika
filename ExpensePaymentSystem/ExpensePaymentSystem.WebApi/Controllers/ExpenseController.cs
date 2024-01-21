using System.Text;
using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Commands.ExpenseCommands.ApproveExpense;
using ExpensePaymentSystem.Business.Commands.ExpenseCommands.CreateExpense;
using ExpensePaymentSystem.Business.Commands.ExpenseCommands.DeleteExpense;
using ExpensePaymentSystem.Business.Commands.ExpenseCommands.RejectExpense;
using ExpensePaymentSystem.Business.Commands.ExpenseCommands.UpdateExpense;
using ExpensePaymentSystem.Business.Queries.ExpenseQueries.GetActiveExpenseClaims;
using ExpensePaymentSystem.Business.Queries.ExpenseQueries.GetAllExpenses;
using ExpensePaymentSystem.Business.Queries.ExpenseQueries.GetExpenseById;
using ExpensePaymentSystem.Business.Queries.ExpenseQueries.GetExpensesByParameter;
using ExpensePaymentSystem.Business.Queries.ExpenseQueries.GetPastExpenseClaims;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace ExpensePaymentSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpenseController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMemoryCache _memoryCache;
    private readonly IDistributedCache _distributedCache;

    public ExpenseController(IMediator mediator, IMemoryCache memoryCache, IDistributedCache distributedCache)
    {
        _mediator = mediator;
        _memoryCache = memoryCache;
        _distributedCache = distributedCache;
    }

    // GetAllExpenses
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<ExpenseResponse>>> GetAll()
    {
        var operation = new GetAllExpensesQuery();
        var result = await _mediator.Send(operation);
        return result;
    }

    // GetExpensesById
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ApiResponse<ExpenseResponse>> GetById(int id)
    {
        var operation = new GetExpenseByIdQuery(id);
        var result = await _mediator.Send(operation);
        return result;
    }
    
    // GetExpensesByRedis
    [HttpGet("ByRedis/{Category}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<ExpenseResponse>>> ByCategoryRedis(string Category)
    {
        var cacheResult = await _distributedCache.GetAsync(Category);
        
        if (cacheResult != null)
        {
            string json = Encoding.UTF8.GetString(cacheResult);
            var response = JsonConvert.DeserializeObject<List<ExpenseResponse>>(json);
            return new ApiResponse<List<ExpenseResponse>>(response);
        }
        
        var operation = new GetExpensesByParameterQuery(null, null, null, null, Category);
        var result = await _mediator.Send(operation);

        if (result.Response.Any())
        {
            string responseJson = JsonConvert.SerializeObject(result.Response);
            byte[] responseArr = Encoding.UTF8.GetBytes(responseJson);
            
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddDays(1),
                SlidingExpiration = TimeSpan.FromHours(1)
            };
            await _distributedCache.SetAsync(Category, responseArr, options);
        }
        return result;
    }
    
    // GetExpensesByMemoryCache
    [HttpGet("ByMemory/{Category}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<ExpenseResponse>>> ByCategoryMemory(string Category)
    {
        var cacheResult =
            _memoryCache.TryGetValue(Category, out ApiResponse<List<ExpenseResponse>> cacheData);
        
        if (cacheResult)
            return cacheData;

        var operation = new GetExpensesByParameterQuery(null,null,null,null, Category);
        var result = await _mediator.Send(operation);

        if (result.Response.Any())
        {
            var options = new MemoryCacheEntryOptions()
            {
                Priority = CacheItemPriority.High,
                AbsoluteExpiration = DateTime.Now.AddDays(1),
                SlidingExpiration = TimeSpan.FromHours(1)
            };
            _memoryCache.Set(Category, result, options);
        }
        return result;
    }
    
    // GetExpensesByParameter
    [HttpGet("parameter")]
    [Authorize] 
    public async Task<ApiResponse<List<ExpenseResponse>>> GetByParameter([FromQuery] int? userId, [FromQuery] ExpenseStatus? status, [FromQuery] decimal? amount, [FromQuery] DateTime? date, [FromQuery] string? category)
    {
        var query = new GetExpensesByParameterQuery(userId, status, amount, date, category);
        var result = await _mediator.Send(query);
        return result;
    }
    
    // GetActiveExpenseClaims
    [HttpGet("active")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<ExpenseResponse>>> GetActiveExpenseClaims()
    {
        var query = new GetActiveExpenseClaimsQuery();
        var result = await _mediator.Send(query);
        return result;
    }
    
    // GetPastExpenseClaims
    [HttpGet("past")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<ExpenseResponse>>> GetPastExpenseClaims()
    {
        var query = new GetPastExpenseClaimsQuery();
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpPost]
    [Authorize(Roles = "Employee")]
    public async Task<ApiResponse<ExpenseResponse>> Post([FromBody] ExpenseRequest Expense)
    {
        var operation = new CreateExpenseCommand(Expense);
        var result = await _mediator.Send(operation);
        return result;
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Personel")]
    public async Task<ApiResponse<ExpenseResponse>> Put(int id, [FromBody] ExpenseRequest Expense)
    {
        var operation = new UpdateExpenseCommand(id, Expense);
        var result = await _mediator.Send(operation);
        return result;
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteExpenseCommand(id);
        var result = await _mediator.Send(operation);
        return result;
    }
    
    // ApproveExpense
    [HttpPost("{id}/approve")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> ApproveExpense(int id)
    {
        var command = new ApproveExpenseCommand(id);
        var result = await _mediator.Send(command);
        return result;
    }

    // RejectExpense
    [HttpPost("{id}/reject")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> RejectExpense(int id, [FromBody] string reason)
    {
        var command = new RejectExpenseCommand(id, reason);
        var result = await _mediator.Send(command);
        return result;
    }
}