using Xunit.Abstractions;
using RecipesData.Database;
using RecipesData.Model;

namespace RecipeDataTest
{
    public class SwipedRecipeDataAccessTest
    {

        private readonly ITestOutputHelper extraOutput;
        readonly private ISwipedRecipeAccess _swipedRecipeAccess;
        readonly string _connectionString = "data Source=foodpanda.dev,1400; Database=ucn; User Id=dev;Password=dev;";
        public SwipedRecipeDataAccessTest(ITestOutputHelper output)
        {
            this.extraOutput = output;
            _swipedRecipeAccess = new SwipedRecipeDatabaseAccess(_connectionString);
            //IRecipeAccess recipeAccess = new RecipeDatabaseAccess(_connectionString);
        }

        // TODO: Do the tests

        
    }
}