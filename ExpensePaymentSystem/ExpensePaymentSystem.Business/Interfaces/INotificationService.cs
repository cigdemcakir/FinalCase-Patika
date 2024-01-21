namespace ExpensePaymentSystem.Business.Interfaces;

public interface INotificationService
{
    Task SendExpensePaymentNotificationAsync(int userId);
}