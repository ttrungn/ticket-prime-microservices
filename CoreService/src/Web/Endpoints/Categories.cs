using CoreService.Application.Categories.Queries.GetCategories;

namespace CoreService.Web.Endpoints;

public class Categories : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetCategories, "/");
    }

    private static async Task<IResult> GetCategories(ISender sender)
    {
        var result = await sender.Send(new GetCategoriesQuery());
        
        return Results.Ok(result);
    }
}
