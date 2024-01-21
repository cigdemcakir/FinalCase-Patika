using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Data.Entity;

namespace ExpensePaymentSystem.Schema;

public class UserRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string? IBAN { get; set; }
}
public class UserResponse 
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public UserRole Role { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string? IBAN { get; set; }
    public DateTime LastActivityDate { get; set; }
    public bool IsActive { get; set; }
    public List<ExpenseResponse> Expenses { get; set; }
    public List<ReportResponse> Reports { get; set; }
    
} 