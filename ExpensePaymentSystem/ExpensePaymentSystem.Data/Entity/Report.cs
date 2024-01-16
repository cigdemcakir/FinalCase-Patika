using System.ComponentModel.DataAnnotations.Schema;
using ExpensePaymentSystem.Base.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpensePaymentSystem.Data.Entity;

[Table("Report", Schema = "dbo")]
public class Report
{
    public int ReportId { get; set; }  
    public int UserId { get; set; }     // ID of the user who performed the action
    public DateTime StartDate { get; set; } // Start date of the report period
    public DateTime EndDate { get; set; } // End date of the report period
    
    // Navigation property for the User (if you have a User entity in your system)
    public virtual User User { get; set; }
    public ICollection<Expense> Expenses { get; set; } // Expenses included in the report

}

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.Property(r => r.StartDate).IsRequired();
        builder.Property(r => r.EndDate).IsRequired();

        // Primary key definition
        builder.HasKey(al => al.ReportId);


        // Establishes a relationship to the Expense entity (if exists)
        builder.HasOne(r => r.User)
            .WithMany(u => u.Reports)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.NoAction);


        // Expenses ile ilişki - Bir rapor birden fazla harcamayı içerebilir
        builder.HasMany(r => r.Expenses)
            .WithOne(e => e.Report)
            .HasForeignKey(e => e.ReportId);

    }
}
