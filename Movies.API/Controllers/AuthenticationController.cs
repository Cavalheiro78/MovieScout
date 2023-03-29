using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Movies.API.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(string username, string password)
        {
            InfoUser user = ValidateUserCredentials(username, password);

            if (user == null)
                return Unauthorized();

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("id", user.Id.ToString()));
            claimsForToken.Add(new Claim("username", user.Username));
            claimsForToken.Add(new Claim("pass", user.Password));

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMonths(2),
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private InfoUser ValidateUserCredentials(string? username, string? password)
        {
            InfoUser infoUser = new InfoUser(1, "demo", "demo");
            if (string.Equals(infoUser.Username, username) && string.Equals(infoUser.Password, password))
                return infoUser;

            return null;
        }
        
        public class InfoUser
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }

            public InfoUser(int id, string username, string password)
            {
                Id = id;
                Username = username;
                Password = password;
            }
        }

        public class AuthenticationRequestBody
        {
            public string? Username { get; set; }
            public string? Password { get; set; }
        }
    }
}
