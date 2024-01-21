using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Data.Entity;

namespace ExpensePaymentSystem.Schema;

public class ExpenseRequest
{
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public ExpenseStatus Status { get; set; }
    public string Category { get; set; }
}
public class ExpenseResponse
{
    public int ExpenseId { get; set; }
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public ExpenseStatus Status { get; set; }
    public string? RejectionReason { get; set; }
}