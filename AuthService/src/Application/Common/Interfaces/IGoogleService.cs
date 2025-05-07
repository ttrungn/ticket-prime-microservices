namespace AuthService.Application.Common.Interfaces;

public interface IGoogleService
{
    Task<(string ProviderKey, string Email)> ExchangeCodeAsync(string code, string redirectUri);
}
