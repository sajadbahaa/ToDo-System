using DataLayer.Entities;
using DTLayer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositary
{
    public class AppUserRepo : Repo<AppUser, string>
    {


        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public AppUserRepo(AppDbContext context, UserManager<AppUser> userManager)
            : base(context)
        {
            _userManager= userManager;

        }

        public async Task<(AppUser?, List<string>?)> LoginAsync(string username, string Password)
        {
            var result = await _context.Users
                .Where(u => u.UserName == username)
                .Join(_context.UserRoles,
                      user => user.Id,
                      ur => ur.UserId,
                      (user, ur) => new { user, ur.RoleId })
                .Join(_context.Roles,
                      combined => combined.RoleId,
                      role => role.Id,
                      (combined, role) => new { combined.user, RoleName = role.Name })
                .GroupBy(x => x.user)
                .Select(g => new
                {
                    User = g.Key,
                    Roles = g.Select(r => r.RoleName).ToList()
                })
                .SingleOrDefaultAsync();

            if (result == null)
                return (null, null);

            if (!await _userManager.CheckPasswordAsync(result.User, Password))
            {
                return (null, null);
            }
            ;
            return (result.User, result.Roles);
        }

    }

}
