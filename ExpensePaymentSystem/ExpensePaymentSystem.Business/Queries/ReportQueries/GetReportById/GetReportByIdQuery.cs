using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Queries.ReportQueries.GetReportById;

public class GetReportByIdQuery : IRequest<ApiResponse<ReportResponse>>
{
    public int Id { get; }

    public GetReportByIdQuery(int id)
    {
        Id = id;
    }
}