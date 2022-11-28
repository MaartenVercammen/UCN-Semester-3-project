using Moq;
using RecipeRestService.Businesslogic;
using RecipesData.Database;
using RecipesData.Model;
using Xunit.Abstractions;

namespace RecipeDataTest.BusinessLogic
{
    public class UserDataControlTest
    {

        private readonly ITestOutputHelper _extraOutput;
        private readonly UserDataControl _sut;
        private readonly Mock<IUserAccess> _acces = new Mock<IUserAccess>();

        // Valid object
        private readonly User _validUser;


        public UserDataControlTest(ITestOutputHelper output)
        {
            _extraOutput = output;
            _sut = new UserDataControl(_acces.Object);
            // Valid object
            _validUser = new User(Guid.Parse("00000000-0000-0000-0000-000000000000"),  "email", "mark", "mark", "pass",
                "home", Role.USER);
        }

        [Fact]
        public void Get_WhenGivenId_ReturnsUser()
        {
            //Arrange
            Guid id = new Guid();
            User inUser = _validUser;
            inUser.UserId = id;
            _acces.Setup(x => x.GetUserById(id))
                .Returns(inUser);
            
            //Act
            var user = _sut.Get(id);

            //Assert
            Assert.NotNull(user);
            Assert.Equal(id, user.UserId);
        }

        [Fact]
        public void Get_WhengivenIdAndExceptionIsThrown_ReturnsIdAsNull()
        {
            //Arrange
            Guid id = Guid.Empty;
            _acces.Setup(x => x.GetUserById(id))
                .Throws(new Exception());
            //Act
            User? user = _sut.Get(id);
            //Assert
            Assert.Null(user);
        }

        [Fact]
        public void Get_WhenGivenNoId_ReturnsListOfUsers()
        {
            //Arrange
            List<User> inUser = new List<User>()
            {
                _validUser,
                _validUser,
                _validUser,
                _validUser
            };
            _acces.Setup(x => x.GetUsers())
                .Returns(inUser);
            
            //Act
            List<User>? users = _sut.Get();

            //Assert
            Assert.NotNull(users);
            Assert.Equal(inUser.Count, users.Count);
        }

        [Fact]
        public void Get_WhenGivenNoIdAndExceptionIsThrown_ReturnEmptyList()
        {
            //Arrange
            _acces.Setup(x => x.GetUsers())
                .Throws(new Exception());
            
            //Act
            List<User>? users = _sut.Get();
            //Assert
            Assert.Null(users);
        }

        [Fact]
        public void Delete_WhenGivenId_ReturnTrue()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            _acces.Setup(x => x.DeleteUser(id))
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
            _acces.Setup(x => x.DeleteUser(id))
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
            _acces.Setup(x => x.DeleteUser(id))
                .Throws(new Exception());
            //Act
            bool isDone = _sut.Delete(id);
            //Assert
            Assert.False(isDone);
        }

        [Fact]
        public void Add_WhenValidUser_ReturnsGuid()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            User inUser = _validUser;
            inUser.UserId = id;
            _acces.Setup(x => x.CreateUser(inUser))
                .Returns(inUser.UserId);
            //Act
            Guid outId = _sut.Add(inUser);

            //Assert
            Assert.Equal(inUser.UserId, outId);
        }
        
        [Fact]
        public void Add_WhenRecipeAndExceptionIsThrown_ReturnsEmptyGuid()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            User inUser = _validUser;
            inUser.UserId = id;
            _acces.Setup(x => x.CreateUser(inUser))
                .Throws(new Exception());
            //Act
            Guid outId = _sut.Add(inUser);

            //Assert
            Assert.Equal(Guid.Empty, outId);
        }        
    }
}