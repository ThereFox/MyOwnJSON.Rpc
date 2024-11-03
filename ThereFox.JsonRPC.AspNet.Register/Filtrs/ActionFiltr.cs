using Microsoft.AspNetCore.Mvc.Filters;
using ThereFox.JsonRPC.AspNet.Register.Helpers;
using ThereFox.JsonRPC.AspNet.Register.Responses;

namespace ThereFox.JsonRPC.AspNet.Register.Filtrs;

public class ActionFiltr : IEndpointFilter
{
    private readonly RequestHandler _handler;
    private readonly ResponseFormatter _responseFormatter;
    
    public ActionFiltr(RequestHandler handler, ResponseFormatter responseFormatter)
    {
        _handler = handler;
        _responseFormatter = responseFormatter;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var bodyReader = await context.HttpContext.GetBodyContentAsync();

        var  response = await _handler.HandleAsync(bodyReader);

        var formatterResponse = _responseFormatter.FormatResponse(response);
        
        return returnJson(formatterResponse, context.HttpContext);
    }

    private string returnJson(string response, HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 200;

        return response;
    }
}