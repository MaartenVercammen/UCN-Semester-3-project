using RecipesData.Model;

namespace RecipeRestService.Businesslogic
{
    public interface IRecipeData
    {
        /*
         Recipe? Get(int id);
        List<Recipe>? Get();
        int Add(Recipe recipeToAdd);
        bool Put(Recipe recipeToUpdate);
        bool Delete(int id); 
         
         */

        Guid Add(Recipe recipeToAdd);
    }
}
