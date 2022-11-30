using System.Diagnostics;
using Moq;
using RecipeRestService.Businesslogic;
using RecipesData.Database;
using RecipesData.Model;
using Xunit.Abstractions;

namespace RecipeDataTest.BusinessLogic
{
    public class RecipeDataControlTest
    {

        private readonly ITestOutputHelper _extraOutput;
        private readonly RecipedataControl _sut;
        private readonly Mock<IRecipeAccess> _acces = new Mock<IRecipeAccess>();

        // Valid object
        private readonly Recipe _validRecipe;


        public RecipeDataControlTest(ITestOutputHelper output)
        {
            _extraOutput = output;
            _sut = new RecipedataControl(_acces.Object);
            // Valid object
            var validUser = new User(Guid.Parse("00000000-0000-0000-0000-000000000000"),  "mail", "mark", "mark", "pass",
                "street", Role.USER);
            var validIngredient = new Ingredient("banana", 5, "kg");
            var validInstruction = new Instruction(1, "peel the banana");
            _validRecipe = new Recipe( "Banana-bread", "best banana bread in the world",
                "http://picture.png", 30, 4, validUser);
            _validRecipe.Ingredients.Add(validIngredient);
            _validRecipe.Instructions.Add(validInstruction);
        }

        [Fact]
        public void Get_WhenGivenId_ReturnsRecipe()
        {
            //Arrange
            Guid id = new Guid();
            Recipe inrecipe = _validRecipe;
            inrecipe.RecipeId = id;
            _acces.Setup(x => x.GetRecipeById(id))
                .Returns(inrecipe);
            
            //Act
            var recipe = _sut.Get(id);

            //Assert
            Assert.NotNull(recipe);
            Assert.Equal(id, recipe.RecipeId);
        }

        [Fact]
        public void Get_WhengivenIdAndExceptionIsThrown_ReturnsIdAsNull()
        {
            //Arrange
            Guid id = Guid.Empty;
            _acces.Setup(x => x.GetRecipeById(id))
                .Throws(new Exception());
            //Act
            Recipe? recipe = _sut.Get(id);
            //Assert
            Assert.Null(recipe);
        }

        [Fact]
        public void Get_WhenGivenNoId_ReturnsListOfRecipes()
        {
            //Arrange
            List<Recipe> inRecipes = new List<Recipe>()
            {
                _validRecipe,
                _validRecipe,
                _validRecipe,
                _validRecipe
            };
            _acces.Setup(x => x.GetRecipesSimplified())
                .Returns(inRecipes);
            
            //Act
            List<Recipe>? recipes = _sut.Get();

            //Assert
            Assert.NotNull(recipes);
            Assert.Equal(inRecipes.Count, recipes.Count);
        }

        [Fact]
        public void Get_WhenGivenNoIdAndExceptionIsThrown_ReturnEmptyList()
        {
            //Arrange
            _acces.Setup(x => x.GetRecipesSimplified())
                .Throws(new Exception());
            
            //Act
            List<Recipe>? recipes = _sut.Get();
            //Assert
            Assert.Null(recipes);
        }

        [Fact]
        public void Delete_WhenGivenId_ReturnTrue()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            _acces.Setup(x => x.DeleteRecipe(id))
                .Returns(true);
            //Act
            bool isDone = _sut.Delete(id);
            //Assert
            Assert.True(isDone);
        }
        
        [Fact]
        public void Delete_WhenGivenNotExistingId_ReturnFalse()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            _acces.Setup(x => x.DeleteRecipe(id))
                .Returns(false);
            //Act
            bool isDone = _sut.Delete(id);
            //Assert
            Assert.False(isDone);
        }
        
        [Fact]
        public void Delete_WhenGivenIdAndExceptionIsThrown_ReturnFalse()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            _acces.Setup(x => x.DeleteRecipe(id))
                .Throws(new Exception());
            //Act
            bool isDone = _sut.Delete(id);
            //Assert
            Assert.False(isDone);
        }

        [Fact]
        public void Add_WhenValidRecipe_ReturnsGuid()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            Recipe inRecipe = _validRecipe;
            inRecipe.RecipeId = id;
            _acces.Setup(x => x.CreateRecipe(inRecipe))
                .Returns(inRecipe.RecipeId);
            //Act
            Guid outId = _sut.Add(inRecipe);

            //Assert
            Assert.Equal(inRecipe.RecipeId, outId);
        }
        
        [Fact]
        public void Add_WhenRecipeAndExceptionIsThrown_ReturnsEmptyGuid()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            Recipe inRecipe = _validRecipe;
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
            Recipe inrecipe = _validRecipe;
            inrecipe.RecipeId = id;
            
            _acces.Setup(x => x.GetRandomRecipe(id))
                .Returns(inrecipe);
            _acces.Setup(x => x.GetRecipeById(id))
                .Returns(_validRecipe);
            //Act
            Recipe recipe = _sut.GetRandomRecipe(id);
            //Assert
            Assert.Equal(recipe.RecipeId, id);
        }

        [Fact]
        public void GetRandomRecipe_WhenGivenIdAndExceptionIsThrown_ReturnsNull()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            List<Recipe> recipes = new List<Recipe>()
            {
                _validRecipe,
                _validRecipe,
                _validRecipe
            };
            _acces.Setup(x => x.GetRandomRecipe(id))
                .Throws(new Exception());
            _acces.Setup(x => x.GetRecipeById(id))
                .Returns(_validRecipe);
            //Act
            Recipe recipe = _sut.GetRandomRecipe(id);
            //Assert
            Assert.Null(recipe);
        }
    }
}