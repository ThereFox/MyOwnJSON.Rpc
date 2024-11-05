using Microsoft.Extensions.DependencyInjection;
using RegistrationTests.TestControllers;
using ThereFox.JsonRPC;
using ThereFox.JsonRPC.AspNet.Register.DIRegister;
using ThereFox.JsonRPC.Core.Client;
using ThereFox.JsonRPC.ValueConverter;

namespace RegistrationTests;

public class SimpleControllerTests
{
    private readonly RequestHandler _sut;
    private readonly JsonRPCRequestBuilder _requestBuilder;
    
    public SimpleControllerTests()
    {
        var parser = new RequestParser();
        var validator = new ArgumentsValidator(new ArgumentConverter());
        var register = new RPCControllerRegister();

        register.Register<SimpleController>();
        
        var defaultActionInfoStore = new ActionInfoGetter(register);

        var services = new ServiceCollection();
        var provider = new DefaultServiceProviderFactory().CreateServiceProvider(services);
        
        var executor = new ActionExecutor(register, provider.GetRequiredService<IServiceScopeFactory>());
        
        _sut = new RequestHandler(parser, validator, defaultActionInfoStore, executor);
        _requestBuilder = new JsonRPCRequestBuilder();
    }
    
    [Fact]
    public async Task RequestHandler_RunMethodWithoutParamsAndWithVoidReturnType_()
    {
        var actionName = "SampleActionName";
        var request = _requestBuilder.CreateRequest(actionName, new List<object>());
        
        var handlResult = await _sut.HandleAsync(request);
        
        Assert.True(handlResult.IsSucsessful);
        Assert.Null(handlResult.Value);
        Assert.Null(handlResult.ErrorSuggestion);
    }
    
    [Fact]
    public async Task RequestHandler_RunMethodWithoutParamsAndWithStringReturnType_()
    {
        var actionName = "SampleActionNameWithReturnType";
        var request = _requestBuilder.CreateRequest(actionName, new List<object>());
        
        var handlResult = await _sut.HandleAsync(request);
        
        Assert.True(handlResult.IsSucsessful);
        Assert.NotNull(handlResult.Value);
        Assert.Equal("TestValue", handlResult.Value);
    }
    
    [Fact]
    public async Task RequestHandler_RunMethodWithoutParamsAndWithTaskReturnType_()
    {
        var actionName = "SampleActionNameAsync";
        var request = _requestBuilder.CreateRequest(actionName, new List<object>());
        
        var handlResult = await _sut.HandleAsync(request);
        
        Assert.True(handlResult.IsSucsessful);
        Assert.Null(handlResult.Value);
    }
    
    [Fact]
    public async Task RequestHandler_RunMethodWithoutParamsAndWithTaskStringReturnType_()
    {
        var actionName = "SampleActionNameWithReturnTypeAsync";
        var request = _requestBuilder.CreateRequest(actionName, new List<object>());
        
        var handlResult = await _sut.HandleAsync(request);
        
        Assert.True(handlResult.IsSucsessful);
        Assert.NotNull(handlResult.Value);
        Assert.Equal("TestValue", handlResult.Value);
    }
    
    [Fact]
    public async Task RequestHandler_RunMethodWitParamsAndWithVoidReturnType_()
    {
        var actionName = "SampleActionNameWithParam";
        var request = _requestBuilder.CreateRequest(actionName, new List<object>() { "test" });
        
        var handlResult = await _sut.HandleAsync(request);
        
        Assert.True(handlResult.IsSucsessful);
        Assert.Null(handlResult.Value);
        Assert.Null(handlResult.ErrorSuggestion);
    }
    
    [Fact]
    public async Task RequestHandler_RunMethodWithParamsAndWithReturnType_()
    {
        var argument = "test";
        
        var actionName = "SampleActionNameWithParamAndReturnType";
        var request = _requestBuilder.CreateRequest(actionName, new List<object>() { argument });
        
        var handlResult = await _sut.HandleAsync(request);
        
        Assert.True(handlResult.IsSucsessful);
        Assert.NotNull(handlResult.Value);
        Assert.Equal(argument, handlResult.Value);
    }
    
    [Fact]
    public async Task RequestHandler_RunMethodWithParamsAndWithoutReturnTypeAsync_()
    {
        var argument = "test";
        
        var actionName = "SampleActionNameWithParamAsync";
        var request = _requestBuilder.CreateRequest(actionName, new List<object>() { argument });
        
        var handlResult = await _sut.HandleAsync(request);
        
        Assert.True(handlResult.IsSucsessful);
        Assert.Null(handlResult.Value);
    }
    
    [Fact]
    public async Task RequestHandler_RunMethodWithParamsAndWithReturnTypeAsync_()
    {
        var argument = "test";
        
        var actionName = "SampleActionNameWithParamAndReturnTypeAsync";
        var request = _requestBuilder.CreateRequest(actionName, new List<object>() { argument });
        
        var handlResult = await _sut.HandleAsync(request);
        
        Assert.True(handlResult.IsSucsessful);
        Assert.NotNull(handlResult.Value);
        Assert.Equal(argument, handlResult.Value);
    }
    
}