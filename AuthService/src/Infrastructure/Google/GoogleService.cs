using System.Text.Json;
using AuthService.Application.Common.Exceptions;
using AuthService.Application.Common.Interfaces;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;

namespace AuthService.Infrastructure.Google;

public class GoogleService : IGoogleService
{
    private readonly IConfiguration _config;
    private readonly string _tokenEndpoint = "https://oauth2.googleapis.com/token";

    public GoogleService(IConfiguration config)
    {
        _config = config;
    }

    public async Task<(string ProviderKey, string Email)> ExchangeCodeAsync(string code, string redirectUri)
    {
        var response = await new HttpClient().PostAsync(
            _tokenEndpoint,
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["code"] = code,
                ["client_id"] = _config["GoogleSettings:OAuth2:ClientId"]!,
                ["client_secret"] = _config["GoogleSettings:OAuth2:ClientSecret"]!,
                ["redirect_uri"] = redirectUri,
                ["grant_type"] = "authorization_code"
            }));

        if (!response.IsSuccessStatusCode)
        {
            throw new ServiceUnavailableException("Failed to exchange authorization code for tokens.");
        }

        await using var stream = await response.Content.ReadAsStreamAsync();
        using var jsonDoc = await JsonDocument.ParseAsync(stream);
        var idToken = jsonDoc.RootElement.GetProperty("id_token").GetString() ?? 
                      throw new ServiceUnavailableException("ID token missing in response.");

        var userPayload = await GoogleJsonWebSignature.ValidateAsync(
            idToken,
            new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { _config["GoogleSettings:OAuth2:ClientId"]! }
            });

        if (userPayload is null)
        {
            throw new ServiceUnavailableException("Failed to validate ID token.");
        }

        return (userPayload.Subject, userPayload.Email);
    }
}
