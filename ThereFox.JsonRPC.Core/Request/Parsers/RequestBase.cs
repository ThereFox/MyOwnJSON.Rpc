using System.Text.Json.Serialization;

namespace ThereFox.JsonRPC.Request;

public class RequestBase
{
    [JsonPropertyName(("jsonrpc"))]
    public string Version { get; set; }
    
    [JsonPropertyName("method")]
    public string CalledMethod { get; set; }
    
    [JsonPropertyName("params")]
    public string Arguments { get; set; }
}