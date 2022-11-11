using Xunit.Abstractions;
using RecipesData.Database;
using RecipesData.Model;

namespace RecipeDataTest
{
    public class RecipeDataAccessTest
    {

        private readonly ITestOutputHelper extraOutput;
        readonly private IRecipeAccess _recipeAccess;
        readonly string _connectionString = "data Source=foodpanda.dev,1400; Database=ucn; User Id=dev;Password=dev;";
        public RecipeDataAccessTest(ITestOutputHelper output)
        {
            this.extraOutput = output;
            _recipeAccess = new RecipeDatabaseAccess(_connectionString);
            //IRecipeAccess recipeAccess = new RecipeDatabaseAccess(_connectionString);
        }

        [Fact]
        public void Test1()
        {
            Assert.Equal(1, 1);
        }

        [Fact]
        public void TestGetRecipeById()
        {
            Guid id = new Guid("0c4cc56f-57db-436f-8276-9826a63d3dae");
            Recipe recipe = null;
            recipe = _recipeAccess.GetRecipeById(id);
            Assert.NotNull(recipe);
        }
    }
}