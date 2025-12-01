using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace NetflixClone.WebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebAPI(this IServiceCollection services)
        {
            // Register MediatR
            services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}