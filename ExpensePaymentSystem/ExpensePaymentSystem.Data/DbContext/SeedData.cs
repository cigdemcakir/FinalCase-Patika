using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ExpensePaymentSystem.Data.DbContext;

public class SeedData
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    
    public SeedData(ExpensePaymentSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void Initialize(IServiceProvider serviceProvider)
    {
        if (_dbContext.Users.Any())
        {
            return;
        }

        // Admin 
        var adminUser = new User
        {
            UserName = "admin",
            Password = "admin123",
            Role = UserRole.Admin,
            FirstName = "Admin",
            LastName = "User",
            Email = "admin@example.com",
            PhoneNumber = "123456789",
            LastActivityDate = DateTime.Now,
            PasswordRetryCount = 0,
            IsActive = true
        };

        // Employee 
        var employeeUser = new User
        {
            UserName = "employee",
            Password = "employee123",
            Role = UserRole.Employee,
            FirstName = "Employee",
            LastName = "User",
            Email = "employee@example.com",
            PhoneNumber = "987654321",
            LastActivityDate = DateTime.Now,
            PasswordRetryCount = 0,
            IsActive = true,
            IBAN = "TR6680167979382566076960"
        };

        _dbContext.Users.AddRange(adminUser, employeeUser);
        _dbContext.SaveChanges();
    }
}
