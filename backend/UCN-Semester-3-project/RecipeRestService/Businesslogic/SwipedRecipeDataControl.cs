using RecipesData.Database;
using RecipesData.Model;

namespace RecipeRestService.Businesslogic
{
    public class SwipedRecipeDataControl : ISwipedRecipeData
    {
        ISwipedRecipeAccess _SwipedRecipeAccess;
        public SwipedRecipeDataControl(IConfiguration inConfiguration)
        {
            _SwipedRecipeAccess = new SwipedRecipeDatabaseAccess(inConfiguration);
        }

        public SwipedRecipe? Get(Guid id)
        {
            SwipedRecipe? foundRecipe;
            try
            {
                foundRecipe = _SwipedRecipeAccess.GetSRById(id);
            }
            catch (Exception e)
            {
                foundRecipe = null;
            }
            return foundRecipe;
        }

        public SwipedRecipe? Add(SwipedRecipe swipedRecipeToAdd)
        {
            try
            {
                _SwipedRecipeAccess.CreateSR(swipedRecipeToAdd);
            }
            catch (Exception e)
            {
                swipedRecipeToAdd = null;
            }
            // change this
            return swipedRecipeToAdd;
        }

        public bool Delete(Guid id)
        {
            bool removed;
            try
            {
                removed = _SwipedRecipeAccess.DeleteSR(id);
            }
            catch (Exception e)
            {
                removed = false;
            }
            return removed;
        }
    }
}