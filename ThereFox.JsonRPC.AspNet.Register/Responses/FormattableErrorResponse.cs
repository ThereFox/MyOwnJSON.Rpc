using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ThereFox.JsonRPC.AspNet.Register.Responses;

public class FormattableErrorResponse
{
    [JsonPropertyName(("jsonrpc"))]
    [JsonProperty("jsonrpc", Required = Required.Always)]
    public string Version { get; set; }

    [JsonPropertyName("error")]
    [JsonProperty("error", Required = Required.Always)]
    public string ErrorMessage { get; set; }
    
    [JsonPropertyName("type")]
    [JsonProperty("type", Required = Required.AllowNull, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string ErrorType { get; set; }
    
    [JsonPropertyName("suggestion")]
    [JsonProperty("suggestion", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string Comment { get; set; }

    public FormattableErrorResponse(string version, string errorMessage)
    {
        Version = version;
        ErrorMessage = errorMessage;
    }

    public FormattableErrorResponse(string version, string errorMessage, string errorType, string comment)
    {
        Version = version;
        ErrorMessage = errorMessage;
        ErrorType = errorType;
        Comment = comment;
    }
    
}