using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JwtApp.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace JwtApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);
            if(user !=  null)
            {
                var token = Generate(user);
                return Ok(token);
            }
            return NotFound("User Not Found");
        }

        private string Generate(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials =new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            var clames = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.EmailAdress),
                new Claim(ClaimTypes.Surname, user.Surname),
                new Claim(ClaimTypes.GivenName, user.GivenName),
                new Claim(ClaimTypes.Role, user.Role),
            };
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                    clames,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: credentials
                ); ;
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private  UserModel Authenticate(UserLogin userLogin)
        {
            var currentUser = UserConstants.Users.FirstOrDefault(o => o.UserName.Equals(userLogin.UserName, StringComparison.CurrentCultureIgnoreCase) && o.Password == userLogin.Password);
            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }
    }
}
