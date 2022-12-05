using RecipeRestService.DTO;
using RecipeRestService.Businesslogic;
using RecipeRestService.ModelConversion;
using RecipesData.Database;
using RecipesData.Model;
using System.Web.Helpers;

namespace RecipeRestService.Businesslogic
{
    public class AuthenticationDataControl : IAuthenticationData
    {

        private readonly IUserAccess _access;

        public AuthenticationDataControl(IUserAccess access)
        {
            _access = access;
        }

        public User? Login(string email, string password){
            try{
                User user = _access.GetUserByEmail(email);
                if(Crypto.VerifyHashedPassword(user.Password, password))
                {
                    return user;
                }
                else
                {
                    return null;
                }


            }catch(Exception) {
                return null;
            }
        }   
    }
}