using RecipesData.Model;

namespace RecipeRestService.Businesslogic
{
    public interface ISwipedRecipeData
    {
        SwipedRecipe? Get(Guid id, Guid userid);
        List<SwipedRecipe>? GetPerUser(Guid userId);
        List<SwipedRecipe>? GetLikedPerUser(Guid userId);
        SwipedRecipe? Add(SwipedRecipe swipedRecipeToAdd);
        bool Delete(Guid id, Guid userId); 
    }
}
