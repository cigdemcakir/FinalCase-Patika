using ExpensePaymentSystem.Business.Interfaces;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Services;

public class UserService : IUserService
{
    private readonly ExpensePaymentSystemDbContext _context;

    public UserService(ExpensePaymentSystemDbContext context)
    {
        _context = context;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> GetUserByIdAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> UpdateUserAsync(int userId, User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return false;
        }
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}
