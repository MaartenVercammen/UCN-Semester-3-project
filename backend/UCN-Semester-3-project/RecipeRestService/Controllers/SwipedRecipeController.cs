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

        [HttpGet, Route("user/{id}")]
        public ActionResult<List<SwipedRecipeDto>> GetPerUser(string id)
        {
            Guid userId = Guid.Parse(id);
            ActionResult<List<SwipedRecipeDto>> foundReturn;
            // retrieve and convert data
            List<SwipedRecipe>? foundRecipes = _swControl.GetPerUser(userId);
            List<SwipedRecipeDto>? foundDts = null;
            if (foundRecipes != null)
            {
                foundDts = SwipedRecipeDtoConvert.FromSwipedRecipeCollection(foundRecipes);
            }
            // evaluate
            if (foundDts != null)
            {
                if (foundDts.Count > 0)
                {
                    foundReturn = Ok(foundDts);                 // Statuscode 200
                }
                else
                {
                    foundReturn = new StatusCodeResult(204);    // Ok, but no content
                }
            }
            else
            {
                foundReturn = new StatusCodeResult(500);        // Internal server error
            }
            // send response back to client
            return foundReturn;

        }

        [HttpGet, Route("user/{id}/liked")]
        public ActionResult<List<SwipedRecipeDto>> GetLikedPerUser(string id)
        {
            Guid userId = Guid.Parse(id);
            ActionResult<List<SwipedRecipeDto>> foundReturn;
            // retrieve and convert data
            List<SwipedRecipe>? foundRecipes = _swControl.GetLikedPerUser(userId);
            List<SwipedRecipeDto>? foundDts = null;
            if (foundRecipes != null)
            {
                foundDts = SwipedRecipeDtoConvert.FromSwipedRecipeCollection(foundRecipes);
            }
            // evaluate
            if (foundDts != null)
            {
                if (foundDts.Count > 0)
                {
                    foundReturn = Ok(foundDts);                 // Statuscode 200
                }
                else
                {
                    foundReturn = new StatusCodeResult(204);    // Ok, but no content
                }
            }
            else
            {
                foundReturn = new StatusCodeResult(500);        // Internal server error
            }
            // send response back to client
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