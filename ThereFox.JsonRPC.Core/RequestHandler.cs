namespace ThereFox.JsonRPC;

public class RequestHandler
{
    private readonly RequestParser _requestParser;
    private readonly ArgumentsValidator _argumentsValidator;
    
    
    public Task<string> Handle(string request)
    {
        var requestContent = _requestParser.Parse(request);
        
        
        
        _argumentsValidator.ValidateValues();
    }
}