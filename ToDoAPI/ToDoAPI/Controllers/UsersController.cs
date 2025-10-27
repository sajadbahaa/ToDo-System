using BussinesLayer;
using BussinesLayer.Services.Jwt;
using DataLayer.Entities;
using Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserServices _userServices;
        readonly IJwtServices _jwtServices;
        public UsersController(UserServices userServices,IJwtServices jwtServices)
        {
            _userServices = userServices;
            _jwtServices = jwtServices;
        }

        // POST: api/Users/login
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {

            var (user, roles) = await _userServices.LogainAsync(request.userName, request.password);

            if (user == null)
                return Unauthorized("Invalid credentials.");
            // هنا تولّد JWT
            var token = _jwtServices.GenerateTokenAsync(user, roles);

            return Ok(new
            {
                token,
                username = user.UserName,
                roles
            });

        }
    }

}
