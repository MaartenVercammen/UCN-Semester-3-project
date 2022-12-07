using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Moq;
using RecipeRestService.Businesslogic;
using RecipeRestService.Controllers;
using RecipeRestService.Security;
using RecipesData.Model;
using RecipeDataTest.Unit.Data;
using RecipeRestService.DTO;

namespace RecipeDataTest.Unit.Controller;

public class AuthorizationConrtollerTest
{
    private readonly Mock<IAuthenticationData> _authenticationData = new Mock<IAuthenticationData>();
    private readonly Mock<ISecurityHelper> _securityHelper = new Mock<ISecurityHelper>();

    private readonly DefaultHttpContext _httpContext = new DefaultHttpContext();
    
    private readonly AuthorizationController _sut;
    
    private readonly User _user;

    private readonly byte[] _bytes;

    public AuthorizationConrtollerTest()
    {
        _httpContext.Response.Headers["token"] = "";
        _httpContext.Response.Headers["Access-Control-Expose-Headers"] = "";
        _sut = new AuthorizationController(_authenticationData.Object, _securityHelper.Object)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = _httpContext
            }
        };
        _user = Data.Data._validUser;
        _bytes = new byte[]
        {
            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20
        };
    }

    [Fact]
    public void Get_WhenGivenLoginData_ReturnUserDto()
    {
        //Arrange
        _authenticationData.Setup(x => x.Login(_user.Email, _user.Password))
            .Returns(_user);
        _securityHelper.Setup(x => x.GetSecurityKey())
            .Returns(new SymmetricSecurityKey(_bytes));
        //Act
        var result = _sut.Login( _user.Password, _user.Email);
        //Assert
        var viewResult = Assert.IsType<ActionResult<UserDto>>(result);
        var model = Assert.IsAssignableFrom<OkObjectResult>(viewResult.Result);
        var user = Assert.IsAssignableFrom<UserDto>(model.Value);
        Assert.Equal(_user.UserId.ToString(), user.UserId.ToString());
    }

    [Fact]
    public void Get_WhenGivenFalseLoginData_ReturnStatusCode401()
    {
        //Arrange
        _authenticationData.Setup(x => x.Login(_user.Email, _user.Password))
            .Returns(null as User);
        _securityHelper.Setup(x => x.GetSecurityKey())
            .Returns(new SymmetricSecurityKey(_bytes));
        //Act
        var result = _sut.Login(_user.Password, _user.Email);
        //Assert
        var viewResult = Assert.IsType<ActionResult<UserDto>>(result);
        var model = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(401, model.StatusCode);
    }

    [Fact]
    public void Get_WhenGivenErrorIsThrown_ReturnStatusCode500()
    {
        //Arrange
        _authenticationData.Setup(x => x.Login(_user.Email, _user.Password))
            .Throws(new  Exception());
        _securityHelper.Setup(x => x.GetSecurityKey())
            .Returns(new SymmetricSecurityKey(_bytes));
        //Act
        var result = _sut.Login(_user.Password, _user.Email);
        //Assert
        var viewResult = Assert.IsType<ActionResult<UserDto>>(result);
        var model = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(500, model.StatusCode);
    }
    
    
}