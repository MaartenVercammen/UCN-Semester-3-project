using Moq;
using RecipeRestService.Businesslogic;
using Xunit.Abstractions;
using RecipesData.Database;
using RecipesData.Model;

namespace RecipeDataTest
{
    public class RecipedataControlTest
    {

        private readonly ITestOutputHelper extraOutput;
        private readonly RecipedataControl _sut;
        private readonly Mock<IRecipeAccess> _acces = new Mock<IRecipeAccess>();

        // Valid object
        private readonly User validUser;
        private readonly Ingredient validIngredient;
        private readonly Instruction validInstruction;
        private readonly Recipe validRecipe;
        
        
        // Invalid objects
        private readonly Ingredient invalidNameIngredient;
        private readonly Ingredient invalidamountIngredient;

        private readonly Instruction invalidDescriptionInstruction;
        private readonly Instruction invalidStepInstruction;
        private readonly Instruction invalidStapZeroInstruction;

        private readonly Recipe invalidNameRecipe;
        private readonly Recipe invalidDescriptionRecipe;
        private readonly Recipe invalidpictureUrlRecipe;
        private readonly Recipe invalidTimeRecipe;
        private readonly Recipe invalidPortionNumberRecipe;
        private readonly Recipe invalidAuthorRecipe;


        public RecipedataControlTest(ITestOutputHelper output)
        {
            extraOutput = output;
            _sut = new RecipedataControl(_acces.Object);
            // Valid object
            validUser = new User(Guid.Parse("00000000-0000-0000-0000-000000000000"),  "mail", "mark", "mark", "pass",
                "street", Role.USER);
            validIngredient = new Ingredient("banana", 5, "kg");
            validInstruction = new Instruction(1, "peel the banana");
            validRecipe = new Recipe( "Bananabread", "best bananabread in the world",
                "http://picture.png", 30, 4, validUser);

            // Invalid objects
            invalidNameIngredient = new Ingredient("", 5, "kg");
            invalidamountIngredient = new Ingredient("banana", -5, "kg");

            invalidDescriptionInstruction = new Instruction(1, "");
            invalidStepInstruction = new Instruction(-1, "");
            invalidStapZeroInstruction = new Instruction(0, "");

            invalidNameRecipe = new Recipe("", "best bananabread in the world", "http://picture.png",
                30, 4, validUser);
            invalidDescriptionRecipe =
                new Recipe("Bananabread", "", "http://picture.png", 30, 4, validUser);
            invalidpictureUrlRecipe = new Recipe( "Bananabread", "best bananabread in the world", "", 30,
                4, validUser);
            invalidTimeRecipe = new Recipe( "Bananabread", "best bananabread in the world",
                "http://picture.png", -10, 4, validUser);
            invalidPortionNumberRecipe = new Recipe( "Bananabread", "best bananabread in the world",
                "http://picture.png", 30, -5, validUser);
            invalidAuthorRecipe = new Recipe( "Bananabread", "best bananabread in the world",
                "http://picture.png", 30, 4, null);
        }

        [Fact]
        public void Get_WhenGivenValidId_ReturnsValidRecipe()
        {
            //Arrange
            Guid id = new Guid();
            Recipe inrecipe = validRecipe;
            inrecipe.RecipeId = id;
            _acces.Setup(x => x.GetRecipeById(id))
                .Returns(inrecipe);
            
            //Act
            var recipe = _sut.Get(id);

            //Assert
            Assert.Equal(id, recipe.RecipeId);
        }

        [Fact]
        public void Get_WhengivenIdAndIdDoesntExist_ReturnsNull()
        {
            //Arrange
            Guid id = Guid.Empty;
            _acces.Setup(x => x.GetRecipeById(id))
                .Throws(new Exception());
            //Act
            Recipe recipe = _sut.Get(id);
            //Assert
            Assert.Null(recipe);
        }

        [Fact]
        public void Get_WhenGivenNoId_ReturnsListOfRecipes()
        {
            //Arrange
            List<Recipe> inrecipes = new List<Recipe>()
            {
                validRecipe,
                validRecipe,
                validRecipe,
                validRecipe
            };
            _acces.Setup(x => x.GetRecipesSimplified())
                .Returns(inrecipes);
            
            //Act
            var recipes = _sut.Get();

            //Assert
            Assert.Equal(inrecipes.Count, recipes.Count);
        }

        [Fact]
        public void Get_WhenGivenNoIdAndNullReturn_ReturnEmptyList()
        {
            //Arrange
            _acces.Setup(x => x.GetRecipesSimplified())
                .Throws(new Exception());
            
            //Act
            List<Recipe> recipes = _sut.Get();
            //Assert
            Assert.Null(recipes);
        }

        [Fact]
        public void Delete_WhenGivenValidId_ReturnTrue()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            _acces.Setup(x => x.DeleteRecipe(id))
                .Returns(true);
            //Act
            bool IsDone = _sut.Delete(id);
            //Assert
            Assert.True(IsDone);
        }
        
        [Fact]
        public void Delete_WhenGivenInValidId_ReturnFalse()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            _acces.Setup(x => x.DeleteRecipe(id))
                .Returns(false);
            //Act
            bool IsDone = _sut.Delete(id);
            //Assert
            Assert.False(IsDone);
        }
        
        [Fact]
        public void Delete_WhenGivenInValidIdAndErrorOccurse_ReturnFalse()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            _acces.Setup(x => x.DeleteRecipe(id))
                .Throws(new Exception());
            //Act
            bool IsDone = _sut.Delete(id);
            //Assert
            Assert.False(IsDone);
        }

        [Fact]
        public void Add_WhenValidRecipe_ReturnsGuid()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            Recipe inRecipe = validRecipe;
            inRecipe.RecipeId = id;
            _acces.Setup(x => x.CreateRecipe(inRecipe))
                .Returns(inRecipe.RecipeId);
            //Act
            Guid outId = _sut.Add(inRecipe);

            //Assert
            Assert.Equal(inRecipe.RecipeId, outId);
        }
        
        [Fact]
        public void Add_WhenInValidRecipe_ReturnsEmptyGuid()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            Recipe inRecipe = validRecipe;
            inRecipe.RecipeId = id;
            _acces.Setup(x => x.CreateRecipe(inRecipe))
                .Throws(new Exception());
            //Act
            Guid outId = _sut.Add(inRecipe);

            //Assert
            Assert.Equal(Guid.Empty, outId);
        }

        [Fact]
        public void GetRandomRecipe_WhenValidUserId_ReturnsRecipe()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            Recipe inrecipe = validRecipe;
            inrecipe.RecipeId = id;
            List<Recipe> recipes = new List<Recipe>()
            {
                inrecipe,
                inrecipe,
                inrecipe
            };
            
            _acces.Setup(x => x.GetRandomRecipe(id))
                .Returns(recipes);
            _acces.Setup(x => x.GetRecipeById(id))
                .Returns(validRecipe);
            //Act
            Recipe recipe = _sut.GetRandomRecipe(id);
            //Assert
            Assert.Equal(recipe.RecipeId, id);
        }

        [Fact]
        public void GetRandomRecipe_WhenErrorAtGetRandom_ReturnsError()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            List<Recipe> recipes = new List<Recipe>()
            {
                validRecipe,
                validRecipe,
                validRecipe
            };
            _acces.Setup(x => x.GetRandomRecipe(id))
                .Throws(new Exception());
            _acces.Setup(x => x.GetRecipeById(id))
                .Returns(validRecipe);
            //Act
            Recipe recipe = _sut.GetRandomRecipe(id);
            //Assert
            Assert.Null(recipe);
        }
    }
}