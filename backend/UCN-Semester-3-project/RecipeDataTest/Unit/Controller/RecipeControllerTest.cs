using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RecipeRestService.Businesslogic;
using RecipeRestService.Controllers;
using RecipeRestService.DTO;
using RecipeRestService.ModelConversion;
using RecipeRestService.Security;
using RecipesData.Model;

namespace RecipeDataTest.Unit.Controller;

public class RecipeControllerTest
{
    private readonly Mock<IRecipeData> _acces = new Mock<IRecipeData>();
    private readonly Mock<IUserData> _userData = new Mock<IUserData>();
    private readonly Mock<ISecurityHelper> _securityHelper = new Mock<ISecurityHelper>();
    private readonly RecipesController _sut;
    private readonly Recipe _validRecipe;
    private readonly RecipeDto? _validRecipeDto;
    private readonly User _validUser;
    private readonly DefaultHttpContext _httpContext = new DefaultHttpContext();
    

    public RecipeControllerTest()
    {
        _httpContext.Request.Headers["Authorization"] = "token";
        _sut = new RecipesController(_userData.Object, _acces.Object, _securityHelper.Object)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = _httpContext
            }
        };
        Guid id = Guid.NewGuid();
        _validUser = new User(Guid.Parse("00000000-0000-0000-0000-000000000000"), "mail", "mark", "mark", "pass",
            "street", Role.USER);
        var validIngredient = new Ingredient("banana", 5, "kg");
        var validInstruction = new Instruction(1, "peel the banana");
        _validRecipe = new Recipe(id, "Banana-bread", "best banana bread in the world",
            "http://picture.png", 30, 4, _validUser);
        _validRecipe.Ingredients.Add(validIngredient);
        _validRecipe.Instructions.Add(validInstruction);
        _validRecipeDto = new RecipeDto(id, "Banana-bread", "best banana bread in the world",
            "http://picture.png", 30, 4, _validUser.UserId);
        _validRecipeDto.Ingredients.Add(validIngredient);
        _validRecipeDto.Instructions.Add(validInstruction);
    }

    [Fact]
    public void Get_WhenGivenValidRecipeId_ReturnRecipeDTO()
    {
        //Arrange
        _acces.Setup(x => x.Get(_validRecipe.RecipeId))
            .Returns(_validRecipe);
        //Act
        var result = _sut.Get(_validRecipe.RecipeId.ToString());
        //Assert
        var viewResult = Assert.IsType<ActionResult<RecipeDto>>(result);
        var model = Assert.IsAssignableFrom<OkObjectResult>(viewResult.Result);
        var recipe = Assert.IsAssignableFrom<RecipeDto>(model.Value);
        Assert.Equal(recipe.RecipeId, _validRecipe.RecipeId);
    }

    [Fact]
    public void Get_WhenGivenInvalidRecipeId_ReturnNotFound()
    {
        //Arrange
        _acces.Setup(x => x.Get(_validRecipe.RecipeId))
            .Returns(null as Recipe);
        //Act
        var result = _sut.Get(_validRecipe.RecipeId.ToString());
        //Assert
        var viewResult = Assert.IsType<ActionResult<RecipeDto>>(result);
        Assert.IsAssignableFrom<NotFoundResult>(viewResult.Result);
    }

    [Fact]
    public void Get_WhenGivenNoId_ReturnListOfRecipeDTOs()
    {
        //Arrange
        List<Recipe> recipes = new List<Recipe>()
        {
            _validRecipe,
            _validRecipe,
            _validRecipe
        };
        _acces.Setup(x => x.Get())
            .Returns(recipes);
        //Act
        var result = _sut.Get();
        //Assert
        var viewResult = Assert.IsType<ActionResult<List<RecipeDto>>>(result);
        var model = Assert.IsAssignableFrom<OkObjectResult>(viewResult.Result);
        var outRecipes = Assert.IsAssignableFrom<List<RecipeDto>>(model.Value);
        Assert.Equal(recipes.Count, outRecipes.Count);
    }

    [Fact]
    public void Get_WhenGivenNoIdAndNoRecipesInDatabase_Return204NoContent()
    {
        //Arrange
        List<Recipe> recipes = new List<Recipe>();
        _acces.Setup(x => x.Get())
            .Returns(recipes);
        //Act
        var result = _sut.Get();
        //Assert
        var viewResult = Assert.IsType<ActionResult<List<RecipeDto>>>(result);
        var statusCodeResult = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(204, statusCodeResult.StatusCode);

    }

    [Fact]
    public void Get_WhenGivenNoIdAndErrorOccured_Return500()
    {
        //Arrange
        _acces.Setup(x => x.Get())
            .Returns(null as List<Recipe>);
        //Act
        var result = _sut.Get();
        //Assert
        var viewResult = Assert.IsType<ActionResult<List<RecipeDto>>>(result);
        var statusCodeResult = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }

    [Fact]
    public void GetLiked_WhenGivenId_ReturnListOfRecipeDTOs()
    {
        //Arrange
        List<Recipe> recipes = new List<Recipe>()
        {
            _validRecipe,
            _validRecipe,
            _validRecipe
        };
        _acces.Setup(x => x.GetLikedByUser(_validUser.UserId))
            .Returns(recipes);
        _securityHelper.Setup(x => x.IsJWTEqualRequestId("token", _validUser.UserId.ToString()))
            .Returns(true);
        //Act
        var result = _sut.GetLiked(_validUser.UserId.ToString());
        //Assert
        var viewResult = Assert.IsType<ActionResult<List<RecipeDto>>>(result);
        var model = Assert.IsAssignableFrom<OkObjectResult>(viewResult.Result);
        var outRecipes = Assert.IsAssignableFrom<List<RecipeDto>>(model.Value);
        Assert.Equal(recipes.Count, outRecipes.Count);
    }

    [Fact]
    public void GetLiked_WhenGivenIdAndNoRecipesInDatabase_Return204NoContent()
    {
        //Arrange
        List<Recipe> recipes = new List<Recipe>();
        _acces.Setup(x => x.GetLikedByUser(_validUser.UserId))
            .Returns(recipes);
        _acces.Setup(x => x.Get(_validRecipe.RecipeId))
            .Returns(_validRecipe);
        _securityHelper.Setup(x => x.IsJWTEqualRequestId("token", _validUser.UserId.ToString()))
            .Returns(true);
        //Act
        var result = _sut.GetLiked(_validUser.UserId.ToString());
        //Assert
        var viewResult = Assert.IsType<ActionResult<List<RecipeDto>>>(result);
        var statusCodeResult = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(204, statusCodeResult.StatusCode);

    }

    [Fact]
    public void GetLiked_WhenGivenIdAndErrorOccured_Return500()
    {
        //Arrange
        _acces.Setup(x => x.GetLikedByUser(_validUser.UserId))
            .Returns(null as List<Recipe>);
        _acces.Setup(x => x.Get(_validRecipe.RecipeId))
            .Returns(_validRecipe);
        _securityHelper.Setup(x => x.IsJWTEqualRequestId("token", _validUser.UserId.ToString()))
            .Returns(true);
        //Act
        var result = _sut.GetLiked(_validUser.UserId.ToString());
        //Assert
        var viewResult = Assert.IsType<ActionResult<List<RecipeDto>>>(result);
        var statusCodeResult = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }

    [Fact]
    public void Post_WhenGivenRecipeDto_ReturnsGuidOfNewRecipe()
    {
        //Arrange
        Recipe convertedRecipe = RecipeDtoConvert.ToRecipe(_validRecipeDto, _validUser);
        _acces.Setup(x => x.Add(It.IsAny<Recipe>()))
            .Returns(convertedRecipe.RecipeId);
        _userData.Setup(x => x.Get(_validUser.UserId))
            .Returns(_validUser);
        _securityHelper.Setup(x => x.IsJWTEqualRequestId("token", _validUser.UserId.ToString()))
            .Returns(true);
        //Act
        var result = _sut.Post(_validRecipeDto);
        //Assert
        var viewResult = Assert.IsType<ActionResult<string>>(result);
        var model = Assert.IsAssignableFrom<OkObjectResult>(viewResult.Result);
        var id = Assert.IsAssignableFrom<Guid>(model.Value);
        Assert.Equal(convertedRecipe.RecipeId, id);
    }

    [Fact]
    public void Post_WhenGivenRecipeDtoAndInsertFailed_ReturnsStatusCode500()
    {
        //Arrange
        _acces.Setup(x => x.Add(It.IsAny<Recipe>()))
            .Returns(Guid.Empty);
        _acces.Setup(x => x.Get(_validRecipe.RecipeId))
            .Returns(_validRecipe);
        _securityHelper.Setup(x => x.IsJWTEqualRequestId("token", _validUser.UserId.ToString()))
            .Returns(true);
        //Act
        var result = _sut.Post(_validRecipeDto);
        //Assert
        var viewResult = Assert.IsType<ActionResult<string>>(result);
        var statusCodeResult = Assert.IsAssignableFrom<StatusCodeResult>(viewResult.Result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }

    [Fact]
    public void Delete_WhenDeleteWasSuccessFull_ReturnsStatusCode200()
    {
        //Arrange
        _acces.Setup(x => x.Delete(_validRecipe.RecipeId))
            .Returns(true);
        _acces.Setup(x => x.Get(_validRecipe.RecipeId))
            .Returns(_validRecipe);
        _securityHelper.Setup(x => x.IsJWTEqualRequestId("token", _validUser.UserId.ToString()))
            .Returns(true);
        //Act
        var result = _sut.Delete(_validRecipe.RecipeId.ToString());
        //Assert
        var statusCodeResult = Assert.IsAssignableFrom<StatusCodeResult>(result);
        Assert.Equal(200, statusCodeResult.StatusCode);
    }

    [Fact]
    public void Delete_WhenDeleteWasUnSuccessFull_ReturnsStatusCode500()
    {
        //Arrange
        _acces.Setup(x => x.Delete(_validRecipe.RecipeId))
            .Returns(false);
        _acces.Setup(x => x.Get(_validRecipe.RecipeId))
            .Returns(_validRecipe);
        _securityHelper.Setup(x => x.IsJWTEqualRequestId("token", _validUser.UserId.ToString()))
            .Returns(true);
        //Act
        var result = _sut.Delete(_validRecipe.RecipeId.ToString());
        //Assert
        var statusCodeResult = Assert.IsAssignableFrom<StatusCodeResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }

    [Fact]
    public void GetRandomRecipe_WhenUnSwipedRecipeIsFound_ReturnsARandomRecipe()
    {
        //Arrange
        _acces.Setup(x => x.GetRandomRecipe(_validUser.UserId))
            .Returns(_validRecipe);
        _acces.Setup(x => x.Get(_validRecipe.RecipeId))
            .Returns(_validRecipe);
        _securityHelper.Setup(x => x.GetUserFromJWT("token"))
            .Returns(_validUser.UserId);
        //Act
        var result = _sut.GetRandomRecipe();
        //Assert
        var viewResult = Assert.IsType<ActionResult<RecipeDto>>(result);
        var model = Assert.IsAssignableFrom<OkObjectResult>(viewResult.Result);
        var recipe = Assert.IsAssignableFrom<RecipeDto>(model.Value);
        Assert.Equal(recipe.RecipeId, _validRecipe.RecipeId);
    }

    [Fact]
    public void GetRandomRecipe_WhenUnSwipedRecipeIsNotFound_ReturnsStatusCode204()
    {
        //Arrange
        _acces.Setup(x => x.GetRandomRecipe(_validUser.UserId))
            .Returns(null as Recipe);
        _acces.Setup(x => x.Get(_validRecipe.RecipeId))
            .Returns(_validRecipe);
        _securityHelper.Setup(x => x.IsJWTEqualRequestId("token", _validUser.UserId.ToString()))
            .Returns(true);
        //Act
        var result = _sut.GetRandomRecipe();
        //Assert
        var viewResult = Assert.IsType<ActionResult<RecipeDto>>(result);
        var statusCodeResult = Assert.IsAssignableFrom<StatusCodeResult>(result.Result);
        Assert.Equal(204, statusCodeResult.StatusCode);
    }

}