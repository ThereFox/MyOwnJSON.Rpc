using ThereFox.JsonRPC;
using ThereFox.JsonRPC.Core.Client;

namespace Example.Controllers;

[RPCController]
public class ClientTester
{
    private readonly JSONRpcClient _client;

    public ClientTester(JSONRpcClient client)
    {
        _client = client;
    }

    public async Task DoCall()
    {
        var callResult = await _client.CallAsync<string>("CustomActionName", [123, "Test"]);
        
        return;
    }
    
}