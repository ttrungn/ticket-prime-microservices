using AuthService.Application.Common.Models;

namespace AuthService.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password, string role);

    Task<(Result Result, string Token, int ExpiresIn)> LoginUserAsync(string email, string password, string role);

    Task<Result> DeleteUserAsync(string userId);

    string CreateToken(string userId, string userEmail, IEnumerable<string> roles);
}
