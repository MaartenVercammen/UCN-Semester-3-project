using RecipeRestService.DTO;
using RecipesData.Model;

namespace RecipeRestService.Businesslogic{

    public interface IAuthenticationData{
        public UserDto? Login(string email, string password);
    }
}