#nullable disable
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TargetChatServer11.Data;
using TargetChatServer11.Interfaces;
using TargetChatServer11.Models;
using TargetChatServer11.Utils;

namespace targetchatserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _users;
        private readonly INotificationRepository _notificationRepository;
        private IConfiguration _configuration;

        public UsersController(IUserRepository users, IConfiguration config, INotificationRepository notificationRepository)
        {
            _users = users;
            _configuration = config;
            _notificationRepository = notificationRepository;
        }

        private string getUserName()
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var userClaims = identity.Claims;

                return userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;

            }
            return null;
        }

        private string Generate(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTParams:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
            };

            var token = new JwtSecurityToken(_configuration["JWTParams:Issuer"],
                _configuration["JWTParams:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // POST: api/Users/login
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<Session>> Login([FromBody] UserLogin userLogin)
        {
            var user = await _users.Authenticate(userLogin);
            if (user == null)
            {
                return NotFound("Invalid details");
            }
            var token = Generate(user);
            return Ok(new Session { user = user, token = token });


        }

        // POST: api/Users/token
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [AllowAnonymous]
        [HttpGet("token")]
        public async Task<ActionResult<UserModel>> Token()
        {
            var userString = getUserName();
            if (userString == null)
            {
                return NotFound("Invalid details");
            }

            var user = await _users.GetUserByUsername(userString);

            return Ok(user);
        }

        // POST: api/Users/register
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserModel user)
        {
            if (await _users.GetUserByUsername(user.Username) != null)
            {
                return BadRequest("User Exists!");
            }

            if (await _users.CreateUser(user) != null)
            {
                var token = Generate(user);
                return Ok(token);
            }

            return BadRequest("Error adding the user");
        }

        [Authorize]
        [HttpPost("registerDevice")]
        public async Task<IActionResult> registerDevice(AndroidDeviceIDModel androidDeviceIDModel)
        {
            var userString = getUserName();
            if (userString == null)
            {
                return NotFound("Invalid details");
            }

            var androidDeviceID = new AndroidDeviceIDModel
            {
                DeviceId = androidDeviceIDModel.DeviceId
            };

            await _notificationRepository.CreateAndoridDeviceOfUser(androidDeviceID, userString);
            return Ok();
        }
    }

}
