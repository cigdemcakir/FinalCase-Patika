using ExpensePaymentSystem.Data.Entity;

namespace ExpensePaymentSystem.Business.Interfaces;

public interface IUserService
{
    Task<User> CreateUserAsync(User user);
    Task<User> GetUserByIdAsync(int userId);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> UpdateUserAsync(int userId, User user);
    Task<bool> DeleteUserAsync(int userId);
}