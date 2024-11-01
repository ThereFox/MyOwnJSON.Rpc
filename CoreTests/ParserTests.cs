using ThereFox.JsonRPC;

namespace CoreTests;

public class ParseTests
{
    [Fact]
    public void Parse_ParseSimpleRequest_ShouldReturnCorrectResponse()
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
    public void Parse_ParseRequestWithOrderedParams_ShouldReturnCorrectResponse()
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
    public void ParseRequestWithNamedParams_ShouldReturnCorrectResponse()
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

    [Fact]
    public void Parse_ParseRequestWithNullParams_ShouldReturnCorrectResponse()
    {
        var request = "{ \"jsonrpc\" : \"2.0\", \"method\":\"Name\", \"params\": null }";
        var requestParser = new RequestParser();

        var parseValueResult = requestParser.Parse(request);
        
        Assert.True(parseValueResult.IsSuccess);
        
        var parsedValue = parseValueResult.Value;
        
        Assert.Equal("2.0", parsedValue.Version);
        Assert.Equal("Name", parsedValue.ActionName);
        Assert.Empty(parsedValue.Arguments);
    }

    [Fact]
    public void Parse_ParseRequestWithZeroOrderedParams_ShouldReturnCorrectResponse()
    {
        var request = "{ \"jsonrpc\" : \"2.0\", \"method\":\"Name\", \"params\": [] }";
        var requestParser = new RequestParser();

        var parseResult = requestParser.Parse(request);
        
        Assert.True(parseResult.IsSuccess);
        Assert.NotNull(parseResult.Value);
        Assert.Equal("2.0", parseResult.Value.Version);
        Assert.Equal("Name", parseResult.Value.ActionName);
        Assert.Equal(0, parseResult.Value.Arguments.Count);
    }
    [Fact]
    public void Parse_ParseRequestWithOneOrderedParams_ShouldReturnCorrectResponse()
    {
        var request = "{ \"jsonrpc\" : \"2.0\", \"method\":\"Name\", \"params\": [ \"123\" ] }";
        var requestParser = new RequestParser();

        var parseResult = requestParser.Parse(request);
        
        Assert.True(parseResult.IsSuccess);
        Assert.NotNull(parseResult.Value);
        Assert.Equal("2.0", parseResult.Value.Version);
        Assert.Equal("Name", parseResult.Value.ActionName);
        Assert.Equal(1, parseResult.Value.Arguments.Count);
        Assert.Equal("123", parseResult.Value.Arguments.Single().Value);
    }
    [Fact]
    public void Parse_ParseRequestWithSomeOrderedParams_ShouldReturnCorrectResponse()
    {
        var request = "{ \"jsonrpc\" : \"2.0\", \"method\":\"Name\", \"params\": [ \"123\", 213, \"test\" ] }";
        var requestParser = new RequestParser();

        var parseResult = requestParser.Parse(request);
        
        Assert.True(parseResult.IsSuccess);
        Assert.NotNull(parseResult.Value);
        Assert.Equal("2.0", parseResult.Value.Version);
        Assert.Equal("Name", parseResult.Value.ActionName);
        Assert.Equal(3, parseResult.Value.Arguments.Count);
        Assert.Equal("123", parseResult.Value.Arguments[0].Value);
        Assert.Equal((long)213, (long)parseResult.Value.Arguments[1].Value);
        Assert.Equal("test", parseResult.Value.Arguments[2].Value);
    }
    
    [Theory]
    [InlineData("{ \"jsonrpc\" : \"2.0\", \"method\":\"Name\", \"params\":[ }")]
    [InlineData("{ \"jsonrpc\" : \"2.0\", \"method\":\"Name\", \"params\":] }")]
    [InlineData("{ \"jsonrpc\" : \"2.0\", \"method\":\"Name\", \"params\":[] ")]
    [InlineData(" \"jsonrpc\" : \"2.0\", \"method\":\"Name\", \"params\":] }")]
    [InlineData("{ \"jsonrpc\" : \"2.0\", \"method\":\"Name\",, \"params\":[] }")]
    [InlineData("{ \"jsonrpc\" : , \"method\":\"Name\", \"params\":[] }")]
    [InlineData("{ \"method\":\"Name\", \"params\":[] }")]
    [InlineData("{ \"jsonrpc\" : \"2.0\", \"params\":[] }")]
    [InlineData("{ \"jsonrpc\" : \"2.0\" }")]
    [InlineData("{ \"jsonrpc\" : \"2.0\", \"method\":\"Name\" }")]
    public void Parse_ParseInvalidRequest_ShouldReturnCorrectFailure(string request)
    {
        var requestParser = new RequestParser();

        var parseValueResult = requestParser.Parse(request);
        
        Assert.True(parseValueResult.IsFailure);
        }

}