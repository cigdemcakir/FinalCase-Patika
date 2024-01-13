using ExpensePaymentSystem.Data.Entity;

namespace ExpensePaymentSystem.Business.Interfaces;

public interface IExpenseCategoryService
{
    Task<IEnumerable<ExpenseCategory>> GetAllCategoriesAsync();
    Task<ExpenseCategory> GetCategoryByIdAsync(int categoryId);
    Task<ExpenseCategory> CreateCategoryAsync(ExpenseCategory category);
    Task<ExpenseCategory> UpdateCategoryAsync(ExpenseCategory category);
    Task<bool> DeleteCategoryAsync(int categoryId);
}
