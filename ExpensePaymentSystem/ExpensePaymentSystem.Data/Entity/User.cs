using System.ComponentModel.DataAnnotations.Schema;
using ExpensePaymentSystem.Base.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpensePaymentSystem.Data.Entity;

[Table("User", Schema = "dbo")]
public class User 
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; } // "Admin", "Employee"
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string? IBAN { get; set; } // Nullable IBAN, only required for employees
    public DateTime LastActivityDate { get; set; }
    public int PasswordRetryCount { get; set; }
    public bool IsActive { get; set; }
    
    public ICollection<Expense> Expenses { get; set; }
    public ICollection<Report> Reports { get; set; }

}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.FirstName).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.LastName).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.UserName).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.Password).IsRequired(true).HasMaxLength(250);
        builder.Property(x => x.Email).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.PhoneNumber).IsRequired(true).HasMaxLength(20);
        builder.Property(x => x.IBAN).HasMaxLength(34);
        builder.Property(x => x.Role).IsRequired(true).HasConversion<string>(); // Enum will be stored as string
        builder.Property(x => x.LastActivityDate).IsRequired(true);
        builder.Property(x => x.PasswordRetryCount).IsRequired(true).HasDefaultValue(0);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.HasKey(u => u.UserId); 
        
        builder.HasMany(u => u.Expenses)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId);
        
        builder.HasMany(u => u.Reports)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId);
    }
}


