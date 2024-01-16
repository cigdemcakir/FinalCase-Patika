namespace ExpensePaymentSystem.Schema;

public class ReportRequest
{
    public int UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
public class ReportResponse
{
    public int ReportId { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal TotalPayments { get; set; }
}