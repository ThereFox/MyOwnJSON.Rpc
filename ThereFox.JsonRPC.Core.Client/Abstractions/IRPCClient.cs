using CSharpFunctionalExtensions;

namespace ThereFox.JsonRPC.Core.Client.Abstractions;

public interface IRPCClient
{
    public Task<Result<TResult>> CallAsync<TResult>(string method, params object[] args);
}