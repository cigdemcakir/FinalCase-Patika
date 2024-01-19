using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Commands.PaymentCommands.CreatePayment;

public class CreatePaymentCommand : IRequest<ApiResponse<PaymentResponse>>
{
    public PaymentRequest Model { get; set; }

    public CreatePaymentCommand(PaymentRequest model)
    {
        Model = model;
    }
}