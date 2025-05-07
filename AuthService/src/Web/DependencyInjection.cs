using AuthService.Application.Common.Interfaces;
using AuthService.Domain.Events;
using AuthService.Infrastructure.Data;
using AuthService.Web.Services;
using Azure.Identity;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace AuthService.Web;

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
                rider.AddProducer<UserRegisteredEvent>(
                    builder.Configuration["KafkaSettings:UserRegisteredTopic:Name"],
                    (ctx, kc) =>
                    {
                        kc.MessageTimeout = TimeSpan.FromMilliseconds(
                            builder.Configuration.GetValue<int>("KafkaSettings:ProducerConfigs:MessageTimeoutMs"));
                        kc.MessageSendMaxRetries =
                            builder.Configuration.GetValue<int>("KafkaSettings:ProducerConfigs:MessageSendMaxRetries");
                        kc.RetryBackoff = TimeSpan.FromMilliseconds(
                            builder.Configuration.GetValue<int>("KafkaSettings:ProducerConfigs:RetryBackoffMs"));
                    });

                rider.UsingKafka((context, k) =>
                {
                    k.Host(
                        builder.Configuration["KafkaSettings:Url"],
                        h =>
                        {
                            h.UseSasl(s =>
                            {
                                s.Mechanism = SaslMechanism.Plain;
                                s.SecurityProtocol = SecurityProtocol.SaslSsl;
                                s.Username = builder.Configuration["KafkaSettings:Username"];
                                s.Password = builder.Configuration["KafkaSettings:Password"];
                            });
                        });
                });
            });
        });

        // Customise default API behaviour
        builder.Services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddOpenApiDocument((configure, sp) =>
        {
            configure.Title = "AuthService API";

            // Add JWT
            configure.AddSecurity("JWT", Enumerable.Empty<string>(),
                new OpenApiSecurityScheme
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
