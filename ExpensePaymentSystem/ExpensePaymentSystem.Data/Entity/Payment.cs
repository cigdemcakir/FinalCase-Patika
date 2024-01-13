using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpensePaymentSystem.Data.Entity;

public class Payment
{
    public int PaymentId { get; set; }
    public int ExpenseId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }

    public Expense Expense { get; set; }
}

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.PaymentId);

        builder.Property(p => p.Amount)
            .IsRequired()
            .HasColumnType("decimal(18, 2)");

        builder.Property(p => p.PaymentDate)
            .IsRequired()
            .HasColumnType("datetime");

        builder.HasOne(p => p.Expense)
            .WithOne(e => e.Payment)
            .HasForeignKey<Payment>(p => p.ExpenseId);
    }
}