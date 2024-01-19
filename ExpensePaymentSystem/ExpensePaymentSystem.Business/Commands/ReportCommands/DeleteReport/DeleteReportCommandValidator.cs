using ExpensePaymentSystem.Business.Commands.ReportCommands.CreateCommand;
using FluentValidation;

namespace ExpensePaymentSystem.Business.Commands.ReportCommands.DeleteReport;

public class DeleteReportCommandValidator: AbstractValidator<DeleteReportCommand>
{
    public DeleteReportCommandValidator()
    {
    }
}