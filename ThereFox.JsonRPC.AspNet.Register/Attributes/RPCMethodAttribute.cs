namespace ThereFox.JsonRPC;

public class RPCActionAttribute : Attribute
{
    public string CustomActionName { get; }

    public RPCActionAttribute(string actionName)
    {
        CustomActionName = actionName;
    }
}