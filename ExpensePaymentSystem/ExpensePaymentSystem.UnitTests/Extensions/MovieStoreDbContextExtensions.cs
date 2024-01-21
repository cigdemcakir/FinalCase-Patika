using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;

namespace ExpensePaymentSystem.UnitTests.Extensions
{
    public static class ExpensePaymentSystemDbContextExtensions
    {
        public static void Initialize(this ExpensePaymentSystemDbContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(GetUsers());
                context.SaveChanges();
            }

            if (!context.Expenses.Any())
            {
                context.Expenses.AddRange(GetExpenses(context));
                context.SaveChanges();
            }

            if (!context.Payments.Any())
            {
                context.Payments.AddRange(GetPayments(context));
                context.SaveChanges();
            }

            if (!context.Reports.Any())
            {
                context.Reports.AddRange(GetReports(context));
                context.SaveChanges();
            }
        }

        private static List<User> GetUsers()
        {
            return new List<User>
            {
                new User { UserName = "admin1", Password = "password1", Role = UserRole.Admin, FirstName = "Alice", LastName = "Johnson", Email = "alice.johnson@example.com", PhoneNumber = "1234567890", IBAN = null, LastActivityDate = DateTime.Now, PasswordRetryCount = 0, IsActive = true },
                new User { UserName = "employee1", Password = "password2", Role = UserRole.Employee, FirstName = "Bob", LastName = "Smith", Email = "bob.smith@example.com", PhoneNumber = "0987654321", IBAN = "TR080006238211588153826233", LastActivityDate = DateTime.Now, PasswordRetryCount = 0, IsActive = true },
                new User { UserName = "admin2", Password = "password3", Role = UserRole.Admin, FirstName = "Carol", LastName = "Williams", Email = "carol.williams@example.com", PhoneNumber = "1231231234", IBAN = null, LastActivityDate = DateTime.Now, PasswordRetryCount = 0, IsActive = true },
                new User { UserName = "employee2", Password = "password4", Role = UserRole.Employee, FirstName = "David", LastName = "Brown", Email = "david.brown@example.com", PhoneNumber = "9876543210", IBAN = "TR980006267695751837738529", LastActivityDate = DateTime.Now, PasswordRetryCount = 0, IsActive = true }
            };
        }

        private static List<Expense> GetExpenses(ExpensePaymentSystemDbContext context)
        {
            return new List<Expense>
            {
                new Expense { UserId = 1, Amount = 200, Date = DateTime.Now.AddDays(-10), Description = "Laptop purchase", Category = "Equipment", Status = ExpenseStatus.Approved },
                new Expense { UserId = 1, Amount = 500, Date = DateTime.Now.AddDays(-7), Description = "Taxi fare", Category = "Transport", Status = ExpenseStatus.Pending },
                new Expense { UserId = 2, Amount = 300, Date = DateTime.Now.AddDays(-5), Description = "Office supplies", Category = "Office", Status = ExpenseStatus.Approved },
                new Expense { UserId = 2, Amount = 100, Date = DateTime.Now.AddDays(-3), Description = "Business dinner", Category = "Meals", Status = ExpenseStatus.Rejected, RejectionReason = "Exceeds daily limit" }
            };
        }

        private static List<Payment> GetPayments(ExpensePaymentSystemDbContext context)
        {
            return new List<Payment>
            {
                new Payment { ExpenseId = 1, Amount = 200, PaymentDate = DateTime.Now.AddDays(-9), PaymentMethod = PaymentMethod.BankTransfer },
                new Payment { ExpenseId = 2, Amount = 500, PaymentDate = DateTime.Now.AddDays(-6), PaymentMethod = PaymentMethod.Cash },
                new Payment { ExpenseId = 3, Amount = 300, PaymentDate = DateTime.Now.AddDays(-4), PaymentMethod = PaymentMethod.CreditCard },
                new Payment { ExpenseId = 4, Amount = 100, PaymentDate = DateTime.Now.AddDays(-2), PaymentMethod = PaymentMethod.BankTransfer }
            };
        }

        private static List<Report> GetReports(ExpensePaymentSystemDbContext context)
        {
            return new List<Report>
            {
                new Report { UserId = 1, StartDate = new DateTime(2024, 1, 1), EndDate = new DateTime(2024, 1, 31), TotalPayment = 250.00m, TotalExpense = 300.00m },
                new Report { UserId = 2, StartDate = new DateTime(2024, 2, 1), EndDate = new DateTime(2024, 2, 28), TotalPayment = 180.00m, TotalExpense = 230.00m },
                new Report { UserId = 1, StartDate = new DateTime(2024, 3, 1), EndDate = new DateTime(2024, 3, 31), TotalPayment = 100.00m, TotalExpense = 150.00m },
                new Report { UserId = 2, StartDate = new DateTime(2024, 4, 1), EndDate = new DateTime(2024, 4, 30), TotalPayment = 300.00m, TotalExpense = 350.00m }
            };
        }
    }
}
