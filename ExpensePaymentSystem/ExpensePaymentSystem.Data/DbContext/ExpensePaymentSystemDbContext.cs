using ExpensePaymentSystem.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Data.DbContext;

public class ExpensePaymentSystemDbContext:Microsoft.EntityFrameworkCore.DbContext
{
    public ExpensePaymentSystemDbContext(DbContextOptions<ExpensePaymentSystemDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Payment> Payments { get; set; }
    
    public DbSet<Report> Reports { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        modelBuilder.ApplyConfiguration(new ReportConfiguration());
        base.OnModelCreating(modelBuilder);
    }

}