using System.Reflection;
using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using ThereFox.JsonRPC.Interfaces;
using ThereFox.JsonRPC.Request;

namespace ThereFox.JsonRPC.AspNet.Register.DIRegister;

public class ActionExecutor : IActionExecutor
{
    private readonly RPCControllerRegister _controllers;
    private readonly IServiceScopeFactory _serviceProvider;

    public ActionExecutor(RPCControllerRegister controllers, IServiceScopeFactory serviceProvider)
    {
        _controllers = controllers;
        _serviceProvider = serviceProvider;
    }
    
    public async Task<Result<object>> Execute(string action, List<ArgumentValue> arguments)
    {
        var getActionMethodResult = _controllers.GetMethodByActionName(action);

        if (getActionMethodResult.IsFailure)
        {
            return getActionMethodResult.ConvertFailure<object>();
        }
        
        var method = getActionMethodResult.Value;
        
        using (var scope = _serviceProvider.CreateAsyncScope())
        {
            var controller = ActivatorUtilities
                .GetServiceOrCreateInstance(scope.ServiceProvider, method.DeclaringType);
            
            var callResult = await ExecuteMethodInControllerAsync(controller, method, arguments);

            return callResult;
        }
    }

    private async Task<Result<object>> ExecuteMethodInControllerAsync<TController>(TController controller, MethodInfo method,
        List<ArgumentValue> arguments)
    {
        try
        {
            if (isAsyncMethod(method))
            {
                return await ExecuteAsyncMethod(controller, method, arguments);
            }

            return await ExecuteSyncMethodInAnoutherThread(controller, method, arguments);
        }
        catch (Exception e)
        {
            return Result.Failure<object>(e.Message);
        }
    }

    private async Task<Result<object>> ExecuteAsyncMethod(object controller, MethodInfo method, List<ArgumentValue> arguments)
    {
        if (
            method.ReturnType == typeof(Task)
            ||
            method.ReturnType == typeof(ValueTask)
        )
        {
                var invokeResult = method.Invoke(controller, arguments.Select(ex => ex.Value).ToArray());
                var task = (Task)invokeResult;
                await task.ConfigureAwait(false);
                return null;
        }

        if (
            method.ReturnType.BaseType == typeof(ValueTask)
            ||
            method.ReturnType.BaseType == typeof(Task)
        )
        {
                var invokeResult = method.Invoke(controller, arguments.Select(ex => ex.Value).ToArray());
                var task = (Task)invokeResult;
                await task.ConfigureAwait(false);
                return (object)((dynamic)task).Result;
        }
        
        return Result.Failure("this should never happen");
    }
    private async Task<Result<object>> ExecuteSyncMethodInAnoutherThread(
        object controller,
        MethodInfo method,
        List<ArgumentValue> arguments)
    {
        return await Task.Run(
            () => method.Invoke(
                controller,
                arguments
                    .Select(ex => ex.Value)
                    .ToArray()
            )
        );
    }

    private bool isAsyncMethod(MethodInfo method)
    {
        return
            method.ReturnType == typeof(Task)
            ||
            method.ReturnType == typeof(ValueTask)
            ||
            method.ReturnType.BaseType == typeof(ValueTask)
            ||
            method.ReturnType.BaseType == typeof(Task);
    }
}