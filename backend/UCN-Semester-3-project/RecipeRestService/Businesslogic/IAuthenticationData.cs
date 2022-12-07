using RecipeRestService.DTO;

namespace RecipeRestService.Businesslogic{

    public interface IAuthenticationData{
        public UserDto? Login(string email, string password);
    }
}