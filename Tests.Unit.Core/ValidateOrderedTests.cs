using ThereFox.JsonRPC;
using ThereFox.JsonRPC.Request;
using ThereFox.JsonRPC.ValueConverter;

namespace CoreTests;

public class ValidateOrderedTests
{
    private readonly ArgumentsValidator _sut;
    
    public ValidateOrderedTests()
    {
        var converter = new ArgumentConverter();
        _sut = new ArgumentsValidator(converter);
    }
    
    [Fact]
    public void Validate_ValidateOrderedParams_ReturnsCorrectResult()
    {
        var passedArguments = new List<ArgumentValue>()
        {
            new ArgumentValue("123")
        };
        var awaitedArguments = new List<AwaitedArguments>()
        {
            new AwaitedArguments("test", typeof(string))
        };

        var validateResult = _sut.ValidateValues(passedArguments, awaitedArguments);

        Assert.True(validateResult.IsSuccess);
        Assert.Equal(1, validateResult.Value.Count);
        Assert.Equal("test", validateResult.Value[0].Name);
        Assert.Equal("123", validateResult.Value[0].Value);
        
    }
    [Fact]
    public void Validate_ValidateSomeOrderedParams_ReturnsCorrectResult()
    {
        var passedArguments = new List<ArgumentValue>()
        {
            new ArgumentValue("123"),
            new ArgumentValue(123),
        };
        var awaitedArguments = new List<AwaitedArguments>()
        {
            new AwaitedArguments("test", typeof(string)),
            new AwaitedArguments("test2", typeof(int))
        };

        var validateResult = _sut.ValidateValues(passedArguments, awaitedArguments);

        Assert.True(validateResult.IsSuccess);
        Assert.Equal(2, validateResult.Value.Count);
        Assert.Equal("test", validateResult.Value[0].Name);
        Assert.Equal("123", validateResult.Value[0].Value);
        Assert.Equal("test2", validateResult.Value[1].Name);
        Assert.Equal(123, validateResult.Value[1].Value);
        
    }
    
    [Fact]
    public void Validate_ValidateOrderedParamsWithDefaultValue_ReturnsCorrectResult()
    {
        var passedArguments = new List<ArgumentValue>()
        {
            new ArgumentValue("123"),
        };
        var awaitedArguments = new List<AwaitedArguments>()
        {
            new AwaitedArguments("test", typeof(string)),
            new AwaitedArguments("test2", typeof(int), 123)
        };

        var validateResult = _sut.ValidateValues(passedArguments, awaitedArguments);

        Assert.True(validateResult.IsSuccess);
        Assert.Equal(2, validateResult.Value.Count);
        Assert.Equal("test", validateResult.Value[0].Name);
        Assert.Equal("123", validateResult.Value[0].Value);
        Assert.Equal("test2", validateResult.Value[1].Name);
        Assert.Equal(123, validateResult.Value[1].Value);
        
    }
    
    [Fact]
    public void Validate_ValidateOrderedParamsWithOnlyOneDefaultValue_ReturnsCorrectResult()
    {
        var passedArguments = new List<ArgumentValue>()
        {
        };
        var awaitedArguments = new List<AwaitedArguments>()
        {
            new AwaitedArguments("test2", typeof(int), 123)
        };

        var validateResult = _sut.ValidateValues(passedArguments, awaitedArguments);

        Assert.True(validateResult.IsSuccess);
        Assert.Equal(1, validateResult.Value.Count);
        Assert.Equal("test2", validateResult.Value[0].Name);
        Assert.Equal(123, validateResult.Value[0].Value);
    }
    
    [Fact]
    public void Validate_ValidateParamsWithOnlySomeDefaultValue_ReturnsCorrectResult()
    {
        var passedArguments = new List<ArgumentValue>()
        {
        };
        var awaitedArguments = new List<AwaitedArguments>()
        {
            new AwaitedArguments("test1", typeof(int), 123),
            new AwaitedArguments("test2", typeof(string), "123"),
            new AwaitedArguments("test3", typeof(int), 123),
        };

        var validateResult = _sut.ValidateValues(passedArguments, awaitedArguments);

        Assert.True(validateResult.IsSuccess);
        Assert.Equal(3, validateResult.Value.Count);
        Assert.Equal("test1", validateResult.Value[0].Name);
        Assert.Equal(123, validateResult.Value[0].Value);
        Assert.Equal("test2", validateResult.Value[1].Name);
        Assert.Equal("123", validateResult.Value[1].Value);
        Assert.Equal("test3", validateResult.Value[2].Name);
        Assert.Equal(123, validateResult.Value[2].Value);
    }
    
    [Fact]
    public void Validate_ValidateMixedParams_ReturnsCorrectResult()
    {
        var passedArguments = new List<ArgumentValue>()
        {
            new ArgumentValue("test1", 123),
            new ArgumentValue("123")
        };
        var awaitedArguments = new List<AwaitedArguments>()
        {
            new AwaitedArguments("test1", typeof(int)),
            new AwaitedArguments("test2", typeof(string))
        };
        var validateResult = _sut.ValidateValues(passedArguments, awaitedArguments);

        Assert.True(validateResult.IsSuccess);
        Assert.Equal(2, validateResult.Value.Count);
        Assert.Equal("test1", validateResult.Value[0].Name);
        Assert.Equal(123, validateResult.Value[0].Value);
        Assert.Equal("test2", validateResult.Value[1].Name);
        Assert.Equal("123", validateResult.Value[1].Value);
    }
}