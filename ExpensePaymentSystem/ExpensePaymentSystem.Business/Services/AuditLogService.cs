using ExpensePaymentSystem.Business.Interfaces;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Models;

namespace ExpensePaymentSystem.Business.Services;

public class AuditLogService : IAuditLogService
{
    private readonly ExpensePaymentSystemDbContext _context;

    public AuditLogService(ExpensePaymentSystemDbContext context)
    {
        _context = context;
    }

    // Records an action or event in the system.
    public async Task RecordActionAsync(string action, int userId, string details)
    {
        // Create a new audit log entry
        var auditLog = new AuditLog
        {
            UserId = userId,
            Action = action,
            Details = details,
            Timestamp = DateTime.UtcNow
        };

        // Add the audit log entry to the database
        _context.AuditLogs.Add(auditLog);
        await _context.SaveChangesAsync();
    }

    // Implementation of other audit logging methods...
}
