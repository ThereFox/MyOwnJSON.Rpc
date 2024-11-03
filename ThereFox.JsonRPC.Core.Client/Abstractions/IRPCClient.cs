namespace ThereFox.JsonRPC.Core.Client.Abstractions;

public interface IRPCClient
{
    public Task<TResult> CallAsync<TResult>(string method, params object[] args);
}