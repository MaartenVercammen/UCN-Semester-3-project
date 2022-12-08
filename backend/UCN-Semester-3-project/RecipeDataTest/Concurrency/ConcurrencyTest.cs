using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using RecipeRestService.Businesslogic;
using RecipeRestService.Controllers;
using RecipeRestService.DTO;
using RecipeRestService.Security;
using RecipesData.Database;
using RecipesData.Model;

namespace RecipeDataTest.Concurrency;

public class ConcurrencyTest
{

    private readonly BambooSessionController _bambooSessionController;
    private readonly BambooSessionDto _bambooSessionDto;
    private readonly Mock<ISecurityHelper> _securityHelper = new Mock<ISecurityHelper>();
    private readonly Mock<IUserData> _userData = new Mock<IUserData>();
    private readonly Mock<IRecipeData> _recipeData = new Mock<IRecipeData>();
    private readonly Mock<IRecipeAccess> _recipeAccess = new Mock<IRecipeAccess>();
    private readonly Mock<IUserAccess> _userAccess = new Mock<IUserAccess>();
    private readonly Mock<IConfiguration> _configuration = new Mock<IConfiguration>();
    private readonly DefaultHttpContext _httpContext = new DefaultHttpContext();
    private Guid _sessionId;
    private Recipe _recipe;
    private User _user;
    private Guid _seatId;

    public ConcurrencyTest()
    {
        var mockConfSection = new Mock<IConfigurationSection>();
        mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "UcnConnection")])
            .Returns("data Source=foodpanda.dev,1400; Database=ucn; User Id=dev;Password=dev;");

        _configuration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings")))
            .Returns(mockConfSection.Object);
        
        IBambooSessionAccess bambooSessionAccess = new BambooSessionDatabaseAccess(_configuration.Object, _recipeAccess.Object, _userAccess.Object);
        IBambooSessionData bambooSessionData = new BambooSessionDataControl(bambooSessionAccess);
        _httpContext.Request.Headers["Authorization"] = "token";
        _bambooSessionController = new BambooSessionController(_securityHelper.Object, bambooSessionData, _userData
            .Object, _recipeData.Object)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = _httpContext
            }
        };
        _user = new User(Guid.Parse("c513d6c8-b67f-4db5-a44f-1d117859ac9d"),"email", "Mark", "Mark", "password", "Home", Role.VERIFIEDUSER);
        _recipe = new Recipe(Guid.Parse("51789a4d-1883-45bb-ac20-f275b94500a3"),"Test", "Test me", "http://test.png", 60, 4, _user);
        _bambooSessionDto = new BambooSessionDto(_user.UserId, "Home", _recipe.RecipeId, "Come and eat", DateTime.Now, 4);
    }

    private void StartUp()
    {
        ActionResult<Guid> actionResult = _bambooSessionController.Post(_bambooSessionDto);
        var viewResult = Assert.IsType<ActionResult<Guid>>(actionResult);
        var model = Assert.IsAssignableFrom<OkObjectResult>(viewResult.Result);
        _sessionId = Assert.IsAssignableFrom<Guid>(model.Value);
        ActionResult<List<SeatDto>> actionResultSeat = _bambooSessionController.GetSeatsBySessionId(_sessionId.ToString());
        var viewResultSeat = Assert.IsType<ActionResult<List<SeatDto>>>(actionResultSeat);
        var modelSeat = Assert.IsAssignableFrom<OkObjectResult>(viewResultSeat.Result);
        List<SeatDto> seats = Assert.IsAssignableFrom<List<SeatDto>>(modelSeat.Value);
        _seatId = seats[0].SeatId;
    }

    private void Teardown()
    {
        _bambooSessionController.Delete(_sessionId.ToString());
    }
    
    [Fact]
    public async Task ConcurrencyTesting()
    {
        //Arrange
        _securityHelper.Setup(x => x.GetUserFromJWT("token"))
            .Returns(_user.UserId);
        _userData.Setup(x => x.Get(_user.UserId))
            .Returns(_user);
        _recipeData.Setup(x => x.Get(_recipe.RecipeId))
            .Returns(_recipe);

        StartUp();
        //Act
        Task<bool> task1 = JoinSession();
        Task<bool> task2 = JoinSession();
        //Assert
        await Task.WhenAll(task1, task2);

        Assert.NotEqual(task1.Result, task2.Result);
        Teardown();
    }

    public async Task<bool> JoinSession()
    {
        ActionResult<bool> actionResult = _bambooSessionController.JoinBambooSession(_sessionId.ToString(), _seatId.ToString());
        ActionResult<bool> viewResult = actionResult.Result;
        OkObjectResult model =  (OkObjectResult)viewResult.Result;
        bool result = (bool)model.Value;
        return result;
    }
}