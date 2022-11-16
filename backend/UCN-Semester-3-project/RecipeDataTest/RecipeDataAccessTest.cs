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
        public void CreateRecipe(){
            User auhtor = new User(Guid.Parse("00000000-0000-0000-0000-000000000000"), "mail", "mark", "mark", "pass", "street", Role.USER);
            RecipeDatabaseAccess access = new RecipeDatabaseAccess(_connectionString);
            Ingredient ingredient1 = new Ingredient("banana", 5, "kg");
            Ingredient ingredient2 = new Ingredient("sugar", 500, "g");
            Instruction instruction1 = new Instruction(1, "peel the banana");
            Instruction instruction2 = new Instruction(2, "Add suggar");
            Recipe recipe = new Recipe("Bananabread", "best bananabread in the world", "http://picture.png", 30, 4, auhtor);
            recipe.Ingredients.Add(ingredient1);
            recipe.Ingredients.Add(ingredient2);
            recipe.Instructions.Add(instruction1);
            recipe.Instructions.Add(instruction2);
            Guid id = access.CreateRecipe(recipe);
            Assert.NotEqual(Guid.Empty, id);
        }

        [Fact]
        public void TestGetRecipeById()
        {
            Guid id = new Guid("42108066-b596-480f-a3b2-16ddb3f56183");
            Recipe recipe = null;
            recipe = _recipeAccess.GetRecipeById(id);
            Assert.NotNull(recipe);
        }

        [Fact]
        public void TestGetAllRecipes()
        {
            List<Recipe> recipes = new List<Recipe>();
            recipes = _recipeAccess.GetRecipes();
            Assert.True(recipes.Count > 0);
        }

        [Fact]
        public void TestGetAllRecipeIds()
        {
            List<Guid> ids = new List<Guid>();
            ids = _recipeAccess.GetGuids();
            Assert.True(ids.Count > 0);
        }
    }
}