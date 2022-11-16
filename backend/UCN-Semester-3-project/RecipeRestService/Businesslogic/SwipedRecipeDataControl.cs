using RecipesData.Database;
using RecipesData.Model;

namespace RecipeRestService.Businesslogic
{
    public class SwipedRecipeDataControl : ISwipedRecipeAccess
    {
        ISwipedRecipeAccess _swipedRecipeAccess;
        public SwipedRecipeDataControl(IConfiguration inConfiguration)
        {
            _swipedRecipeAccess = new SwipedRecipeDataAccess(configuration);
        }

        public SwipedRecipe? Get(Guid id)
        {
            Recipe? foundRecipe;
            try{
                foundRecipe = _swipedRecipeAccess.GetSwipedRecipeById(id);
            }
            catch(Exception e)
            {
                foundRecipe = null;
            }
            return foundRecipe;
        }

        public void AddSwipedRecipe(SwipedRecipe swipedRecipe)
        {
            throw new NotImplementedException();
        }

        bool Put(SwipedRecipe SwipedRecipeToUpdate)
        {
            throw new NotImplementedException();
        }

        bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}