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

    public AwaitedArguments(string name, Type type)
    {
        Name = name;
        Type = type;
        HasDefaultValue = false;
    }

    public AwaitedArguments(string name, Type type, object? defaultValue)
    {
        Name = name;
        Type = type;
        _defaultValue = defaultValue;
        HasDefaultValue = true;
    }
}