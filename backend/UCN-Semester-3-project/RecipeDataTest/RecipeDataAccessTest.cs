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
            Assert.equals(1,1);
        }

        [fact]
        public void CreateRecipe(){
            Ingredient ingredient = new Ingredient("banana", 5, "kg");
            Ingredient ingredient = new Ingredient("sugar", 500, "g");
            Instruction instruction = new Instruction(1, "peel the banana");
            Recipe recipe = new Recipe(new System.Guid(), "Bananabread", "best bananabread in the world", "aaa-aaa-aaa-aaa", "http://picture.png", 30, 4);
            Assert.True(RecipeDatabaseAccess.CreateRecipe());
        }
    }
}