using CSharpFunctionalExtensions;
using ThereFox.JsonRPC.Request;

namespace ThereFox.JsonRPC.Interfaces;

public interface IRequestParser
{
    public Result<RequestContent> Parse(string requestBody);
}