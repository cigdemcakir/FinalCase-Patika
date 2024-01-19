using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Commands.PaymentCommands.UpdatePayment;

public class UpdatePaymentCommand : IRequest<ApiResponse<PaymentResponse>>
{
    public int Id { get; }
    public PaymentRequest Model { get; }

    public UpdatePaymentCommand(int id, PaymentRequest model)
    {
        Id = id;
        Model = model;
    }
}