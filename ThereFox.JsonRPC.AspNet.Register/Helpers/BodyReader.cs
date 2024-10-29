namespace ThereFox.JsonRPC.AspNet.Register.Helpers;

public static class BodyReader
{
    public static async Task<string> GetBodyContentAsync(this HttpContext context)
    {
        var bodyStream = context.Request.Body;
        var reader = new StreamReader(bodyStream);
        
        var result = await reader.ReadToEndAsync();
        
        reader.Close();
        bodyStream.Close();
        
        return result;
    }
}