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
            }
            catch(Exception)
            {
                guid = Guid.Empty;
            }
            return guid;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        // TODO: Fix this method - retrieve random from db
        public Recipe GetRandomRecipe(){
            List<Recipe> recipes = new List<Recipe>();
            try{
                recipes = _RecipeAccess.GetRandomRecipe(Guid.Parse("00000000-0000-0000-0000-000000000000"));
                Random rnd = new Random();
                int randomnumber = rnd.Next(0, recipes.Count - 1);
                return _RecipeAccess.GetRecipeById(recipes[randomnumber].RecipeId);
            }
            catch (Exception e){
                return null;
            }
        }
    }
}
