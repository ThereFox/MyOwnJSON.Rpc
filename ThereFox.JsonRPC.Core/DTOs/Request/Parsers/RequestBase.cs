using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ThereFox.JsonRPC.Request;

public class RequestBase
{
    [JsonPropertyName(("jsonrpc"))]
    [JsonProperty("jsonrpc", Required = Required.Always)]
    public string Version { get; set; }
    
    [JsonPropertyName("method")]
    [JsonProperty("method", Required = Required.Always)]
    public string CalledMethod { get; set; }
    
    [JsonPropertyName("params")]
    [JsonProperty("params", Required = Required.AllowNull)]
    public object Arguments { get; set; }
}