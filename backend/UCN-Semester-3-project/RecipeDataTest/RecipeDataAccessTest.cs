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

        }

        [Fact]
        public void CreateRecipe(){
            RecipeDatabaseAccess access = new RecipeDatabaseAccess(_connectionString);
            Ingredient ingredient1 = new Ingredient("banana", 5, "kg");
            Ingredient ingredient2 = new Ingredient("sugar", 500, "g");
            Instruction instruction1 = new Instruction(1, "peel the banana");
            Instruction instruction2 = new Instruction(2, "Add suggar");
            Recipe recipe = new Recipe("Bananabread", "best bananabread in the world", "http://picture.png", 30, 4);
            recipe.Ingredients.Add(ingredient1);
            recipe.Ingredients.Add(ingredient2);
            recipe.Instructions.Add(instruction1);
            recipe.Instructions.Add(instruction2);
            Guid id = access.CreateRecipe(recipe);
            Assert.NotEqual(Guid.Empty, id);
        }
    }
}