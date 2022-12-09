using RecipesData.Database;
using RecipesData.Model;

namespace RecipeRestService.Businesslogic
{
    public class RecipedataControl : IRecipeData
    {
        IRecipeAccess _RecipeAccess;
        public RecipedataControl(IRecipeAccess access)
        {
            _RecipeAccess = access;
        }

        public Guid Add(Recipe recipeToAdd)
        {
            Guid guid;
            try
            {
                guid = _RecipeAccess.CreateRecipe(recipeToAdd);
            }
            catch (Exception)
            {
                guid = Guid.Empty;
            }
            return guid;
        }

        public bool Delete(Guid id)
        {
            bool IsCompleted = false;
            try
            {
                IsCompleted = _RecipeAccess.DeleteRecipe(id);

            }
            catch (Exception)
            {
                IsCompleted = false;
            }
            return IsCompleted;
        }

        public Recipe? Get(Guid id)
        {
            Recipe? foundRecipe;
            try
            {
                foundRecipe = _RecipeAccess.GetRecipeById(id);
            }
            catch (Exception)
            {
                foundRecipe = null;
            }
            return foundRecipe;
        }

        public List<Recipe>? Get()
        {
            List<Recipe>? foundRecipes;
            try
            {
                foundRecipes = _RecipeAccess.GetRecipesSimplified();
            }
            catch (Exception)
            {
                foundRecipes = null;
            }
            return foundRecipes;
        }
        public bool Put(Recipe recipeToUpdate)
        {
            bool update = false;
            try
            {
                update = _RecipeAccess.UpdateRecipe(recipeToUpdate);
            }
            catch (Exception)
            {
                update = false;
            }
            return update;
        }

        public Recipe GetRandomRecipe(Guid userId)
        {
            Recipe? recipe;
            try
            {
                recipe = _RecipeAccess.GetRandomRecipe(userId);
            }
            catch (Exception)
            {
                recipe = null;
            }

            return recipe;
        }

        public List<Recipe>? GetLikedByUser(Guid userId)
        {
            List<Recipe>? recipes = new List<Recipe>();
            try
            {
                recipes = _RecipeAccess.GetLikedByUser(userId);
            }
            catch (Exception)
            {
                return null;
            }
            return recipes;
        }
    }
}