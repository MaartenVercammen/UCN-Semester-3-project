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
using BusinessLogic.IUserData;

namespace RecipeRestService.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class AuthorizationConrtoller : ControllerBase
    {

        private readonly IUserData _access;
        public AuthorizationConrtoller(IUserData access)
        {
            _access = access;
        }

        [HttpPost]
        public IActionResult<UserDto> Login()
        {
            string password = Request.Headers["Password"];
            string username = Request.Headers["Email"];

            ActionResult actionResult;

            try{
                UserDto user = _access.Login(email, password);
                if(user != null){
                    Response.Headers["JWT"] = GenerateToken(user);
                    actionResult = Ok(user);
                }
                else{
                    actionResult = new StatusCodeResult(401);
                }
                
            }catch(Exeption ex){
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