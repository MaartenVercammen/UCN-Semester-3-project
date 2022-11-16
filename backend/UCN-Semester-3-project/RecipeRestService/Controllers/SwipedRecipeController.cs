using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeRestService.Businesslogic;
using RecipeRestService.DTO;
using RecipeRestService.ModelConversion;
using RecipesData.Model;

namespace RecipeRestService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SwipedRecipeController : ControllerBase
    {
        private readonly SwipedRecipeDataControl _swControl;
        private readonly IConfiguration _configuration;

        public SwipedRecipeController(IConfiguration inConfiguration)
        {
            _configuration = inConfiguration;
            _swControl = new SwipedRecipeDataControl(_configuration);
        }


        [HttpGet, Route("{id}")]
        public ActionResult<SwipedRecipeDto> Get(string id)
        {
            Guid swRecipeId = Guid.Parse(id);

            ActionResult<SwipedRecipeDto> foundReturn;
            SwipedRecipe? foundSwipedRecipe = _swControl.Get(swRecipeId);

            if (foundSwipedRecipe != null)
            {
                foundReturn = Ok(SwipedRecipeDtoConvert.FromSwipedRecipe(foundSwipedRecipe));
            }
            else
            {
                foundReturn = NotFound();
            }

            return foundReturn;
        }


        [HttpPost]
        public ActionResult<SwipedRecipeDto> Post(SwipedRecipeDto inSwipedRecipeDto)
        {
            ActionResult<SwipedRecipeDto> foundReturn;
            SwipedRecipe? foundSwipedRecipe;
            if (inSwipedRecipeDto != null)
            {
                foundSwipedRecipe = _swControl.Add(SwipedRecipeDtoConvert.ToSWRecipe(inSwipedRecipeDto));
                foundReturn= Ok(foundSwipedRecipe);
            }
            else
            {
                foundReturn = new StatusCodeResult(500);
            }
            return foundReturn;
        }

    }
}