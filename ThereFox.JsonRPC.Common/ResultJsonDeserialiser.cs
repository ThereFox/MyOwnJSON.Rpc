using CSharpFunctionalExtensions;
using Newtonsoft.Json;

namespace ThereFox.JsonRPC.Common;

public static class ResultJsonDeserialiser
{
    public static Result<T> Deserialise<T>(string jsonString)
        where T: class, new()
    {
        try
        {
            var result = JsonConvert.DeserializeObject<T>(jsonString);

            if (result == default)
            {
                return Result.Failure<T>("Failed to deserialise request");
            }
            
            return Result.Success(result);
        }
        catch (Exception e)
        {
            return Result.Failure<T>("Invalid format");
        }
    }
}