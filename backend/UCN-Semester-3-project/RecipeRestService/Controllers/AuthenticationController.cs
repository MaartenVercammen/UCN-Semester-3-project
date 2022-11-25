using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeRestService.Businesslogic;
using RecipeRestService.DTO;
using RecipeRestService.ModelConversion;
using RecipesData.Model;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RecipeRestService.Security;
using System.Security.Claims;

namespace RecipeRestService.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class AuthorizationConrtoller : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public AuthorizationConrtoller(IConfiguration inConfiguration)
        {
            _configuration = inConfiguration;
        }

        [HttpGet]
        public IActionResult Authorize()
        {
            User user = new User(Guid.NewGuid(), "email", "Mark", "Markson", "Mark132", "Mark street 15234 Aalborg", Role.USER);
            return Ok(GenerateToken(user));
        }

        private string GenerateToken(User user)
        {
            string tokenString;
            string Issuer = "https://localhost:7088";

            var claims = new[] {    
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.NameIdentifier,
                user.UserId.ToString())
            };

            SecurityHelper securityHelper = new SecurityHelper(_configuration);
            // Create header with algorithm and token type - and secret added
            SymmetricSecurityKey SIGNING_KEY = securityHelper.GetSecurityKey();
            SigningCredentials credentials = new SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256);

            // Time to live for newly created JWT Token
            int ttlInMinutes = 60;
            DateTime expiry = DateTime.UtcNow.AddMinutes(ttlInMinutes);


            JwtSecurityToken secToken = new JwtSecurityToken(Issuer, Issuer, claims, expires: expiry, signingCredentials: credentials);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            tokenString = handler.WriteToken(secToken);

            return tokenString;
        }
    }
}