using Microsoft.Extensions.DependencyInjection;

namespace ThereFox.JsonRPC;

public static class DI
{
    public static IServiceCollection AddJSONRPCHandlers(this IServiceCollection services)
    {
        services.AddScoped<RequestHandler>();
        services.AddScoped<RequestParser>();
        services.AddScoped<ArgumentsValidator>();
        
        return services;
    }
}