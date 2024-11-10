# How to use it

## Server

### Declare controller

```c#
[RPCController]
public class ControllerExample
{
    public TOutput ActionName(...args)
    {
        ...
    }
    
    [RPCAction("CustomActionName")]
    public TOutput CustomisedActionName(...args)
    {
        ...
    }
}
```

### Registrate services

#### in Program

```c#
builder.Services
    .AddJsonRPC()
    .AddJsonRPCStandartResponseFormatter();
```

```c#
app.Services.RegistrateActionControllersFromAssembly(typeof(Program).Assembly);
or
app.Services.RegistrateActionController<ControllerExample>();
    
app.MapJsonRPCRoute("test");

```

## Client

### Build-in tools

This lib declare own `JSONRpcClient`.

This class has one constructor with one parametr `URL`.

```c#
var client = new JSONRpcClient("http://...");
```

This client can be registred in DI (and getted afrer it)

registrate
```c#
builder.Services.RegistrateJSONRpcClient($"http://...");
```

it registrate JSONRpcClient as Scoped service.

Call actions looks like:
```c#
_client.CallAsync<ReturnType>(ActionName, Args);
```

### Low level works

#### call directly

Post request by mapped endpoint (`MapJsonRPCRoute`).

Body:
```json
Ordered args
{
  "jsonrpc": "2.0",
  "method": MethodName,
  "params": [argList]
}

or

Named args
{
"jsonrpc": "2.0",
"method": MethodName,
"params":
    {
      "ArgName": argValue,
      ...
    }
}
```


Responses:

```json

OK with result:
{
  "jsonrpc": "2.0",
  "result": "test"
}
        
Ok without result (void):
{
  "jsonrpc": "2.0",
  "additionalInfo": "All OK"
}

Error:
{
  "jsonrpc": "2.0",
  "error": Message
}
```
