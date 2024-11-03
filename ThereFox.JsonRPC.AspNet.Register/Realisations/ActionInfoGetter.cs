using CSharpFunctionalExtensions;
using ThereFox.JsonRPC.Actions;
using ThereFox.JsonRPC.Interfaces;

namespace ThereFox.JsonRPC.AspNet.Register.DIRegister;

public class ActionInfoGetter : IActionInfoStore
{
    private readonly PRCControllerRegister _register;

    public ActionInfoGetter(PRCControllerRegister controllers)
    {
        _register = controllers;
    }
    
    public Result<ActionInfo> GetActionInfo(string actionName)
    {
        var method = _register.GetControllerTypeByAction(actionName);
        
        
        
    }
}