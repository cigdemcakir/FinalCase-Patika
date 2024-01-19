using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Queries.ReportQueries.GetAllReports;

public class GetAllReportsQuery : IRequest<ApiResponse<List<ReportResponse>>>
{
}