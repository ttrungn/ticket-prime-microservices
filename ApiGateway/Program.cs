using ApiGateway;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiGatewayServices();

var app = builder.Build();

app.UseRouting();
app.UseCors("TicketPrimeUserWebApp");
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapReverseProxy();
app.Run();
