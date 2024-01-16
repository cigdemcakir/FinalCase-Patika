using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Cqrs;

public record CreateExpenseCommand(ExpenseRequest Model) : IRequest<ApiResponse<ExpenseResponse>>;
public record UpdateExpenseCommand(int Id,ExpenseRequest Model) : IRequest<ApiResponse>;
public record DeleteExpenseCommand(int Id) : IRequest<ApiResponse>;

public record GetAllExpenseQuery() : IRequest<ApiResponse<List<ExpenseResponse>>>;
public record GetExpenseByIdQuery(int Id) : IRequest<ApiResponse<ExpenseResponse>>;
public record GetExpenseByParameterQuery(string FirstName,string LastName,string IdentityNumber) : IRequest<ApiResponse<List<ExpenseResponse>>>;