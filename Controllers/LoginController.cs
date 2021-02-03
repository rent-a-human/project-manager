using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectManagerJohn.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerJohn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet]
        public IActionResult Login(string username, string pass)
        {
            Users login = new Users();
            login.userName = username;
            login.Password = pass;
            IActionResult response = Unauthorized();

            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenStr = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenStr });
            }

            return response;

        }

        private Users AuthenticateUser (Users login)
        {
            Users user = null;
            if (login.userName == "john" && login.Password == "yayita")
            {
                user = new Users { userName = "john", Email = "lenincomp@gmail.com" };
            }
            return user;
        }

        private string GenerateJSONWebToken(Users userinfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,userinfo.userName),
                new Claim(JwtRegisteredClaimNames.Email,userinfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }

        [Authorize]
        [HttpPost("Post")]
        public string Post() 
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var userName = claim[0].Value;
            return "Welcome to: " + userName;
        }

        [Authorize]
        [HttpGet("GetValue")]
        public ActionResult<IEnumerable<string>> Get()
        {

            return new string[] { "v1", "v2", "v3"};
        }

    }
}
