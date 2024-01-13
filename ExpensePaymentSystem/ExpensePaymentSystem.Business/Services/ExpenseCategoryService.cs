using ExpensePaymentSystem.Business.Interfaces;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Services;

public class ExpenseCategoryService : IExpenseCategoryService
{
    private readonly ExpensePaymentSystemDbContext _context;

    public ExpenseCategoryService(ExpensePaymentSystemDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ExpenseCategory>> GetAllCategoriesAsync()
    {
        return await _context.ExpenseCategories.ToListAsync();
    }

    public async Task<ExpenseCategory> GetCategoryByIdAsync(int categoryId)
    {
        return await _context.ExpenseCategories.FindAsync(categoryId);
    }

    public async Task<ExpenseCategory> CreateCategoryAsync(ExpenseCategory category)
    {
        _context.ExpenseCategories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<ExpenseCategory> UpdateCategoryAsync(ExpenseCategory category)
    {
        _context.ExpenseCategories.Update(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<bool> DeleteCategoryAsync(int categoryId)
    {
        var category = await _context.ExpenseCategories.FindAsync(categoryId);
        if (category == null)
        {
            return false;
        }
        _context.ExpenseCategories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }
}
