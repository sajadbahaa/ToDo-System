using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositary.Services
{
    public static  class RepoServices
    {
        public static IServiceCollection AddRepoServices(this IServiceCollection services)
        {
            services.AddScoped<AppUserRepo>();
            services.AddScoped<ToDoTaskRepo>();
            return services;
        }
    }
}
