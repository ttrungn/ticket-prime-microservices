var builder = WebApplication.CreateBuilder(args);

builder.AddApiGatewayServices();

var app = builder.Build();

app.UseRouting();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapReverseProxy();
app.Run();
