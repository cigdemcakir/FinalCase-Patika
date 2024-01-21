using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;

namespace ExpensePaymentSystem.Business.Interfaces;

public interface IExcelService
{
    byte[] CreateExcelReport(List<ReportResponse> reportResponses);
}