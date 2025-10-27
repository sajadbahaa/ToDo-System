using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Configurations
{
    public  class UserRplesConfigurations:IEntityTypeConfiguration<IdentityUserRole<string>>
    {

        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            // Assign Admin role to the existing user-admin
            builder.HasData(
                new IdentityUserRole<string>
                {
                    UserId = "user-admin",
                    RoleId = "role-admin"
                }
                , new IdentityUserRole<string>
                {
                    UserId = "user-normal",
                    RoleId = "role-user"
                }
            );
        }
    }
}
