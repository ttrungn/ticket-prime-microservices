using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Yarp.ReverseProxy.Transforms;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddApiGatewayServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var publicKeyText = File.ReadAllText("Keys/public.key");
                    var rsa = RSA.Create();
                    rsa.ImportFromPem(publicKeyText);
                    var validationKey = new RsaSecurityKey(rsa);
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                        ValidAudience = builder.Configuration["JwtSettings:Audience"],

                        IssuerSigningKey = validationKey
                    };
                });

            builder.Services.AddAuthorization();

            var globalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
            {
                var clientIp = context.Connection.RemoteIpAddress?.ToString() ?? "anonymous";

                return RateLimitPartition.GetFixedWindowLimiter(clientIp, _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = builder.Configuration.GetValue<int>("RateLimiter:PermitLimit"),
                    Window = TimeSpan.FromMinutes(builder.Configuration.GetValue<int>("RateLimiter:WindowM")),
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    QueueLimit = builder.Configuration.GetValue<int>("RateLimiter:QueueLimit")
                });
            });

            builder.Services
                .AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
                .AddTransforms(transforms =>
                {
                    transforms.AddRequestTransform(async ctx =>
                    {
                        var h = ctx.ProxyRequest.Headers;
                        if (h.Authorization != null)
                        {
                            var result = await ctx.HttpContext
                                        .AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);

                            if (!result.Succeeded)
                            {
                                ctx.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                await ctx.HttpContext.Response.WriteAsJsonAsync(new ProblemDetails
                                {
                                    Status = StatusCodes.Status401Unauthorized,
                                    Title = "Unauthorized",
                                    Type = "https://tools.ietf.org/html/rfc7235#section-3.1",
                                    Detail = "Invalid token"
                                });
                                ctx.HttpContext.RequestAborted.ThrowIfCancellationRequested();
                                return;
                            }

                            var user = result.Principal!;
                            var id = user.FindFirstValue(ClaimTypes.NameIdentifier);
                            var email = user.FindFirstValue(ClaimTypes.Email);
                            var name = user.FindFirstValue(JwtRegisteredClaimNames.Name);
                            var roles = user.FindAll(ClaimTypes.Role).Select(c => c.Value);

                            h.Add("X-User-Id", id ?? "");
                            h.Add("X-User-Email", email ?? "");
                            h.Add("X-User-Name", name ?? "");
                            h.Add("X-User-Roles", string.Join(",", roles));
                        }
                        await ValueTask.CompletedTask;
                    });
                });

            builder.Services.AddRateLimiter(options =>
            {
                options.GlobalLimiter = globalLimiter;
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });
        }
    }
}

