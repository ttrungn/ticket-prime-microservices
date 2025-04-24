using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using AuthService.Application.Common.Interfaces;
using AuthService.Application.Common.Models;
using AuthService.Domain.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly IConfiguration _config;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;
    private readonly RsaSecurityKey _signingKey;

    public IdentityService(
        IConfiguration config,
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService)
    {
        _config = config;
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        var privateText = File.ReadAllText("Keys/private.key");
        var rsa = RSA.Create();
        rsa.ImportFromPem(privateText);
        _signingKey = new RsaSecurityKey(rsa);
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user?.UserName;
    }

    public async Task<(Result Result, string UserId)> CreateUserAsync(string email, string password, string role)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email
        };
        user.AddDomainEvent(new UserRegisteredEvent(user.Id, email));

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            return (Result.Failure(["Email existed."]), string.Empty);
        }

        var roleResult = await _userManager.AddToRoleAsync(user, role);
        if (!roleResult.Succeeded)
        {
            await _userManager.DeleteAsync(user);
            return (Result.Failure(["You do not have the permissions."]), user.Id);
        }

        return (Result.Success(), user.Id);
    }

    public async Task<(Result Result, string Token, int ExpiresIn)> LoginUserAsync(string email, string password, string role)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            return (Result.Failure(["Invalid email or password."]), string.Empty, 0);

        var passwordValid = await _userManager.CheckPasswordAsync(user, password);
        if (!passwordValid)
            return (Result.Failure(["Invalid email or password."]), string.Empty, 0);

        var roles = await _userManager.GetRolesAsync(user);
        if (!roles.Contains(role))
            return (Result.Failure(["You do not have the permissions."]), string.Empty, 0);

        var token = CreateToken(user.Id, user.Email!, roles);
        var expiresIn = _config.GetValue<int>("JwtSettings:TokenLifetimeMinutes") * 60;

        return (Result.Success(), token, expiresIn);
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    public string CreateToken(string userId, string userEmail, IEnumerable<string> roles)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId),
            new(JwtRegisteredClaimNames.Email, userEmail),
            new(JwtRegisteredClaimNames.Name, userEmail),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var credentials = new SigningCredentials(_signingKey, SecurityAlgorithms.RsaSha256);

        var token = new JwtSecurityToken(
            issuer: _config["JwtSettings:Issuer"],
            audience: _config["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_config.GetValue<int>("JwtSettings:TokenLifetimeMinutes")),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
