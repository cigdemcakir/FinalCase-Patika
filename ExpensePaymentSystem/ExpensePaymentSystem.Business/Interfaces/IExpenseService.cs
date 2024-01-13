using ExpensePaymentSystem.Data.Entity;

namespace ExpensePaymentSystem.Business.Interfaces;

public interface IExpenseService
{
    Task<Expense> CreateExpenseAsync(Expense expense);
    Task<Expense> GetExpenseByIdAsync(int expenseId);
    Task<IEnumerable<Expense>> GetAllExpensesAsync();
    Task<Expense> UpdateExpenseAsync(int expenseId, Expense expense);
    Task<bool> DeleteExpenseAsync(int expenseId);
}
