using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Cqrs;

public record CreateReportCommand(ReportRequest Model) : IRequest<ApiResponse<ReportResponse>>;
public record UpdateReportCommand(int Id,ReportRequest Model) : IRequest<ApiResponse>;
public record DeleteReportCommand(int Id) : IRequest<ApiResponse>;

public record GetAllReportQuery() : IRequest<ApiResponse<List<ReportResponse>>>;
public record GetReportByIdQuery(int Id) : IRequest<ApiResponse<ReportResponse>>;
public record GetReportByParameterQuery(string FirstName,string LastName,string IdentityNumber) : IRequest<ApiResponse<List<ReportResponse>>>;