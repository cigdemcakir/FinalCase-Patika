using ExpensePaymentSystem.Business.Interfaces;
using ExpensePaymentSystem.Data.DbContext;

namespace ExpensePaymentSystem.Business.Services;

public class NotificationService : INotificationService
{
    // Dependencies for sending notifications (e.g., email service, SMS service)
    private readonly ExpensePaymentSystemDbContext _context;

    
    public NotificationService(ExpensePaymentSystemDbContext context/* Dependencies here */)
    {
        _context = context;
        // Initialize dependencies
    }

    // Sends a notification to the user about the approval status of their expense.
    public async Task SendExpenseApprovalNotificationAsync(int userId, bool isApproved)
    {
        // Logic to send notification
        // This is a simplified example, and you should replace it with actual notification sending logic.
        var user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            var message = isApproved ? "Your expense has been approved." : "Your expense has been rejected.";
            // Send the notification (e.g., email, SMS)
        }
    }
}
