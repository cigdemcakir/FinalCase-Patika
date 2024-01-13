namespace ExpensePaymentSystem.Business.Interfaces;

public interface IAuthorizationService
{
    // Authenticates the user and returns a JWT token if successful.
    Task<string> AuthenticateAsync(string username, string password);

    // Checks if the user is authorized for a given permission.
    Task<bool> IsUserAuthorizedAsync(int userId, string permission);
}
