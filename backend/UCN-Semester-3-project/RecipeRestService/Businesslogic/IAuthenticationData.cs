using RecipeRestService.DTO;
using RecipesData.Model;

namespace RecipeRestService.Businesslogic{

    public interface IAuthenticationData{
        public User Login(string email, string password);
    }
}