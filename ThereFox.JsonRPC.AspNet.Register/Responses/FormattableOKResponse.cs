using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ThereFox.JsonRPC.AspNet.Register.Responses;

public class FormattableOKResponse
{
    [JsonPropertyName(("jsonrpc"))]
    [JsonProperty("jsonrpc", Required = Required.Always)]
    public string Version { get; set; }

    [JsonPropertyName("result")]
    [JsonProperty("result", Required = Required.AllowNull, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public object Result { get; set; }
    
    [JsonPropertyName("additionalInfo")]
    [JsonProperty("additionalInfo", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string Comment { get; set; }

    private FormattableOKResponse()
    {
        
    }
    
    public FormattableOKResponse(string version, object result, string comment)
    {
        if (result == null)
        {
            Comment = "All OK";
        }
        else
        {
            Result = result;
            Comment = comment;
        }
        
        Version = version;
    }
    
    public FormattableOKResponse(string version, object result)
    {
        Version = version;
        Result = result;
    }
    
}