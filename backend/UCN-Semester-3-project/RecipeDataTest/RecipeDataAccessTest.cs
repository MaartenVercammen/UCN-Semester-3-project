using Xunit.Abstractions;
using RecipesData.DatabaseLayer;

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

        }

        [Fact]
        public void Test1()
        {
            Assert.Equal(1, 1);
        }
    }
}