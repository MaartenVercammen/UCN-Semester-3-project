using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using RecipesData.Model;

namespace RecipeRestService.Security;

public interface ISecurityHelper
{
    public SymmetricSecurityKey GetSecurityKey();

    public Guid GetUserFromJWT(string token);

    public bool IsJWTEqualRequestId(string token, string userid);

    public Role GetRoleFromJWT(string token);
}