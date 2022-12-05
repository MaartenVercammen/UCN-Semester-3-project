using Moq;
using RecipeRestService.Businesslogic;
using RecipesData.Database;
using RecipesData.Model;

namespace RecipeDataTest.Unit.BusinessLogic;

public class AuthenticationDataControlTest
{
    private readonly AuthenticationDataControl _sut;
    private readonly Mock<IUserAccess> _access = new Mock<IUserAccess>();

    private readonly User _validUser;
    private readonly User _inValidUser;
    
    public AuthenticationDataControlTest()
    {
        _sut = new AuthenticationDataControl(_access.Object);
        _validUser = new User("email", "mark", "mark", "password", "home", Role.USER);
        _inValidUser = new User("email", "mark", "mark", "wrong", "home", Role.USER);
    }

    [Fact]
    public void Login_WhenSucces_ReturnsUser()
    {
        //Arrange
        string hashedPassword = 
        _access.Setup(x => x.GetUserByEmail(_validUser.Email))
            .Returns(_validUser);
        //Act
        var response = _sut.Login(_validUser.Email, _validUser.Password);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(_validUser.UserId, response.UserId);
    }

    [Fact]
    public void Login_WhenInValidCreaentials_ReturnsNull()
    {
        //Arrange
        _access.Setup(x => x.GetUserByEmail(_validUser.Email))
            .Returns(_inValidUser);
        //Act
        var response = _sut.Login(_validUser.Email, _validUser.Password);

        //Assert
        Assert.Null(response);
    }

    [Fact]
    public void Login_WhenThrownError_ReturnsNull()
    {
        //Arrange
        _access.Setup(x => x.GetUserByEmail(_validUser.Email))
            .Throws(new Exception());
        //Act
        var response = _sut.Login(_validUser.Email, _validUser.Password);

        //Assert
        Assert.Null(response);
    }
}