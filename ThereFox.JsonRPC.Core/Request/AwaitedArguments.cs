namespace ThereFox.JsonRPC.Request;

public class AwaitedArguments
{
    private readonly object? _defaultValue;
    
    public string Name { get; }
    public Type Type { get; }
    public bool HasDefaultValue { get; }

    public object? DefaultValue
    {
        get
        {
            if (HasDefaultValue == false)
            {
                throw new InvalidOperationException();
            }

            return _defaultValue;
        }
    }
}