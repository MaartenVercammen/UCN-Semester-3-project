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
        public void Get_WhenValidRecipeIdAndUserID_ReturnsSwipedRecipe()
        {
            //Arrange
            _access.Setup(x => x.GetSwipedRecipeById(_recipe.RecipeId, _user.UserId))
                .Returns(_swipedRecipeLiked);
            //Act
            var response = _sut.Get(_recipe.RecipeId, _user.UserId);

            //Assert    
            Assert.NotNull(response);
            Assert.Equal(_swipedRecipeLiked.RecipeId, response.RecipeId);
        }

        [Fact]
        public void Get_WhenThrownError_ReturnsNull()
        {
            //Arrange
            _access.Setup(x => x.GetSwipedRecipeById(_recipe.RecipeId, _user.UserId))
                .Throws(new Exception());
            //Act
            var response = _sut.Get(_recipe.RecipeId, _user.UserId);

            //Assert    
            Assert.Null(response);
        }

        [Fact]
        public void GetPerUser_WhenValidUserID_ReturnsRecipesForUser()
        {
            //Arrange
            List<SwipedRecipe> recipes = new List<SwipedRecipe>()
            {
                _swipedRecipeLiked,
                _swipedRecipeLiked,
                _swipedRecipeLiked,
                _swipedRecipeLiked,
                _swipedRecipeLiked
            };
            _access.Setup(x => x.GetSwipeRecipesByUser(_user.UserId))
                .Returns(recipes);
            //Act
            var response = _sut.GetPerUser(_user.UserId);
            //Assert
            Assert.NotNull(response);
            Assert.Equal(recipes.Count, response.Count);
        }

        [Fact]
        public void GetPerUser_WhenThrownError_ReturnsNull()
        {
            //Arrange
            _access.Setup(x => x.GetSwipeRecipesByUser(_user.UserId))
                .Throws(new Exception());
            //Act
            var response = _sut.GetPerUser(_user.UserId);
            //Assert
            Assert.Null(response);
        }

        [Fact]
        public void GetLikedPerUser_WhenValidUserID_ReturnsRecipesForUser()
        {
            //Arrange
            List<SwipedRecipe> recipes = new List<SwipedRecipe>()
            {
                _swipedRecipeLiked,
                _swipedRecipeLiked,
                _swipedRecipeLiked,
                _swipedRecipeLiked,
                _swipedRecipeLiked
            };
            _access.Setup(x => x.GetLikedByUser(_user.UserId))
                .Returns(recipes);
            //Act
            var response = _sut.GetLikedPerUser(_user.UserId);
            //Assert
            Assert.NotNull(response);
            Assert.Equal(recipes.Count, response.Count);
        }

        [Fact]
        public void GetLikedPerUser_WhenThrownError_ReturnsNull()
        {
            //Arrange
            _access.Setup(x => x.GetLikedByUser(_user.UserId))
                .Throws(new Exception());
            //Act
            var response = _sut.GetLikedPerUser(_user.UserId);
            //Assert
            Assert.Null(response);
        }

        [Fact]
        public void Add_WhenValid_ReturnsAddedRecipe()
        {
            //Arrange
            _access.Setup(x => x.CreateSwipedRecipe(_swipedRecipeLiked))
                .Returns(_swipedRecipeLiked);
            //Act
            var response = _sut.Add(_swipedRecipeLiked);
            //Assert
            Assert.NotNull(response);
            Assert.Equal(_swipedRecipeLiked.RecipeId, response.RecipeId);
        }

        [Fact]
        public void Add_WhenErrorIsThrown_ReturnsNull()
        {
            //Arrange
            _access.Setup(x => x.CreateSwipedRecipe(_swipedRecipeLiked))
                .Throws(new Exception());
            //Act
            var response = _sut.Add(_swipedRecipeLiked);
            //Assert
            Assert.Null(response);
         }

        [Fact]
        public void Delete_WhenValid_ReturnsTrue()
        {
            //Arrange
            _access.Setup(x => x.DeleteSR(_swipedRecipeLiked.RecipeId, _user.UserId))
                .Returns(true);
            //Act
            var response = _sut.Delete(_swipedRecipeLiked.RecipeId, _user.UserId);
            //Assert
            Assert.NotNull(response);
            Assert.True(response);
        }

        [Fact]
        public void Delete_WhenInValid_ReturnsFalse()
        {
            //Arrange
            _access.Setup(x => x.DeleteSR(_swipedRecipeLiked.RecipeId, _user.UserId))
                .Returns(false);
            //Act
            var response = _sut.Delete(_swipedRecipeLiked.RecipeId, _user.UserId);
            //Assert
            Assert.NotNull(response);
            Assert.False(response);
        }

        [Fact]
        public void Delete_WhenThrownError_ReturnsFalse()
        {
            //Arrange
            _access.Setup(x => x.DeleteSR(_swipedRecipeLiked.RecipeId, _user.UserId))
                .Throws(new Exception());
            //Act
            var response = _sut.Delete(_swipedRecipeLiked.RecipeId, _user.UserId);
            //Assert
            Assert.False(response);
        }
    }
}