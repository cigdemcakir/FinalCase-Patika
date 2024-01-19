using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Queries.UserQueries.GetAllUsers;


public class GetAllUsersQuery : IRequest<ApiResponse<List<UserResponse>>>
{
}