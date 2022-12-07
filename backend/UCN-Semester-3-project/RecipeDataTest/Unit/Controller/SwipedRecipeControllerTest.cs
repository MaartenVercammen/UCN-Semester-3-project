using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RecipeRestService.Businesslogic;
using RecipeRestService.Controllers;
using RecipeRestService.DTO;
using RecipeRestService.ModelConversion;
using RecipeRestService.Security;

namespace RecipeDataTest.Unit.Controller;

public class SwipedRecipeControllerTest
{
    private readonly Mock<ISwipedRecipeData> _swControl = new Mock<ISwipedRecipeData>();
    private readonly Mock<ISecurityHelper> _securityHelper = new Mock<ISecurityHelper>();

    private readonly SwipedRecipeController _sut;
    private readonly DefaultHttpContext _httpContext = new DefaultHttpContext();

    public SwipedRecipeControllerTest()
    {
        _httpContext.Request.Headers["Authorization"] = "token";
        _sut = new SwipedRecipeController(_swControl.Object, _securityHelper.Object)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = _httpContext
            }
        };
    }

    [Fact]
    public void Get_WhenGivenValidId_RetunsSwipedDto()
    {
        //Arrange
        _securityHelper.Setup(x => x.GetUserFromJWT("token"))
            .Returns(Data.Data._validUser.UserId);
        _swControl.Setup(x => x.Get(Data.Data._swipedRecipeLiked.RecipeId, Data.Data._validUser.UserId))
            .Returns(Data.Data._swipedRecipeLiked);
        //Act
        var result = _sut.Get(Data.Data._swipedRecipeLiked.RecipeId.ToString());
        //Assert
        var viewResult = Assert.IsType<ActionResult<SwipedRecipeDto>>(result);
        var model = Assert.IsAssignableFrom<OkObjectResult>(viewResult.Result);
        var recipe = Assert.IsAssignableFrom<SwipedRecipeDto>(model.Value);
        Assert.Equal(recipe.RecipeId, Data.Data._swipedRecipeLiked.RecipeId);
    }

    [Fact]
    public void Get_WhenGivenInValidId_RetunsstatusCode404()
    {
        //Arrange
        _securityHelper.Setup(x => x.GetUserFromJWT("token"))
            .Returns(Data.Data._validUser.UserId);
        _swControl.Setup(x => x.Get(Data.Data._swipedRecipeLiked.RecipeId, Data.Data._validUser.UserId))
            .Returns(null as SwipedRecipe);
        //Act
        var result = _sut.Get(Data.Data._swipedRecipeLiked.RecipeId.ToString());
        //Assert
        var viewResult = Assert.IsType<ActionResult<SwipedRecipeDto>>(result);
        var model = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(404, model.StatusCode);
    }

    [Fact]
    public void GetPerUser_WhenGivenValidId_RetunsListOfSwipedDto()
    {
        //Arrange
        _securityHelper.Setup(x => x.IsJWTEqualRequestId("token", Data.Data._validUser.UserId.ToString()))
            .Returns(false);
        _swControl.Setup(x => x.GetPerUser(Data.Data._validUser.UserId))
            .Returns(Data.Data._SwipedRecipes);
        //Act
        var result = _sut.GetPerUser(Data.Data._validUser.UserId.ToString());
        //Assert
        var viewResult = Assert.IsType<ActionResult<List<SwipedRecipeDto>>>(result);
        var model = Assert.IsAssignableFrom<OkObjectResult>(viewResult.Result);
        var recipes = Assert.IsAssignableFrom<List<SwipedRecipeDto>>(model.Value);
        Assert.Equal(recipes.Count, Data.Data._SwipedRecipes.Count);
    }

    [Fact]
    public void GetPerUser_WhenGivenInValidId_RetunsStatusCode204()
    {
        //Arrange
        _securityHelper.Setup(x => x.IsJWTEqualRequestId("token", Data.Data._validUser.UserId.ToString()))
            .Returns(false);
        _swControl.Setup(x => x.GetPerUser(Data.Data._validUser.UserId))
            .Returns(new List<SwipedRecipe>());
        //Act
        var result = _sut.GetPerUser(Data.Data._validUser.UserId.ToString());
        //Assert
        var viewResult = Assert.IsType<ActionResult<List<SwipedRecipeDto>>>(result);
        var model = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(204, model.StatusCode);
    }

    [Fact]
    public void GetPerUser_WhenGivenInValidId_RetunsStatusCode500()
    {
        //Arrange
        _securityHelper.Setup(x => x.IsJWTEqualRequestId("token", Data.Data._validUser.UserId.ToString()))
            .Returns(false);
        _swControl.Setup(x => x.GetPerUser(Data.Data._validUser.UserId))
            .Returns(null as List<SwipedRecipe>);
        //Act
        var result = _sut.GetPerUser(Data.Data._validUser.UserId.ToString());
        //Assert
        var viewResult = Assert.IsType<ActionResult<List<SwipedRecipeDto>>>(result);
        var model = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(500, model.StatusCode);
    }

    [Fact]
    public void GetPerUser_WhenGivenNotMatchingId_RetunsStatusCode403()
    {
        //Arrange
        _securityHelper.Setup(x => x.IsJWTEqualRequestId("token", Data.Data._validUser.UserId.ToString()))
            .Returns(true);
        //Act
        var result = _sut.GetPerUser(Data.Data._validUser.UserId.ToString());
        //Assert
        var viewResult = Assert.IsType<ActionResult<List<SwipedRecipeDto>>>(result);
        var model = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(403, model.StatusCode);
    }

    [Fact]
    public void GetLikedPerUser_WhenGivenValidId_RetunsListOfSwipedDto()
    {
        //Arrange
        _securityHelper.Setup(x => x.IsJWTEqualRequestId("token", Data.Data._validUser.UserId.ToString()))
            .Returns(false);
        _swControl.Setup(x => x.GetLikedPerUser(Data.Data._validUser.UserId))
            .Returns(Data.Data._SwipedRecipes);
        //Act
        var result = _sut.GetLikedPerUser(Data.Data._validUser.UserId.ToString());
        //Assert
        var viewResult = Assert.IsType<ActionResult<List<SwipedRecipeDto>>>(result);
        var model = Assert.IsAssignableFrom<OkObjectResult>(viewResult.Result);
        var recipes = Assert.IsAssignableFrom<List<SwipedRecipeDto>>(model.Value);
        Assert.Equal(recipes.Count, Data.Data._SwipedRecipes.Count);
    }

    [Fact]
    public void GetLikedPerUser_WhenGivenInValidId_RetunsStatusCode204()
    {
        //Arrange
        _securityHelper.Setup(x => x.IsJWTEqualRequestId("token", Data.Data._validUser.UserId.ToString()))
            .Returns(false);
        _swControl.Setup(x => x.GetLikedPerUser(Data.Data._validUser.UserId))
            .Returns(new List<SwipedRecipe>());
        //Act
        var result = _sut.GetLikedPerUser(Data.Data._validUser.UserId.ToString());
        //Assert
        var viewResult = Assert.IsType<ActionResult<List<SwipedRecipeDto>>>(result);
        var model = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(204, model.StatusCode);
    }

    [Fact]
    public void GetLikedPerUser_WhenGivenInValidId_RetunsStatusCode500()
    {
        //Arrange
        _securityHelper.Setup(x => x.IsJWTEqualRequestId("token", Data.Data._validUser.UserId.ToString()))
            .Returns(false);
        _swControl.Setup(x => x.GetLikedPerUser(Data.Data._validUser.UserId))
            .Returns(null as List<SwipedRecipe>);
        //Act
        var result = _sut.GetLikedPerUser(Data.Data._validUser.UserId.ToString());
        //Assert
        var viewResult = Assert.IsType<ActionResult<List<SwipedRecipeDto>>>(result);
        var model = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(500, model.StatusCode);
    }

    [Fact]
    public void GetLikedPerUser_WhenGivenNotMatchingId_RetunsStatusCode403()
    {
        //Arrange
        _securityHelper.Setup(x => x.IsJWTEqualRequestId("token", Data.Data._validUser.UserId.ToString()))
            .Returns(true);
        //Act
        var result = _sut.GetLikedPerUser(Data.Data._validUser.UserId.ToString());
        //Assert
        var viewResult = Assert.IsType<ActionResult<List<SwipedRecipeDto>>>(result);
        var model = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(403, model.StatusCode);
    }
    
    [Fact]
    public void Post_WhenValidInfo_ReturnSwipedRecipeDto()
    {
        //Arrange
        _securityHelper.Setup(x => x.GetUserFromJWT("token"))
            .Returns(Data.Data._validUser.UserId);
        _swControl.Setup(x => x.Add(It.IsAny<SwipedRecipe>()))
            .Returns(Data.Data._swipedRecipeLiked);
        //Act
        var result = _sut.Post(Data.Data._SwipedRecipeLikedDto);

        //Assert
        var viewResult = Assert.IsType<ActionResult<SwipedRecipeDto>>(result);
        var model = Assert.IsAssignableFrom<OkObjectResult>(viewResult.Result);
        var recipes = Assert.IsAssignableFrom<SwipedRecipeDto>(model.Value);
        Assert.Equal(recipes.RecipeId, Data.Data._SwipedRecipeLikedDto.RecipeId);

    }

    [Fact]
    public void Post_WhenThrowsError_ReturnStatusCode500()
    {
        //Arrange
        _securityHelper.Setup(x => x.GetUserFromJWT("token"))
            .Returns(Data.Data._validUser.UserId);
        _swControl.Setup(x => x.Add(Data.Data._swipedRecipeLiked))
            .Throws(new Exception());
        //Act
        var result = _sut.Post(Data.Data._SwipedRecipeLikedDto);

        //Assert
        var viewResult = Assert.IsType<ActionResult<SwipedRecipeDto>>(result);
        var model = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(500, model.StatusCode);
    }
}