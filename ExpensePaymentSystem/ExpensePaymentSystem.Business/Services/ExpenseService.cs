using ExpensePaymentSystem.Business.Interfaces;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Services;

public class ExpenseService : IExpenseService
{
    private readonly ExpensePaymentSystemDbContext _context;

    public ExpenseService(ExpensePaymentSystemDbContext context)
    {
        _context = context;
    }

    public async Task<Expense> CreateExpenseAsync(Expense expense)
    {
        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();
        return expense;
    }

    public async Task<Expense> GetExpenseByIdAsync(int expenseId)
    {
        return await _context.Expenses.FindAsync(expenseId);
    }

    public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
    {
        return await _context.Expenses.ToListAsync();
    }

    public async Task<Expense> UpdateExpenseAsync(int expenseId, Expense expense)
    {
        _context.Expenses.Update(expense);
        await _context.SaveChangesAsync();
        return expense;
    }

    public async Task<bool> DeleteExpenseAsync(int expenseId)
    {
        var expense = await _context.Expenses.FindAsync(expenseId);
        if (expense == null)
        {
            return false;
        }
        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
        return true;
    }
}
