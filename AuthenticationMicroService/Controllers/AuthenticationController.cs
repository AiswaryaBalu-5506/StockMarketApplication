using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthenticationMicroService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StockMarketWebService.DataEntities;
using StockMarketWebService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthenticationMicroService.Controllers
{
    
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly AuthenticationAppDbContext _db;
        private readonly IConfiguration _configuration;
        public AuthenticationController(AuthenticationAppDbContext db, IConfiguration config)
        {
            _db = db;
            _configuration = config;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginModel lmodel)
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;

            var user = _db.Users.Where(u => u.UserName == lmodel.Username).FirstOrDefault();

            if(user.confirmed == true)
            {
                if (user != null && user.Password == lmodel.Password)
                {
                    UserType userRole = user.UserType;

                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("Role", userRole.ToString())
                };
                    //authClaims.Add(new Claim("Role", userRole));

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddHours(3),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }
                return Unauthorized();
            }
            else
            {
                return Unauthorized(new Response { Status = "UnAuthorised", Message = "Please complete E-mail verification to login"});
            }            
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] User model)
        {
            var userExists = _db.Users.Where(u => u.UserName == model.UserName).FirstOrDefault();
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });


            _db.Users.Add(model);
            _db.SaveChanges();
            /* if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            */
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }
    }

    internal class Response
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
