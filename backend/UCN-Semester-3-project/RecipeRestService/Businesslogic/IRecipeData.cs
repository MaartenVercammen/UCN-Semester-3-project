using RecipesData.Model;

namespace RecipeRestService.Businesslogic
{
    public interface IRecipeData
    {
        Recipe? Get(Guid id);
        List<Recipe>? Get();
        Guid Add(Recipe recipeToAdd);
        bool Put(Recipe recipeToUpdate);
        bool Delete(Guid id);
        Recipe GetRandomRecipe(Guid userId);
    }
}
