using System.Security.Claims;

using AuthService.Application.Common.Interfaces;

namespace AuthService.Web.Services;

public class CurrentUser : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        var ctx = _httpContextAccessor.HttpContext;
        if (ctx?.Request?.Headers != null)
        {
            foreach (var header in ctx.Request.Headers)
            {
                // header.Key is the name, header.Value is a StringValues (can be multiple values)
                Console.WriteLine($"Header {header.Key}: {header.Value.ToString()}");
            }
        }
    }

    public string? Id => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

    public string? UserName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);

    public string? Email => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);

    public IEnumerable<string>? Roles => _httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role)?.Select(c => c.Value);
}
