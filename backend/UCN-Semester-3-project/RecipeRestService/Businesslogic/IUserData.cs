using RecipeRestService.DTO;
using RecipesData.Model;

namespace UserRestService.Businesslogic
{
    public interface IUserData
    {
        User? Get(Guid id);
        List<User>? Get();
        Guid Add(User UserToAdd);
        bool Put(User UserToUpdate);
        bool Delete(Guid id);
    }
}
