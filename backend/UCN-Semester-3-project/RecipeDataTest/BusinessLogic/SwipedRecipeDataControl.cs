using Moq;
using RecipeRestService.Businesslogic;
using Xunit.Abstractions;
using RecipesData.Database;
using RecipesData.Model;

namespace RecipeDataTest
{
    public class SwipedRecipeDataAccessTest
    {
        //Acting classes
        private readonly ITestOutputHelper extraOutput;
        private readonly Mock<ISwipedRecipeAccess> _access = new Mock<ISwipedRecipeAccess>();
        private readonly SwipedRecipeDataControl _sut;

        //helper classes
        private readonly User _user;
        private readonly Recipe _recipe;
        
        //validation classes
        private readonly SwipedRecipe _swipedRecipeLiked;
        private readonly SwipedRecipe _swipedRecipeDisliked;
        
        readonly string _connectionString = "data Source=foodpanda.dev,1400; Database=ucn; User Id=dev;Password=dev;";
        public SwipedRecipeDataAccessTest(ITestOutputHelper output)
        {
            this.extraOutput = output;
            _sut = new SwipedRecipeDataControl(_access.Object);
            _user = new User("mail", "mark", "mark", "password","recipe street 3200 Diest" ,Role.USER);
            _recipe = new Recipe("bananan", "just a fruit", "http://banana.png", 10, 5, _user);
            _swipedRecipeLiked = new SwipedRecipe(_user.UserId, _recipe.RecipeId, true);
            _swipedRecipeDisliked = new SwipedRecipe(_user.UserId, _recipe.RecipeId, false);
        }

        [Fact]
        public void Get_WhenGivenId_ReturnsSwipedRecipe()
        {
            //TODO: resolve issue github https://github.com/MaartenVercammen/UCN-Semester-3-project/issues/58
            //Arrange
            Guid id = Guid.NewGuid();
            SwipedRecipe inSwipedRecipe = _swipedRecipeLiked;
            
            _access.Setup(x => x.GetSwipedRecipeById(id))
                .Returns(_swipedRecipeLiked);
            //Act

            //Assert
        }

        
    }
}