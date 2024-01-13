using ExpensePaymentSystem.Business.Models;

namespace ExpensePaymentSystem.Business.Interfaces;

public interface IReportService
{
    // Retrieves a detailed expense report for a specific user over a given date range.
    Task<IEnumerable<ExpenseReport>> GetExpenseReportByUserAsync(int userId, DateTime startDate, DateTime endDate);

    // Retrieves a detailed expense report by category over a given date range.
    Task<IEnumerable<ExpenseReport>> GetExpenseReportByCategoryAsync(DateTime startDate, DateTime endDate);

    // Retrieves a general expense report for a specified period. This can provide an overview for all users and categories.
    Task<IEnumerable<ExpenseReport>> GetGeneralExpenseReportAsync(DateTime startDate, DateTime endDate);
}
