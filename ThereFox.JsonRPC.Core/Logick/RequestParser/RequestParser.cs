using CSharpFunctionalExtensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ThereFox.JsonRPC.Common;
using ThereFox.JsonRPC.Interfaces;
using ThereFox.JsonRPC.Request;

namespace ThereFox.JsonRPC;

public class RequestParser : IRequestParser
{
    public Result<RequestContent> Parse(string requestBody)
    {
        var parseBodyBase = ResultJsonDeserialiser.Deserialise<RequestBase>(requestBody);

        if (parseBodyBase.IsFailure)
        {
            return parseBodyBase.ConvertFailure<RequestContent>();
        }

        var bodyContent = parseBodyBase.Value;
        var arguments = bodyContent.Arguments;

        if (isEmptyArgument(arguments))
        {
            var emptyArgumentResult = new RequestContent(
                bodyContent.Version, 
                bodyContent.CalledMethod, 
                []);
            return Result.Success(emptyArgumentResult);
        }

        var parseArgumentValuesResult = ParseArguments(arguments);

        if (parseArgumentValuesResult.IsFailure)
        {
            return parseArgumentValuesResult.ConvertFailure<RequestContent>();
        }
        
        var result = new RequestContent(
            bodyContent.Version, 
            bodyContent.CalledMethod, 
            parseArgumentValuesResult.Value);
        
        return Result.Success(result);
    }

    private Result<List<ArgumentValue>> ParseArguments(object arguments)
    {
        if (arguments is not JToken)
        {
            return Result.Failure<List<ArgumentValue>>("Arguments must be JObject");
        }
        
        var JtokenValue = (JToken)arguments;

        if (JtokenValue.Type == JTokenType.Array)
        {
            return parseOrderedValues(JtokenValue.ToString());
        }
        else if (JtokenValue.Type == JTokenType.Object)
        {
            return parseNamedValues(JtokenValue.ToString());
        }
        else
        {
            return Result.Success(new List<ArgumentValue>(){new ArgumentValue(arguments)});
        }
    }

    private bool isEmptyArgument(object arguments)
    {
        if (arguments == default)
        {
            return true;
        }

        if (arguments is not JToken)
        {
            return true;
        }

        return false;
    }
    
    private Result<List<ArgumentValue>> parseOrderedValues(string arguments)
    {
        var parseValues = ResultJsonDeserialiser.Deserialise<List<object>>(arguments);

        if (parseValues.IsFailure)
        {
            return parseValues.ConvertFailure<List<ArgumentValue>>();
        }

        var valuesList = parseValues.Value;

        var result = new List<ArgumentValue>();
        
        foreach (var value in valuesList)
        {
            result.Add(new ArgumentValue(value));
        }

        return result;
    }

    private List<ArgumentValue> parseNamedValues(string arguments)
    {
        var obj = JObject.Parse(arguments);

        var propertyes = obj.Properties();

        var result = new List<ArgumentValue>();
        
        foreach (var property in propertyes)
        {
            var name = property.Name;
            var value = property.Value.Value<string>();
            
            result.Add(new ArgumentValue(name, value));
        }

        return result;
    }

}