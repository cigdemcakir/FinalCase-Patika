using ExpensePaymentSystem.Business.Commands.ReportCommands.CreateCommand;
using FluentValidation;

namespace ExpensePaymentSystem.Business.Commands.ReportCommands.UpdateReport;

public class UpdateReportCommandValidator: AbstractValidator<UpdateReportCommand>
{
    public UpdateReportCommandValidator()
    {
    }
}