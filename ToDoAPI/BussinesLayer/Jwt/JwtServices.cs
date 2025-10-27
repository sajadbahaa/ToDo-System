using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Services.Jwt
{
    public class JwtServices : IJwtServices
    {
        //        🎯 أولًا: الغرض من هذا الكلاس JwtService

        //كلاس JwtService هو المسؤول عن:

        //توليد(Generate) الـ JWT Token للمستخدم بعد تسجيل الدخول بنجاح.

        //يعني لما المستخدم (Customer / Barber / Admin) يسجل دخوله، راح نتحقق من بياناته
        //وإذا صحيحة، نستخدم JwtService حتى ننشئ له توكن يحتوي على معلوماته وصلاحياته(Claims & Roles).
        private readonly IConfiguration _config; 
        //private readonly UserManager<AppUser> _userManager; 
        public JwtServices(IConfiguration config) 
        { _config = config;  }
        public string GenerateTokenAsync(AppUser user, List<string> roles)
        {
            
            // create claim
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                , new Claim(ClaimTypes.Name, user.UserName ?? string.Empty)
                , new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
            }
            ;
            //SymmetricSecurityKey
            //add role claims 
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r))); 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken
                (
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: creds ); 
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        //JwtSecurityToken




    }
    }
