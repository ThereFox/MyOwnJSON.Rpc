using Microsoft.Extensions.DependencyInjection;

namespace ThereFox.JsonRPC.Core.Client;

public static class DI
{
    public static IServiceCollection RegistrateJSONRpcClient(this IServiceCollection serviceCollection, string url)
    {
        serviceCollection.AddScoped<JSONRpcClient>(

            ex =>
            {
                var URI = new Uri(url);
                return new JSONRpcClient(URI);
            }
        );

        return serviceCollection;
    }
}