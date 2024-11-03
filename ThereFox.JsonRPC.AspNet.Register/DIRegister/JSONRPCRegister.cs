using Microsoft.AspNetCore.Mvc;
using ThereFox.JsonRPC.AspNet.Register.Filtrs;
using ThereFox.JsonRPC.AspNet.Register.Responses;
using ThereFox.JsonRPC.Interfaces;

namespace ThereFox.JsonRPC.AspNet.Register.DIRegister;

public static class JSONRPCRegister
{
    public static IServiceCollection AddJsonRPC(this IServiceCollection services)
    {
        services.AddJSONRPCHandlers();
        
        services.AddSingleton<RPCControllerRegister>();
        services.AddScoped<IActionInfoStore, ActionInfoGetter>();
        services.AddScoped<IActionExecutor, ActionExecutor>();
        
        return services;
    }

    public static IServiceCollection AddJsonRPCStandartResponseFormatter(this IServiceCollection services)
    {
        services.AddTransient<ResponseFormatter>();

        return services;
    }

    public static IServiceCollection AddCustomJsonRPCResponseFormatter<T>(this IServiceCollection services)
        where T : class
    {
        services.AddTransient<T>();

        return services;
    }
    
    public static IServiceProvider RegistrateActionController<T>(this IServiceProvider services)
        where T: class
    {
        var service = services.GetService<RPCControllerRegister>();

        service.Register<T>();
        
        return services;
    }
    
    public static WebApplication MapJsonRPCRoute(this WebApplication services, string route)
    {
        services
            .MapPost(route, async () => await Task.Delay(5000))
            .AddEndpointFilter<ActionFiltr>();
        
        return services;
    }
}