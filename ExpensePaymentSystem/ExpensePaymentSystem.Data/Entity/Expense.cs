using System.ComponentModel.DataAnnotations.Schema;
using ExpensePaymentSystem.Base.Entity;
using ExpensePaymentSystem.Base.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpensePaymentSystem.Data.Entity;

[Table("Expense", Schema = "dbo")]
public class Expense 
{
    public int ExpenseId { get; set; }
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public ExpenseStatus Status { get; set; } 
    public string? RejectionReason { get; set; } // Reddedilme nedeni
    public virtual User User { get; set; }
    public int? PaymentId { get; set; } // Nullable, çünkü her masrafın bir ödemesi olmayabilir
    public virtual Payment Payment { get; set; }
    public int? ReportId { get; set; }
    public virtual Report Report { get; set; }
    
}

public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.HasKey(e => e.ExpenseId);

        builder.Property(e => e.Amount).IsRequired().HasColumnType("decimal(18, 2)");
        builder.Property(e => e.RejectionReason).HasMaxLength(100);
        builder.Property(e => e.Category).HasMaxLength(50);

        builder.Property(e => e.Date)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.Property(e => e.Description)
            .HasMaxLength(100);

        builder.Property(e => e.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.HasOne(e => e.User)
            .WithMany(u => u.Expenses)
            .HasForeignKey(e => e.UserId);

        builder.HasOne(e => e.Payment)
            .WithOne(p => p.Expense)
            .HasForeignKey<Expense>(e => e.PaymentId);

        builder.HasOne(e => e.Report)
            .WithMany(r => r.Expenses)
            .HasForeignKey(e => e.ReportId)
            .IsRequired(false);
    }
}


