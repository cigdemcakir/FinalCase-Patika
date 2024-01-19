using FluentValidation;

namespace ExpensePaymentSystem.Business.Commands.ReportCommands.CreateCommand;

public class CreateReportCommandValidator: AbstractValidator<CreateReportCommand>
{
    public CreateReportCommandValidator()
    {
    }
}