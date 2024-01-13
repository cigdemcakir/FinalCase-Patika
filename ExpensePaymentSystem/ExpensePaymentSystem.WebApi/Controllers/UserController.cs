using ExpensePaymentSystem.Business.Interfaces;
using ExpensePaymentSystem.Data.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ExpensePaymentSystem.WebApi.Controllers;

     /// <summary>
    /// Controller for managing user-related operations.
    /// </summary>
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor to initialize the UserController with IUserService.
        /// </summary>
        /// <param name="userService">The user service to handle user-related operations.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get a list of all users.
        /// </summary>
        /// <returns>A list of user objects.</returns>
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Get user by ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The user object with the specified ID.</returns>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="user">The user object to create.</param>
        /// <returns>The created user object.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            var createdUser = await _userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { userId = createdUser.UserId }, createdUser);
        }

        /// <summary>
        /// Update an existing user.
        /// </summary>
        /// <param name="userId">The ID of the user to update.</param>
        /// <param name="user">The updated user object.</param>
        /// <returns>The updated user object.</returns>
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, User user)
        {
            var updatedUser = await _userService.UpdateUserAsync(userId, user);
            if (updatedUser == null)
            {
                return NotFound();
            }

            return Ok(updatedUser);
        }

        /// <summary>
        /// Delete a user by ID.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        /// <returns>No content if successful, or not found if the user doesn't exist.</returns>
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var result = await _userService.DeleteUserAsync(userId);
            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }