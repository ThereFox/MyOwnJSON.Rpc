using Microsoft.AspNetCore.Mvc.Filters;
using ThereFox.JsonRPC.AspNet.Register.Helpers;

namespace ThereFox.JsonRPC.AspNet.Register.Filtrs;

public class ResourceFiltr : IAsyncResourceFilter
{
    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        var bodyReader = await context.HttpContext.GetBodyContentAsync();
        
        
        
    }
}