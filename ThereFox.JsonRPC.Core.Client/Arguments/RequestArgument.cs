namespace ThereFox.JsonRPC.Core.Client.Arguments;

public record RequestArgument
(
    string argumentName,
    object value
);