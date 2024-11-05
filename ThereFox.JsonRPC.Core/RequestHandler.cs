using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using ThereFox.JsonRPC.Interfaces;
using ThereFox.JsonRPC.Response;

namespace ThereFox.JsonRPC;

public class RequestHandler
{
    private readonly RequestParser _requestParser;
    private readonly ArgumentsValidator _argumentsValidator;
    private readonly IActionInfoStore _actionInfoStore;
    private readonly IActionExecutor _actionExecutor;

    public RequestHandler(RequestParser parser, ArgumentsValidator validator, IActionInfoStore store, IActionExecutor executor)
    {
        _requestParser = parser;
        _argumentsValidator = validator;
        _actionInfoStore = store;
        _actionExecutor = executor;
    }
    
    public async Task<CommonResponse> HandleAsync(string request)
    {
        var parseRequestResult = _requestParser.Parse(request);
        
        if(parseRequestResult.IsFailure)
        {
            return constructErrorResponse(parseRequestResult.Error);
        }

        var requestContent = parseRequestResult.Value;
        
        var getActionInfoResult = _actionInfoStore.GetActionInfo(requestContent.ActionName);

        if (getActionInfoResult.IsFailure)
        {
            return constructErrorResponse(getActionInfoResult.Error);
        }
        
        var actionInfo = getActionInfoResult.Value;

        var validateArgumentsResult = _argumentsValidator.ValidateValues(requestContent.Arguments.ToList(), actionInfo.Arguments);

        if (validateArgumentsResult.IsFailure)
        {
            return constructErrorResponse(validateArgumentsResult.Error);
        }
        
        var validatedArguments = validateArgumentsResult.Value;
        
        var callResult = await _actionExecutor.Execute(requestContent.ActionName, validatedArguments);

        if (callResult.IsFailure)
        {
            return constructErrorResponse(callResult.Error);
        }
        
        if (actionInfo.ReturnedType == typeof(void))
        {
            return CommonResponse.VoidSucsess;
        }
        
        return constructSucsessResponse(callResult.Value);
    }

    private CommonResponse constructSucsessResponse(object response)
    {
        return new CommonResponse(response);
    }
    
    private CommonResponse constructErrorResponse(string errorMessage)
    {
        return new CommonResponse(errorMessage);
    }
}