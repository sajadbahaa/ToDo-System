using DataLayer.Entities;
using DataLayer.Services;
using DTLayer.Data;
using Microsoft.AspNetCore.Identity;

//using DTLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTLayer.Services
{
   static  public  class ServiceExtensions
    {
        //        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        //        {
        //            // قراءة الـ Connection String من appsettings.json
        //            var connectionString = configuration.GetConnectionString("DefaultConnection");

        //            services.AddDbContext<AppDbContext>(options =>
        //                options.UseSqlServer(connectionString));



        //            services.AddDataProtection();                     // ✅ لحل IDataProtectionProvider
        //            services.AddSingleton<TimeProvider>(TimeProvider.System); // ✅ لحل TimeProvider



        //            // ✅ تسجيل كامل لخدمات الهوية
        //            services.AddIdentity<AppUser, IdentityRole>(options =>
        //            {
        //                options.User.RequireUniqueEmail = true;
        //                options.Password.RequireDigit = false;
        //                options.Password.RequireUppercase = false;
        //                options.Password.RequireLowercase = false;
        //                options.Password.RequireNonAlphanumeric = false;
        //                options.Password.RequiredLength = 6;
        //            })
        //        //الأول: يخبر Identity وين يخزن كل شيء في DB.

        //          .AddEntityFrameworkStores<AppDbContext>()

        ////الثاني: يخلي Identity قادر يولد Tokens لأي عملية تتعلق بالمصادقة والأمان.
        //.AddDefaultTokenProviders();

        //            return services;
        //        }


        public class DatabaseConfigurator : IDatabaseConfigurator
        {
            public void Configure(IServiceCollection services, IConfiguration configuration)
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");

                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(connectionString));

                services.AddIdentity<AppUser, IdentityRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequiredLength = 6;
                    

                    options.Password.RequireDigit = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            }
        }


    }

}
