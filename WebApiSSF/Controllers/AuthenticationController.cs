using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApiSSF.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public class AuthenticationRequestBody 
        {
            public string? Usuario { get; set; }
            public string? Password { get; set; }

        }

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public ActionResult<string> Authenticate(AuthenticationRequestBody body) 
        {
            var user = ValidaCredencialesUsuario(body.Usuario, body.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("Name", body.Usuario));  
            claimsForToken.Add(new Claim("Surname", body.Password)); 

            var jwtToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(1),
                signingCredentials);

            var tokenReturned = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return Ok(tokenReturned);
        }

        private object ValidaCredencialesUsuario(string? usuario, string? password)
        {
            throw new NotImplementedException();
        }
    }
}
