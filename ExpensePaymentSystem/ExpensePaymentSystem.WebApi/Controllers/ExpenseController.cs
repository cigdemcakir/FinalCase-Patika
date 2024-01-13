using ExpensePaymentSystem.Business.Interfaces;
using ExpensePaymentSystem.Data.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ExpensePaymentSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    /// Get all expenses for a specific user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>A list of expenses for the user.</returns>
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<Expense>>> GetExpensesByUser(int userId)
    {
        // Retrieve expenses for the specified user.
        var expenses = await _expenseService.GetExpenseByIdAsync(userId);
        return Ok(expenses);
    }

    /// <summary>
    /// Create a new expense record.
    /// </summary>
    /// <param name="expense">The expense details to create.</param>
    /// <returns>The created expense record.</returns>
    [HttpPost]
    public async Task<ActionResult<Expense>> CreateExpense(Expense expense)
    {
        // Create a new expense record.
        var createdExpense = await _expenseService.CreateExpenseAsync(expense);
        return CreatedAtAction(nameof(GetExpenseById), new { id = createdExpense.ExpenseId }, createdExpense);
    }

    /// <summary>
    /// Get details of a specific expense by its ID.
    /// </summary>
    /// <param name="id">The ID of the expense.</param>
    /// <returns>The details of the expense.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Expense>> GetExpenseById(int id)
    {
        // Retrieve expense details by ID.
        var expense = await _expenseService.GetExpenseByIdAsync(id);
        if (expense == null)
        {
            return NotFound();
        }
        return Ok(expense);
    }

    /// <summary>
    /// Update an existing expense record.
    /// </summary>
    /// <param name="id">The ID of the expense to update.</param>
    /// <param name="expense">The updated expense details.</param>
    /// <returns>The updated expense record.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpense(int id, Expense expense)
    {
        // Check if the provided ID matches the expense.
        if (id != expense.ExpenseId)
        {
            return BadRequest();
        }

        // Update the expense record.
        try
        {
            await _expenseService.UpdateExpenseAsync(id, expense);
        }
        catch (Exception)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Delete an existing expense record.
    /// </summary>
    /// <param name="id">The ID of the expense to delete.</param>
    /// <returns>No content if successful, NotFound if the expense is not found.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(int id)
    {
        // Delete the expense record.
        var result = await _expenseService.DeleteExpenseAsync(id);
        if (result)
        {
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }
}