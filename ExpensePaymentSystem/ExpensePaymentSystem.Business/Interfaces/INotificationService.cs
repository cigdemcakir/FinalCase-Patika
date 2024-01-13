namespace ExpensePaymentSystem.Business.Interfaces;

public interface INotificationService
{
    // Sends a notification about the approval status of an expense.
    Task SendExpenseApprovalNotificationAsync(int userId, bool isApproved);
}
