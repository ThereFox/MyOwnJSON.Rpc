using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ThereFox.JsonRPC.Request;

public class RequestBase
{
    [JsonPropertyName(("jsonrpc"))]
    [JsonProperty("jsonrpc")]
    public string Version { get; set; }
    
    [JsonPropertyName("method")]
    [JsonProperty("method")]
    public string CalledMethod { get; set; }
    
    [JsonPropertyName("params")]
    [JsonProperty("params")]
    public object Arguments { get; set; }
}