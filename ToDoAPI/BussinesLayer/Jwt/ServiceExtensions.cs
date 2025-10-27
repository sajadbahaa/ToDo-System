using BussinesLayer.Services.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Services.Jwt
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddJwtServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IJwtServices, JwtServices>();
            services.AddJwtAuthentication(configuration);
            return services;
        }
    }
}
