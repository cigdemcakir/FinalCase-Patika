using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpensePaymentSystem.Data.Entity;

[Table("Report", Schema = "dbo")]
public class Report
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ReportId { get; set; }  
    public int? UserId { get; set; }     // ID of the user who performed the action
    public DateTime StartDate { get; set; } // Start date of the report period
    public DateTime EndDate { get; set; } // End date of the report period
    public decimal TotalPayment { get; set; } // Total payment of the report period
    public decimal TotalExpense { get; set; } // Total expense of the report period
    
    public virtual User User { get; set; }
    public ICollection<Expense> Expenses { get; set; } // Expenses included in the report

}

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.Property(r => r.UserId).IsRequired(false);
        builder.Property(r => r.StartDate).IsRequired();
        builder.Property(r => r.EndDate).IsRequired();
        builder.Property(r => r.TotalPayment).IsRequired()
            .HasColumnType("decimal(18, 2)");
        
        builder.Property(r => r.TotalExpense).IsRequired()
            .HasColumnType("decimal(18, 2)");

        // Primary key 
        builder.HasKey(al => al.ReportId);
        
        builder.HasOne(r => r.User)
            .WithMany(u => u.Reports)
            .HasForeignKey(r => r.UserId);
        
        builder.HasMany(r => r.Expenses)
            .WithOne(e => e.Report)
            .HasForeignKey(e => e.ReportId);
    }
}
