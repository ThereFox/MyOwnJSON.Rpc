using Microsoft.AspNetCore.Mvc.Filters;
using ThereFox.JsonRPC.AspNet.Register.Helpers;

namespace ThereFox.JsonRPC.AspNet.Register.Filtrs;

public class ResourceFiltr : IAsyncResourceFilter
{
    private readonly RequestHandler _handler;

    public ResourceFiltr(RequestHandler handler)
    {
        _handler = handler;
    }
    
    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        var bodyReader = await context.HttpContext.GetBodyContentAsync();

        await _handler.HandleAsync(bodyReader);

    }
}