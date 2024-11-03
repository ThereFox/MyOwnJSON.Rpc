using CSharpFunctionalExtensions;
using ThereFox.JsonRPC.Actions;

namespace ThereFox.JsonRPC.Interfaces;

public interface IActionInfoStore
{
    public Result<ActionInfo> GetActionInfo(string actionName);
}