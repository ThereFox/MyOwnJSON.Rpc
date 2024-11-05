using ThereFox.JsonRPC;
using ThereFox.JsonRPC.AspNet.Register;

namespace RegistrationTests.TestControllers;

[RPCController]
public class SimpleController : RPCController
{
    public void SampleActionName()
    {
        return;
    }

    public string SampleActionNameWithReturnType()
    {
        return "TestValue";
    }

    public async Task SampleActionNameAsync()
    {
        await Task.CompletedTask;
        return;
    }
    
    public async Task<string> SampleActionNameWithReturnTypeAsync()
    {
        await Task.CompletedTask;
        return "TestValue";
    }
    
}