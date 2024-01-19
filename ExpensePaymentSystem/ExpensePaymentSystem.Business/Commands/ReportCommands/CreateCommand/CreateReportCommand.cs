using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Commands.ReportCommands.CreateCommand;

public class CreateReportCommand: IRequest<ApiResponse<ReportResponse>>
{
    public ReportRequest Model { get; set; }

    public CreateReportCommand(ReportRequest model)
    {
        Model = model;
    }
}