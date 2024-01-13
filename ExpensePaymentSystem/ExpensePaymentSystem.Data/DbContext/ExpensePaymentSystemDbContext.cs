using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Data.DbContext;

public class ExpensePaymentSystemDbContext:Microsoft.EntityFrameworkCore.DbContext
{
    public ExpensePaymentSystemDbContext(DbContextOptions<ExpensePaymentSystemDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        modelBuilder.ApplyConfiguration(new AuditLogConfiguration());
        base.OnModelCreating(modelBuilder);
    }

}