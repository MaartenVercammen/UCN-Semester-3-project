using RecipesData.Database;
using RecipesData.Model;

namespace RecipeRestService.Businesslogic
{
    public class RecipedataControl : IRecipeData
    {
        IRecipeAccess _RecipeAccess;
        public RecipedataControl(IConfiguration inConfiguration)
        {
            _RecipeAccess = new RecipeDatabaseAccess(inConfiguration);
        }

        public Guid Add(Recipe recipeToAdd)
        {
            Guid guid;
            try
            {
                guid = _RecipeAccess.CreateRecipe(recipeToAdd);
            }catch(Exception ex)
            {
                guid = Guid.Empty;
            }
            return guid;
        }

        //implement IRecipeData

    }
}
