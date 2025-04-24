using AuthService.Application.Users.Commands.LoginUser;
using AuthService.Application.Users.Commands.RegisterUser;
using AuthService.Domain.Constants;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Web.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(RegisterCustomer, "/register/customer")
            .MapPost(LoginCustomer, "/login/customer");
    }

    public async Task<Results<Ok, UnauthorizedHttpResult>> RegisterCustomer([FromBody] RegisterUserCommand registerUserCommand, ISender sender)
    {
        registerUserCommand.Role = Roles.Customer;
        await sender.Send(registerUserCommand);

        return TypedResults.Ok();
    }

    public async Task<Results<Ok<LoginUserResult>, UnauthorizedHttpResult>> LoginCustomer([FromBody] LoginUserCommand loginUserCommand, ISender sender)
    {
        loginUserCommand.Role = Roles.Customer;
        var result = await sender.Send(loginUserCommand);
        return TypedResults.Ok(result);
    }
}
