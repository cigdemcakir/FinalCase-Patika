using ExpensePaymentSystem.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpensePaymentSystem.Data.Models;

public class AuditLog
{
    public int AuditLogId { get; set; } // Unique identifier for the audit log entry
    public int UserId { get; set; }     // ID of the user who performed the action
    public string Action { get; set; }  // The action that was performed
    public string Details { get; set; } // Additional details about the action
    public DateTime Timestamp { get; set; } // The time when the action was performed

    // Navigation property for the User (if you have a User entity in your system)
    public User User { get; set; }
}

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        // Primary key definition
        builder.HasKey(al => al.AuditLogId);

        // UserId is required
        builder.Property(al => al.UserId)
            .IsRequired();

        // Action is required and has a maximum length of 256 characters
        builder.Property(al => al.Action)
            .IsRequired()
            .HasMaxLength(256);

        // Details has a maximum length of 1000 characters
        builder.Property(al => al.Details)
            .HasMaxLength(1000);

        // Timestamp is required
        builder.Property(al => al.Timestamp)
            .IsRequired();

        // Establishes a relationship to the User entity (if exists)
        builder.HasOne(al => al.User)
                .WithMany(u => u.AuditLogs)
                .HasForeignKey(al => al.UserId);
    }
}
