using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
    public interface IDatabaseConfigurator
    {
        void Configure(IServiceCollection services, IConfiguration configuration);
    }
}
