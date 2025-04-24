using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using AuthService.Application.Common.Interfaces;
using AuthService.Infrastructure.Data;
using AuthService.Infrastructure.Data.Interceptors;
using AuthService.Infrastructure.Identity;
using AuthService.Infrastructure.Kafka;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("AuthServiceDb");
        Guard.Against.Null(connectionString, message: "Connection string 'AuthServiceDb' not found.");

        builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        builder.Services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString);
        });


        builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        builder.Services.AddScoped<ApplicationDbContextInitialiser>();

        builder.Services.AddAuthorizationBuilder();

        builder.Services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options =>
                        {
                            var privateKeyText = File.ReadAllText("Keys/private.key");
                            var rsa = RSA.Create();
                            rsa.ImportFromPem(privateKeyText);
                            var signingKey = new RsaSecurityKey(rsa);

                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidateLifetime = true,
                                ValidateIssuerSigningKey = true,
                                ClockSkew = TimeSpan.Zero,
                                ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                                ValidAudience = builder.Configuration["JwtSettings:Audience"],

                                IssuerSigningKey = signingKey
                            };
                        });


        builder.Services.AddSingleton(TimeProvider.System);
        builder.Services.AddTransient<IIdentityService, IdentityService>();
        builder.Services.AddScoped(typeof(IMassTransitService<>), typeof(MassTransitService<>));

        builder.Services.AddAuthorization();
    }
}
