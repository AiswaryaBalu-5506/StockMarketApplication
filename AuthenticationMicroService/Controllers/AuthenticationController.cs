using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthenticationMicroService.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MimeKit.Text;
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
        private readonly IOptions<EmailSettingsModel> _emailSettings;
        public AuthenticationController(AuthenticationAppDbContext db, IConfiguration config, IOptions<EmailSettingsModel> emailSettings)
        {
            _db = db;
            _configuration = config;
            _emailSettings = emailSettings;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginModel lmodel)
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;

            var user = _db.Users.Where(u => u.UserName == lmodel.Username).FirstOrDefault();

            if (user != null)
            {
                if (user.Password == lmodel.Password)
                {
                    if (user.confirmed == true)
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
                    else
                    {
                        return Unauthorized(new Response { Status = "unAuthorised", Message = "Please complete E-mail verification to login" });
                    }
                }
                else
                {
                    return Unauthorized(new Response { Status = "unAuthorised", Message = "UserName or Password incorrect" });
                }

            }
            else
            {
                return Unauthorized(new Response { Status = "UnAuthorised", Message = "Please register and complete E-mail verification to login" });
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
            var result = _db.SaveChanges();
            if (result == 1)
            {
                //send email verification

                // create email message
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_emailSettings.Value.smtpUserName));
                email.To.Add(MailboxAddress.Parse(model.email));
                email.Subject = "Please verify your email to proceed into Stock Market Application";
                email.Body = new TextPart(TextFormat.Html) 
                             { Text = "<h3>Please copy paste the following URL.</h3> <br> " +
                                      "URL: https://localhost:44349/api/Authentication/VerifyEmail/" + model.email };

                // send email
                using var smtp = new SmtpClient();
                smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_emailSettings.Value.smtpUserName, _emailSettings.Value.smtpPassword);
                smtp.Send(email);
                smtp.Disconnect(true);

                return Ok(new Response { Status = "Success", Message = "User created successfully! Please complete email verification." });                
            }                
            else return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            
            
        }

        [HttpGet]
        [Route("VerifyEmail/{address}")]
        public IActionResult verifyEmail(string address)
        {
            if(address != null)
            {
                var user = _db.Users.Where(u => u.email == address).SingleOrDefault();
                if(user != null)
                {
                    user.confirmed = true;
                    _db.Users.Update(user);
                    var res = _db.SaveChanges();
                    if(res == 1)
                    {
                        return Ok(new Response { Status = "Success", Message = "Email verified." });
                    }
                    else
                    {
                        return NotFound(new Response { Status = "Error", Message = "Internal Error." });
                    }
                }
                else
                {
                    return BadRequest(new Response { Status = "Error", Message = "Email address not found. Please register to verify email" });
                }
            }
            else
            {
                return BadRequest(new Response { Status = "Error", Message = "Email address not found in URL" });
            }
        }

    }

    internal class Response
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
