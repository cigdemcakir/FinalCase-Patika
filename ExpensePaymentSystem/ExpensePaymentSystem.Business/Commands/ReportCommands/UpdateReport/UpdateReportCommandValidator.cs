using ExpensePaymentSystem.Business.Commands.ReportCommands.CreateCommand;
using ExpensePaymentSystem.Business.Validators;
using FluentValidation;

namespace ExpensePaymentSystem.Business.Commands.ReportCommands.UpdateReport;

public class UpdateReportCommandValidator: AbstractValidator<UpdateReportCommand>
{
    public UpdateReportCommandValidator()
    {
        RuleFor(command => command.Id)
            .GreaterThan(0).WithMessage("Id must be a positive number.");

        RuleFor(command => command.Model)
            .SetValidator(new ReportRequestValidator());
    }
}