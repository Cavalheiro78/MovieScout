using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Movies.API.Services;
using MovieScout.DbContexts;
using MovieScout.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Movies.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserInfoRepository _users;
        MovieContext _context;

        public UsersController(IUserInfoRepository users, IConfiguration configuration, MovieContext context) 
        {
            _users = users ?? throw new ArgumentNullException(nameof(users));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet("userid")]
        public ActionResult<string> GetUserId(string username)
        {
            UserEntity user = _context.Users.Where(u => u.Username == username).FirstOrDefault();

            if (user == null)
                return NotFound();

            return Ok(user.Id);
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(string username, string password)
        {
            UserEntity user = ValidateUserCredentials(username, password);

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
                DateTime.UtcNow.AddHours(2),
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

        [HttpPost("register")]
        public ActionResult Register(string username, string password, string email)
        {
            if (_context.Users.Any(u => u.Username == username))
                return NoContent();
            
            _users.AddUser(username, password, email);
            return Ok();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private UserEntity ValidateUserCredentials(string? username, string? password)
        {
            UserEntity infoUser = _context.Users.Where(c => c.Username == username && c.Password == password).FirstOrDefault();
            if (string.Equals(infoUser.Username, username) && string.Equals(infoUser.Password, password))
                return infoUser;

            return null;
        }

        public class AuthenticationRequestBody
        {
            public string? Username { get; set; }
            public string? Password { get; set; }
        }
    }
}
