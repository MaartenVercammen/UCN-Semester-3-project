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

        [Fact]
        public void TestGetSwipedRecipeById() 
        {
            Guid recipeId = new Guid("04c74da5-3035-4a4a-b732-77b40fa4ab17");
            SwipedRecipe retrievedSRecipe =  _swipedRecipeAccess.GetSwipedRecipeById(recipeId);
            Assert.Equal(recipeId, retrievedSRecipe.RecipeId);
        }

        [Fact]
        public void CreateSwipedRecipe()
        {
            Guid recipeId = new Guid("b4ea22a7-8fea-4e27-b359-7dd2ce8da8ae");
            User author = new User(Guid.Parse("00000000-0000-0000-0000-000000000000"), "mail", "mark", "mark", "pass", "street", Role.USER);
            SwipedRecipe swipedRecipe = new SwipedRecipe(author.UserId, recipeId, true);
            _swipedRecipeAccess.CreateSwipedRecipe(swipedRecipe);
            //SwipedRecipe retrievedSRecipe = _swipedRecipeAccess.GetSwipedRecipeById(author.UserId);
            Assert.Equal(recipeId, swipedRecipe.RecipeId);
            _swipedRecipeAccess.DeleteSwipedRecipe(recipeId);
        }

        
    }
}