using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Queries.ReportQueries.GetReportsByParameter;

public class GetReportsByParameterQuery : IRequest<ApiResponse<List<ReportResponse>>>
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    

    public GetReportsByParameterQuery(DateTime? startDate = null, DateTime? endDate = null)
    {
        StartDate = startDate;
        EndDate = endDate;
    }
}