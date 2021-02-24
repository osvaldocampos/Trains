using Microsoft.Extensions.DependencyInjection;

namespace Trains.DependencyResolution
{
    public static class ServicesRegistry
    {
        public static IServiceCollection Register(this IServiceCollection services)
        {
            return services;
        }
    }
}
