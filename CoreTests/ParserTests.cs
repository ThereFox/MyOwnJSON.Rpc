using ThereFox.JsonRPC;

namespace CoreTests;

public class ParseTests
{
    [Fact]
    public void ParseSimpleRequest()
    {
        var request = "{ \"jsonrpc\" : \"2.0\", \"method\":\"Name\", \"params\":[] }";
        var requestParser = new RequestParser();

        var parseValueResult = requestParser.Parse(request);
        
        Assert.True(parseValueResult.IsSuccess);
        
        var parsedValue = parseValueResult.Value;
        
        Assert.Equal("2.0", parsedValue.Version);
        Assert.Equal("Name", parsedValue.ActionName);
        Assert.Empty(parsedValue.Arguments);

    }
    [Fact]
    public void ParseRequestWithOrderedParams()
    {
        var request = "{ \"jsonrpc\" : \"2.0\", \"method\":\"Name\", \"params\":[\"1\", \"2\"] }";
        var requestParser = new RequestParser();

        var parseValueResult = requestParser.Parse(request);
        
        Assert.True(parseValueResult.IsSuccess);
        
        var parsedValue = parseValueResult.Value;
        
        Assert.Equal("2.0", parsedValue.Version);
        Assert.Equal("Name", parsedValue.ActionName);
        Assert.NotEmpty(parsedValue.Arguments);
        Assert.Equal("1", parsedValue.Arguments.First().Value);
        Assert.Equal("2", parsedValue.Arguments.Last().Value);
    }
    
    [Fact]
    public void ParseRequestWithNamedParams()
    {
        var request = "{ \"jsonrpc\" : \"2.0\", \"method\":\"Name\", \"params\":{\"ParamName\" : \"ParamValue\"} }";
        var requestParser = new RequestParser();

        var parseValueResult = requestParser.Parse(request);
        
        Assert.True(parseValueResult.IsSuccess);
        
        var parsedValue = parseValueResult.Value;
        
        Assert.Equal("2.0", parsedValue.Version);
        Assert.Equal("Name", parsedValue.ActionName);
        Assert.NotEmpty(parsedValue.Arguments);
        Assert.True(parsedValue.Arguments.Count == 1);
        Assert.Equal("ParamName", parsedValue.Arguments.Single().Name);
        Assert.Equal("ParamValue", parsedValue.Arguments.Single().Value);
    }
}