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

        public Guid Add(SwipedRecipe swipedRecipeToAdd)
        {
            Guid guid;
            try
            {
                guid = _SwipedRecipeAccess.CreateSR(swipedRecipeToAdd).RecipeId;
            } catch(Exception e) {
                guid = Guid.Empty;
            }
            // change this
            return guid;
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