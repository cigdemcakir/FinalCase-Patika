using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Commands.ReportCommands.DeleteReport;

public class DeleteReportCommand: IRequest<ApiResponse>
{
    public int Id { get; }

    public DeleteReportCommand(int id)
    {
        Id = id;
    }
}