using Newtonsoft.Json;
using ThereFox.JsonRPC.Response;

namespace ThereFox.JsonRPC;

public class RequestHandler
{
    private readonly RequestParser _requestParser;
    private readonly ArgumentsValidator _argumentsValidator;
    
    
    public async Task<string> HandleAsync(string request)
    {
        var requestContent = _requestParser.Parse(request);
        
        if(requestContent.IsFailure)
        {
            return constructErrorResponse(requestContent.Error);
        }

        return "";
    }

    private string constructErrorResponse(string errorMessage)
    {
        return JsonConvert.SerializeObject(new CommonResponse("2.0", errorMessage));
    }
}