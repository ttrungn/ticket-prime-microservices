using CoreService.Application.Test.Queries.GetTest;

namespace CoreService.Web.Endpoints;

public class Test : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetTest, "/");
    }

    private async Task GetTest(ISender sender)
    {
        await sender.Send(new GetTestQuery());
    }
}
