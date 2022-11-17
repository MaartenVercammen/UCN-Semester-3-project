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
                foundRecipes = _RecipeAccess.GetRecipes();
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

        public Recipe GetRandomRecipe(){
            List<Guid> guids = new List<Guid>();
            try{
                guids = _RecipeAccess.GetGuids();
                Random rnd = new Random();
                int randomnumber = rnd.Next(0, guids.Count - 1);
                return _RecipeAccess.GetRecipeById(guids[randomnumber]);
            }
            catch (Exception e){
                return null;
            }
        }
    }
}
