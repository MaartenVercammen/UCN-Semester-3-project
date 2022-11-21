using RecipesData.Model;

namespace RecipeRestService.Businesslogic
{
    public interface ISwipedRecipeData
    {
        SwipedRecipe? Get(Guid id);
        List<SwipedRecipe>? GetPerUser(Guid userId);
        List<SwipedRecipe>? GetLikedPerUser(Guid userId);
        SwipedRecipe? Add(SwipedRecipe swipedRecipeToAdd);
        bool Delete(Guid id); 
    }
}
