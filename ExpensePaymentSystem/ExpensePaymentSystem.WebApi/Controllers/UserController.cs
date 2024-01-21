using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Commands.UserCommands.CreateUser;
using ExpensePaymentSystem.Business.Commands.UserCommands.DeleteUser;
using ExpensePaymentSystem.Business.Commands.UserCommands.UpdateUser;
using ExpensePaymentSystem.Business.Queries.UserQueries.GetAllUsers;
using ExpensePaymentSystem.Business.Queries.UserQueries.GetUserById;
using ExpensePaymentSystem.Business.Queries.UserQueries.GetUsersByParameter;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpensePaymentSystem.WebApi.Controllers;

/// <summary>
/// Controller for managing user-related operations.
/// </summary>
[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Constructor to initialize the UserController with IUserService.
    /// </summary>
    /// <param name="userService">The user service to handle user-related operations.</param>
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get a list of all users.
    /// </summary>
    /// <returns>A list of user objects.</returns>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<UserResponse>>> GetUsers()
    {
        var operation = new GetAllUsersQuery();
        var result = await _mediator.Send(operation);
        return result;
    }

    /// <summary>
    /// Get user by ID.
    /// </summary>
    /// <param name="userId">The ID of the user to retrieve.</param>
    /// <returns>The user object with the specified ID.</returns>
    [HttpGet("{userId}")]
    [Authorize]
    public async Task<ApiResponse<UserResponse>> GetUserById(int userId)
    {
        var operation = new GetUserByIdQuery(userId);
        var result = await _mediator.Send(operation);
        return result;
    }

    [HttpGet("parameter")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<UserResponse>>> GetByParameter([FromQuery] string? userName,
        [FromQuery] string? firstName, [FromQuery] string? lastName)
    {
        var query = new GetUsersByParameterQuery(userName, firstName, lastName);
        var result = await _mediator.Send(query);
        return result;
    }


    /// <summary>
    /// Create a new user.
    /// </summary>
    /// <param name="user">The user object to create.</param>
    /// <returns>The created user object.</returns>
    [HttpPost]
    //[Authorize(Roles = "Admin")]
    public async Task<ApiResponse<UserResponse>> CreateUser([FromBody] UserRequest User)
    {
        var operation = new CreateUserCommand(User);
        var result = await _mediator.Send(operation);
        return result;
    }

    /// <summary>
    /// Update an existing user.
    /// </summary>
    /// <param name="userId">The ID of the user to update.</param>
    /// <param name="user">The updated user object.</param>
    /// <returns>The updated user object.</returns>
    [HttpPut("{userId}")]
    [Authorize]
    public async Task<ApiResponse<UserResponse>> UpdateUser(int userId, [FromBody] UserRequest User)
    {
        var operation = new UpdateUserCommand(userId, User);
        var result = await _mediator.Send(operation);
        return result;
    }

    /// <summary>
    /// Delete a user by ID.
    /// </summary>
    /// <param name="userId">The ID of the user to delete.</param>
    /// <returns>No content if successful, or not found if the user doesn't exist.</returns>
    [HttpDelete("{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> DeleteUser(int userId)
    {
        var operation = new DeleteUserCommand(userId);
        var result = await _mediator.Send(operation);
        return result;
    }
}