using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ThereFox.JsonRPC.AspNet.Register.Responses;

public class FormattableOKResponse
{
    [JsonPropertyName(("jsonrpc"))]
    [JsonProperty("jsonrpc", Required = Required.Always)]
    public string Version { get; set; }

    [JsonPropertyName("method")]
    [JsonProperty("method", Required = Required.Always)]
    public string CalledMethod { get; set; }

    [JsonPropertyName("result")]
    [JsonProperty("result", Required = Required.AllowNull)]
    public object Result { get; set; }
}