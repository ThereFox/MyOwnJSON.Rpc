using Microsoft.AspNetCore.Mvc;
using ThereFox.JsonRPC;

namespace Example.Controllers;

[RPCController]
public class SimpleController
{

    public void Action()
    {
        return;
    }
    [RPCAction("CustomActionName")]
    public string Do(int id, string appendedValue)
    {
        return "test";
    }
}