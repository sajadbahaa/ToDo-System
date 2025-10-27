using Microsoft.Extensions.DependencyInjection;

namespace BussinesLayer.ServiceCollection
{
   static public class ServiceCollection
    {
        public static IServiceCollection addBussinesServices (this IServiceCollection services)
        {
            services.AddScoped<UserServices>();
            services.AddScoped<ToDoTaskServices>();

            return services;
        }

    }
}
