using Azure.Identity;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Application.Common.Interfaces;
using NotificationService.Domain.Shared.Events.Users;
using NotificationService.Infrastructure.Data;
using NotificationService.Web.Consumers;
using NotificationService.Web.Services;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddWebServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddScoped<IUser, CurrentUser>();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        builder.Services.AddExceptionHandler<CustomExceptionHandler>();

        // Background Service
        builder.Services.AddMassTransit(x =>
        {
            x.UsingInMemory();

            x.AddRider(rider =>
            {
                rider.AddConsumer<UserRegisteredEventConsumer>();

                rider.UsingKafka((context, k) =>
                {
                    k.Host(builder.Configuration["KafkaSettings:Url"], h =>
                    {
                        if (
                                builder.Configuration["KafkaSettings:Username"] != null &&
                                builder.Configuration["KafkaSettings:Username"] != string.Empty &&
                                builder.Configuration["KafkaSettings:Password"] != null &&
                                builder.Configuration["KafkaSettings:Password"] != string.Empty
                                )
                        {

                            h.UseSasl(s =>
                            {
                                s.Mechanism = SaslMechanism.Plain;
                                s.SecurityProtocol = SecurityProtocol.SaslSsl;
                                s.Username = builder.Configuration["KafkaSettings:Username"];
                                s.Password = builder.Configuration["KafkaSettings:Password"];
                            });
                        }
                    });

                    k.TopicEndpoint<UserRegisteredEvent>(
                        builder.Configuration["KafkaSettings:UserEvents:Name"],
                        builder.Configuration["KafkaSettings:UserEvents:Group"],
                        e =>
                        {
                            e.ConfigureConsumer<UserRegisteredEventConsumer>(context);
                        }
                    );
                });
            });
        });

        // Customise default API behaviour
        builder.Services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddOpenApiDocument((configure, sp) =>
        {
            configure.Title = "NotificationService API";

            // Add JWT
            configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Name = "Authorization",
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = "Type into the textbox: Bearer {your JWT token}."
            });

            configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        });
    }

    public static void AddKeyVaultIfConfigured(this IHostApplicationBuilder builder)
    {
        var keyVaultUri = builder.Configuration["AZURE_KEY_VAULT_ENDPOINT"];
        if (!string.IsNullOrWhiteSpace(keyVaultUri))
        {
            builder.Configuration.AddAzureKeyVault(
                new Uri(keyVaultUri),
                new DefaultAzureCredential());
        }
    }
}
