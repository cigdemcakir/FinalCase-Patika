using ExpensePaymentSystem.Base.Enums;

namespace ExpensePaymentSystem.Schema;

public class ExpenseRequest
{
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
}
public class ExpenseResponse
{
    public int ExpenseId { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string CategoryName { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public ExpenseStatus Status { get; set; }
    public string? RejectionReason { get; set; }
}