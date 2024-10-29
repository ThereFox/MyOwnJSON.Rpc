namespace ThereFox.JsonRPC.Request;

public class RequestContent
{
    public string Version { get; }
    public string ActionName { get; }
    
    public IReadOnlyList<ArgumentValue> Arguments { get; }

    public RequestContent(string version, string actionName, List<ArgumentValue> argument)
    {
        Version = version;
        ActionName = actionName;
        Arguments = argument;
    }
    
}