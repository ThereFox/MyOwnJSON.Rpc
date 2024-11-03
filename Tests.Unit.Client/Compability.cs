using ThereFox.JsonRPC;
using ThereFox.JsonRPC.Core.Client;

namespace Tests.Unit.Client;

public class Compability
{
    private readonly JsonRPCRequestBuilder _sut;
    private readonly RequestParser _corrector;

    public Compability()
    {
        _sut = new JsonRPCRequestBuilder();
        _corrector = new RequestParser();
    }

    [Fact]
    public void Client_ParserReadRequest_WithoutParams__ShoultSucsesfully()
    {
        var method = "MethodName";

        var requestJson = _sut.CreateRequest(method, new List<object>());
        var requestContent = _corrector.Parse(requestJson);
        
        Assert.True(requestContent.IsSuccess);
        Assert.Empty(requestContent.Value.Arguments);
        Assert.Equal(method, requestContent.Value.ActionName);
    }
    
    [Fact]
    public void Client_ParserReadRequest_WithOneArguments__ShoultSucsesfully()
    {
        var method = "MethodName";

        var requestJson = _sut.CreateRequest(method, new List<object>() { "1" });
        var requestContent = _corrector.Parse(requestJson);
        
        Assert.True(requestContent.IsSuccess);
        Assert.NotEmpty(requestContent.Value.Arguments);
        Assert.Equal(1, requestContent.Value.Arguments.Count());
        Assert.Equal("1", requestContent.Value.Arguments.First().Value);
        Assert.Equal(method, requestContent.Value.ActionName);
    }
    
    [Fact]
    public void Client_ParserReadRequest_WithSomeArguments__ShoultSucsesfully()
    {
        var method = "MethodName";

        var requestJson = _sut.CreateRequest(method, new List<object>() { "1", 3, "3" });
        var requestContent = _corrector.Parse(requestJson);
        
        Assert.True(requestContent.IsSuccess);
        Assert.NotEmpty(requestContent.Value.Arguments);
        Assert.Equal(3, requestContent.Value.Arguments.Count());
        Assert.Equal("1", requestContent.Value.Arguments[0].Value);
        Assert.Equal((long)3, requestContent.Value.Arguments[1].Value);
        Assert.Equal("3", requestContent.Value.Arguments[2].Value);
        Assert.Equal(method, requestContent.Value.ActionName);
    }
    
    
}