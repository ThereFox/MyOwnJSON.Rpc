using ThereFox.JsonRPC;
using ThereFox.JsonRPC.Request;

namespace CoreTests;

public class ValidatorTests
{
    [Fact]
    public void Validate_FullCompareNamedParam_ShouldReturnTrue()
    {
        var values = new List<ArgumentValue>()
        {
            new ArgumentValue("Test", "213")
        };
        var awaitedArguments = new List<AwaitedArguments>()
        {
            new AwaitedArguments("Test", typeof(string))
        };
        var Validator = new ArgumentsValidator();
        
        var validateResult = Validator.ValidateValues(values, awaitedArguments);
        
        Assert.True(validateResult.IsSuccess);
        Assert.True(validateResult.Value.Count == 1);
        Assert.Equal("213", validateResult.Value.Single().Value);
        Assert.Equal("Test", validateResult.Value.Single().Name);
    }
    [Fact]
    public void Validate_NamedParamNotAllWithDefaultValue_ShouldReturnTrue()
    {
        var values = new List<ArgumentValue>()
        {
            new ArgumentValue("Test", "213")
        };
        var awaitedArguments = new List<AwaitedArguments>()
        {
            new AwaitedArguments("Test", typeof(string)),
            new AwaitedArguments("NotTest", typeof(string), "empty")
        };
        var Validator = new ArgumentsValidator();
        
        var validateResult = Validator.ValidateValues(values, awaitedArguments);
        
        Assert.True(validateResult.IsSuccess);
        Assert.True(validateResult.Value.Count == 2);
        Assert.Equal("Test", validateResult.Value.First().Name);
        Assert.Equal("213", validateResult.Value.First().Value);
        Assert.Equal("NotTest", validateResult.Value.Last().Name);
        Assert.Equal("empty", validateResult.Value.Last().Value);
    }
}