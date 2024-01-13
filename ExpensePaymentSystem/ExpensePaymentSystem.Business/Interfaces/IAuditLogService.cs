namespace ExpensePaymentSystem.Business.Interfaces;

public interface IAuditLogService
{
    // Records an action or event in the system.
    Task RecordActionAsync(string action, int userId, string details);

}