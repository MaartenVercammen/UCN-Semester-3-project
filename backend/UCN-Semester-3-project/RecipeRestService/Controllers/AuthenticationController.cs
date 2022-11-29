using System.ComponentModel.DataAnnotations;
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
using UserRestService.Businesslogic;

namespace RecipeRestService.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class AuthorizationConrtoller : ControllerBase
    {

        private readonly IAuthenticationData _access;
        private readonly IConfiguration _configuration;
        public AuthorizationConrtoller(IAuthenticationData access, IConfiguration configuration)
        {
            _access = access;
            _configuration = configuration;
        }

        [HttpPost]
        public ActionResult<UserDto> Login([FromHeader(Name = "Password")] [Required] string password,
            [FromHeader(Name = "Email")] [Required] string email)
        {
            ActionResult actionResult;
            
            try{
                UserDto user = _access.Login(email, password);
                if(user != null){
                    Response.Headers["token"] = GenerateToken(UserDtoConvert.ToUser(user));
                    Response.Headers["Access-Control-Expose-Headers"] = "token";
                    actionResult = Ok(user);
                }
                else{
                    actionResult = new StatusCodeResult(401);
                }
                
            }catch(Exception ex){
                actionResult = new StatusCodeResult(500);
            }
            return actionResult;
        }

        private string GenerateToken(User user)
        {
            string tokenString;
            string Issuer = "https://localhost:7088";

            // DO NOT EDIT THIS, in case something goes wrong edit SecurityHelper.GetRoleFromJWT ElementAt index
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