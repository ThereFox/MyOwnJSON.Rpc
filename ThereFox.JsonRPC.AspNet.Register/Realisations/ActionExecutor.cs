using System.Reflection;
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
        if (method.ReturnType is Task)
        {
            try
            {
                var result = await (Task<object>)method.Invoke(controller, arguments.ToArray());
                return result;
            }
            catch (Exception e)
            {
                return Result.Failure<object>(e.Message);
            }
        }

        try
        {
            var asyncCallSync = await Task.Run(() => method.Invoke(controller, arguments.Select(ex => ex.Value).ToArray()));

            return asyncCallSync;
        }
        catch (Exception e)
        {
            return Result.Failure<object>(e.Message);
        }
    }
}