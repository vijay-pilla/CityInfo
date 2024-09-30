using CityInfo.API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CityInfo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthenticationController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationReq authReq)
        {
            //validate the username and password
            var user = ValidateUserCredentials(
                authReq.UserName, authReq.Password);
            if(user == null)
            {
                return Unauthorized("Invalid Credentials");
            }

            // Create JWT Token
            var token = GenerateJwtToken(user);

            return Ok(token);
        }

        private string GenerateJwtToken(CityInfoUser user)
        {
            //create token
            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_config["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //creating claims for the token
            //claims = (Identity related information on the user)contains who the user is (key-value pairs)
            var claims = new List<Claim>
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim("city", user.City),
            new Claim("role", user.Role) // You can add custom claims like user roles
            };
            //Generate the token
            var token = new JwtSecurityToken(
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"],
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(10),
                signingCredentials);
            //write the token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private CityInfoUser ValidateUserCredentials(string userName, string password)
        {
            //As we Dont have a user DB or table. If having, check the passed-through userName and Password against what's stored in Database
            //For Demo we assume the credentials are valid
            //return a new CityInfoUser (values would normally come from your user DB/ table
            if(userName == "admin" && password == "password")
            {
                return new CityInfoUser(
                1,
                "Vijay32",
                "vijay",
                "pilla",
                "Antwerp",
                "admin"
            );
            }
            return null;
        }
    }
}
