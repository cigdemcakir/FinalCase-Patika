using ExpensePaymentSystem.Base.Response;
using MediatR;

namespace ExpensePaymentSystem.Business.Commands.PaymentCommands.DeletePayment;

public class DeletePaymentCommand : IRequest<ApiResponse>
{
    public int Id { get; }

    public DeletePaymentCommand(int id)
    {
        Id = id;
    }
}