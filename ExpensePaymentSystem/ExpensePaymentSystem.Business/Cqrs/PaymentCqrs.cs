using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Cqrs;

// public record CreatePaymentCommand(PaymentRequest Model) : IRequest<ApiResponse<PaymentResponse>>;
// public record UpdatePaymentCommand(int Id,PaymentRequest Model) : IRequest<ApiResponse>;
// public record DeletePaymentCommand(int Id) : IRequest<ApiResponse>;

public record GetAllPaymentQuery() : IRequest<ApiResponse<List<PaymentResponse>>>;
public record GetPaymentByIdQuery(int Id) : IRequest<ApiResponse<PaymentResponse>>;
public record GetPaymentByParameterQuery(string FirstName,string LastName,string IdentityNumber) : IRequest<ApiResponse<List<PaymentResponse>>>;