using RecipesData.Model;

namespace RecipeRestService.Businesslogic
{
    public interface ISwipedRecipeData
    {
        SwipedRecipe? Get(Guid id);
        //List<Recipe>? Get();
        Guid Add(SwipedRecipe swipedRecipeToAdd);
        bool Delete(Guid id); 
    }
}
