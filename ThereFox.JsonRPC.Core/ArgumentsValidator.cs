using System.Globalization;
using System.Reflection;
using CSharpFunctionalExtensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ThereFox.JsonRPC.Common;
using ThereFox.JsonRPC.Request;

namespace ThereFox.JsonRPC;

public class ArgumentsValidator
{
    public Result<List<ArgumentValue>> ValidateValues(List<ArgumentValue> settedValues, List<AwaitedArguments> awaitedArguments)
    {
        var countOfDefaultValues = awaitedArguments.Where(ex => ex.HasDefaultValue).Count();

        if (settedValues.Count + countOfDefaultValues < awaitedArguments.Count)
        {
            return Result.Failure<List<ArgumentValue>>("count of values low thet needed");
        }

        var resultCollection = new List<ArgumentValue>();
        
        for (int i = 0; i < awaitedArguments.Count; i++)
        {
            var value = pickArgument(awaitedArguments[i], settedValues);

            if (value.IsFailure)
            {
                return value.ConvertFailure<List<ArgumentValue>>();
            }
            
            resultCollection.Add(value.Value);
        }

        return resultCollection;
    }

    private Result<ArgumentValue> pickArgument(AwaitedArguments argument, List<ArgumentValue> avaliableValues)
    {
        if (avaliableValues.Count == 0)
        {
            if (argument.HasDefaultValue)
            {
                var res = new ArgumentValue(argument.Name, argument.DefaultValue);
                return Result.Success(res);
            }
            return Result.Failure<ArgumentValue>("No default value provided");
        }

        var tryGetByName = avaliableValues
            .Where(ex => ex.Name is not null && ex.Name.ToLower() == argument.Name.ToLower());

        if (tryGetByName.Count() >= 1)
        {
            if (tryGetByName.Count() != 1)
            {
                return Result.Failure<ArgumentValue>("Multiple values provided");
            }
            var res = new ArgumentValue(tryGetByName.First().Name, tryGetByName.First().Value);
            avaliableValues.Remove(tryGetByName.Single());
            return Result.Success(res);
        }

        foreach (var argumentValue in avaliableValues.Where(ex => ex.Name == default))
        {
            var parse = tryParse(
                argumentValue.Value, 
                argument.Type
                );
            if (parse.IsSuccess)
            {
                return new ArgumentValue(argument.Name, parse.Value);
            }
        }

        return Result.Failure<ArgumentValue>("No value provided");
    }

    private Result<object> tryParse(object value, Type destinateType)
    {
        var initType = value.GetType();
        
        
        if (initType == destinateType || isNumbersBoth(initType, destinateType))
        {
            return Result.Success(Convert.ChangeType(value, destinateType));
        }

        if (initType == typeof(string))
        {
            return ResultJsonDeserialiser.Deserialise((string)value, destinateType);
        }

        return Result.Failure("cannot parse");
    }
    private Result<T> tryParse<T>(object value)
    {
        var initType = value.GetType();
        
        
        if (initType is T)
        {
            return Result.Success<T>((T)value);
        }

        if (initType == typeof(string))
        {
            return ResultJsonDeserialiser.Deserialise<T>((string)value);
        }

        return Result.Failure<T>("cannot parse");
    }

    private bool isNumbersBoth(Type a, Type b)
    {
        return a.IsPrimitive || b.IsPrimitive;
    }
    
}