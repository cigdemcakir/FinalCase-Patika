using ClosedXML.Excel;
using ExpensePaymentSystem.Business.Interfaces;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;

namespace ExpensePaymentSystem.Business.Services;

public class ExcelService : IExcelService
{

    public byte[] CreateExcelReport(List<ReportResponse> reportResponses)
    {
        using (var workbook = new XLWorkbook())
        {
            var summaryWorksheet = workbook.Worksheets.Add("Summary");

            var headers = new[]
                { "ReportId", "UserId", "StartDate", "EndDate", "TotalExpense", "TotalPayment", "Expenses Link" };
            for (int i = 0; i < headers.Length; i++)
            {
                var cell = summaryWorksheet.Cell(1, i + 1);
                cell.Value = headers[i];
                cell.Style.Fill.BackgroundColor = i % 2 == 0 ? XLColor.White : XLColor.LightBlue;
            }

            int summaryRow = 2;
            int reportCount = 1;
            
            foreach (var report in reportResponses)
            {
                summaryWorksheet.Cell(summaryRow, 1).Value = report.ReportId;
                summaryWorksheet.Cell(summaryRow, 2).Value = report.UserId;
                summaryWorksheet.Cell(summaryRow, 3).Value = report.StartDate;
                summaryWorksheet.Cell(summaryRow, 4).Value = report.EndDate;
                summaryWorksheet.Cell(summaryRow, 5).Value = report.TotalExpense;
                summaryWorksheet.Cell(summaryRow, 6).Value = report.TotalPayment;
                
                if (report.TotalExpense != 0)
                {
                    var expenseWorksheet = workbook.Worksheets.Add($"Expenses_{reportCount}");
                    PopulateExpenseWorksheet(expenseWorksheet, report.Expenses);

                    summaryWorksheet.Cell(summaryRow, 7).Value = "View Expenses";
                    summaryWorksheet.Cell(summaryRow, 7).SetHyperlink(new XLHyperlink($"Expenses_{reportCount}!A1"));
                }
                else
                {
                    summaryWorksheet.Cell(summaryRow, 7).Value = ""; 
                }

                summaryRow++;
                reportCount++;

                summaryWorksheet.Columns().AdjustToContents();
            }
            
            
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                return stream.ToArray();
            }
        }
    }

    private void PopulateExpenseWorksheet(IXLWorksheet worksheet, List<ExpenseResponse> expenses)
    {
        worksheet.Cell("A1").Value = "ExpenseId";
        worksheet.Cell("B1").Value = "User Id";
        worksheet.Cell("C1").Value = "Amount";
        worksheet.Cell("D1").Value = "Date";
        worksheet.Cell("E1").Value = "Description";
        worksheet.Cell("F1").Value = "Category";
        worksheet.Cell("G1").Value = "Status";

        int row = 2;
        foreach (var expense in expenses)
        {
            worksheet.Cell(row, 1).Value = expense.ExpenseId;
            worksheet.Cell(row, 2).Value = expense.UserId;
            worksheet.Cell(row, 3).Value = expense.Amount;
            worksheet.Cell(row, 4).Value = expense.Date;
            worksheet.Cell(row, 5).Value = expense.Description;
            worksheet.Cell(row, 6).Value = expense.Category;
            worksheet.Cell(row, 7).Value = expense.Status.ToString();

            row++;

            worksheet.Columns().AdjustToContents();
        }
    }

}