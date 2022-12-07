using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RecipeRestService.Businesslogic;
using RecipeRestService.Controllers;
using RecipeRestService.DTO;
using RecipeRestService.Security;
using RecipesData.Model;

namespace RecipeDataTest.Unit.Controller;

public class BambooSessionControllerTest
{
    private readonly Mock<IBambooSessionData> _bControl = new Mock<IBambooSessionData>();
    private readonly Mock<ISecurityHelper> _securityHelper = new Mock<ISecurityHelper>();

    private readonly Mock<IUserData> _userData = new Mock<IUserData>();

    private readonly Mock<IRecipeData> _recipeData = new Mock<IRecipeData>();

    private readonly BambooSessionController _sut;
    private readonly DefaultHttpContext _httpContext = new DefaultHttpContext();


    public BambooSessionControllerTest()
    {
        _httpContext.Request.Headers["Authorization"] = "token";
        _sut = new BambooSessionController(_securityHelper.Object, _bControl.Object, _userData.Object,
            _recipeData.Object)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = _httpContext
            }
        };
    }

    [Fact]
    public void Get_WhenGivenValidId_ReturnsBambooSessionDto()
    {
        //Arrange
        _bControl.Setup(x => x.Get(Data.Data._validBambooSession.SessionId))
            .Returns(Data.Data._validBambooSession);
        //Act
        var result = _sut.GetBambooSession(Data.Data._validBambooSession.SessionId.ToString());
        //Assert
        var viewResult = Assert.IsType<ActionResult<BambooSessionDto>>(result);
        var model = Assert.IsAssignableFrom<OkObjectResult>(viewResult.Result);
        var bambooSessionDto = Assert.IsAssignableFrom<BambooSessionDto>(model.Value);
        Assert.Equal(bambooSessionDto.SessionId, Data.Data._validBambooSession.SessionId);
    }

    [Fact]
    public void Get_WhenGivenInValidId_ReturnsNotFound()
    {
        //Arrange
        _bControl.Setup(x => x.Get(Data.Data._validBambooSession.SessionId))
            .Returns(null as BambooSession);
        //Act
        var result = _sut.GetBambooSession(Data.Data._validBambooSession.SessionId.ToString());
        //Assert
        var viewResult = Assert.IsType<ActionResult<BambooSessionDto>>(result);
        var statusCodeResult = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(404, statusCodeResult.StatusCode);

    }

    [Fact]
    public void Get_WhenGivenNoId_ReturnsListOfBambooSessionDto()
    {
        //Arrange
        _bControl.Setup(x => x.Get())
            .Returns(Data.Data._ListofBambooSessions);
        //Act
        var result = _sut.GetBambooSessions();
        //Assert
        var viewResult = Assert.IsType<ActionResult<List<BambooSessionDto>>>(result);
        var model = Assert.IsAssignableFrom<OkObjectResult>(viewResult.Result);
        var bambooSessionDtos = Assert.IsAssignableFrom<List<BambooSessionDto>>(model.Value);
        Assert.Equal(bambooSessionDtos.Count, Data.Data._ListofBambooSessions.Count);
    }

    [Fact]
    public void Get_WhenGivenNoIdAndNoSessionsInDB_ReturnsNotContent()
    {
        //Arrange
        _bControl.Setup(x => x.Get())
            .Returns(new List<BambooSession>());
        //Act
        var result = _sut.GetBambooSessions();
        //Assert
        var viewResult = Assert.IsType<ActionResult< List < BambooSessionDto>>>(result);
        var statusCodeResult = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(204, statusCodeResult.StatusCode);

    }

    [Fact]
    public void Get_WhenError_Returns500()
    {
        //Arrange
        _bControl.Setup(x => x.Get())
            .Returns(null as List<BambooSession>);
        //Act
        var result = _sut.GetBambooSessions();
        //Assert
        var viewResult = Assert.IsType<ActionResult< List < BambooSessionDto>>>(result);
        var statusCodeResult = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }

    [Fact]
    public void Post_WhenValid_ReturnsGuid()
    {
        //Arrange
        _securityHelper.Setup(x => x.GetUserFromJWT("token"))
            .Returns(Data.Data._validUser.UserId);
        _userData.Setup(x => x.Get(Data.Data._validUser.UserId))
            .Returns(Data.Data._validUser);
        _recipeData.Setup(x => x.Get(Data.Data._recipe.RecipeId))
            .Returns(Data.Data._recipe);
        _bControl.Setup(x => x.Add(It.IsAny<BambooSession>()))
            .Returns(Data.Data._validBambooSession.SessionId);
        //Act
        var result = _sut.Post(Data.Data._validBambooSessionDto);

        //Assert
        var viewResult = Assert.IsType<ActionResult<Guid>>(result);
        var model = Assert.IsAssignableFrom<OkObjectResult>(viewResult.Result);
        var id = Assert.IsAssignableFrom<Guid>(model.Value);
        Assert.Equal(id.ToString(), Data.Data._validBambooSession.SessionId.ToString());

    }

    [Fact]
    public void Post_WhenInValid_Returns500()
    {
        //Arrange
        _securityHelper.Setup(x => x.GetUserFromJWT("token"))
            .Returns(Data.Data._validUser.UserId);
        _userData.Setup(x => x.Get(Data.Data._validUser.UserId))
            .Returns(Data.Data._validUser);
        _recipeData.Setup(x => x.Get(Data.Data._recipe.RecipeId))
            .Returns(Data.Data._recipe);
        _bControl.Setup(x => x.Add(It.IsAny<BambooSession>()))
            .Returns(Guid.Empty);
        //Act
        var result = _sut.Post(Data.Data._validBambooSessionDto);

        //Assert
        var viewResult = Assert.IsType<ActionResult<Guid>>(result);
        var statusCodeResult = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(500, statusCodeResult.StatusCode);

    }

    [Fact]
    public void Post_WhenInValidUserOrRecipe_Returns500()
    {
        //Arrange
        _securityHelper.Setup(x => x.GetUserFromJWT("token"))
            .Returns(Data.Data._validUser.UserId);
        _userData.Setup(x => x.Get(Data.Data._validUser.UserId))
            .Returns(null as User);
        _recipeData.Setup(x => x.Get(Data.Data._recipe.RecipeId))
            .Returns(null as Recipe);
        _bControl.Setup(x => x.Add(It.IsAny<BambooSession>()))
            .Returns(Data.Data._validBambooSession.SessionId);
        //Act
        var result = _sut.Post(Data.Data._validBambooSessionDto);

        //Assert
        var viewResult = Assert.IsType<ActionResult<Guid>>(result);
        var statusCodeResult = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }

    [Fact]
    public void JoinBambooSession_When_Valid_Returns_True()
    {
        //Arrange
        _securityHelper.Setup(x => x.GetUserFromJWT("token"))
            .Returns(Data.Data._validUser.UserId);
        _bControl.Setup(x => x.GetSeatBySessionAndSeatId(Data.Data._validBambooSession, Data.Data._seat.SeatId))
            .Returns(Data.Data._seat);
        _bControl.Setup(x =>
                x.Join(Data.Data._validBambooSession, Data.Data._validUser, Data.Data._seat))
            .Returns(true);
        _bControl.Setup(x => x.Get(Data.Data._validBambooSession.SessionId))
            .Returns(Data.Data._validBambooSession);
        _userData.Setup(x => x.Get(Data.Data._validUser.UserId))
            .Returns(Data.Data._validUser);
        //Act
        var result = _sut.JoinBambooSession(Data.Data._validBambooSession.SessionId.ToString(),
            Data.Data._seat.SeatId.ToString());
        //Assert
        var viewResult = Assert.IsType<ActionResult<bool>>(result);
        var model = Assert.IsAssignableFrom<OkObjectResult>(viewResult.Result);
        var isDone = Assert.IsAssignableFrom<bool>(model.Value);
        Assert.True(isDone);
    }

    [Fact]
    public void JoinBambooSession_When_Invalid_Returns_False()
    {
        //Arrange
        _securityHelper.Setup(x => x.GetUserFromJWT("token"))
            .Returns(Data.Data._validUser.UserId);
        _bControl.Setup(x =>
                x.Join(Data.Data._validBambooSession, Data.Data._validUser, Data.Data._seat))
            .Returns(false);
        //Act
        var result = _sut.JoinBambooSession(Data.Data._validBambooSession.SessionId.ToString(),
            Data.Data._seat.SeatId.ToString());
        //Assert
        var viewResult = Assert.IsType<ActionResult<bool>>(result);
        var model = Assert.IsAssignableFrom<OkObjectResult>(viewResult.Result);
        var isDone = Assert.IsAssignableFrom<bool>(model.Value);
        Assert.False(isDone);
    }

    [Fact]
    private void GetSeatsBySessionId_When_Valid_SessionId_ReturnsOk()
    {
        //Arrange
        _bControl.Setup(x => x.GetSeatsBySessionId(Data.Data._validBambooSession.SessionId))
            .Returns(Data.Data._seats);
        //Act
        var result = _sut.GetSeatsBySessionId(Data.Data._validBambooSession.SessionId.ToString());
        //Assert
        var viewResult = Assert.IsType<ActionResult<List<SeatDto>>>(result);
        var model = Assert.IsAssignableFrom<OkObjectResult>(viewResult.Result);
        var seatsDto = Assert.IsAssignableFrom<List<SeatDto>>(model.Value);
        Assert.Equal(Data.Data._seats.Count, seatsDto.Count);
    }

    [Fact]
    private void GetSeatsBySessionId_When_Invalid_SessionId_Returns500()
    {
        //Arrange
        _bControl.Setup(x => x.GetSeatsBySessionId(Data.Data._validBambooSession.SessionId))
            .Returns(null as List<Seat>);
        //Act
        var result = _sut.GetSeatsBySessionId(Data.Data._validBambooSession.SessionId.ToString());
        //Assert
        var viewResult = Assert.IsType<ActionResult<List<SeatDto>>>(result);
        var statusCodeResult = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }

    [Fact]
    private void GetSeatsBySessionId_WhenValidSessionId_Returns_NotFound()
    {
        //Arrange
        _bControl.Setup(x => x.GetSeatsBySessionId(Data.Data._validBambooSession.SessionId))
            .Returns(new List<Seat>());
        //Act
        var result = _sut.GetSeatsBySessionId(Data.Data._validBambooSession.SessionId.ToString());
        //Assert
        var viewResult = Assert.IsType<ActionResult<List<SeatDto>>>(result);
        var statusCodeResult = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(404, statusCodeResult.StatusCode);
    }

    [Fact]
    private void Delete_When_ValidId_Returns_True()
    {
        //Arrange
        _bControl.Setup(x => x.Delete(Data.Data._validBambooSession.SessionId))
            .Returns(true);
        //Act
        var result = _sut.Delete(Data.Data._validBambooSession.SessionId.ToString());
        //Assert
        var viewResult = Assert.IsType<ActionResult<bool>>(result);
        var model = Assert.IsAssignableFrom<OkObjectResult>(viewResult.Result);
        var isDone = Assert.IsAssignableFrom<bool>(model.Value);
        Assert.True(isDone);
    }

    [Fact]
    private void Delete_When_InvalidId_Returns_False()
    {
        //Arrange
        _bControl.Setup(x => x.Delete(Data.Data._validBambooSession.SessionId))
            .Returns(false);
        //Act
        var result = _sut.Delete(Data.Data._validBambooSession.SessionId.ToString());
        //Assert
        var viewResult = Assert.IsType<ActionResult<bool>>(result);
        var statusCodeResult = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(404, statusCodeResult.StatusCode);
    }
}
