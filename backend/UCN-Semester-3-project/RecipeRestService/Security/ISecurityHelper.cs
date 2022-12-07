using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RecipesData.Model;

namespace RecipeRestService.Security {
    public interface ISecurityHelper {

        // Create key for signing
        public SymmetricSecurityKey GetSecurityKey();

        public Guid GetUserFromJWT(string token);

        public bool IsJWTEqualRequestId(HttpRequest request, string userid);

        public Role GetRoleFromJWT(string token);
    }
}