namespace ThereFox.JsonRPC.Request;

public class ArgumentValue
{
    public string? Name { get; }
    public object Value { get; }

    public ArgumentValue(string name, object value)
    {
        Name = name;
        Value = value;
    }

    public ArgumentValue(object value)
    {
        Name = null;
        Value = value;
    }
}