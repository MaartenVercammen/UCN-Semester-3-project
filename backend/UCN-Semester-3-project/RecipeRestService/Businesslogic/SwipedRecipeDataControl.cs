using RecipesData.Database;
using RecipesData.Model;

namespace RecipeRestService.Businesslogic
{
    public class SwipedRecipeDataControl : ISwipedRecipeData
    {
        ISwipedRecipeAccess _SwipedRecipeAccess;
        public SwipedRecipeDataControl(ISwipedRecipeAccess access)
        {
            _SwipedRecipeAccess = access;
        }

        public SwipedRecipe? Get(Guid id)
        {
            SwipedRecipe? foundRecipe;
            try
            {
                foundRecipe = _SwipedRecipeAccess.GetSwipedRecipeById(id);
            }
            catch (Exception)
            {
                foundRecipe = null;
            }
            return foundRecipe;
        }

        public List<SwipedRecipe>? GetPerUser(Guid userId)
        {
            List<SwipedRecipe>? foundRecipes;
            try
            {
                foundRecipes = _SwipedRecipeAccess.GetSwipeRecipesByUser(userId);
            }
            catch (Exception)
            {
                foundRecipes = null;
            }
            return foundRecipes;
        }

        public List<SwipedRecipe>? GetLikedPerUser(Guid userId)
        {
            List<SwipedRecipe>? foundRecipes;
            try
            {
                foundRecipes = _SwipedRecipeAccess.GetLikedByUser(userId);
            }
            catch (Exception)
            {
                foundRecipes = null;
            }
            return foundRecipes;
        }

        public SwipedRecipe? Add(SwipedRecipe swipedRecipeToAdd)
        {
            try
            {
                _SwipedRecipeAccess.CreateSwipedRecipe(swipedRecipeToAdd);
            }
            catch (Exception)
            {
                return null;
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
            catch (Exception)
            {
                removed = false;
            }
            return removed;
        }
    }
}