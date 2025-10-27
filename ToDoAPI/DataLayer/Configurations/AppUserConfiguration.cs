using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName)
               .HasMaxLength(30)      // ← هنا تحدد الطول الجديد
               .IsRequired()
               .HasColumnType("varchar");


            builder.Property(x => x.Email)
               .HasMaxLength(30)      // ← هنا تحدد الطول الجديد
               .IsRequired()
               .HasColumnType("varchar");


            builder.Property(x => x.NormalizedEmail)
               .HasMaxLength(30)      // ← هنا تحدد الطول الجديد
               .HasColumnType("varchar");


            builder.Property(x => x.ConcurrencyStamp)
               .HasMaxLength(256)      // ← هنا تحدد الطول الجديد
               .HasColumnType("varchar");

            builder.Property(x => x.NormalizedUserName)
               .HasMaxLength(30)      // ← هنا تحدد الطول الجديد
               .HasColumnType("varchar");

            // تغيير طول Password (اختياري)
            builder.Property(x => x.PasswordHash)
                   .HasMaxLength(256)
                   .HasColumnName("PasswordHash");
            // الافتراضي طويل جداً، إذا تقصره تأكد أنه كافي للـ Hash
            // معرفات ثابتة (نفسها تُستخدم في المهام)
            var adminId = "user-admin";
            var userId = "user-normal";

            // توليد كلمات مرور مشفرة مرة واحدة فقط
            var hasher = new PasswordHasher<AppUser>();

            var adminPassword = hasher.HashPassword(null, "Admin123!");
            var userPassword = hasher.HashPassword(null, "User123!");

            // القيم الثابتة (بدون DateTime.UtcNow أو Guid.NewGuid)
            
            builder.HasData(
               new AppUser
               {
                   Id = adminId,
                   UserName = "admin",
                   NormalizedUserName = "ADMIN",
                   Email = "admin@todo.com",
                   NormalizedEmail = "ADMIN@TODO.COM",
                   EmailConfirmed = true,
                   CreateAt = new DateTime(2025, 10, 27, 0, 0, 0, DateTimeKind.Utc),
                   UpdateAt = new DateTime(2025, 10, 27, 0, 0, 0, DateTimeKind.Utc),
                   SecurityStamp = "fixed-securitystamp-1",
                   ConcurrencyStamp = "fixed-concurrencystamp-1",
                   PasswordHash = "AQAAAAIAAYagAAAAEFnPoIcV8horRkKxGkVEH8SrA5g93qVWQmPxfcXHfOsi0iFlZfJIh2gzDhvt3c6GnQ==" // مسبقاً مولد
               },
    new AppUser
    {
        Id = userId,
        UserName = "user",
        NormalizedUserName = "USER",
        Email = "user@todo.com",
        NormalizedEmail = "USER@TODO.COM",
        EmailConfirmed = true,
        CreateAt = new DateTime(2025, 10, 27, 0, 0, 0, DateTimeKind.Utc),
        UpdateAt = new DateTime(2025, 10, 27, 0, 0, 0, DateTimeKind.Utc),
        SecurityStamp = "fixed-securitystamp-2",
        ConcurrencyStamp = "fixed-concurrencystamp-2",
        PasswordHash = "AQAAAAIAAYagAAAAEIAGNE7EugpOqbWYVidEjyXBjeZjfLXwG0i/hlY+SmNF1+L4IJCKiOlSYKhd3iySuw=="
    }
            );



        }
    }
}
