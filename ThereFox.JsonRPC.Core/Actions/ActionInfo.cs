using ThereFox.JsonRPC.Request;

namespace ThereFox.JsonRPC.Actions;

public class ActionInfo
{
    public List<AwaitedArguments> Arguments { get; set; }
    public Type ReturnedType { get; set; }

    public ActionInfo(Type returnedType, List<AwaitedArguments> arguments)
    {
        ReturnedType = returnedType;
        Arguments = arguments;
    }
    
}