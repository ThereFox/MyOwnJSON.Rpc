using CSharpFunctionalExtensions;
using Newtonsoft.Json;
using ThereFox.JsonRPC.Common;

namespace ThereFox.JsonRPC.ValueConverter;

public class ArgumentConverter
{
    public Result<object> TryConvert(object value, Type targetType)
    {
        if (value == null)
        {
            if (targetType.IsValueType && targetType.IsGenericType && targetType is Nullable)
            {
                return Convert.ChangeType(value, targetType);
            }
            if (targetType.IsClass)
            {
                return Convert.ChangeType(value, targetType);
            }
            return Result.Failure<object>($"Cannot convert null to type {targetType}.");
            
        }

        var initType = value.GetType();

        if (initType == targetType || isNumbersBoth(initType, targetType))
        {
            return Result.Success(Convert.ChangeType(value, targetType));
        }

        if (initType == typeof(string))
        {
            return stringConvertHandler((string)value, targetType);
        }

        if (targetType == typeof(string))
        {
            return JsonConvert.SerializeObject(value);
        }
        
        return Result.Failure<object>($"Cannot convert {value} to type {targetType}.");
    }

    private Result<object> stringConvertHandler(string value, Type targetType)
    {
        if (targetType == typeof(Guid))
        {
            var tryParseGuid = Guid.TryParse(value, out var result);
            if (tryParseGuid)
            {
                return Result.Success(result);
            }
            else
            {
                return Result.Failure<object>($"Cannot convert '{value}' to type {targetType}.");
            }
        }
        
        var tryDeserialise = ResultJsonDeserialiser.Deserialise(value, targetType);
        
        if (tryDeserialise.IsSuccess)
        {
            return tryDeserialise;
        }

        return tryDeserialise.Error;
    }
    
    private bool isNumbersBoth(Type a, Type b)
    {
        return a.IsPrimitive || b.IsPrimitive;
    }
}