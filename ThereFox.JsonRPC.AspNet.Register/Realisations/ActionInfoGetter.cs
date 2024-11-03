using CSharpFunctionalExtensions;
using ThereFox.JsonRPC.Actions;
using ThereFox.JsonRPC.Interfaces;
using ThereFox.JsonRPC.Request;

namespace ThereFox.JsonRPC.AspNet.Register.DIRegister;

public class ActionInfoGetter : IActionInfoStore
{
    private readonly RPCControllerRegister _register;

    public ActionInfoGetter(RPCControllerRegister controllers)
    {
        _register = controllers;
    }
    
    public Result<ActionInfo> GetActionInfo(string actionName)
    {
        var getMethodResult = _register.GetMethodByActionName(actionName);

        if (getMethodResult.IsFailure)
        {
            return getMethodResult.ConvertFailure<ActionInfo>();
        }
        
        var method = getMethodResult.Value;

        var returnedType = method.ReturnType;

        var resultArguments = new List<AwaitedArguments>();

        foreach (var arguments in method.GetParameters())
        {
            var argumentName = arguments.Name;
            var argumentType = arguments.ParameterType;
            var hasDefaultValue = arguments.HasDefaultValue;

            if (hasDefaultValue)
            {
                var defaultValue = arguments.DefaultValue;

                resultArguments.Add(new AwaitedArguments(argumentName, argumentType, defaultValue));
            }
            else
            {
                resultArguments.Add(new AwaitedArguments(argumentName, argumentType));
            }
        }

        return Result.Success( new ActionInfo(returnedType, resultArguments));
    }
}