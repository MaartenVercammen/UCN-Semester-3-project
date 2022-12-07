using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RecipeRestService.DTO;
using RecipeRestService.Security;
using RecipesData.Model;
using UserRestService.Businesslogic;
using UserRestService.Controllers;

namespace RecipeDataTest.Unit.Controller;

public class UserControllerTest
{
    private readonly Mock<IUserData> _acces = new Mock<IUserData>();
    private readonly Mock<ISecurityHelper> _securityHelper = new Mock<ISecurityHelper>();

    private readonly UserController _sut;

    private readonly DefaultHttpContext _httpContext = new DefaultHttpContext();
    
    public UserControllerTest()
    {
        _httpContext.Request.Headers["Authorization"] = "token";

        _sut = new UserController(_acces.Object, _securityHelper.Object)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = _httpContext
            }
        };
    }

    [Fact]
    public void Get_WhenGivenValidId_ReturnsUserDto()
    {
        //Arrange
        _securityHelper.Setup(x => x.GetRoleFromJWT("token"))
            .Returns(Role.USER);
        _securityHelper.Setup(x => x.IsJWTEqualRequestId("token", Data.Data._validUser.UserId.ToString()))
            .Returns(false);
        _acces.Setup(x => x.Get(Data.Data._validUser.UserId))
            .Returns(Data.Data._validUser);
        //Act
        var result = _sut.Get(Data.Data._validUser.UserId.ToString());

        //Assert
        var viewResult = Assert.IsType<ActionResult<UserDto>>(result);
        var model = Assert.IsAssignableFrom<OkObjectResult>(viewResult.Result);
        var user = Assert.IsAssignableFrom<UserDto>(model.Value);
        Assert.Equal(user.UserId, Data.Data._validUser.UserId);
    }

    [Fact]
    public void Get_WhenGivenInValidId_ReturnsStatusCodeNotFound()
    {
        //Arrange
        _securityHelper.Setup(x => x.GetRoleFromJWT("token"))
            .Returns(Role.USER);
        _securityHelper.Setup(x => x.IsJWTEqualRequestId("token", Data.Data._validUser.UserId.ToString()))
            .Returns(false);
        _acces.Setup(x => x.Get(Data.Data._validUser.UserId))
            .Returns(null as User);
        //Act
        var result = _sut.Get(Data.Data._validUser.UserId.ToString());

        //Assert
        var viewResult = Assert.IsType<ActionResult<UserDto>>(result);
        var model = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(404, model.StatusCode);
    }

    [Fact]
    public void Get_WhenGivenValidIdNotUserIdWhenUser_ReturnsStatusCodeNotFound()
    {
        //Arrange
        _securityHelper.Setup(x => x.GetRoleFromJWT("token"))
            .Returns(Role.USER);
        _securityHelper.Setup(x => x.IsJWTEqualRequestId("token", Data.Data._validUser.UserId.ToString()))
            .Returns(true);
        _acces.Setup(x => x.Get(Data.Data._validUser.UserId))
            .Returns(null as User);
        //Act
        var result = _sut.Get(Data.Data._validUser.UserId.ToString());

        //Assert
        var viewResult = Assert.IsType<ActionResult<UserDto>>(result);
        var model = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(403, model.StatusCode);
    }

    [Fact]
    public void Get_WhenGivenNoId_ReturnsListOfUserDto()
    {
        //Arrange
        _acces.Setup(x => x.Get())
            .Returns(Data.Data._users);
        //Act
        var result = _sut.Get();

        //Assert
        var viewResult = Assert.IsType<ActionResult<List<UserDto>>>(result);
        var model = Assert.IsAssignableFrom<OkObjectResult>(viewResult.Result);
        var users = Assert.IsAssignableFrom<List<UserDto>>(model.Value);
        Assert.Equal(users.Count, Data.Data._users.Count);

    }

    [Fact]
    public void Get_WhenGivenNoIdAndNoUsersInDB_ReturnsStatusCode204()
    {
        //Arrange
        _acces.Setup(x => x.Get())
            .Returns(new List<User>());
        //Act
        var result = _sut.Get();

        //Assert
        var viewResult = Assert.IsType<ActionResult<List<UserDto>>>(result);
        var model = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(204, model.StatusCode);
    }

    [Fact]
    public void Get_WhenGivenNoIdAndExceptionIsThrown_ReturnsStatusCode500()
    {
        //Arrange
        _acces.Setup(x => x.Get())
            .Returns(null as List<User>);
        //Act
        var result = _sut.Get();

        //Assert
        var viewResult = Assert.IsType<ActionResult<List<UserDto>>>(result);
        var model = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(500, model.StatusCode);
    }

    [Fact]
    public void Post_WhenGivenNoInput_ReturnsInsertedGuid()
    {
        //Arrange
        _acces.Setup(x => x.Add(It.IsAny<User>()))
            .Returns(Data.Data._validUser.UserId);
        //Act
        var result = _sut.Post(Data.Data._UserDto);

        //Assert
        var viewResult = Assert.IsType<ActionResult<Guid>>(result);
        var model = Assert.IsAssignableFrom<OkObjectResult>(viewResult.Result);
        var id = Assert.IsAssignableFrom<Guid>(model.Value);
        Assert.Equal(id, Data.Data._validUser.UserId);

    }

    [Fact]
    public void Post_WhenGivenInvalidData_ReturnsStatusCode500()
    {
        //Arrange
        _acces.Setup(x => x.Add(Data.Data._validUser))
            .Returns(Guid.Empty);
        //Act
        var result = _sut.Post(Data.Data._UserDto);

        //Assert
        var viewResult = Assert.IsType<ActionResult<Guid>>(result);
        var model = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(500, model.StatusCode);
    }

    [Fact]
    public void Edit_WhenGivenValidInput_ReturnsTrue()
    {
        //Arrange
        _securityHelper.Setup(x => x.GetRoleFromJWT("token"))
            .Returns(Role.USER);
        _securityHelper.Setup(x => x.IsJWTEqualRequestId("token", Data.Data._validUser.UserId.ToString()))
            .Returns(false);
        _acces.Setup(x => x.Put(It.IsAny<User>()))
            .Returns(true);
        //Act
        var result = _sut.Edit(Data.Data._UserDto);

        //Assert
        var viewResult = Assert.IsType<ActionResult<bool>>(result);
        var model = Assert.IsAssignableFrom<OkObjectResult>(viewResult.Result);
        var IsDone = Assert.IsAssignableFrom<bool>(model.Value);
        Assert.True(IsDone);
    }

    [Fact]
    public void Put_WhenGivenInValidInput_ReturnsStatusCode500()
    {
        //Arrange
        _securityHelper.Setup(x => x.GetRoleFromJWT("token"))
            .Returns(Role.USER);
        _securityHelper.Setup(x => x.IsJWTEqualRequestId("token", Data.Data._validUser.UserId.ToString()))
            .Returns(false);
        _acces.Setup(x => x.Put(It.IsAny<User>()))
            .Returns(false);
        //Act
        var result = _sut.Edit(Data.Data._UserDto);

        //Assert
        var viewResult = Assert.IsType<ActionResult<bool>>(result);
        var model = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(500, model.StatusCode);
    }

    [Fact]
    public void Put_WhenGivenValidInputAndUserIsRoleUserAndUserToUpdateNotUser_ReturnsStatusCode403()
    {
        //Arrange
        _securityHelper.Setup(x => x.GetRoleFromJWT("token"))
            .Returns(Role.USER);
        _securityHelper.Setup(x => x.IsJWTEqualRequestId("token", Data.Data._UserDto.UserId.ToString()))
            .Returns(true);
        _acces.Setup(x => x.Put(It.IsAny<User>()))
            .Returns(false);
        //Act
        var result = _sut.Edit(Data.Data._UserDto);

        //Assert
        var viewResult = Assert.IsType<ActionResult<bool>>(result);
        var model = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(403, model.StatusCode);
    }
    
    [Fact]
    public void Delete_WhenGivenValidId_ReturnsTrue()
    {
        //Arrange
        _securityHelper.Setup(x => x.GetRoleFromJWT("token"))
            .Returns(Role.USER);
        _securityHelper.Setup(x => x.IsJWTEqualRequestId("token", Data.Data._validUser.UserId.ToString()))
            .Returns(false);
        _acces.Setup(x => x.Delete(Data.Data._validUser.UserId))
            .Returns(true);
        //Act
        var result = _sut.Delete(Data.Data._validUser.UserId.ToString());

        //Assert
        var viewResult = Assert.IsType<ActionResult<bool>>(result);
        var model = Assert.IsAssignableFrom<OkObjectResult>(viewResult.Result);
        var IsDone = Assert.IsAssignableFrom<bool>(model.Value);
        Assert.True(IsDone);
    }

    [Fact]
    public void Delete_WhenGivenInValidInput_ReturnsStatusCode500()
    {
        //Arrange
        _securityHelper.Setup(x => x.GetRoleFromJWT("token"))
            .Returns(Role.USER);
        _securityHelper.Setup(x => x.IsJWTEqualRequestId("token", Data.Data._validUser.UserId.ToString()))
            .Returns(false);
        _acces.Setup(x => x.Delete(Data.Data._validUser.UserId))
            .Returns(false);
        //Act
        var result = _sut.Delete(Data.Data._validUser.UserId.ToString());

        //Assert
        var viewResult = Assert.IsType<ActionResult<bool>>(result);
        var model = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(500, model.StatusCode);
    }

    [Fact]
    public void Delete_WhenGivenValidInputAndUserIsRoleUserAndUserToUpdateNotUser_ReturnsStatusCode403()
    {
        //Arrange
        _securityHelper.Setup(x => x.GetRoleFromJWT("token"))
            .Returns(Role.USER);
        _securityHelper.Setup(x => x.IsJWTEqualRequestId("token", Data.Data._validUser.UserId.ToString()))
            .Returns(true);
        _acces.Setup(x => x.Delete(Data.Data._validUser.UserId))
            .Returns(false);
        //Act
        var result = _sut.Delete(Data.Data._validUser.UserId.ToString());

        //Assert
        var viewResult = Assert.IsType<ActionResult<bool>>(result);
        var model = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(403, model.StatusCode);
    }

}