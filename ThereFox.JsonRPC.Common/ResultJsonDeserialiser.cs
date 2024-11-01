using CSharpFunctionalExtensions;
using Newtonsoft.Json;

namespace ThereFox.JsonRPC.Common;

public static class ResultJsonDeserialiser
{
    public static Result<T> Deserialise<T>(string jsonString)
    {
        try
        {
            var result = JsonConvert.DeserializeObject<T>(jsonString);

            if (result == null)
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

    public static Result<object> Deserialise(string jsonString, Type type)
    {
        try
        {
            var result = JsonConvert.DeserializeObject(jsonString, type);

            if (result == null)
            {
                return Result.Failure("Failed to deserialise request");
            }
            
            return Result.Success(result);
        }
        catch (Exception e)
        {
            return Result.Failure("Invalid format");
        }
    }
}