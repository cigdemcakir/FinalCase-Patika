using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Schema;
using MediatR;

namespace ExpensePaymentSystem.Business.Queries.UserQueries.GetUsersByParameter;


public class GetUsersByParameterQuery : IRequest<ApiResponse<List<UserResponse>>>
{
    public string UserName { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }

    public GetUsersByParameterQuery(string? userName = null, string? firstName = null, string? lastName = null)
    {
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
    }
}