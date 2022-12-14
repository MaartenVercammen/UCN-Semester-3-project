using RecipesData.Database;
using RecipesData.Model;

namespace RecipeRestService.Businesslogic
{
    public class SwipedRecipeDataControl : ISwipedRecipeData
    {
        ISwipedRecipeAccess _SwipedRecipeAccess;
<<<<<<< HEAD
        public SwipedRecipeDataControl(ISwipedRecipeAccess access)
        {
            _SwipedRecipeAccess = access;
        }

        public SwipedRecipe? Get(Guid id, Guid userId)
        {
            SwipedRecipe? foundSwipedRecipe;
            try
            {
                foundSwipedRecipe = _SwipedRecipeAccess.GetSwipedRecipeById(id, userId);
            }
            catch (Exception)
            {
                foundSwipedRecipe = null;
            }
            return foundSwipedRecipe;
        }


=======
        public SwipedRecipeDataControl(IConfiguration inConfiguration)
        {
            _SwipedRecipeAccess = new SwipedRecipeDatabaseAccess(inConfiguration);
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

>>>>>>> main
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

<<<<<<< HEAD
        public bool Delete(Guid recipeId, Guid userId)
=======
        public bool Delete(Guid id)
>>>>>>> main
        {
            bool removed;
            try
            {
<<<<<<< HEAD
                removed = _SwipedRecipeAccess.DeleteSR(recipeId, userId);
=======
                removed = _SwipedRecipeAccess.DeleteSR(id);
>>>>>>> main
            }
            catch (Exception)
            {
                removed = false;
            }
            return removed;
        }
    }
}