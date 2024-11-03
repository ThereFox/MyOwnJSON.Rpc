using Newtonsoft.Json;
using ThereFox.JsonRPC.Core.Client.Arguments;
using ThereFox.JsonRPC.Request;

namespace ThereFox.JsonRPC.Core.Client;

public class JsonRPCRequestBuilder
{
    public string CreateRequest(string method, List<object> arguments)
    {
        var requestContent = new RequestBase()
        {
            Version = "2.0",
            CalledMethod = method,
            Arguments = arguments
        };

        var requestJSONContent = JsonConvert.SerializeObject(requestContent);

        return requestJSONContent;
    }
}