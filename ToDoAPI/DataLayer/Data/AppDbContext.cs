
using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DTLayer.Data
{
    public class AppDbContext : IdentityDbContext<AppUser> 
        
        // UserRole>

    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
         
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            //modelBuilder.ApplyConfiguration(new AppUserConfiguration());
        }
        public DbSet<TodoTask> todoTasks { get; set; }
    }
}
