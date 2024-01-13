namespace ExpensePaymentSystem.Business.Models;

public class ExpenseReport
{
    public DateTime Date { get; set; }
    public decimal TotalAmount { get; set; }
    public string CategoryName { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public int NumberOfExpenses { get; set; }
}