using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExpensePaymentSystem.Business.Interfaces;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ExpensePaymentSystem.Business.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly ExpensePaymentSystemDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthorizationService(ExpensePaymentSystemDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    // Authenticates a user and generates a JWT token.
    public async Task<string> AuthenticateAsync(string username, string password)
    {
        // Validate the user in the database
        var user = await _context.Users
                                 .SingleOrDefaultAsync(u => u.UserName == username && u.Password == password);
        if (user == null)
        {
            return null; // User not found or password incorrect
        }

        // Generate a JWT token
        var token = GenerateJwtToken(user);
        return token;
    }

    // Checks if a user is authorized for a specific permission.
    public async Task<bool> IsUserAuthorizedAsync(int userId, string permission)
    {
        // Check if the user has the specified permission
        // This example is simplified; implement your authorization logic here
        var user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            // Implement your authorization logic here
            return true; // Example returns true for simplicity
        }
        return false;
    }

    // Generates a JWT token for a given user.
    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[] 
            {
                new Claim(ClaimTypes.Name, user.UserName),
                // Add other claims as needed
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
