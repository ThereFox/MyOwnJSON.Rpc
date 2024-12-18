using System.Reflection;
using CSharpFunctionalExtensions;
using ThereFox.JsonRPC.Interfaces;

namespace ThereFox.JsonRPC.AspNet.Register.DIRegister;

public class RPCControllerRegister
{
    private Dictionary<string, MethodInfo> _endpointsToControllers { get; } = new();
    private HashSet<Type> _controllerTypes { get; } = new();

    public RPCControllerRegister Register<T>() => Register(typeof(T));
    public RPCControllerRegister Register(Type controllerType)
    {
        if (_controllerTypes.Contains(controllerType))
        {
            return this;
        }
        
        

        if (isRpcController(controllerType) == false)
        {
            throw new InvalidCastException($"controller type {controllerType} is not a RPC controller");
        }

        registerControllerActions(controllerType);
        _controllerTypes.Add(controllerType);

        return this;
    }

    public Result<MethodInfo> GetMethodByActionName(string action)
    {
        if(_endpointsToControllers.ContainsKey(action.ToLower()) == false)
        {
            return Result.Failure<MethodInfo>("action not found");
        }

        return Result.Success(_endpointsToControllers[action.ToLower()]);
    }
    
    private void registerControllerActions(Type controllerType)
    {
        var actions = controllerType
            .GetMethods()
            .Where(
                ex => 
                    ex.IsPublic &&
                    (ex.DeclaringType == controllerType || isRpcController(ex.DeclaringType)) &&
                    ex.IsConstructor == false &&
                    ex.IsAbstract == false &&
                    ex.IsStatic == false &&
                    ex.IsGenericMethodDefinition == false &&
                    ex
                        .GetCustomAttributes(true)
                        .All(ex => ex.GetType() != typeof(NotRPCActionAttribute)
                )
            );

        foreach (var action in actions)
        {
            var hasCumstomName = hasCustomName(action);

            var name = hasCumstomName ? getCustomActionName(action) : action.Name;

            if (_endpointsToControllers.ContainsKey(name.ToLower()))
            {
                throw new Exception($"Duplicate action name: {name}");
            }

            _endpointsToControllers.Add(name.ToLower(), action);
        }
    }

    private bool isRpcController(Type type)
    {
        return type
            .GetCustomAttributes(true)
            .Any(ex => ex is RPCControllerAttribute);
    }
    
    private bool hasCustomName(MethodInfo method)
    {
        return method
            .GetCustomAttributes(true)
            .Any(ex => ex.GetType() == typeof(RPCActionAttribute));
    }

    private string getCustomActionName(MethodInfo method)
    {
        var attribute = (RPCActionAttribute)(method
            .GetCustomAttributes(true)
            .First(ex => ex.GetType() == typeof(RPCActionAttribute))
            );
        return attribute.CustomActionName;
    }
    
}