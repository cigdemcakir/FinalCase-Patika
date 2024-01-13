using ExpensePaymentSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpensePaymentSystem.Data.Entity;

public class User
{
    public int UserId { get; set; }
    
    public string Username { get; set; }
    
    public string Password { get; set; } // Şifreleme ve güvenlik önlemleri dikkate alınmalıdır
    
    public string Role { get; set; } // "Admin" veya "Personel"
    
    public ICollection<Expense> Expenses { get; set; }
    
    public ICollection<AuditLog> AuditLogs { get; set; }

}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.UserId);

        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(u => u.Role)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasMany(u => u.Expenses)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId);
        
        builder.HasMany(u => u.AuditLogs)
            .WithOne()
            .HasForeignKey("UserId");
    }
}