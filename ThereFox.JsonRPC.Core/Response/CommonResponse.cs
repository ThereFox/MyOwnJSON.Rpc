using System.Text.Json.Serialization;

namespace ThereFox.JsonRPC.Response;

public class CommonResponse
{
    [JsonPropertyName(("jsonrpc"))]
    public string Version { get; }
    
    [JsonPropertyName("response")]
    public string Arguments { get; }

    public CommonResponse(string version, string argumentJson)
    {
        Version = version;
        Arguments = argumentJson;
    }
}