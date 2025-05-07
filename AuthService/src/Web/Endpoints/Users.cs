using AuthService.Application.Users.Commands.LoginUser;
using AuthService.Application.Users.Commands.LoginUserWithGoogle;
using AuthService.Application.Users.Commands.RegisterUser;
using AuthService.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Web.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(RegisterCustomer, "/register/customer")
            .MapPost(LoginCustomer, "/login/customer")
            .MapPost(LoginCustomerWithGoogle, "/login/customer/google")
            .MapPost(RegisterOrganizer, "/register/organizer")
            .MapPost(LoginOrganizer, "/login/organizer")
            .MapPost(LoginOrganizerWithGoogle, "/login/organizer/google");
    }

    private static async Task<IResult> RegisterCustomer(
        [FromBody] RegisterUserCommand registerUserCommand, ISender sender)
    {
        registerUserCommand.Role = Roles.Customer;
        var result = await sender.Send(registerUserCommand);

        return result.Length == 0 ? Results.BadRequest(new { message = result }) : Results.Ok();
    }

    private static async Task<IResult> LoginCustomer(
        [FromBody] LoginUserCommand loginUserCommand,
        ISender sender)
    {
        loginUserCommand.Role = Roles.Customer;
        var result = await sender.Send(loginUserCommand);
        return result is null ? Results.Unauthorized() : Results.Ok(result);
    }

    private static async Task<IResult> LoginCustomerWithGoogle(
        [FromBody] LoginUserWithGoogleCommand loginUserCommand,
        ISender sender)
    {
        loginUserCommand.Role = Roles.Customer;
        var result = await sender.Send(loginUserCommand);
        return result is null ? Results.Unauthorized() : Results.Ok(result);
    }
    
    private static async Task<IResult> RegisterOrganizer(
        [FromBody] RegisterUserCommand registerUserCommand,
        ISender sender)
    {
        registerUserCommand.Role = Roles.Organizer;
        var result = await sender.Send(registerUserCommand);

        return result.Length == 0 ? Results.BadRequest(new { message = result }) : Results.Ok();
    }

    private static async Task<IResult> LoginOrganizer(
        [FromBody] LoginUserCommand loginUserCommand, 
        ISender sender)
    {
        loginUserCommand.Role = Roles.Organizer;
        var result = await sender.Send(loginUserCommand);
        return result is null ? Results.Unauthorized() : Results.Ok(result);
    }
    
    private static async Task<IResult> LoginOrganizerWithGoogle(
        [FromBody] LoginUserWithGoogleCommand loginUserCommand,
        ISender sender)
    {
        loginUserCommand.Role = Roles.Organizer;
        var result = await sender.Send(loginUserCommand);
        return result is null ? Results.Unauthorized() : Results.Ok(result);
    }
}
