using Newtonsoft.Json;
using ThereFox.JsonRPC.Response;

namespace ThereFox.JsonRPC.AspNet.Register.Responses;

public class ResponseFormatter
{
    public string FormatResponse(CommonResponse response)
    {
        if (response.IsSucsessful)
        {
            return JsonConvert.SerializeObject(
                new FormattableOKResponse("2.0", response.Value, response.ErrorSuggestion)
            );
        }
        
        return JsonConvert.SerializeObject(
            new FormattableErrorResponse("2.0", response.ErrorSuggestion)
        );
    }
}