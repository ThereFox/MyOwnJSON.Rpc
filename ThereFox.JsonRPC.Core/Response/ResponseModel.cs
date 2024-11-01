using System.Text.Json.Serialization;

namespace ThereFox.JsonRPC.Response;

public class ResponseModel
{
    [JsonPropertyName(("jsonrpc"))]
    public string Version { get; set; }
    
    [JsonPropertyName("result")]
    public string Arguments { get; set; }
}