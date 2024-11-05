using Microsoft.Extensions.DependencyInjection;
using ThereFox.JsonRPC.ValueConverter;

namespace ThereFox.JsonRPC;

public static class DI
{
    public static IServiceCollection AddJSONRPCHandlers(this IServiceCollection services)
    {
        services.AddScoped<RequestHandler>();
        services.AddScoped <ArgumentConverter>();
        services.AddScoped<RequestParser>();
        services.AddScoped<ArgumentsValidator>();
        
        return services;
    }
}