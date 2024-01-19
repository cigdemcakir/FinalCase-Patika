using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Commands.ReportCommands.UpdateReport;

public class UpdateReportCommand: IRequest<ApiResponse<ReportResponse>>
{
    public int Id { get; }
    public ReportRequest Model { get; set; }

    public UpdateReportCommand(int id, ReportRequest model)
    {
        Id = id;
        Model = model;
    }
}