using CoreService.Application.Venues.Commands.ImportVenue;
using Microsoft.AspNetCore.Mvc;

namespace CoreService.Web.Endpoints;

public class Venues : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(ImportVenue, "/");
    }

    private static async Task<IResult> ImportVenue([FromBody] ImportVenueCommand importVenueCommand, ISender sender)
    {
        var result = await sender.Send(importVenueCommand);
        
        return Results.Ok(result);
    }
}
