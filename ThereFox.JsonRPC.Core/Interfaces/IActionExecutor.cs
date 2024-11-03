using CSharpFunctionalExtensions;
using ThereFox.JsonRPC.Request;

namespace ThereFox.JsonRPC.Interfaces;

public interface IActionExecutor
{
    public Task<Result<object>> Execute(string action, List<ArgumentValue> arguments);
}