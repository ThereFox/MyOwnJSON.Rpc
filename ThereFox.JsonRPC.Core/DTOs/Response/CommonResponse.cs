using System.Text.Json.Serialization;
using Microsoft.VisualBasic.CompilerServices;

namespace ThereFox.JsonRPC.Response;

public class CommonResponse
{
    public static CommonResponse VoidSucsess => new CommonResponse();
    
    public bool IsSucsessful { get; }
    public object? Value { get; }
    public string? ErrorSuggestion { get; }

    private CommonResponse()
    {
        IsSucsessful = true;
        ErrorSuggestion = null;
        Value = default;
    }
    public CommonResponse(string response)
    {
        IsSucsessful = false;
        ErrorSuggestion = response;
        Value = default;
    }
    public CommonResponse(object value)
    {
        IsSucsessful = true;
        ErrorSuggestion = null;
        Value = value;
    }
}