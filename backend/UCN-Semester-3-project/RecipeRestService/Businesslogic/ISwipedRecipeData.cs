using RecipesData.Model;

namespace RecipeRestService.Businesslogic
{
    public interface IRecipeData
    {
        SwipedRecipe? Get(Guid id);
        //List<Recipe>? Get();
        Guid Add(SwipedRecipe swipedRecipeToAdd);
        bool Put(SwipedRecipe SwipedRecipeToUpdate);
        bool Delete(Guid id); 
    }
}
