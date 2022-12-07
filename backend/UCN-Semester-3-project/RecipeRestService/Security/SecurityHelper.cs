using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RecipesData.Model;

namespace RecipeRestService.Security {
    public class SecurityHelper : ISecurityHelper {

        private readonly IConfiguration _configuration;

        // Fetches configuration from more sources
        public SecurityHelper(IConfiguration inConfiguration) {
            _configuration = inConfiguration;
        }

        // Create key for signing
        public SymmetricSecurityKey GetSecurityKey() {
            SymmetricSecurityKey SIGNING_KEY;
            string SECRET_KEY = _configuration["SECRET_KEY"];
            SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
            return SIGNING_KEY;
        }

        public Guid GetUserFromJWT(string token){
            string tokenItself = token.Split(' ')[1];
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(tokenItself);
            var claimValue = securityToken.Claims.ElementAt(2).Value;
            return Guid.Parse(claimValue);
        }

        public bool IsJWTEqualRequestId(string token, string userid){
            Guid tokenId = GetUserFromJWT(token);
            
            if(tokenId.ToString() != userid){
                return true;
            }
            return false;
        }

        public Role GetRoleFromJWT(string token){
            string tokenItself = token.Split(' ')[1];
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(tokenItself);
            var claimValue = securityToken.Claims.ElementAt(1).Value;
            return (Role)Enum.Parse(typeof(Role), claimValue);
        }
    }
}