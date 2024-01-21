using ExpensePaymentSystem.Base.Enums;

namespace ExpensePaymentSystem.Schema;

public class PaymentRequest
{
    public int ExpenseId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}
public class PaymentResponse
{
    public int PaymentId { get; set; }
    public int ExpenseId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}