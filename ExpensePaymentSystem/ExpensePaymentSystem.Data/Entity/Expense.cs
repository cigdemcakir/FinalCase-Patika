using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpensePaymentSystem.Data.Entity;

public class Expense
{
    public int ExpenseId { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public string Status { get; set; } // Örneğin: "Approved", "Rejected", "Pending"
    public string RejectionReason { get; set; } // Reddedilme nedeni

    public User User { get; set; }
    public ExpenseCategory Category { get; set; }
    
    // Payment ile ilişkili property'ler
    public int? PaymentId { get; set; } // Nullable, çünkü her masrafın bir ödemesi olmayabilir
    public Payment Payment { get; set; }
}

public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.HasKey(e => e.ExpenseId);

        builder.Property(e => e.Amount)
            .IsRequired()
            .HasColumnType("decimal(18, 2)");

        builder.Property(e => e.Date)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(e => e.Description)
            .HasMaxLength(500);

        builder.Property(e => e.Status)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(e => e.RejectionReason)
            .HasMaxLength(500);

        builder.HasOne(e => e.User)
            .WithMany(u => u.Expenses)
            .HasForeignKey(e => e.UserId);

        builder.HasOne(e => e.Category)
            .WithMany(c => c.Expenses)
            .HasForeignKey(e => e.CategoryId);
    }
}